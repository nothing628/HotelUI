using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using UIHotel2.Data;
using UIHotel2.Misc;
using UIHotel2.Properties;

namespace UIHotel2.AppObject
{
    public class SettingObject : BaseObject
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
            var App_Key = Self.AddDynamicProperty("App_Key");
            var App_Name = Self.AddDynamicProperty("App_Name");
            var Hotel_Address = Self.AddDynamicProperty("Hotel_Address");
            var Hotel_Logo = Self.AddDynamicProperty("Hotel_Logo");
            var Hotel_Name = Self.AddDynamicProperty("Hotel_Name");
            var Hotel_Phone = Self.AddDynamicProperty("Hotel_Phone");
            var Hotel_Email = Self.AddDynamicProperty("Hotel_Email");
            var Deposit = Self.AddDynamicProperty("Deposit");
            var Penalty = Self.AddDynamicProperty("Penalty");
            var Time_Checkin = Self.AddDynamicProperty("Time_Checkin");
            var Time_Checkout = Self.AddDynamicProperty("Time_Checkout");
            var Time_Fullcharge = Self.AddDynamicProperty("Time_Fullcharge");

            Self.AddFunction("Save").Execute += SaveExecute;
            Self.AddFunction("Test").Execute += TestExecute;
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
            Hotel_Email.PropertyGet += Hotel_Email_PropertyGet;
            Hotel_Email.PropertySet += Hotel_Email_PropertySet;
            Hotel_Phone.PropertyGet += Hotel_Phone_PropertyGet;
            Hotel_Phone.PropertySet += Hotel_Phone_PropertySet;
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
        }

        private void Hotel_Phone_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SettingHelper.HotelPhone = e.Value.StringValue;
                return;
            }

            e.Exception = "String value expected";
        }

        private void Hotel_Phone_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.HotelPhone;
            e.SetReturnValue(true);
        }

        private void Hotel_Email_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SettingHelper.HotelEmail = e.Value.StringValue;
                return;
            }

            e.Exception = "String value expected";
        }

        private void Hotel_Email_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.HotelEmail;
            e.SetReturnValue(true);
        }

        private void TestExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            if (e.Arguments.Length != 6)
            {
                e.Exception = "6 parameters expected!";
                return;
            }

            var server = e.Arguments[0];
            var port = e.Arguments[1];
            var user = e.Arguments[2];
            var password = e.Arguments[3];
            var database = e.Arguments[4];
            var callback = e.Arguments[5];

            if (!server.IsString || !port.IsInt || !user.IsString || !password.IsString || !database.IsString || !callback.IsFunction)
            {
                //Invalid parameter;
                e.Exception = "Invalid parameter type";
                return;
            }

            if (callback.IsFunction)
            {
                // Callback to result
                Settings.Default.SQL_DATABASE = database.StringValue;
                Settings.Default.SQL_HOST = server.StringValue;
                Settings.Default.SQL_PORT = port.IntValue;
                Settings.Default.SQL_USER = user.StringValue;
                Settings.Default.SQL_PASSWORD = password.StringValue;

                using (var context = new HotelContext())
                {
                    var connection = context.Database.Connection;

                    try
                    {
                        connection.Open();

                        var result = context.Database.SqlQuery<int>("SELECT COUNT(*) FROM __migrationhistory");
                        var arr_result = result.ToArray();

                        if (arr_result.Length > 0 && arr_result[0] == 31)
                        {
                            callback.ExecuteFunction(null, new CfrV8Value[] { true });
                        } else
                        {
                            callback.ExecuteFunction(null, new CfrV8Value[] { false });
                        }
                    } catch
                    {
                        callback.ExecuteFunction(null, new CfrV8Value[] { false });
                    }
                }

                Settings.Default.Reload();
            }
        }

        private void Time_Fullcharge_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SettingHelper.TimeFullcharge = e.Value.StringValue;
                return;
            }

            e.Exception = "String value expected";
        }

        private void Time_Checkout_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SettingHelper.TimeCheckout = e.Value.StringValue;
                return;
            }

            e.Exception = "String value expected";
        }

        private void Time_Checkin_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SettingHelper.TimeCheckin = e.Value.StringValue;
                return;
            }

            e.Exception = "String value expected";
        }

        private void Penalty_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsInt)
            {
                SettingHelper.Penalty = e.Value.IntValue;
                return;
            }

            e.Exception = "Number value expected";
        }

        private void Deposit_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsInt)
            {
                SettingHelper.Deposit = e.Value.IntValue;
                return;
            }

            e.Exception = "Number value expected";
        }

        private void Time_Fullcharge_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.TimeFullcharge;
            e.SetReturnValue(true);
        }

        private void Time_Checkout_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.TimeCheckout;
            e.SetReturnValue(true);
        }

        private void Time_Checkin_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.TimeCheckin;
            e.SetReturnValue(true);
        }

        private void Penalty_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.Penalty;
            e.SetReturnValue(true);
        }

        private void Deposit_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.Deposit;
            e.SetReturnValue(true);
        }

        private void Hotel_Name_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SettingHelper.HotelName = e.Value.StringValue;
                return;
            }

            e.Exception = "String value expected";
        }

        private void Hotel_Logo_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SettingHelper.HotelLogo = e.Value.StringValue;
                return;
            }

            e.Exception = "String value expected";
        }

        private void Hotel_Address_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            if (e.Value.IsString)
            {
                SettingHelper.HotelAddress = e.Value.StringValue;
                return;
            }

            e.Exception = "String value expected";
        }

        private void Hotel_Name_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.HotelName;
            e.SetReturnValue(true);
        }

        private void Hotel_Logo_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            //Need to convert to local url
            e.Retval = SettingHelper.HotelLogo;
            e.SetReturnValue(true);
        }

        private void Hotel_Address_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.HotelAddress;
            e.SetReturnValue(true);
        }

        private void App_Name_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.AppName;
            e.SetReturnValue(true);
        }

        private void App_Key_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = SettingHelper.AppKey;
            e.SetReturnValue(true);
        }

        private void SQL_Database_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Settings.Default.SQL_DATABASE = e.Value.StringValue;
        }

        private void SQL_Database_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Settings.Default.SQL_DATABASE;
            e.SetReturnValue(true);
        }

        private void SQL_Port_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Settings.Default.SQL_PORT = e.Value.IntValue;
        }

        private void SQL_Port_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Settings.Default.SQL_PORT;
            e.SetReturnValue(true);
        }

        private void SQL_Host_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Settings.Default.SQL_HOST = e.Value.StringValue;
        }

        private void SQL_Host_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Settings.Default.SQL_HOST;
            e.SetReturnValue(true);
        }

        private void SQL_Password_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Settings.Default.SQL_PASSWORD = e.Value.StringValue;
        }

        private void SQL_Password_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Settings.Default.SQL_PASSWORD;
            e.SetReturnValue(true);
        }

        private void LoadExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            Settings.Default.Reload();
            SettingHelper.Load();
        }

        private void SaveExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            Settings.Default.Save();
            SettingHelper.Save();
        }

        private void SQL_User_PropertySet(object sender, CfrV8AccessorSetEventArgs e)
        {
            Settings.Default.SQL_USER = e.Value.StringValue;
        }

        private void SQL_User_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            e.Retval = Settings.Default.SQL_USER;
            e.SetReturnValue(true);
        }
    }
}
