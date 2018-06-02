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
                    //
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
    }
}
