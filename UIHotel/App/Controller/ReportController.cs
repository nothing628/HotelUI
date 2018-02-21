using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Attributes;
using UIHotel.Data;

namespace UIHotel.App.Controller
{
    [Authorize(Auth.AuthLevel.Manager)]
    [Authorize(Auth.AuthLevel.Administrator)]
    public class ReportController : BaseController
    {
        public ReportController(IRequest request) : base(request)
        {

        }

        public IResourceHandler checkin()
        {
            return View("Report.Checkin");
        }

        public IResourceHandler finance()
        {
            return View("Report.Finance");
        }

        public IResourceHandler getReportMoney()
        {
            var token = jToken;
            var range_type = token.Value<string>("range_type");
            var bdate = token.Value<DateTime?>("bdate");
            var edate = token.Value<DateTime?>("edate");

            using (var model = new DataContext())
            {
                try
                {
                    switch (range_type)
                    {
                        case "d":
                            // Days
                            break;
                        case "m":
                            // Monthly
                            break;
                        case "y":
                            // Yearly
                            break;
                    }

                    return Json(new { success = true });
                }
                catch
                {
                    return Json(new { success = false });
                }
            }
        }

        public IResourceHandler getReportCheckin()
        {
            var token = jToken;
            var range_type = token.Value<string>("range_type");
            var bdate = token.Value<DateTime?>("bdate");
            var edate = token.Value<DateTime?>("edate");

            using (var model = new DataContext())
            {
                try
                {
                    switch (range_type)
                    {
                        case "d":
                            // Days
                            break;
                        case "m":
                            // Monthly
                            break;
                        case "y":
                            // Yearly
                            break;
                    }

                    return Json(new { success = true });
                } catch
                {
                    return Json(new { success = false });
                }
            }
        }
    }
}
