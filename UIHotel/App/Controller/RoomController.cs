using CefSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using UIHotel.App.Attributes;
using UIHotel.Data;
using UIHotel.Data.Table;
using UIHotel.ViewModel;

namespace UIHotel.App.Controller
{
    [Authorize]
    public class RoomController : BaseController
    {
        public RoomController(IRequest request) : base(request)
        {

        }

        #region View
        public IResourceHandler index()
        {
            return View("Room.List");
        }

        public IResourceHandler change()
        {
            var id = Query["roomId"];
            var roomId = Convert.ToInt64(id);

            using (var model = new DataContext())
            {
                try
                {
                    var room = (from a in model.Rooms
                                where a.Id == roomId
                                select a).FirstOrDefault();

                    if (room != null)
                    {
                        //Push to view
                    }
                } catch
                {

                }
            }

            return View("Room.Change");
        }

        public IResourceHandler finishClean()
        {
            var id = Query["roomId"];
            var roomId = Convert.ToInt64(id);

            using (var model = new DataContext())
            {
                try
                {
                    var room = (from a in model.Rooms
                                where a.Id == roomId
                                select a).FirstOrDefault();

                    if (room != null)
                    {
                        room.IdStatus = 1;

                        model.Entry(room).State = EntityState.Modified;
                        model.SaveChanges();
                    }
                } catch
                {
                }
            }

            return Redirect("http://localhost.com/room/get/index");
        }

        public IResourceHandler detail()
        {
            var id = Query["roomId"];
            var roomId = Convert.ToInt64(id);

            using (var model = new DataContext())
            {
                try
                {
                    var room = (from a in model.Rooms
                                where a.Id == roomId
                                select a).FirstOrDefault();

                    if (room != null)
                    {
                        return View("Room.Detail", room);
                    }
                }
                catch
                {
                }
            }

            return Redirect("http://localhost.com/room/get/index");
        }

        public IResourceHandler price()
        {
            return View("Room.Price");
        }

        public IResourceHandler category()
        {
            return View("Room.Category");
        }
        #endregion
        #region Room Maintain
        public IResourceHandler getRoomData()
        {
            var search = jToken.Value<string>("search");
            var page = jToken.Value<int>("page");
            var rowPerPage = jToken.Value<int>("rowsPerPage");

            using (var model = new DataContext())
            {
                var iQuery = (from a in model.Rooms
                              join b in model.RoomCategory on a.IdCategory equals b.Id into c
                              from f in c
                              join d in model.RoomStatus on a.IdStatus equals d.Id into e
                              from g in e
                              where (search != null) ? a.RoomNumber.StartsWith(search) : true
                              orderby a.RoomNumber ascending
                              select new
                              {
                                  Id = a.Id,
                                  IdCategory = a.IdCategory,
                                  RoomFloor = a.RoomFloor,
                                  Status = a.Status,
                                  RoomNumber = a.RoomNumber,
                                  RoomCategory = f.Category,
                                  RoomStatus = g.Status,
                              });
                var count = iQuery.Count();
                var tmpData = iQuery
                    .Skip(rowPerPage * (page - 1))
                    .Take(rowPerPage)
                    .ToList();

                return Json(new { data = tmpData, total = count });
            }
        }

        public IResourceHandler storeRoom()
        {
            var room = new Room()
            {
                RoomNumber = jToken.Value<string>("roomNumber"),
                RoomFloor = jToken.Value<short>("roomFloor"),
                IdCategory = jToken.Value<long>("roomCategory"),
                IdStatus = 1
            };

            using (var model = new DataContext())
            {
                try
                {
                    model.Rooms.Add(room);
                    model.SaveChanges();

                    return Json(new { success = true, message = "Success insert data" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        public IResourceHandler updateRoom()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var roomId = Convert.ToInt64(jToken.Value<string>("id"));
                    var room = (from a in model.Rooms
                                where a.Id == roomId
                                select a).FirstOrDefault();

                    if (room != null)
                    {
                        room.RoomNumber = jToken.Value<string>("roomNumber");
                        room.RoomFloor = jToken.Value<short>("roomFloor");
                        room.IdCategory = jToken.Value<long>("roomCategory");

                        model.Entry(room).State = EntityState.Modified;
                        model.SaveChanges();

                        return Json(new { success = true, message = "Success update data" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Room not found" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        public IResourceHandler deleteRoom()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var roomId = Convert.ToInt64(jToken.Value<string>("id"));
                    var room = (from a in model.Rooms
                                where a.Id == roomId
                                where a.IdStatus == 1
                                select a).FirstOrDefault();

                    if (room != null)
                    {
                        model.Rooms.Remove(room);
                        model.SaveChanges();

                        return Json(new { success = true, message = "Data deleted" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Room status must be 'Vacant' or Room not exists!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        #endregion
        #region Category Maintain
        public IResourceHandler getCategoryData()
        {
            var search = jToken.Value<string>("search");
            var page = jToken.Value<int>("page");
            var rowPerPage = jToken.Value<int>("rowsPerPage");

            using (var model = new DataContext())
            {
                var iQuery = (from a in model.RoomCategory
                              where (search != null) ? a.Category.StartsWith(search) : true
                              orderby a.Category ascending
                              select new
                              {
                                  a.Id,
                                  a.Category,
                                  a.Description
                              });
                var count = iQuery.Count();
                var tmpData = iQuery
                    .Skip(rowPerPage * (page - 1))
                    .Take(rowPerPage)
                    .ToList();

                return Json(new { data = tmpData, total = count });
            }
        }

        public IResourceHandler storeCategory()
        {
            var category = new RoomCategory()
            {
                Category = jToken.Value<string>("category"),
                Description = jToken.Value<string>("description"),
            };

            using (var model = new DataContext())
            {
                try
                {
                    model.RoomCategory.Add(category);
                    model.SaveChanges();

                    return Json(new { success = true, message = "Success insert data" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        public IResourceHandler updateCategory()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var categoryId = Convert.ToInt64(jToken.Value<string>("id"));
                    var category = (from a in model.RoomCategory
                                    where a.Id == categoryId
                                    select a).FirstOrDefault();

                    if (category != null)
                    {
                        category.Category = jToken.Value<string>("category");
                        category.Description = jToken.Value<string>("description");

                        model.Entry(category).State = EntityState.Modified;
                        model.SaveChanges();

                        return Json(new { success = true, message = "Success update data" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Category Not Found!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        public IResourceHandler deleteCategory()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var categoryId = Convert.ToInt64(jToken.Value<string>("id"));
                    var category = (from a in model.RoomCategory
                                    where a.Id == categoryId
                                    select a).FirstOrDefault();

                    if (category != null)
                    {
                        model.RoomCategory.Remove(category);
                        model.SaveChanges();

                        return Json(new { success = true, message = "Success remove data" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Category Not Found!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        #endregion
        #region RoomList
        public IResourceHandler getRoomList()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var search = jToken.Value<string>("search");
                    var categories = GetCategoryList();
                    var status = GetStatusList();
                    var rooms = (from a in model.Rooms
                                 where (search == "") ? true : a.RoomNumber.StartsWith(search)
                                 select a).ToList();

                    var result = (from a in rooms
                                  join b in categories on a.IdCategory equals b.Id into c
                                  from d in c
                                  join e in status on a.IdStatus equals e.Id into f
                                  from g in f
                                  select new RoomModel()
                                  {
                                      DataRoom = a,
                                      DataStatus = g,
                                      RoomCategory = d.Category,
                                  }).ToList();

                    var grp = (from a in result
                               group a by a.RoomCategory into b
                               select new RoomContainer
                               {
                                   Category = b.Key,
                                   Status = status,
                                   Rooms = b.ToList()
                               }).ToList();

                    return Json(new { data = grp, status, success = true, message = "" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.ToString() });
                }
            }
        }
        public IResourceHandler getVacantRoomList()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var rooms = (from a in model.Rooms.Include(x => x.Status).Include(x => x.Category)
                                 where a.IdStatus == 1
                                 select a).ToList();

                    return Json(new { success = true, data = rooms });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.ToString() });
                }
            }
        }
        #endregion
        #region Day Maintain
        public IResourceHandler getDayEffect()
        {
            using (var model = new DataContext())
            {
                var res = GetDayEffectList();
                var days = (from a in model.DayCycles
                            select a).ToList();
                var day = (from a in days
                           join b in res on a.IdEffect equals b.Id into c
                           from d in c
                           select new { Date = a.DateAt, Effect = d.Effect })
                           .ToList()
                           .ToDictionary(x => x.Date.ToString("yyyy-MM-dd"), x => x.Effect);
                var mutate = res.ToDictionary(x => x.Effect, x => x.EffectColor);

                return Json(new { colors = mutate, items = day });
            }
        }
        public IResourceHandler setDayEffect()
        {
            var date = jToken.Value<string>("date");
            var effect = jToken.Value<string>("type");

            using (var model = new DataContext())
            {
                try
                {
                    var pdate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.CurrentCulture);
                    var dayCycle = (from a in model.DayCycles
                                    where a.DateAt == pdate
                                    select a).FirstOrDefault();
                    dayCycle.IdEffect = (from a in model.DayEffect
                                         where a.Effect == effect
                                         select a.Id).FirstOrDefault();

                    model.Entry(dayCycle).State = EntityState.Modified;
                    model.SaveChanges();

                    return Json(new { success = true, message = "Success update data" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        public IResourceHandler setColor()
        {
            var type = jToken.Value<string>("type");
            var color = jToken.Value<string>("color");

            using (var model = new DataContext())
            {
                try
                {
                    var data = (from a in model.DayEffect
                                where a.Effect == type
                                select a).SingleOrDefault();

                    if (data != null)
                    {
                        data.EffectColor = color;

                        model.SaveChanges();
                    }

                    return Json(new { success = true, message = "Success update data" });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        public IResourceHandler getPrice()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var type = jToken.Value<string>("type");
                    var eff = (from effect in model.DayEffect
                               join b in model.RoomPrice on effect.Id equals b.IdEffect into c
                               from price in c
                               join e in model.RoomCategory on price.IdCategory equals e.Id into f
                               from category in f
                               where effect.Effect == type
                               select new { effect = effect, price = price, category = category }).ToList();
                    var arrPrice = (from a in eff
                                    select new RoomPriceModel()
                                    {
                                        Category = a.category.Category,
                                        IdCategory = a.category.Id,
                                        IdEffect = a.effect.Id,
                                        Price = a.price.Price,
                                    }).ToList();

                    return Json(new { success = true, data = arrPrice });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        public IResourceHandler setPrice()
        {
            var items = jToken.Values<JToken>();

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in items)
                    {
                        var idCategory = item.Value<long>("IdCategory");
                        var idEffect = item.Value<int>("IdEffect");
                        var price = item.Value<decimal>("Price");
                        var data = (from a in model.RoomPrice
                                    where a.IdCategory == idCategory
                                    where a.IdEffect == idEffect
                                    select a).FirstOrDefault();

                        if (data != null)
                        {
                            data.Price = price;

                            model.Entry(data).State = EntityState.Modified;
                            model.SaveChanges();
                        }
                    }

                    trans.Commit();
                    return Json(new { success = true, message = "Success update data" });
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        #endregion
        #region Room Detail
        public IResourceHandler getRoomDetail()
        {
            var roomId = jToken.Value<string>("roomId");
            var Id = Convert.ToInt64(roomId);

            using (var model = new DataContext())
            {
                try
                {
                    var room = (from a in model.Rooms
                                join b in model.RoomCategory on a.IdCategory equals b.Id into c
                                from d in c
                                join e in model.RoomStatus on a.IdStatus equals e.Id into f
                                from g in f
                                where a.Id == Id
                                select new { a = a, b = d, c = g }).FirstOrDefault();

                    if (room != null)
                    {
                        var roomModel = new RoomModel()
                        {
                            DataRoom = room.a,
                            DataStatus = room.c,
                            RoomCategory = room.b.Category,
                        };

                        return Json(new { success = true, data = roomModel });
                    }

                    return Json(new { success = false, message = "Room Not Found" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        #endregion
        
        public IResourceHandler getCategory()
        {
            return Json(GetCategoryList());
        }

        private List<RoomCategory> GetCategoryList()
        {
            using (var model = new DataContext())
            {
                return (from a in model.RoomCategory
                        select a).ToList();
            }
        }

        private List<RoomStatus> GetStatusList()
        {
            using (var model = new DataContext())
            {
                return (from a in model.RoomStatus
                        select a).ToList();
            }
        }

        private List<DayEffect> GetDayEffectList()
        {
            using (var model = new DataContext())
            {
                return (from a in model.DayEffect
                        select a).ToList();
            }
        }
    }
}
