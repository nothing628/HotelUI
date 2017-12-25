using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.Controller
{
    public class SettingController : BaseController
    {
        public SettingController(IRequest request) : base(request)
        {

        }

        public IResourceHandler index()
        {
            return View("Setting.Index");
        }

        public IResourceHandler calendar()
        {
            return View("Setting.Calendar");
        }

        public IResourceHandler rate()
        {
            return View("Setting.RateType");
        }

        public IResourceHandler user()
        {
            return View("Setting.User");
        }

        public IResourceHandler room()
        {
            return View("Setting.Room");
        }
    }
}
