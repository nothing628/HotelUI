using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.Router
{
    public static class RouterControl
    {
        public static void GetRoute(List<RouteModel> listRoute)
        {
            listRoute.Add(new RouteModel("home/get/{action}", "HomeController"));
            listRoute.Add(new RouteModel("home/post/{action}", "HomeController", Method: "POST"));
            listRoute.Add(new RouteModel("checkin/get/{action}", "CheckinController"));
            listRoute.Add(new RouteModel("checkin/post/{action}", "CheckinController", Method: "POST"));
            listRoute.Add(new RouteModel("room/get/{action}", "RoomController"));
            listRoute.Add(new RouteModel("room/post/{action}", "RoomController", Method: "POST"));
            listRoute.Add(new RouteModel("setting/get/{action}", "SettingController"));
            listRoute.Add(new RouteModel("setting/post/{action}", "SettingController", Method: "POST"));
            listRoute.Add(new RouteModel("report/get/{action}", "ReportController"));
            listRoute.Add(new RouteModel("report/post/{action}", "ReportController", Method: "POST"));
            listRoute.Add(new RouteModel("money/get/{action}", "MoneyController"));
            listRoute.Add(new RouteModel("money/post/{action}", "MoneyController", Method: "POST"));
            listRoute.Add(new RouteModel("guest/get/{action}", "GuestController"));
            listRoute.Add(new RouteModel("guest/post/{action}", "GuestController", Method: "POST"));
        }
    }
}
