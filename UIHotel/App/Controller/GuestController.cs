using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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

        public IResourceHandler detail()
        {
            var id_number = Query["id_number"];

            using (var model = new DataContext())
            {
                try
                {
                    var guest = (from a in model.Guests
                                 where a.IdNumber == id_number
                                 select a).FirstOrDefault();

                    return View("Guest.Detail", guest);
                } catch
                {
                    //
                }
            }

            return Redirect("http://localhost.com/guest/get/list");
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

        public IResourceHandler getGuestList()
        {
            var search = jToken.Value<string>("search");
            var page = jToken.Value<int>("page");
            var rowPerPage = jToken.Value<int>("rowsPerPage");

            using (var model = new DataContext())
            {
                try
                {
                    var guests = (from a in model.Guests
                                  orderby a.Fullname ascending
                                  select a).ToList();

                    return Json(new { success = true, data = guests });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.ToString() });
                }
            }
        }
    }
}
