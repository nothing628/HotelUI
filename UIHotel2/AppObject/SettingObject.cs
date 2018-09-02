using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromium.Remote.Event;
using Chromium.WebBrowser;

namespace UIHotel2.AppObject
{
    class SettingObject : BaseObject
    {
        public override string ObjectName => "Setting";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            var SQL_User = Self.AddDynamicProperty("SQL_User");
            var SQL_Password = Self.AddDynamicProperty("SQL_Password");
            var SQL_Host = Self.AddDynamicProperty("SQL_Host");
            var SQL_Port = Self.AddDynamicProperty("SQL_Port");
            var SQL_Database = Self.AddDynamicProperty("SQL_Database");
            Self.AddFunction("Save").Execute += SaveExecute;
            Self.AddFunction("Load").Execute += LoadExecute;

            SQL_User.PropertyGet += SQL_User_PropertyGet;
            SQL_User.PropertySet += SQL_User_PropertySet;
            SQL_Password.PropertyGet += SQL_Password_PropertyGet;
            SQL_Password.PropertySet += SQL_Password_PropertySet;
            SQL_Host.PropertyGet += SQL_Host_PropertyGet;
            SQL_Host.PropertySet += SQL_Host_PropertySet;
            SQL_Port.PropertyGet += SQL_Port_PropertyGet;
            SQL_Port.PropertySet += SQL_Port_PropertySet;
            SQL_Database.PropertyGet += SQL_Database_PropertyGet;
            SQL_Database.PropertySet += SQL_Database_PropertySet;
        }

        private void SQL_Database_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Properties.Settings.Default.SQL_DATABASE = e.Value.StringValue;
        }

        private void SQL_Database_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Properties.Settings.Default.SQL_DATABASE;
            e.SetReturnValue(true);
        }

        private void SQL_Port_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Properties.Settings.Default.SQL_PORT = e.Value.IntValue;
        }

        private void SQL_Port_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Properties.Settings.Default.SQL_PORT;
            e.SetReturnValue(true);
        }

        private void SQL_Host_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Properties.Settings.Default.SQL_HOST = e.Value.StringValue;
        }

        private void SQL_Host_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Properties.Settings.Default.SQL_HOST;
            e.SetReturnValue(true);
        }

        private void SQL_Password_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Properties.Settings.Default.SQL_PASSWORD = e.Value.StringValue;
        }

        private void SQL_Password_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Properties.Settings.Default.SQL_PASSWORD;
            e.SetReturnValue(true);
        }

        private void LoadExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        private void SaveExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void SQL_User_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Properties.Settings.Default.SQL_USER = e.Value.StringValue;
        }

        private void SQL_User_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Properties.Settings.Default.SQL_USER;
            e.SetReturnValue(true);
        }

    }
}
