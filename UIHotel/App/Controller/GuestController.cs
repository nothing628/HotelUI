using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data;
using UIHotel.Data.Table;
using System.Globalization;

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
            var id = Convert.ToInt64(Query["id"]);

            using (var model = new DataContext())
            {
                try
                {
                    var guest = (from a in model.Guests
                                 where a.IdNumber == id_number || a.Id == id
                                 select a).FirstOrDefault();

                    if (guest != null)
                        return View("Guest.Detail", guest);
                } catch (Exception ex)
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

        public IResourceHandler edit()
        {
            var id_number = Query["id_number"];
            var id = Convert.ToInt64(Query["id"]);

            using (var model = new DataContext())
            {
                try
                {
                    var guest = (from a in model.Guests
                                 where a.IdNumber == id_number || a.Id == id
                                 select a).FirstOrDefault();

                    if (guest != null)
                        return View("Guest.Edit", guest);
                }
                catch (Exception ex)
                {
                    //
                }
            }

            return Redirect("http://localhost.com/guest/get/list");
        }

        public IResourceHandler create()
        {
            return View("Guest.Create");
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
        public IResourceHandler getGuestDetail()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var id = jToken.Value<long>("id");
                    var guest = (from a in model.Guests
                                 where a.Id == id
                                 select a).FirstOrDefault();

                    if (guest != null)
                        return Json(new { success = true, data = guest });
                    else
                        return Json(new { success = false, message = "User not found!" });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.ToString() });
                }
            }
        }
        public IResourceHandler udpateGuest()
        {
            var idGuest = jToken.Value<long>("Id");

            using (var model = new DataContext())
            {
                try
                {
                    //
                } catch
                {
                    //
                }
            }

            return Json(new { });
        }
        public IResourceHandler deleteGuest()
        {
            var idGuest = jToken.Value<long>("Id");

            using (var model = new DataContext())
            {
                try
                {
                    var guest = (from a in model.Guests
                                 where a.Id == idGuest
                                 select a).FirstOrDefault();

                    if (guest != null && AllowDelete(guest))
                    {
                        //Delete guest
                        model.Guests.Remove(guest);
                        model.SaveChanges();

                        return Json(new { success = true, message = "Guest Delete successfuly" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Guest still checkin!" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        public IResourceHandler storeGuest()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var type = jToken.Value<string>("Type");
                    var birthDay = jToken.Value<string>("BirthDay");
                    var BirthDay = DateTime.ParseExact(birthDay, "yyyy-MM-dd", CultureInfo.CurrentCulture);
                    var guest = new Guest()
                    {
                        IdNumber = jToken.Value<string>("ID"),
                        IdKind = jToken.Value<string>("Fullname"),
                        Fullname = jToken.Value<string>("IdKind"),
                        Email = jToken.Value<string>("Email"),
                        Address = jToken.Value<string>("Address"),
                        Province = jToken.Value<string>("Province"),
                        City = jToken.Value<string>("City"),
                        State = jToken.Value<string>("State"),
                        PostCode = jToken.Value<string>("PostCode"),
                        BirthPlace = jToken.Value<string>("BirthPlace"),
                        BirthDay = BirthDay,
                        Phone1 = jToken.Value<string>("Phone1"),
                        Phone2 = jToken.Value<string>("Phone2"),
                        PhotoDoc = jToken.Value<string>("DocHash"),
                        PhotoGuest = jToken.Value<string>("PhotoHash"),
                        IsVIP = (type == "VIP"),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                    };

                    model.Guests.Add(guest);
                    model.SaveChanges();

                    return Json(new { success = true, message = "Success store data" });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        public bool AllowDelete(Guest guest)
        {
            using (var model = new DataContext())
            {
                try
                {
                    var Checkin = (from a in model.CheckIn
                                   where a.IdGuest == guest.Id
                                   where !a.CheckoutAt.HasValue
                                   select a).FirstOrDefault();

                    return Checkin == null;
                } catch
                {
                    return false;
                }
            }
        }
    }
}
