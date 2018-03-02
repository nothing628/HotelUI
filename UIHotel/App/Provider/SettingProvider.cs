using System;
using System.Collections.Generic;
using System.Linq;
using UIHotel.Data;
using UIHotel.Data.Table;

namespace UIHotel.App.Provider
{
    public static class SettingProvider
    {
        public static SettingAccessor Accessor;
        static SettingProvider()
        {
            Accessor = new SettingAccessor();
        }

        public static string AppKey
        {
            get
            {
                return Accessor["app.key"];
            }
            set
            {
                Accessor["app.key"] = value;
            }
        }
        public static string HotelName
        {
            get
            {
                return Accessor["hotel.name"];
            }
            set
            {
                Accessor["hotel.name"] = value;
            }
        }
        public static string HotelLogo
        {
            get
            {
                return Accessor["hotel.logo"];
            }
            set
            {
                Accessor["hotel.logo"] = value;
            }
        }
        public static string HotelAddress
        {
            get
            {
                return Accessor["hotel.address"];
            }
            set
            {
                Accessor["hotel.address"] = value;
            }
        }
        public static decimal Deposit
        {
            get
            {
                return Convert.ToDecimal(Accessor["deposit"]);
            }
            set
            {
                Accessor["deposit"] = value.ToString();
            }
        }
        public static decimal Pinalty
        {
            get
            {
                return Convert.ToDecimal(Accessor["penalty"]);
            }
            set
            {
                Accessor["penalty"] = value.ToString();
            }
        }
        public static TimeSpan CheckinTime
        {
            get
            {
                var time = Accessor["checkin.time"];

                return TimeSpan.Parse(time);
            }
            set
            {
                Accessor["checkin.time"] = value.ToString();
            }
        }
        public static TimeSpan CheckoutTime
        {
            get
            {
                var time = Accessor["checkout.time"];

                return TimeSpan.Parse(time);
            }
            set
            {
                Accessor["checkout.time"] = value.ToString();
            }
        }
        public static string SQL_Connection_Str
        {
            get
            {
                var connStr = string.Format(Properties.Settings.Default.MyDB,
                    SQL_Server,
                    SQL_User,
                    SQL_Password,
                    SQL_Port,
                    SQL_Database);

                return connStr;
            }
        }
        public static string SQL_Server
        {
            get => Properties.Settings.Default.SQL_Server;
            set
            {
                Properties.Settings.Default.SQL_Server = value;
            }
        }
        public static int SQL_Port
        {
            get => Properties.Settings.Default.SQL_Port;
            set
            {
                Properties.Settings.Default.SQL_Port = value;
            }
        }
        public static string SQL_Database
        {
            get => Properties.Settings.Default.SQL_Database;
            set
            {
                Properties.Settings.Default.SQL_Database = value;
            }
        }
        public static string SQL_User
        {
            get => Properties.Settings.Default.SQL_User;
            set
            {
                Properties.Settings.Default.SQL_User = value;
            }
        }
        public static string SQL_Password
        {
            get => Properties.Settings.Default.SQL_Password;
            set
            {
                Properties.Settings.Default.SQL_Password = value;
            }
        }
        public static void SaveSetting()
        {
            Accessor.SaveSetting();
            Properties.Settings.Default.Save();
        }
    }

    public class SettingAccessor
    {
        private Dictionary<string, string> SettingData = new Dictionary<string, string>();

        public string this[string index]
        {
            get
            {
                return this.GetSetting(index);
            }
            set
            {
                this.SetSetting(index, value);
            }
        }

        public SettingAccessor()
        {
            this.GetSettings();
        }

        private void GetSettings()
        {
            using (var model = new DataContext())
            {
                try
                {
                    var settings = (from a in model.Settings
                                    select a).ToDictionary(x => x.Key, x => x.Value);

                    this.SettingData.Clear();

                    foreach (var setting in settings)
                        this.SettingData.Add(setting.Key, setting.Value);
                } catch
                {
                    //
                }
            }
        }

        public string GetSetting(string key)
        {
            if (this.SettingData.ContainsKey(key))
                return this.SettingData[key];
            return String.Empty;
        }

        public void SetSetting(string key, string value)
        {
            if (SettingData.ContainsKey(key))
                SettingData[key] = value;
            else
                SettingData.Add(key, value);
        }

        public void SaveSetting()
        {
            using (var model = new DataContext())
            using (var trans = model.Database.BeginTransaction())
            {
                try
                {
                    foreach (var setting in SettingData)
                    {
                        var set = (from a in model.Settings
                                   where a.Key == setting.Key
                                   select a).FirstOrDefault();

                        if (set == null)
                        {
                            model.Settings.Add(new Setting() { Key = setting.Key, Value = setting.Value });
                        }
                        else
                        {
                            set.Value = setting.Value;
                        }

                        model.SaveChanges();
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
        }
    }
}
