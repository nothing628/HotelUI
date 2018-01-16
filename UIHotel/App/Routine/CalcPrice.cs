using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data;
using UIHotel.Data.Table;

namespace UIHotel.App.Routine
{
    public class CalcPrice
    {
        public void DoWork()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var invoices = (from a in model.Invoices.Include(x => x.CheckinInfo).Include(x => x.Details)
                                   where !a.IsClosed
                                   select a).ToList();

                    foreach (var invoice in invoices)
                        this.CalculateInvoice(invoice);
                } catch
                {
                    //
                }
            }
        }

        private void CalculateInvoice(Invoice invoice)
        {
            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var startDate = invoice.CheckinInfo.ArriveAt.Date;
                    var todayDate = DateTime.Today.Date;
                    var pointDate = startDate;

                    do
                    {
                        var detail = (from a in invoice.Details
                                      where a.IsSystem
                                      where a.CreateAt.Date == pointDate
                                      select a).FirstOrDefault();

                        if (detail == null)
                        {
                            var roomPrice = GetRoomPrice(invoice.CheckinInfo.IdRoom, pointDate);
                            var description = "PAY ROOM<br><i>" + pointDate.ToString("dd-MM-yyyy") + "</i>";
                            var newDetail = new InvoiceDetail()
                            {
                                IsSystem = true,
                                CreateAt = DateTime.Now,
                                UpdateAt = DateTime.Now,
                                IdInvoice = invoice.Id,
                                Description = description,
                                AmmountOut = roomPrice,
                                AmmountIn = 0,
                            };

                            model.InvoiceDetails.Add(newDetail);
                            model.SaveChanges();
                        }

                        pointDate = pointDate.AddDays(1.0d);
                    } while (pointDate == todayDate);

                    trans.Commit();
                } catch
                {
                    trans.Rollback();
                }
            }
        }

        private decimal GetRoomPrice(long roomId, DateTime date)
        {
            using (var model = new DataContext())
            {
                try
                {
                    var room = (from a in model.Rooms
                                where a.Id == roomId
                                select a).FirstOrDefault();

                    if (room == null) return 0;

                    var price = (from b in model.RoomPrice
                                    join e in model.DayCycles on b.IdEffect equals e.IdEffect into f
                                    from g in f
                                    where g.DateAt == date
                                    where b.IdCategory == room.IdCategory
                                    select b.Price).FirstOrDefault();

                    return price;
                } catch
                {
                    return 0;
                }
            }
        }
    }
}
