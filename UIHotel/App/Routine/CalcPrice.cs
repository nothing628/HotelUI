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
                    var invoices = (from a in model.Invoices.Include(x => x.Details)
                                   where !a.IsClosed
                                   select a).ToList();

                    foreach (var invoice in invoices)
                        this.CalculateInvoice(invoice);
                } catch (Exception ex)
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
                    var endDate = invoice.CheckinInfo.DepartureAt.Date;
                    var todayDate = DateTime.Today.Date;
                    var pointDate = startDate;

                    do
                    {
                        var detail = (from a in invoice.Details
                                      where a.IsSystem
                                      where a.TransactionDate == pointDate
                                      select a).FirstOrDefault();

                        if (detail == null)
                        {
                            var roomPrice = GetRoomPrice(invoice.CheckinInfo.IdRoom, pointDate);
                            var description = "PAY ROOM<br><i>" + pointDate.ToString("dd-MM-yyyy") + "</i>";
                            var newDetail = new InvoiceDetail()
                            {
                                IsSystem = true,
                                TransactionDate = pointDate,
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
                    } while (pointDate <= todayDate && pointDate <= endDate);

                    var startPinalty = endDate.Add(new TimeSpan(13, 0, 0));
                    var pinaltyHour = (DateTime.Today - startPinalty).TotalHours;
                    var pinaltyCount = Convert.ToDecimal(Math.Ceiling(pinaltyHour) * 40000);           //TODO : Change to setting

                    var pinalty = (from a in model.InvoiceDetails
                                   where a.IdInvoice == invoice.Id
                                   where a.IsSystem
                                   where a.Description == "Pinalty"
                                   select a).FirstOrDefault();

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
