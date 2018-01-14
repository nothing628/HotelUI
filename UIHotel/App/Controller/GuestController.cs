using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data;

namespace UIHotel.App.Controller
{
    public class GuestController : BaseController
    {
        public GuestController(IRequest request) : base(request)
        {
        }

        public IResourceHandler list()
        {
            return View("Guest.List");
        }

        public IResourceHandler invoice()
        {
            var invoiceId = Query["id"];

            using (var model = new DataContext())
            {
                try
                {
                    var invoice = (from a in model.Invoices
                                   where a.Id == invoiceId
                                   select a).FirstOrDefault();

                    if (invoice != null)
                        return View("Guest.Invoice", invoice);
                }
                catch
                {

                }
            }

            return Redirect("http://localhost.com/room/get/index");
        }

        public IResourceHandler pay()
        {
            return View("Guest.Pay");
        }
    }
}
