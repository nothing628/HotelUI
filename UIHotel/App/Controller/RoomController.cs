using CefSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
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

        public IResourceHandler getCategory()
        {
            var tmpData = (from a in Model.RoomCategory
                           select a).ToList();

            return Json(tmpData);
        }

        public IResourceHandler setRoom()
        {
            var data = PostData.Elements;
            var rpp = data[0].GetBody();
            var reader = new JsonTextReader(new StringReader(rpp));
            var jtoken = JToken.ReadFrom(reader);
            var room = new Room()
            {
                RoomNumber = jtoken.Value<string>("roomNumber"),
                RoomFloor = jtoken.Value<short>("roomFloor"),
                IdCategory = jtoken.Value<long>("roomCategory"),
                Status = 1
            };

            try
            {
                Model.Rooms.Add(room);
                Model.SaveChanges();
            } catch (Exception ex)
            {
                //
            }
            
            return Json(new { success = true });
        }
    }
}
