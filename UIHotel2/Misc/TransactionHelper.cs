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
                var selectData = (from a in context.Bookings.Include(p => p.Invoices).Include(p => p.Type)
                                  where !a.CheckoutAt.HasValue
                                  select a).ToList();
                var checkinData = (from a in selectData
                                   where a.CheckinAt.HasValue
                                   select a).ToList();
                foreach(var data in checkinData)
                {
                    CalculateInvoice(data.Invoices.First(), data.Type, data.RoomId, data.ArrivalDate, data.DepartureDate);
                }
            }
        }

        private static void CalculateInvoice(Invoice invoice, BookingType type, long IdRoom, DateTime arrivalDate, DateTime departureDate)
        {
            var timeCheckout = AppHelper.GetTimespan(SettingHelper.TimeCheckout);
            var timeFullcharge = AppHelper.GetTimespan(SettingHelper.TimeFullcharge);
            var lateCheckout = departureDate.Add(timeCheckout);
            var fullPay = DateTime.Today.Add(timeFullcharge);

            if (type.IsLocal)
            {
                // Calculate room price
            }

            // Check checkout date
            // Ketelatan checkout akan dihitung mulai dari jam 13:00 s/d 18:00
            // Setelah jam tersebut, harga akan terhitung full.
            if (DateTime.Now > lateCheckout)
            {
                //
            }
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
