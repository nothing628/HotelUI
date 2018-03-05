using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Attributes;

namespace UIHotel.App.Controller
{
    [AllowAuthorize]
    public class SetupController : BaseController
    {
        public SetupController(IRequest request) : base(request)
        {
            //
        }

        #region View
        public IResourceHandler index()
        {
            return View("Setup.Index");
        }
        #endregion

        #region API
        public IResourceHandler SetDatabase()
        {
            return Json(new { success = false });
        }
        #endregion
    }
}
