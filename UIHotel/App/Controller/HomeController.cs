using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.Controller
{
    public class HomeController : BaseController
    {
        public HomeController(IRequest request) : base(request)
        {
            //
        }

        public IResourceHandler index()
        {
            return View("Index");
        }

        public IResourceHandler login()
        {
            return View("Login");
        }

        public IResourceHandler booking()
        {
            return View("Checkin.Booking");
        }

        public IResourceHandler checkin()
        {
            return View("Checkin.Checkin");
        }

        public IResourceHandler checkout()
        {
            return View("Checkin.Checkout");
        }

        public IResourceHandler room()
        {
            return View("Checkin.Room");
        }

        public IResourceHandler roomdetail()
        {
            return View("Checkin.RoomDetail");
        }
    }
}
