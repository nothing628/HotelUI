using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using UIHotel2.Data;
using UIHotel2.Data.Tables;
using UIHotel2.Misc;

namespace UIHotel2.AppObject
{
    class AuthObject : BaseObject
    {
        public override string ObjectName => "Auth";
        static string AppKey => SettingHelper.AppKey;

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("Validate").Execute += ValidateExecute;
            Self.AddFunction("Create").Execute += CreateExecute;
            Self.AddFunction("Update").Execute += UpdateExecute;
            Self.AddFunction("Delete").Execute += DeleteExecute;
            Self.AddFunction("Get").Execute += GetExecute;
            Self.AddFunction("List").Execute += ListExecute;
            Self.AddFunction("ChangePassword").Execute += ChangePasswordExecute;
        }

        private void ListExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            using(var context = new HotelContext())
            {
                var list = context.Users.ToArray();
                var convertval = ConvertValue(list);

                e.SetReturnValue(convertval);
            }
        }
        
        private void GetExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var userId = e.Arguments[0].IntValue;

                using (var context = new HotelContext())
                {
                    var user = context.Users
                        .Where(x => x.Id == userId)
                        .SingleOrDefault();
                    var convertVal = ConvertValue(user);

                    e.SetReturnValue(convertVal);
                }
            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
            }
        }

        private void DeleteExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var userId = e.Arguments[0].IntValue;

                using (var context = new HotelContext())
                {
                    var user = context.Users.Where(x => x.Id == userId).Single();

                    context.Entry(user).State = EntityState.Deleted;
                    context.SaveChanges();

                    e.SetReturnValue(true);
                }
            }
            catch
            {
                e.SetReturnValue(false);
            }
        }

        private void UpdateExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var userId = e.Arguments[0].IntValue;
                var userData = e.Arguments[1];
                var fullname = userData.GetValue("Fullname").StringValue;
                var password = userData.GetValue("Password").StringValue;
                var level = userData.GetValue("Level").IntValue;
                var is_active = userData.GetValue("IsActive").BoolValue;

                using (var context = new HotelContext())
                {
                    var user = context.Users
                        .Where(x => x.Id == userId)
                        .Single();

                    user.Fullname = fullname;
                    user.Level = Convert.ToByte(level);
                    user.IsActive = is_active;
                    user.UpdatePassword(password);

                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();

                    var convertVal = ConvertValue(user);
                    e.SetReturnValue(convertVal);
                }
            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
            }
        }

        private void ChangePasswordExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var userId = e.Arguments[0].IntValue;
                var oldPassword = e.Arguments[1].StringValue;
                var newPassword = e.Arguments[2].StringValue;
                var hashOldPassword = AuthHelper.HashText(oldPassword, AppKey);

                using (var context = new HotelContext())
                {
                    var user = context.Users
                        .Where(x => x.Id == userId)
                        .Where(x => x.Password == hashOldPassword)
                        .Where(x => x.IsActive)
                        .SingleOrDefault();

                    if (user != null)
                    {
                        user.UpdatePassword(newPassword);

                        context.Entry(user).State = EntityState.Modified;
                        context.SaveChanges();
                        e.SetReturnValue(true);
                    } else
                    {
                        e.SetReturnValue(false);
                    }
                }
            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
            }
        }

        private void CreateExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var data = e.Arguments[0];
                var username = data.GetValue("Username").StringValue;
                var fullname = data.GetValue("Fullname").StringValue;
                var password = data.GetValue("Password").StringValue;
                var level = data.GetValue("Level").IntValue;
                var is_active = data.GetValue("IsActive").BoolValue;
                var user = new User
                {
                    Fullname = fullname,
                    Username = username,
                    IsActive = is_active,
                    Level = Convert.ToByte(level)
                };

                user.UpdatePassword(password);

                using (var context = new HotelContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                    var convertVal = ConvertValue(user);
                    e.SetReturnValue(convertVal);
                }
            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
            }
        }

        private void ValidateExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var username = e.Arguments[0].StringValue;
                var password = e.Arguments[1].StringValue;
                var hashPassword = AuthHelper.HashText(password, AppKey);
                
                using (var context = new HotelContext())
                {
                    var user = context.Users
                        .Where(x => x.Username == username)
                        .Where(x => x.Password == hashPassword)
                        .Where(x => x.IsActive)
                        .SingleOrDefault();
                    var convertuser = ConvertValue(user);

                    e.SetReturnValue(convertuser);
                }
            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
            }
        }
    }
}
