using CefSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data.Table;
using UIHotel.ViewModel;

namespace UIHotel.App.Controller
{
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
        #endregion
        #region Room Maintain
        public IResourceHandler getRoomData()
        {
            var search = jToken.Value<string>("search");
            var page = jToken.Value<int>("page");
            var rowPerPage = jToken.Value<int>("rowsPerPage");
            var iQuery = (from a in Model.Rooms
                          join b in Model.RoomCategory on a.IdCategory equals b.Id into c
                          from f in c
                          join d in Model.RoomStatus on a.Status equals d.Id into e
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

        public IResourceHandler storeRoom()
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
            }
            catch (Exception ex)
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
        #endregion
        #region Category Maintain
        public IResourceHandler getCategoryData()
        {
            var search = jToken.Value<string>("search");
            var page = jToken.Value<int>("page");
            var rowPerPage = jToken.Value<int>("rowsPerPage");
            var iQuery = (from a in Model.RoomCategory
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

        public IResourceHandler storeCategory()
        {
            var category = new RoomCategory()
            {
                Category = jToken.Value<string>("category"),
                Description = jToken.Value<string>("description"),
            };

            try
            {
                Model.RoomCategory.Add(category);
                Model.SaveChanges();

                return Json(new { success = true, message = "Success insert data" });
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IResourceHandler updateCategory()
        {
            try
            {
                var categoryId = Convert.ToInt64(jToken.Value<string>("id"));
                var category = (from a in Model.RoomCategory
                                where a.Id == categoryId
                                select a).FirstOrDefault();

                if (category != null)
                {
                    category.Category = jToken.Value<string>("category");
                    category.Description = jToken.Value<string>("description");

                    Model.Entry(category).State = EntityState.Modified;
                    Model.SaveChanges();

                    return Json(new { success = true, message = "Success update data" });
                }
                else
                {
                    return Json(new { success = false, message = "Category Not Found!" });
                }
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IResourceHandler deleteCategory()
        {
            try
            {
                var categoryId = Convert.ToInt64(jToken.Value<string>("id"));
                var category = (from a in Model.RoomCategory
                                where a.Id == categoryId
                                select a).FirstOrDefault();

                if (category != null)
                {
                    Model.RoomCategory.Remove(category);
                    Model.SaveChanges();

                    return Json(new { success = true, message = "Success remove data" });
                } else
                {
                    return Json(new { success = false, message = "Category Not Found!" });
                }
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion
        #region RoomList
        public IResourceHandler getRoomList()
        {
            try
            {
                var search = jToken.Value<string>("search");
                var categories = GetCategoryList();
                var status = GetStatusList();
                var rooms = (from a in Model.Rooms
                             where (search == "") ? true : a.RoomNumber.StartsWith(search)
                             select a).ToList();

                var result = (from a in rooms
                             join b in categories on a.IdCategory equals b.Id into c
                             from d in c
                             join e in status on a.Status equals e.Id into f
                             from g in f
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

                var grp = (from a in result
                           group a by a.RoomCategory into b
                           select new
                           {
                               Category = b.Key,
                               Rooms = b.ToList()
                           }).ToList();

                return Json(new { data = grp, success = true, message = "" });
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString() });
            }
        }
        #endregion
        #region Day Maintain
        public IResourceHandler getDayEffect()
        {
            var res = GetDayEffectList();
            var days = (from a in Model.DayCycles
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
        public IResourceHandler setDayEffect()
        {
            var date = jToken.Value<string>("date");
            var effect = jToken.Value<string>("type");

            try
            {
                var pdate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.CurrentCulture);
                var dayCycle = (from a in Model.DayCycles
                                where a.DateAt == pdate
                                select a).FirstOrDefault();
                var effectID = (from a in Model.DayEffect
                                where a.Effect == effect
                                select a.Id).FirstOrDefault();

                dayCycle.IdEffect = effectID;

                Model.Entry(dayCycle).State = EntityState.Modified;
                Model.SaveChanges();

                return Json(new { success = true, message = "Success update data" });
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IResourceHandler getPrice()
        {
            try
            {
                var type = jToken.Value<string>("type");
                var eff = (from effect in Model.DayEffect
                           join b in Model.RoomPrice on effect.Id equals b.IdEffect into c
                           from price in c
                           join e in Model.RoomCategory on price.IdCategory equals e.Id into f
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
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IResourceHandler setPrice()
        {
            var idCategory = jToken.Value<long>("IdCategory");
            var idEffect = jToken.Value<int>("IdEffect");
            var price = jToken.Value<decimal>("Price");

            try
            {
                var data = (from a in Model.RoomPrice
                            where a.IdCategory == idCategory
                            where a.IdEffect == idEffect
                            select a).FirstOrDefault();

                if (data != null)
                {
                    data.Price = price;

                    Model.Entry(data).State = EntityState.Modified;
                    Model.SaveChanges();
                }

                return Json(new { success = true, message = "Success update data" });
            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        public IResourceHandler getCategory()
        {
            return Json(GetCategoryList());
        }

        private List<RoomCategory> GetCategoryList()
        {
            return (from a in Model.RoomCategory
                    select a).ToList();
        }

        private List<RoomStatus> GetStatusList()
        {
            return (from a in Model.RoomStatus
                    select a).ToList();
        }

        private List<DayEffect> GetDayEffectList()
        {
            return (from a in Model.DayEffect
                    select a).ToList();
        }
    }
}
