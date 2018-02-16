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
using UIHotel.App.Attributes;

namespace UIHotel.App.Controller
{
    [Authorize(Auth.AuthLevel.Receptionist)]
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

            return Redirect("http://localhost.com/checkin/get/list");
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
                catch
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
            var invoiceId = Query["id"];

            using (var model = new DataContext())
            {
                try
                {
                    var invoice = (from a in model.Invoices
                                   where a.Id == invoiceId
                                   select a).FirstOrDefault();

                    if (invoice != null)
                        return View("Guest.Pay", invoice);
                } catch
                {
                    //
                }
            }

            return Redirect("http://localhost.com/checkin/get/list");
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
                    var guests = new List<Guest>();

                    if (search != "")
                    {
                        guests = (from a in model.Guests
                                  where a.Fullname.StartsWith(search)
                                  orderby a.Fullname ascending
                                  select a).ToList();
                    } else
                    {
                        guests = (from a in model.Guests
                                  orderby a.Fullname ascending
                                  select a).ToList();
                    }

                    var count = guests.Count;
                    var tmpData = guests
                        .Skip(rowPerPage * (page - 1))
                        .Take(rowPerPage)
                        .ToList();

                    return Json(new { success = true, data = tmpData, total = count });
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
                    var type = jToken.Value<string>("Type");
                    var birthDay = jToken.Value<string>("BirthDay");
                    var BirthDay = DateTime.ParseExact(birthDay, "yyyy-MM-dd", CultureInfo.CurrentCulture);
                    var guest = (from a in model.Guests
                                 where a.Id == idGuest
                                 select a).FirstOrDefault();

                    if (guest == null)
                    {
                        return Json(new { success = false, message = "Guest Not Found!" });
                    }

                    guest.IdNumber = jToken.Value<string>("IdNumber");
                    guest.IdKind = jToken.Value<string>("IdKind");
                    guest.Fullname = jToken.Value<string>("Fullname");
                    guest.Email = jToken.Value<string>("Email");
                    guest.Address = jToken.Value<string>("Address");
                    guest.Province = jToken.Value<string>("Province");
                    guest.City = jToken.Value<string>("City");
                    guest.State = jToken.Value<string>("State");
                    guest.PostCode = jToken.Value<string>("PostCode");
                    guest.BirthPlace = jToken.Value<string>("BirthPlace");
                    guest.BirthDay = BirthDay;
                    guest.Phone1 = jToken.Value<string>("Phone1");
                    guest.Phone2 = jToken.Value<string>("Phone2");
                    guest.PhotoDoc = jToken.Value<string>("DocHash");
                    guest.PhotoGuest = jToken.Value<string>("PhotoHash");
                    guest.IsVIP = (type == "VIP");
                    guest.UpdateAt = DateTime.Now;

                    model.Entry(guest).State = EntityState.Modified;
                    model.SaveChanges();

                    return Json(new { success = true, message = "Success Update" });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
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
                        IdKind = jToken.Value<string>("IdKind"),
                        Fullname = jToken.Value<string>("Fullname"),
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
