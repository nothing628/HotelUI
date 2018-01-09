using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data.Table;

namespace UIHotel.App.Controller
{
    public class CheckinController : BaseController
    {
        public CheckinController(IRequest request) : base(request)
        {
            //
        }

        public IResourceHandler index()
        {
            return View("Checkin.Checkin");
        }

        public IResourceHandler checkout()
        {
            return View("Checkin.Checkout");
        }

        public IResourceHandler booking()
        {
            return View("Checkin.Booking");
        }
        
        public IResourceHandler listBooking()
        {
            return View("Booking.List");
        }

        #region Process Checkin
        public IResourceHandler postCheckin()
        {
            return Json(new { });
        }

        public Guest postGuest()
        {
            return new Guest();
        }
        #endregion
    }
}
