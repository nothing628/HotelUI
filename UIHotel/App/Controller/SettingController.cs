using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Attributes;
using UIHotel.App.Auth;
using UIHotel.App.Provider;
using UIHotel.Data;
using UIHotel.Data.Table;

namespace UIHotel.App.Controller
{
    [Authorize(Auth.AuthLevel.Administrator)]
    [Authorize(Auth.AuthLevel.Manager)]
    public class SettingController : BaseController
    {
        public SettingController(IRequest request) : base(request)
        {

        }

        #region Page
        public IResourceHandler index()
        {
            return View("Setting.Index");
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

        [AllowAuthorize]
        public IResourceHandler app()
        {
            return View("Setting.App");
        }
        #endregion

        #region API
        public IResourceHandler GetUsers()
        {
            var username = jToken.Value<string>("item_username");

            using (var model = new DataContext())
            {
                try
                {
                    var users = new List<User>();

                    if (username == "")
                    {
                        users = (from a in model.Users
                                orderby a.Username ascending
                                select a).ToList();
                    } else
                    {
                        users = (from a in model.Users
                                 where a.Username.Contains(username) || a.Fullname.Contains(username)
                                 orderby a.Username ascending
                                 select a).ToList();
                    }

                    var roles = GetRoles();

                    return Json(new { success = true, data = users, roles });
                } catch
                {
                    return Json(new { success = false });
                }
            }
        }

        public IResourceHandler SaveUser()
        {
            var username = jToken.Value<string>("item_username");
            var password = jToken.Value<string>("item_password");
            var role = jToken.Value<int>("item_role");
            var fullname = jToken.Value<string>("item_fullname");

            using (var model = new DataContext())
            {
                try
                {
                    var user = new User()
                    {
                        Username = username,
                        Password = AuthHelper.HashText(password, SettingProvider.AppKey),
                        Fullname = fullname,
                        Permission = role,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                    };

                    model.Users.Add(user);
                    model.SaveChanges();

                    return Json(new { success = true });
                }
                catch
                {
                    return Json(new { success = false });
                }
            }
        }

        public IResourceHandler UpdateUser()
        {
            var id = jToken.Value<long>("item_id");
            var username = jToken.Value<string>("item_username");
            var password = jToken.Value<string>("item_password");
            var role = jToken.Value<int>("item_role");
            var fullname = jToken.Value<string>("item_fullname");

            using (var model = new DataContext())
            {
                try
                {
                    var user = (from a in model.Users
                                where a.Id == id
                                select a).First();

                    user.Username = username;
                    user.Fullname = fullname;
                    user.Permission = role;
                    user.UpdateAt = DateTime.Now;

                    if (password.Length > 0)
                        user.Password = AuthHelper.HashText(password, SettingProvider.AppKey);

                    model.SaveChanges();

                    return Json(new { success = true });
                }
                catch
                {
                    return Json(new { success = false });
                }
            }
        }

        public IResourceHandler DeleteUser()
        {
            var id = jToken.Value<long>("item_id");

            using (var model = new DataContext())
            {
                try
                {
                    var user = (from a in model.Users
                                where a.Id == id
                                select a).First();

                    model.Users.Remove(user);
                    model.SaveChanges();

                    return Json(new { success = true });
                }
                catch
                {
                    return Json(new { success = false });
                }
            }
        }

        [AllowAuthorize]
        public IResourceHandler GetSettingApp()
        {
            return Json(new {
                success = true,
                SettingProvider.SQL_Server,
                SettingProvider.SQL_Port,
                SettingProvider.SQL_Database,
                SettingProvider.SQL_User,
                SettingProvider.SQL_Password,
            });
        }

        public IResourceHandler SaveSettingApp()
        {
            try
            {
                SettingProvider.SQL_Database = jToken.Value<string>("SQL_Database");
                SettingProvider.SQL_Server = jToken.Value<string>("SQL_Server");
                SettingProvider.SQL_Port = jToken.Value<int>("SQL_Port");
                SettingProvider.SQL_User = jToken.Value<string>("SQL_User");
                SettingProvider.SQL_Password = jToken.Value<string>("SQL_Password");
                SettingProvider.SaveLocalSetting();

                return Json(new { success = true });
            } catch
            {
                return Json(new { success = false });
            }
        }

        [AllowAuthorize]
        public IResourceHandler GetSettings()
        {
            return Json(new
            {
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
                SettingProvider.SaveDBSetting();

                return Json(new { success = true, message = "Setting Saved" });
            }
            catch
            {
                return Json(new { success = false, message = "Something Error" });
            }
        }
        #endregion

        #region Helper 
        public object GetRoles()
        {
            var values = Enum.GetValues(typeof(AuthLevel));
            var names = Enum.GetNames(typeof(AuthLevel));

            var x = (from a in names
                     select new
                     {
                         Role = a,
                         Value = values.GetValue(Array.IndexOf(names, a))
                     });

            return x.ToList();
        }
        #endregion
    }
}
