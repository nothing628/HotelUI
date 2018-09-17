using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using UIHotel2.Data;

namespace UIHotel2.AppObject
{
    class SettingObject : BaseObject
    {
        public override string ObjectName => "Setting";
        private IDictionary<string, string> DatabaseSetting { get; set; }

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            var SQL_User = Self.AddDynamicProperty("SQL_User");
            var SQL_Password = Self.AddDynamicProperty("SQL_Password");
            var SQL_Host = Self.AddDynamicProperty("SQL_Host");
            var SQL_Port = Self.AddDynamicProperty("SQL_Port");
            var SQL_Database = Self.AddDynamicProperty("SQL_Database");
            var App_Key = Self.AddDynamicProperty("App_Key");
            var App_Name = Self.AddDynamicProperty("App_Name");
            var Hotel_Address = Self.AddDynamicProperty("Hotel_Address");
            var Hotel_Logo = Self.AddDynamicProperty("Hotel_Logo");
            var Hotel_Name = Self.AddDynamicProperty("Hotel_Name");
            var Deposit = Self.AddDynamicProperty("Deposit");
            var Penalty = Self.AddDynamicProperty("Penalty");
            var Time_Checkin = Self.AddDynamicProperty("Time_Checkin");
            var Time_Checkout = Self.AddDynamicProperty("Time_Checkout");
            var Time_Fullcharge = Self.AddDynamicProperty("Time_Fullcharge");

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

            App_Key.PropertyGet += App_Key_PropertyGet;
            App_Name.PropertyGet += App_Name_PropertyGet;
            Hotel_Address.PropertyGet += Hotel_Address_PropertyGet;
            Hotel_Address.PropertySet += Hotel_Address_PropertySet;
            Hotel_Logo.PropertyGet += Hotel_Logo_PropertyGet;
            Hotel_Logo.PropertySet += Hotel_Logo_PropertySet;
            Hotel_Name.PropertyGet += Hotel_Name_PropertyGet;
            Hotel_Name.PropertySet += Hotel_Name_PropertySet;
            Deposit.PropertyGet += Deposit_PropertyGet;
            Deposit.PropertySet += Deposit_PropertySet;
            Penalty.PropertyGet += Penalty_PropertyGet;
            Penalty.PropertySet += Penalty_PropertySet;
            Time_Checkin.PropertyGet += Time_Checkin_PropertyGet;
            Time_Checkin.PropertySet += Time_Checkin_PropertySet;
            Time_Checkout.PropertyGet += Time_Checkout_PropertyGet;
            Time_Checkout.PropertySet += Time_Checkout_PropertySet;
            Time_Fullcharge.PropertyGet += Time_Fullcharge_PropertyGet;
            Time_Fullcharge.PropertySet += Time_Fullcharge_PropertySet;

            LoadDatabaseSetting();
        }

        private void Time_Fullcharge_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SetDatabaseSetting("time.fullcharge", e.Value.StringValue);
                return;
            }

            e.Exception = "String value expected";
        }

        private void Time_Checkout_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SetDatabaseSetting("time.checkout", e.Value.StringValue);
                return;
            }

            e.Exception = "String value expected";
        }

        private void Time_Checkin_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SetDatabaseSetting("time.checkin", e.Value.StringValue);
                return;
            }

            e.Exception = "String value expected";
        }

        private void Penalty_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsInt)
            {
                SetDatabaseSetting("penalty", e.Value.IntValue.ToString());
                return;
            }

            e.Exception = "Number value expected";
        }

        private void Deposit_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsInt)
            {
                SetDatabaseSetting("deposit", e.Value.IntValue.ToString());
                return;
            }

            e.Exception = "Number value expected";
        }

        private void Time_Fullcharge_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = GetDatabaseSetting("time.fullcharge");
            e.SetReturnValue(true);
        }

        private void Time_Checkout_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = GetDatabaseSetting("time.checkout");
            e.SetReturnValue(true);
        }

        private void Time_Checkin_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = GetDatabaseSetting("time.checkin");
            e.SetReturnValue(true);
        }

        private void Penalty_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            var penalty = GetDatabaseSetting("penalty");
            e.Retval = Convert.ToInt32(penalty);
            e.SetReturnValue(true);
        }

        private void Deposit_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            var deposit = GetDatabaseSetting("deposit");
            e.Retval = Convert.ToInt32(deposit);
            e.SetReturnValue(true);
        }

        private void Hotel_Name_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SetDatabaseSetting("hotel.name", e.Value.StringValue);
                return;
            }

            e.Exception = "String value expected";
        }

        private void Hotel_Logo_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SetDatabaseSetting("hotel.logo", e.Value.StringValue);
                return;
            }

            e.Exception = "String value expected";
        }

        private void Hotel_Address_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SetDatabaseSetting("hotel.address", e.Value.StringValue);
                return;
            }

            e.Exception = "String value expected";
        }

        private void Hotel_Name_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = GetDatabaseSetting("hotel.name");
            e.SetReturnValue(true);
        }

        private void Hotel_Logo_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            //Need to convert to local url
            e.Retval = GetDatabaseSetting("hotel.logo");
            e.SetReturnValue(true);
        }

        private void Hotel_Address_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = GetDatabaseSetting("hotel.address");
            e.SetReturnValue(true);
        }

        private void App_Name_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = GetDatabaseSetting("app.name");
            e.SetReturnValue(true);
        }

        private void App_Key_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = GetDatabaseSetting("app.key");
            e.SetReturnValue(true);
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
            LoadDatabaseSetting();
        }

        private void SaveExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            Properties.Settings.Default.Save();
            SaveDatabaseSetting();
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

        private void LoadDatabaseSetting()
        {
            if (DatabaseSetting == null)
            {
                DatabaseSetting = new Dictionary<string, string>();
            }

            using (var context = new HotelContext())
            {
                try
                {
                    var settings = context.Settings.ToList();

                    foreach (var setting in settings)
                    {
                        DatabaseSetting.Add(setting.Key, setting.Value);
                    }
                }
                catch { }
            }
        }

        private void SaveDatabaseSetting()
        {
            if (DatabaseSetting == null)
                return;

            using (var context = new HotelContext())
            {
                try
                {
                    var settings = context.Settings.ToList();

                    foreach (var keyValPair in DatabaseSetting)
                    {
                        var setting = settings.Where(x => x.Key == keyValPair.Key).Single();

                        setting.Value = keyValPair.Value;
                        context.Entry(setting).State = EntityState.Modified;
                    }

                    context.SaveChanges();
                } catch { }
            }
        }

        private string GetDatabaseSetting(string Key, string Default = "")
        {
            if (DatabaseSetting.ContainsKey(Key))
                return DatabaseSetting[Key];
            return Default;
        }

        private void SetDatabaseSetting(string Key, string Value)
        {
            if (DatabaseSetting.ContainsKey(Key))
                DatabaseSetting[Key] = Value;
        }
    }
}
