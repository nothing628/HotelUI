using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using UIHotel2.Data;
using UIHotel2.Misc;

namespace UIHotel2.AppObject
{
    class AuthObject : BaseObject
    {
        public override string ObjectName => "Auth";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("Validate").Execute += ValidateExecute;
            Self.AddFunction("Create").Execute += CreateExecute;
            Self.AddFunction("Update").Execute += UpdateExecute;
            Self.AddFunction("Delete").Execute += DeleteExecute;
            Self.AddFunction("Get").Execute += GetExecute;
            Self.AddFunction("ChangePassword").Execute += ChangePasswordExecute;
        }

        private void GetExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpdateExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChangePasswordExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreateExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ValidateExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var username = e.Arguments[0].StringValue;
                var password = e.Arguments[1].StringValue;
                var appkey = SettingHelper.AppKey;
                var hashPassword = AuthHelper.HashText(password, appkey);
                
                using (var context = new HotelContext())
                {
                    var user = context.Users
                        .Where(x => x.Username == username)
                        .Where(x => x.Password == hashPassword)
                        .SingleOrDefault();
                    var convertuser = ConvertValue(user);

                    e.SetReturnValue(convertuser);
                }
            } catch (Exception ex)
            {
                e.Exception = ex.Message;
            }
        }
    }
}
