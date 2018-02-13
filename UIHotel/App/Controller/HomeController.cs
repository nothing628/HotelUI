using CefSharp;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Auth;
using UIHotel.Data;

namespace UIHotel.App.Controller
{
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

        public IResourceHandler postLogin()
        {
            var username = jToken.Value<string>("username");
            var password = jToken.Value<string>("password");
            var hashPassword = AuthHelper.HashText(password, "appkey"); //TODO: provide public appkey

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

        public IResourceHandler test()
        {
            var query = Query["foo"];
            var data = Query["data"];
            
            var obj = new { Nama = "A" };

            ViewBag.Name = "test";

            return View("Test", obj);
        }
    }
}
