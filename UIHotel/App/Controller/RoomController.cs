using CefSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var room = new Room()
            {
                RoomNumber = jToken.Value<string>("roomNumber"),
                RoomFloor = jToken.Value<short>("roomFloor"),
                IdCategory = jToken.Value<long>("roomCategory"),
                Status = 1
            };

            try
            {
                Model.Rooms.Add(room);
                Model.SaveChanges();

                return Json(new { success = true, message = "Success insert data" });
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IResourceHandler updateRoom()
        {
            try
            {
                var roomId = Convert.ToInt64(jToken.Value<string>("id"));
                var room = (from a in Model.Rooms
                            where a.Id == roomId
                            select a).FirstOrDefault();

                if (room != null)
                {
                    room.RoomNumber = jToken.Value<string>("roomNumber");
                    room.RoomFloor = jToken.Value<short>("roomFloor");
                    room.IdCategory = jToken.Value<long>("roomCategory");

                    Model.Entry(room).State = EntityState.Modified;
                    Model.SaveChanges();

                    return Json(new { success = true, message = "Success update data" });
                } else
                {
                    return Json(new { success = false, message = "Room not found" });
                }
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IResourceHandler deleteRoom()
        {
            try
            {
                var roomId = Convert.ToInt64(jToken.Value<string>("id"));
                var room = (from a in Model.Rooms
                            where a.Id == roomId
                            where a.Status == 1
                            select a).FirstOrDefault();

                if (room != null)
                {
                    Model.Rooms.Remove(room);
                    Model.SaveChanges();

                    return Json(new { success = true, message = "Data deleted" });
                } else
                {
                    return Json(new { success = false, message = "Room status must be 'Vacant'" });
                }
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
