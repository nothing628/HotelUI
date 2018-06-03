using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Provider;
using UIHotel.Data;
using UIHotel.Data.Table;

namespace UIHotel.App.Routine
{
    public class CalcPinalty
    {
        public void DoWork()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var invoices = (from a in model.Invoices.Include(x => x.Details)
                                   where !a.IsClosed
                                   select a).ToList();

                    foreach (var invoice in invoices)
                    {
                        CalculateRoomCharge(invoice);
                        CalculatePinalty(invoice);
                        CalculateTax(invoice);
                    }
                } catch
                {
                    //
                }
            }
        }

        public void CalculateRoomCharge(Invoice invoice)
        {
            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {                    
                    var pointDate = DateTime.Today;
                    var checkin = (from a in model.CheckIn
                                   where a.Id == invoice.IdCheckin
                                   select a).SingleOrDefault();

                    if (checkin != null && checkin.DepartureAt.Date >= pointDate)
                    {
                        // If already charged for today, just skip this function.
                        if (IsAlreadyCharged(invoice.Id, checkin.IdRoom, pointDate)) return;

                        var room = GetRoom(checkin.IdRoom);
                        var roomPrice = GetRoomPrice(checkin.IdRoom, pointDate);
                        var description = "Invoice Room " +  room.RoomNumber + "<br><i>" + pointDate.ToString("dd-MM-yyyy") + "</i>";
                        var newDetail = new InvoiceDetail()
                        {
                            IsSystem = true,
                            TransactionDate = pointDate,
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                            IdInvoice = invoice.Id,
                            IdKind = 1,
                            IdRoom = checkin.IdRoom,
                            Description = description,
                            AmmountOut = roomPrice,
                            AmmountIn = 0,
                        };

                        model.InvoiceDetails.Add(newDetail);
                        model.SaveChanges();
                    }

                    trans.Commit();
                } catch
                {
                    trans.Rollback();
                }
            }
        }
        public void CalculatePinalty(Invoice invoice)
        {
            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var endDate = invoice.CheckinInfo.DepartureAt.Date;
                    var startPinalty = endDate.Add(new TimeSpan(13, 0, 0));
                    var pinaltyHour = (DateTime.Today - startPinalty).TotalHours;
                    var pinaltyCount = Convert.ToDecimal(Math.Ceiling(pinaltyHour)) * SettingProvider.Pinalty;

                    var pinalty = (from a in model.InvoiceDetails
                                   where a.IdInvoice == invoice.Id
                                   where a.IsSystem
                                   where a.IdKind == 3
                                   select a).FirstOrDefault();

                    if (pinaltyHour < 0) return;
                    if (pinalty == null)
                    {
                        pinalty = new InvoiceDetail()
                        {
                            AmmountIn = 0,
                            AmmountOut = pinaltyCount,
                            Description = "Pinalty",
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                            IdInvoice = invoice.Id,
                            IdKind = 3,
                            IsSystem = true,
                            TransactionDate = endDate
                        };

                        model.InvoiceDetails.Add(pinalty);
                        model.SaveChanges();
                    } else
                    {
                        pinalty.AmmountOut = pinaltyCount;
                        pinalty.UpdateAt = DateTime.Now;

                        model.Entry(pinalty).State = EntityState.Modified;
                        model.SaveChanges();
                    }
                    
                    trans.Commit();
                } catch
                {
                    trans.Rollback();
                }
            }
        }
        public void CalculateTax(Invoice invoice)
        {
            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var invoiceDetails = (from a in model.InvoiceDetails
                                      where a.IdInvoice == invoice.Id
                                      select a).ToList();
                    var invoiceTax = (from a in invoiceDetails
                                      where a.IdKind == 99
                                      select a).SingleOrDefault();
                    var invoiceCalc = (from a in invoiceDetails
                                       where a.IdKind != 98
                                       where a.IdKind != 99
                                       where a.IdKind != 100
                                       select a).ToList();

                    var taxPer = SettingProvider.TaxPercent;
                    var ammountOut = invoiceCalc.Sum(x => x.AmmountOut);
                    var ammountTax = ammountOut * SettingProvider.TaxFloat;

                    if (invoiceTax == null)
                    {
                        invoiceTax = new InvoiceDetail()
                        {
                            IdKind = 99,
                            IdInvoice = invoice.Id,
                            AmmountIn = 0,
                            AmmountOut = ammountTax,
                            TransactionDate = DateTime.Today,
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                            IsSystem = true,
                            Description = "Tax " + taxPer + "%",
                        };

                        model.InvoiceDetails.Add(invoiceTax);
                    } else
                    {
                        invoiceTax.AmmountOut = ammountTax;
                        invoiceTax.TransactionDate = DateTime.Today;
                        invoiceTax.UpdateAt = DateTime.Now;

                        model.Entry(invoiceTax).State = EntityState.Modified;
                    }

                    model.SaveChanges();
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// Get Room Price from room id and date
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private decimal GetRoomPrice(long roomId, DateTime date)
        {
            using (var model = new DataContext())
            {
                try
                {
                    var room = GetRoom(roomId);

                    if (room == null) return 0;

                    return (from b in model.RoomPrice
                                 join e in model.DayCycles on b.IdEffect equals e.IdEffect into f
                                 from g in f
                                 where g.DateAt == date
                                 where b.IdCategory == room.IdCategory
                                 select b.Price).FirstOrDefault();
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// Get Room Model from room id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        private Room GetRoom(long roomId)
        {
            using (var model = new DataContext())
            {
                try
                {
                    return (from a in model.Rooms
                            where a.Id == roomId
                            select a).SingleOrDefault();
                } catch
                {
                    //
                }
            }

            return null;
        }
        /// <summary>
        /// Check if we already calculate for today
        /// </summary>
        /// <param name="invoice_id"></param>
        /// <param name="room_id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsAlreadyCharged(string invoice_id, long room_id, DateTime date)
        {
            var count = 0L;

            using (var model = new DataContext())
            {
                count = (from a in model.InvoiceDetails
                          where a.IdRoom == room_id
                          where a.IdInvoice == invoice_id
                          where a.IdKind == 1
                          where a.TransactionDate == date
                          select a).LongCount();
            }

            return count > 0;
        }
    }
}
