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
                    var endDate = invoice.CheckinInfo.DepartureAt.Date;
                    var startPinalty = endDate.Add(new TimeSpan(13, 0, 0));
                    var pinaltyHour = (DateTime.Today - startPinalty).TotalHours;
                    var pinaltyCount = Convert.ToDecimal(Math.Ceiling(pinaltyHour)) * SettingProvider.Pinalty;

                    var pinalty = (from a in model.InvoiceDetails
                                   where a.IdInvoice == invoice.Id
                                   where a.IsSystem
                                   where a.Description == "Pinalty"
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
    }
}
