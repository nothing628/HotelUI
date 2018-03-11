using CefSharp;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Attributes;
using UIHotel.App.Auth;
using UIHotel.App.Provider;
using UIHotel.Data;

namespace UIHotel.App.Controller
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IRequest request) : base(request)
        {
            //
        }

        public IResourceHandler index()
        {
            if (AuthState.IsLogin)
                return View("Index");
            else
                return Redirect("http://localhost.com/home/get/login");
        }

        [Unauthorize]
        public IResourceHandler login()
        {
            return View("Login");
        }

        public IResourceHandler logout()
        {
            AuthState.CurrentUserId = null;
            //
            return Redirect("http://localhost.com/home/get/login");
        }

        [Unauthorize]
        public IResourceHandler postLogin()
        {
            var username = jToken.Value<string>("username");
            var password = jToken.Value<string>("password");
            var hashPassword = AuthHelper.HashText(password, SettingProvider.AppKey);

            using (var data = new DataContext())
            {
                try
                {
                    var userdata = (from a in data.Users
                                    where a.Username == username
                                    where a.Password == hashPassword
                                    select a).FirstOrDefault();

                    if (userdata == null)
                        return Json(new { success = false });
                    else
                    {
                        AuthState.CurrentUserId = userdata.Id;

                        return Json(new { success = true });
                    }
                } catch
                {
                    return Json(new { success = false });
                }
            }
        }

        public IResourceHandler getDashboardData()
        {
            using (var data = new DataContext())
            {
                try
                {
                    var bdate = DateTime.Today;
                    var edate = bdate.AddDays(1);
                    var rooms = (from a in data.Rooms
                                 select new { a.Id, a.IdStatus}).ToList();

                    var free_room = (from a in rooms
                                     where a.IdStatus == 1 || a.IdStatus == 2
                                     select a).Count();
                    var used_room = rooms.Count - free_room;
                    var transaction = (from a in data.LedgerLogs
                                       where a.Date >= bdate
                                       where a.Date <= edate
                                       select a.Debit - a.Kredit).ToList();
                    var balance = transaction.Sum(x => x);

                    return Json(new { success = true, data = new {
                        guest = 0,
                        used_room,
                        free_room,
                        balance,
                    }});
                } catch
                {
                    return Json(new { success = false });
                }
            }
        }
    }
}
