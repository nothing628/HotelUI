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
    }
}
