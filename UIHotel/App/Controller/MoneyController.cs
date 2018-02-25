using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Attributes;
using UIHotel.Data;

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
    }
}
