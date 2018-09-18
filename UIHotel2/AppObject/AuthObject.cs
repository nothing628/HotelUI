using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromium.WebBrowser;

namespace UIHotel2.AppObject
{
    class AuthObject : BaseObject
    {
        public override string ObjectName => "Auth";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            obj.AddFunction("Validate").Execute += ValidateExecute;
            obj.AddFunction("Create").Execute += CreateExecute;
            obj.AddFunction("Update").Execute += UpdateExecute;
            obj.AddFunction("Delete").Execute += DeleteExecute;
            obj.AddFunction("Get").Execute += GetExecute;
            obj.AddFunction("ChangePassword").Execute += ChangePasswordExecute;
        }

        private void GetExecute(object sender, Chromium.Remote.Event.CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteExecute(object sender, Chromium.Remote.Event.CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpdateExecute(object sender, Chromium.Remote.Event.CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChangePasswordExecute(object sender, Chromium.Remote.Event.CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreateExecute(object sender, Chromium.Remote.Event.CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ValidateExecute(object sender, Chromium.Remote.Event.CfrV8HandlerExecuteEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
