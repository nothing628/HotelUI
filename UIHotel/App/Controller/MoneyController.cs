using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Attributes;
using UIHotel.Data;
using UIHotel.Data.Table;

namespace UIHotel.App.Controller
{
    [Authorize(Auth.AuthLevel.Administrator)]
    [Authorize(Auth.AuthLevel.Manager)]
    public class MoneyController : BaseController
    {
        public MoneyController(IRequest request) : base (request)
        {
            //
        }

        public IResourceHandler transaksi()
        {
            return View("Money.Transaction");
        }

        public IResourceHandler graph()
        {
            return View("Money.Graph");
        }

        public IResourceHandler getCategories()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var categories = (from a in model.LedgerCategories
                                      orderby a.IsExpense ascending
                                      select a).ToList();

                    return Json(new { success = true, data = categories });
                } catch
                {
                    return Json(new { success = false });
                }
            }
        }
        public IResourceHandler getTransaction()
        {
            var token = jToken;
            var bdate = jToken.Value<DateTime>("item_date");
            var edate = bdate.AddDays(1);

            using (var model = new DataContext())
            {
                try
                {
                    var data = (from a in model.LedgerLogs
                                where a.Date >= bdate && a.Date < edate
                                select a).ToList();

                    return Json(new { success = true, data });
                } catch
                {
                    return Json(new { success = false });
                }
            }
        }
        public IResourceHandler saveTransaction()
        {
            var date = jToken.Value<DateTime>("date");
            var time = jToken.Value<string>("time");
            var ttime = TimeSpan.Parse(time);
            var amount = jToken.Value<decimal>("amount");
            var desc = jToken.Value<string>("description");
            var isOutcome = jToken.Value<bool>("isOutcome");
            var idCategory = jToken.Value<long>("idCategory");

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var log = new LedgerLog()
                    {
                        Id = LedgerLog.GenerateID(),
                        IdCategory = idCategory,
                        Date = date.Add(ttime),
                        Description = desc,
                        Debit = (isOutcome) ? 0 : amount,
                        Kredit = (isOutcome) ? amount : 0,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                    };

                    model.LedgerLogs.Add(log);
                    model.SaveChanges();
                    trans.Commit();
                    return Json(new { success = true, data = log });
                }
                catch
                {
                    trans.Rollback();
                    return Json(new { success = false });
                }
            }
        }
    }
}
