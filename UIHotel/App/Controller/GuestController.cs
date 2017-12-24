using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IResourceHandler listCheckin()
        {
            return View("Checkin.List");
        }

        public IResourceHandler invoice()
        {
            return View("Guest.Invoice");
        }

        public IResourceHandler pay()
        {
            return View("Guest.Pay");
        }
    }
}
