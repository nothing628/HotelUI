using CefSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using UIHotel.Data;
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

        #region Views
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

        public IResourceHandler detail()
        {
            var checkinID = Query["id"];

            using (var model = new DataContext())
            {
                try
                {
                    var checkin = (from a in model.CheckIn
                                   where a.Id == checkinID
                                   select a).FirstOrDefault();

                    if (checkin != null)
                        return View("Checkin.Detail");
                } catch 
                {
                    //
                }
            }

            return Redirect("http://localhost.com/checkin/get/list");
        }

        public IResourceHandler listBooking()
        {
            return View("Booking.List");
        }

        public IResourceHandler listCheckin()
        {
            return View("Checkin.List");
        }
        #endregion

        #region Process Checkin
        public IResourceHandler postCheckin()
        {
            var guest = jToken["guest"];
            var registration = jToken["registration"];
            var room = jToken["room"];

            try
            {
                var dataGuest = ProcessGuest(guest);
                var dataCheckin = ProcessCheckin(room, registration, dataGuest);
                var dataInvoice = ProcessInvoice(dataCheckin, registration, dataGuest);
                var retUrl = string.Format("http://localhost.com/guest/get/invoice?id={0}", dataInvoice.Id);

                return Json(new { success = true, redirect_url = retUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString() });
            }
        }

        public Guest ProcessGuest(JToken token)
        {
            var guestId = token.Value<string>("id_number");

            using (var model = new DataContext())
            {
                try
                {
                    var dataGuest = (from a in model.Guests
                                     where a.IdNumber == guestId
                                     select a).FirstOrDefault();

                    if (dataGuest == null)
                    {
                        var name = token.Value<string>("name");
                        var email = token.Value<string>("email");
                        var birth_place = token.Value<string>("birth_place");
                        var birth_day = token.Value<string>("birth_day");
                        var photo_doc = token.Value<string>("photo_doc");
                        var photo_guest = token.Value<string>("photo_guest");
                        var address = token["address"].Value<string>("note");
                        var city = token["address"].Value<string>("city");
                        var province = token["address"].Value<string>("province");
                        var postcode = token["address"].Value<string>("postcode");
                        var state = token["address"].Value<string>("state");
                        var type = token.Value<string>("type");
                        var phone1 = token["phone"].Value<string>("phone1");
                        var phone2 = token["phone"].Value<string>("phone2");

                        var BirthDay = DateTime.ParseExact(birth_day, "yyyy-MM-dd", CultureInfo.CurrentCulture);

                        var guest = new Guest()
                        {
                            Address = address,
                            City = city,
                            Province = province,
                            State = state,
                            PostCode = postcode,
                            Phone1 = phone1,
                            Phone2 = phone2,
                            PhotoDoc = photo_doc,
                            PhotoGuest = photo_guest,
                            IdNumber = guestId,
                            BirthPlace = birth_place,
                            BirthDay = BirthDay,
                            IsVIP = (type == "VIP"),
                            Fullname = name,
                            Email = email,
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                        };

                        model.Guests.Add(guest);
                        model.SaveChanges();
                        // Create new User Data
                        return guest;
                    }
                    else
                    {
                        return dataGuest;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public Checkin ProcessCheckin(JToken tokenRoom, JToken tokenReg, Guest guest)
        {
            var note = tokenRoom.Value<string>("note");
            var room_id = tokenRoom.Value<long>("room_id");
            var booking = tokenReg.Value<string>("book_no");

            var arrAt = tokenReg.Value<DateTime>("arr_date");
            var deptAt = tokenReg.Value<DateTime>("dep_date");
            var countAdl = tokenReg.Value<short>("adl_count");
            var countChl = tokenReg.Value<short>("chl_count");

            var dataCheckin = new Checkin()
            {
                Id = Checkin.GenerateID(),
                ArriveAt = arrAt,
                DepartureAt = deptAt,
                CountAdult = countAdl,
                CountChild = countChl,
                IdGuest = guest.Id,
                IdRoom = room_id,
                CheckinAt = DateTime.Now,
                Note = note,
            };

            //TODO : should update booking detail too..
            if (booking != null)
                dataCheckin.IdBooking = booking;

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var room = (from a in model.Rooms
                                where a.Id == room_id
                                select a).FirstOrDefault();

                    if (room != null)
                    {
                        //Change room status to Occupied
                        room.Status = 3;
                        model.Entry(room).State = System.Data.Entity.EntityState.Modified;
                    }

                    //Add checkin record
                    model.CheckIn.Add(dataCheckin);
                    model.SaveChanges();

                    trans.Commit();

                    return dataCheckin;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public Invoice ProcessInvoice(Checkin checkin, JToken tokenReg, Guest guest)
        {
            var deposit = tokenReg.Value<decimal>("deposit");
            var inv = new Invoice()
            {
                Id = Invoice.GenerateID(),
                IdCheckin = checkin.Id,
                IdGuest = guest.Id,
                IsClosed = false,
                UpdateAt = DateTime.Now,
                CreateAt = DateTime.Now
            };
            var invDet = new InvoiceDetail()
            {
                IdInvoice = inv.Id,
                AmmountIn = deposit,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Description = "Deposit",
            };

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    model.Invoices.Add(inv);
                    model.InvoiceDetails.Add(invDet);
                    model.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return inv;
        }

        /// <summary>
        /// Get available room
        /// </summary>
        /// <returns></returns>
        public IResourceHandler getRooms()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var search = jToken.Value<string>("search");
                    var result = (from a in model.Rooms
                                  join b in model.RoomCategory on a.IdCategory equals b.Id into c
                                  from d in c
                                  join e in model.RoomStatus on a.Status equals e.Id into f
                                  from g in f
                                  where g.Id == 1 || g.Id == 2
                                  where a.RoomNumber.Contains(search) || d.Category.Contains(search)
                                  select new RoomModel()
                                  {
                                      DataRoom = a,
                                      DataStatus = g,
                                      RoomCategory = d.Category,
                                  }).ToList();

                    return Json(new { data = result, success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { message = ex.Message, success = false });
                }
            }
        }
        #endregion

        #region API
        /// <summary>
        /// Get Checkin list, including the late checkout
        /// </summary>
        /// <returns></returns>
        public IResourceHandler getCheckinList()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var data = (from a in model.CheckIn
                                .Include(x => x.Room)
                                .Include(x => x.Room.Category)
                                .Include(x => x.Guest)
                                join b in model.Invoices on a.Id equals b.IdCheckin into c
                                from d in c
                                select new CheckinContainer()
                                {
                                    DataCheckin = a,
                                    DataGuest = a.Guest,
                                    DataRoom = a.Room,
                                    DataCategory = a.Room.Category,
                                    DataInvoice = d,
                                }).ToList();

                    return Json(new { data = data, success = true });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
        #endregion
    }
}
