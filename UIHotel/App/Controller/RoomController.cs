using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data.Table;

namespace UIHotel.App.Controller
{
    public class RoomController : BaseController
    {
        public RoomController(IRequest request) : base(request)
        {

        }

        public IResourceHandler index()
        {
            return View("Room.List");
        }

        public IResourceHandler detail()
        {
            return View("Room.Detail");
        }

        public IResourceHandler price()
        {
            return View("Room.Price");
        }

        public IResourceHandler category()
        {
            return View("Room.Category");
        }

        public IResourceHandler getRoom()
        {
            var tmpData = (from a in Model.Rooms
                           join b in Model.RoomCategory on a.IdCategory equals b.Id into c
                           from f in c
                           join d in Model.RoomStatus on a.Status equals d.Id into e
                           from g in e
                           select new {
                               Id = a.Id,
                               IdCategory = a.IdCategory,
                               RoomFloor = a.RoomFloor,
                               Status = a.Status,
                               RoomNumber = a.RoomNumber,
                               RoomCategory = f.Category,
                               RoomStatus = g.Status,
                           }).ToList();
            
            return Json(tmpData);
        }
    }
}
