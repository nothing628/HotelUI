using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Provider;

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

        public IResourceHandler GetSettings()
        {
            return Json(new {
                success = true,
                SettingProvider.HotelName,
                SettingProvider.HotelAddress,
                SettingProvider.HotelLogo,
                SettingProvider.CheckinTime,
                SettingProvider.CheckoutTime,
                SettingProvider.Pinalty,
                SettingProvider.Deposit,
            });
        }

        public IResourceHandler SetSettings()
        {
            var token = jToken;
            var deposit = token.Value<int>("Deposit");
            var pinalty = token.Value<int>("Pinalty");
            var hotel_name = token.Value<string>("HotelName");
            var hotel_logo = token.Value<string>("HotelLogo");
            var hotel_address = token.Value<string>("HotelAddress");
            var checkin_time = token.Value<string>("CheckinTime");
            var checkout_time = token.Value<string>("CheckoutTime");

            try
            {
                SettingProvider.CheckinTime = TimeSpan.Parse(checkin_time);
                SettingProvider.CheckoutTime = TimeSpan.Parse(checkout_time);
                SettingProvider.HotelName = hotel_name;
                SettingProvider.HotelLogo = hotel_logo;
                SettingProvider.HotelAddress = hotel_address;
                SettingProvider.Deposit = deposit;
                SettingProvider.Pinalty = pinalty;
                SettingProvider.SaveSetting();

                return Json(new { success = true, message = "Setting Saved" });
            } catch
            {
                return Json(new { success = false, message = "Something Error" });
            }
        }
    }
}
