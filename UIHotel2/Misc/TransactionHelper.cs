using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.Data;
using UIHotel2.Data.Tables;

namespace UIHotel2.Misc
{
    public static class TransactionHelper
    {
        public static void CalculateBooking()
        {
            using (var context = new HotelContext())
            {
                var selectData = (from a in context.Invoices.Include(p => p.Booking).Include(p => p.Booking.Type)
                                  where !a.CloseAt.HasValue
                                  select a).ToList();
                var checkinData = (from a in selectData
                                   where a.Booking.CheckinAt.HasValue
                                   select a).ToList();
                foreach(var data in checkinData)
                {
                    CalculateInvoice(data, data.Booking.Type, data.Booking.RoomId, data.Booking.ArrivalDate, data.Booking.DepartureDate);
                }
            }
        }

        private static void CalculateInvoice(Invoice invoice, BookingType type, long IdRoom, DateTime arrivalDate, DateTime departureDate)
        {
            var timeCheckout = AppHelper.GetTimespan(SettingHelper.TimeCheckout);
            var timeFullcharge = AppHelper.GetTimespan(SettingHelper.TimeFullcharge);
            var penalty = SettingHelper.Penalty;
            var lateCheckoutAt = departureDate.Add(timeCheckout);
            var fullPayAt = DateTime.Today.Add(timeFullcharge);
            var detailInvoice = GetDetailInvoice(invoice);
            var insInvoice = new List<InvoiceDetail>();
            var updInvoice = new List<InvoiceDetail>();
            var delInvoice = new List<InvoiceDetail>();

            if (invoice.IsClosed)
            {
                return;
            }

            if (type.IsLocal)
            {
                // Calculate room price
                var start = arrivalDate;
                var end = departureDate;

                while (start <= end)
                {
                    var price = DataHelper.GetRoomPrice(IdRoom, start);
                    var isExists = detailInvoice.Any(p => p.KindId == 1 && p.TransactionAt == start);

                    if (!isExists)
                    {
                        var newInvoice = new InvoiceDetail
                        {
                            AmmountOut = price,
                            AmmountIn = 0,
                            Description = "Room Invoice " + start.ToShortDateString(),
                            IsSystem = true,
                            InvoiceId = invoice.Id,
                            KindId = 1,
                            TransactionAt = start,
                        };
                        insInvoice.Add(newInvoice);
                    }
                    start = start.AddDays(1);
                }
            }

            // Check checkout date
            // Ketelatan checkout akan dihitung mulai dari jam 13:00 s/d 18:00
            // Setelah jam tersebut, harga akan terhitung full.
            if (DateTime.Now > lateCheckoutAt)
            {
                var start = departureDate.AddDays(1);
                var end = DateTime.Now;

                while (start < end)
                {
                    // Hari ini dan masih belum terhitung fullcharge
                    if (start == end.Date && end < start.Add(timeFullcharge))
                    {
                        var lateStart = start.Add(timeCheckout);
                        var diffLate = end - lateStart;
                        var hourDiff = Math.Ceiling(diffLate.TotalHours);
                        var totalPenalty = penalty * Convert.ToDecimal(hourDiff);
                        var isExists = detailInvoice.Any(a => a.KindId == 99);

                        if (hourDiff > 0)
                        {
                            if (isExists)
                            {
                                var penaltyData = detailInvoice.First(p => p.KindId == 99);
                                penaltyData.AmmountOut = totalPenalty;
                                penaltyData.TransactionAt = end;
                                updInvoice.Add(penaltyData);
                            } else
                            {
                                var penaltyData = new InvoiceDetail
                                {
                                    AmmountIn = 0,
                                    AmmountOut = totalPenalty,
                                    Description = "Penalty",
                                    IsSystem = true,
                                    InvoiceId = invoice.Id,
                                    KindId = 99,
                                    TransactionAt = end,
                                };
                                insInvoice.Add(penaltyData);
                            }
                        }
                        else if (isExists)
                        {
                            var penaltyData = detailInvoice.First(p => p.KindId == 99);
                            penaltyData.AmmountOut = 0;
                            penaltyData.TransactionAt = end;
                            updInvoice.Add(penaltyData);
                        }
                    } else
                    {
                        //Hitung fullcharge, kind 3 adalah late checkout
                        var price = DataHelper.GetRoomPrice(IdRoom, start);
                        var isExists = detailInvoice.Any(a => a.KindId == 3 && a.TransactionAt == start);

                        if (!isExists)
                        {
                            var newInvoice = new InvoiceDetail
                            {
                                AmmountOut = price,
                                AmmountIn = 0,
                                Description = "Late Checkout " + start.ToShortDateString(),
                                IsSystem = true,
                                InvoiceId = invoice.Id,
                                KindId = 3,
                                TransactionAt = start,
                            };
                            insInvoice.Add(newInvoice);
                        }
                    }

                    start = start.AddDays(1);
                }
            }

            ChangeInvoiceDetail(insInvoice, updInvoice, delInvoice);
        }

        private static void ChangeInvoiceDetail(List<InvoiceDetail> newInvoice, List<InvoiceDetail> updateInvoice, List<InvoiceDetail> deleteInvoice)
        {
            using (var context = new HotelContext())
            {
                using (var transIns = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var invoice in newInvoice)
                        {
                            context.InvoiceDetails.Add(invoice);
                            context.SaveChanges();
                        }

                        transIns.Commit();
                    } catch
                    {
                        transIns.Rollback();
                    }
                }

                using (var transUpd = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var invoice in updateInvoice)
                        {
                            context.Entry(invoice).State = EntityState.Modified;
                            context.SaveChanges();
                        }

                        transUpd.Commit();
                    }
                    catch
                    {
                        transUpd.Rollback();
                    }
                }

                using (var transDel = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var invoice in deleteInvoice)
                        {
                            context.InvoiceDetails.Remove(invoice);
                            context.SaveChanges();
                        }

                        transDel.Commit();
                    }
                    catch
                    {
                        transDel.Rollback();
                    }
                }
            }
        }

        private static ICollection<InvoiceDetail> GetDetailInvoice(Invoice invoice)
        {
            var listResult = new List<InvoiceDetail>();

            using (var context = new HotelContext())
            {
                try
                {
                    var details = from a in context.InvoiceDetails
                                  where a.InvoiceId == invoice.Id
                                  select a;

                    listResult.AddRange(details.ToList());
                } catch { }
            }

            return listResult;
        }

        private static void CalculateList(List<Transaction> lst, decimal lastSubtotal)
        {
            var lstSubtotal = lastSubtotal;

            using (var context = new HotelContext())
            using (var dbTrans = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var trans in lst)
                    {
                        var newSubtotal = lstSubtotal + trans.AmmountIn - trans.AmmountOut;
                        trans.Subtotal = newSubtotal;
                        lstSubtotal = newSubtotal;
                        context.Entry(trans).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    dbTrans.Commit();
                }
                catch
                {
                    dbTrans.Rollback();
                }
            }
        }

        public static void CalculateSubtotal()
        {
            using (var context = new HotelContext())
            {
                decimal lstSubtotal = 0;
                var today = DateTime.Today.AddDays(1);
                var last7 = DateTime.Today.AddDays(-7);

                var lastTrans = (from t in context.Transactions
                                    where t.IsClosed
                                    orderby t.TransactionAt descending
                                    select t).FirstOrDefault();

                var transaction = (from t in context.Transactions
                                   where !t.IsClosed
                                   where t.TransactionAt >= last7
                                   where t.TransactionAt <= today
                                   orderby t.TransactionAt ascending
                                   select t).ToList();

                if (lastTrans != null)
                {
                    lstSubtotal = lastTrans.Subtotal;
                }

                CalculateList(transaction, lstSubtotal);
            }
        }

        public static void ClosingTransaction()
        {
            //
        }
    }
}
