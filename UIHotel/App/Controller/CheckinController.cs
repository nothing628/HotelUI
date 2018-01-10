using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data.Table;
using UIHotel.ViewModel;

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

        public IResourceHandler getRooms()
        {
            try
            {
                var search = jToken.Value<string>("search");
                var result = (from a in Model.Rooms
                              join b in Model.RoomCategory on a.IdCategory equals b.Id into c
                              from d in c
                              join e in Model.RoomStatus on a.Status equals e.Id into f
                              from g in f
                              where g.Id == 1 || g.Id == 2
                              where a.RoomNumber.Contains(search) || d.Category.Contains(search)
                              select new RoomModel()
                              {
                                  Id = a.Id,
                                  RoomFloor = a.RoomFloor,
                                  RoomNumber = a.RoomNumber,
                                  RoomCategory = d.Category,
                                  StatusID = g.Id,
                                  Status = g.Status,
                                  StatusColor = g.StatusColor,
                              }).ToList();

                return Json(new { data = result, success = true });
            } catch (Exception ex)
            {
                return Json(new { message = ex.Message, success = false });
            }
        }
        #endregion
    }
}
