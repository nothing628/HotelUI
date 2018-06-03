using CefSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using UIHotel.App.Attributes;
using UIHotel.App.Routine;
using UIHotel.Data;
using UIHotel.Data.Table;
using UIHotel.ViewModel;

namespace UIHotel.App.Controller
{
    [Authorize(Auth.AuthLevel.Receptionist)]
    [Authorize(Auth.AuthLevel.Administrator)]
    [Authorize(Auth.AuthLevel.Manager)]
    public class CheckinController : BaseController
    {
        public CheckinController(IRequest request) : base(request)
        {
            //
        }

        #region Views
        public IResourceHandler index()
        {
            var roomId = Query["roomId"];
            var bookId = Query["bookid"];

            if (roomId != null)
            {
                using (var model = new DataContext())
                {
                    var id = Convert.ToInt32(roomId);
                    var room = (from a in model.Rooms.Include(x => x.Category).Include(x => x.Status)
                                where a.Id == id
                                select a).FirstOrDefault();

                    if (room != null)
                    {
                        return View("Checkin.Checkin", new CheckModel(){ room = room });
                    }
                }
            }

            if (bookId != null)
            {
                // If this from booking list
                using (var model = new DataContext())
                {
                    var id = bookId;
                    var book = (from a in model.Bookings
                                where a.Id == id
                                select a).FirstOrDefault();

                    if (book != null)
                    {
                        return View("Checkin.Checkin", new CheckModel() { booking = book });
                    }
                }
            }

            return View("Checkin.Checkin");
        }

        public IResourceHandler checkout()
        {
            var id = Query["id"];

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var checkin = (from a in model.CheckIn
                                   where a.Id == id
                                   select a).FirstOrDefault();

                    if (checkin != null)
                    {
                        checkin.CheckoutAt = DateTime.Now;

                        var room = (from a in model.Rooms
                                    where a.Id == checkin.IdRoom
                                    select a).FirstOrDefault();

                        if (room != null)
                            room.IdStatus = 4;

                        model.SaveChanges();
                    }

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }

            return Redirect("http://localhost.com/checkin/get/list");
        }

        public IResourceHandler booking()
        {
            return View("Booking.Index");
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
                                   where a.CheckoutAt == null
                                   select a).FirstOrDefault();

                    if (checkin != null)
                        return View("Checkin.Detail", checkin);
                } catch
                {
                    //
                }
            }

            return Redirect("http://localhost.com/checkin/get/list");
        }

        public IResourceHandler editBooking()
        {
            var id = Query["id"];

            using (var model = new DataContext())
            {
                try
                {
                    var booking = (from a in model.Bookings
                                   where a.Id == id
                                   select a).FirstOrDefault();

                    if (booking != null)
                        return View("Booking.Edit", booking);
                } catch
                {
                    //
                }
            }

            return Redirect("http://localhost.com/checkin/get/listBooking");
        }

        public IResourceHandler cancelBooking()
        {
            var id = Query["id"];

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var booking = (from a in model.Bookings
                                   where a.Id == id
                                   select a).FirstOrDefault();

                    if (booking != null)
                    {
                        var room = (from a in model.Rooms
                                    where a.Id == booking.IdRoom
                                    select a).Single();

                        room.IdStatus = 1;

                        model.Entry(room).State = EntityState.Modified;
                        model.Entry(booking).State = EntityState.Deleted;
                        model.SaveChanges();

                        trans.Commit();

                        return Json(new { success = true });
                    }
                }
                catch
                {
                    trans.Rollback();
                }
            }

            return Json(new { success = false });
        }

        public IResourceHandler listBooking()
        {
            return View("Booking.List");
        }

        public IResourceHandler list()
        {
            return View("Checkin.List");
        }

        public IResourceHandler change()
        {
            var checkid = Query["id"];

            using (var model = new DataContext())
            {
                try
                {
                    var checkin = (from a in model.CheckIn
                                   where a.Id == checkid
                                   where a.CheckoutAt == null
                                   select a).SingleOrDefault();

                    if (checkin != null)
                    {
                        if (DateTime.Now > checkin.CheckinAt.AddMinutes(30))
                        {
                            return Redirect("http://localhost.com/checkin/get/detail?id=" + checkid);
                        } else
                        {
                            //Push to view
                            return View("Room.Change", checkin);
                        }
                    }
                }
                catch
                {

                }
            }

            return Redirect("http://localhost.com/checkin/get/index");
        }
        #endregion

        #region Process Checkin & Booking
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
                var retUrl = string.Format("http://localhost.com/checkin/get/detail?id={0}", dataInvoice.Id);

                return Json(new { success = true, redirect_url = retUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString() });
            }
        }

        public IResourceHandler postBooking()
        {
            var guest = jToken["guest"];
            var registration = jToken["registration"];
            var room = jToken["room"];

            try
            {
                var dataGuest = ProcessGuest(guest);
                var dataBooking = ProcessBooking(room, registration, dataGuest);
                var retUrl = string.Format("http://localhost.com/checkin/get/listBooking");
                var checkinUrl = string.Format("http://localhost.com/checkin/get/index?bookid={0}", dataBooking.Id);

                return Json(new {
                    success = true,
                    redirect_url = retUrl,
                    checkin_url = checkinUrl,
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.ToString() });
            }
        }

        public Guest ProcessGuest(JToken token)
        {
            using (var model = new DataContext())
            {
                try
                {
                    var id = token.Value<long?>("id");
                    var dataGuest = (from a in model.Guests
                                     where a.Id == id
                                     select a).FirstOrDefault();

                    if (dataGuest == null || !id.HasValue)
                    {
                        var birth_day = token.Value<string>("birth_day");
                        var type = token.Value<string>("type");

                        var BirthDay = DateTime.ParseExact(birth_day, "yyyy-MM-dd", CultureInfo.CurrentCulture);

                        var guest = new Guest()
                        {
                            Address = token["address"].Value<string>("note"),
                            City = token["address"].Value<string>("city"),
                            Province = token["address"].Value<string>("province"),
                            State = token["address"].Value<string>("state"),
                            PostCode = token["address"].Value<string>("postcode"),
                            Phone1 = token["phone"].Value<string>("phone1"),
                            Phone2 = token["phone"].Value<string>("phone2"),
                            PhotoDoc = token.Value<string>("photo_doc"),
                            PhotoGuest = token.Value<string>("photo_guest"),
                            IdNumber = token.Value<string>("id_number"),
                            IdKind = token.Value<string>("id_kind"),
                            BirthPlace = token.Value<string>("birth_place"),
                            BirthDay = BirthDay,
                            IsVIP = (type == "VIP"),
                            Fullname = token.Value<string>("name"),
                            Email = token.Value<string>("email"),
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
                catch
                {
                    throw;
                }
            }
        }

        public Booking ProcessBooking(JToken tokenRoom, JToken tokenReg, Guest guest)
        {
            Booking result = null;

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var id_room = tokenRoom["room_id"].Value<long>();
                    var bookno = tokenReg.Value<string>("book_no");
                    var type = tokenReg.Value<int>("book_type");
                    var oltype = tokenReg.Value<int>("book_oltype");
                    var arrAt = tokenReg.Value<DateTime>("arr_date");
                    var deptAt = tokenReg.Value<DateTime>("dep_date");
                    var countAdl = tokenReg.Value<short>("adl_count");
                    var countChl = tokenReg.Value<short>("chl_count");

                    result = new Booking()
                    {
                        ArriveAt = arrAt,
                        DepartureAt = deptAt,
                        CountAdult = countAdl,
                        CountChild = countChl,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IdGuest = guest.Id,
                        IdRoom = id_room,
                        IsCheckin = false,
                        Id = Booking.GenerateID(),
                    };

                    var room = (from a in model.Rooms
                                where a.Id == id_room
                                select a).Single();

                    room.IdStatus = 2;

                    result.IdType = (type != -1) ? type : oltype;
                    result.Id = (type != -1) ? Booking.GenerateID() : bookno;

                    model.Bookings.Add(result);
                    model.SaveChanges();

                    trans.Commit();
                } catch
                {
                    trans.Rollback();
                }
            }

            return result;
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
            
            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (!string.IsNullOrEmpty(booking))
                    {
                        //TODO: Update booking here
                        var book = (from a in model.Bookings
                                    where a.Id == booking
                                    select a).Single();

                        book.IsCheckin = true;
                        book.IdCheckin = dataCheckin.Id;

                        model.SaveChanges();
                    }

                    var room = (from a in model.Rooms
                                where a.Id == room_id
                                select a).FirstOrDefault();

                    if (room != null)
                    {
                        //Change room status to Occupied
                        room.IdStatus = 3;
                        model.Entry(room).State = EntityState.Modified;
                    }

                    //Add checkin record
                    model.CheckIn.Add(dataCheckin);
                    model.SaveChanges();

                    trans.Commit();

                    return dataCheckin;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public Invoice ProcessInvoice(Checkin checkin, JToken tokenReg, Guest guest)
        {
            var pointDate = checkin.ArriveAt.Date;
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
                IdKind = 97,
                AmmountIn = deposit,
                TransactionDate = DateTime.Now,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Description = "Deposit",
            };

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    model.Invoices.Add(inv);
                    model.SaveChanges();
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }

            new CalcPinalty().CalculateRoomCharge(inv);

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
                    var book_id = jToken.Value<string>("book_id");
                    var result = new List<RoomModel>();
                    var rawResult = (from a in model.Rooms.Include(x => x.Category).Include(x => x.Status)
                                     where a.IdStatus == 1 || a.IdStatus == 2
                                     where a.RoomNumber.Contains(search) || a.Category.Category.Contains(search)
                                     select a).ToList();

                    if (book_id != "")
                    {
                        var bookDetail = (from a in model.Bookings
                                          where a.Id == book_id
                                          where !a.IsCheckin
                                          select a).ToList();

                        result = (from a in rawResult
                                  join b in bookDetail on a.Id equals b.IdRoom into c
                                  from d in c
                                  select new RoomModel()
                                  {
                                      DataRoom = a,
                                      DataStatus = a.Status,
                                      RoomCategory = a.RoomCategory,
                                  }).ToList();
                    } else
                    {
                        result = (from a in rawResult
                                  select new RoomModel()
                                  {
                                      DataRoom = a,
                                      DataStatus = a.Status,
                                      RoomCategory = a.RoomCategory,
                                  }).ToList();
                    }

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
                                where a.CheckoutAt == null
                                select new CheckinContainer()
                                {
                                    DataCheckin = a,
                                    DataGuest = a.Guest,
                                    DataRoom = a.Room,
                                    DataCategory = a.Room.Category,
                                    DataInvoice = d,
                                }).ToList();

                    return Json(new { data, success = true });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        public IResourceHandler getCheckinDetail()
        {
            var checkId = jToken.Value<string>("id");

            using (var model = new DataContext())
            {
                try
                {
                    var checkin = (from a in model.CheckIn
                                   .Include(x => x.Guest)
                                   .Include(x => x.Room)
                                   .Include(x => x.Room.Status)
                                   .Include(x => x.Room.Category)
                                   where a.Id == checkId
                                   select a).FirstOrDefault();

                    if (checkin != null)
                        return Json(new { success = true, data = checkin });
                    else
                        return Json(new { success = false, message = "Checkin Not Found" });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.ToString() });
                }   
            }
        }

        public IResourceHandler getInvoiceDetail()
        {
            var id = jToken.Value<string>("id");

            using (var model = new DataContext())
            {
                try
                {
                    var invoices = (from a in model.Invoices.Include(x => x.Details)
                                    where a.Id == id
                                    select a).FirstOrDefault();
                    var guest = (from a in model.Guests
                                 where a.Id == invoices.IdGuest
                                 select a).FirstOrDefault();

                    invoices.Details = invoices.Details.OrderBy(x => x.CreateAt).ToList();

                    if (invoices != null && guest != null)
                        return Json(new { success = true, data = invoices, guest });
                    else
                        return Json(new { success = false, message = "Invoice not found!" });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.ToString() });
                }
            }
        }

        public IResourceHandler getBookingType()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var lstType = (from a in model.BookingType
                                   select a).ToList();

                    return Json(new { success = true, data = lstType });
                } catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.ToString() });
                }
            }
        }

        public IResourceHandler getBookingList()
        {
            var page = jToken.Value<int>("page");
            var rowPerPage = jToken.Value<int>("rowsPerPage");

            using (var model = new DataContext())
            {
                try
                {
                    var iQuery = (from a in model.Bookings
                                  where !a.IsCheckin
                                  orderby a.ArriveAt ascending
                                  select a);

                    var count = iQuery.Count();
                    var tmpData = iQuery
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

        public IResourceHandler payInvoice()
        {
            var id = jToken.Value<string>("id");
            var pay = jToken.Value<decimal>("pay");

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var invoice = (from a in model.Invoices
                                   where a.Id == id
                                   select a).FirstOrDefault();

                    if (invoice != null && pay > 0)
                    {
                        var description = "Payment<br><i>" + DateTime.Now.ToString("dd-MM-yyyy") + "</i>";
                        var detail = new InvoiceDetail()
                        {
                            AmmountIn = pay,
                            AmmountOut = 0,
                            Description = description,
                            IsSystem = false,
                            IdInvoice = invoice.Id,
                            IdKind = 100,
                            TransactionDate = DateTime.Now,
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                        };

                        model.InvoiceDetails.Add(detail);
                        model.SaveChanges();
                    }

                    trans.Commit();
                    return Json(new { success = true, message = "Success update data" });
                } catch
                {
                    trans.Rollback();
                    return Json(new { success = false, message = "Failed to update invoice table" });
                }
            }
        }

        public IResourceHandler closeInvoice()
        {
            var id = jToken.Value<string>("id");
            var cashback = jToken.Value<decimal>("cashback");

            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    var invoice = (from a in model.Invoices
                                   where a.Id == id
                                   select a).SingleOrDefault();
                    
                    if (invoice != null)
                    {
                        // Recalculate Tax
                        new CalcPinalty().CalculateTax(invoice);

                        var detail = new InvoiceDetail()
                        {
                            AmmountOut = cashback,
                            AmmountIn = 0,
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                            TransactionDate = DateTime.Now,
                            Description = "Cashback",
                            IsSystem = true,
                            IdKind = 98,
                            IdInvoice = invoice.Id,
                        };

                        model.InvoiceDetails.Add(detail);

                        // Allow to close invoice
                        invoice.IsClosed = true;
                        invoice.UpdateAt = DateTime.Now;

                        model.Entry(invoice).State = EntityState.Modified;
                        model.SaveChanges();
                    }

                    trans.Commit();
                    return Json(new { success = true, message = "Success update data" });
                }
                catch
                {
                    trans.Rollback();
                    return Json(new { success = false, message = "Failed to update invoice table" });
                }
            }
        }

        public IResourceHandler changeRoom()
        {
            var roomid = jToken.Value<int>("roomid");
            var checkid = jToken.Value<string>("checkid");

            using (var model = new DataContext())
            {
                try
                {
                    var checkin = (from a in model.CheckIn
                                   where a.Id == checkid
                                   select a).Single();
                    var roomFirst = (from a in model.Rooms.Include(x => x.Category)
                                     where a.Id == checkin.IdRoom
                                     select a).Single();
                    var roomLast = (from a in model.Rooms.Include(x => x.Category)
                                    where a.Id == roomid
                                    select a).Single();

                    checkin.IdRoom = roomid;
                    roomFirst.IdStatus = 1;
                    roomLast.IdStatus = 3;

                    if (roomLast.GetPrice() > roomFirst.GetPrice())
                    {
                        //Create invoice here
                        var diff = roomLast.GetPrice() - roomFirst.GetPrice();
                        var inv = new InvoiceDetail()
                        {
                            IdInvoice = checkin.Invoice.Id,
                            IdKind = 2,
                            IdRoom = roomLast.Id,
                            AmmountOut = diff,
                            AmmountIn = 0,
                            Description = "Move room Charge from " + roomFirst.RoomNumber + "' to '" + roomLast.RoomNumber + "'",
                            IsSystem = true,
                            TransactionDate = DateTime.Today,
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now,
                        };

                        model.InvoiceDetails.Add(inv);
                    }

                    model.SaveChanges();
                    return Json(new { success = true, redirect_uri = "http://localhost.com/checkin/get/detail?id=" + checkin.Id });
                }
                catch
                {
                    //
                }
            }

            return Json(new { success = false });
        }
        #endregion
    }

    public class CheckModel {
        public Room room { get; set; }
        public Booking booking { get; set; }
    }
}
