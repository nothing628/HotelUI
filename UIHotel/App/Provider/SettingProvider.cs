using System;

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
        public static decimal TaxPercent
        {
            get
            {
                return Convert.ToDecimal(Accessor["tax"]);
            }
            set
            {
                Accessor["tax"] = value.ToString();
            }
        }
        public static decimal TaxFloat
        {
            get
            {
                return TaxPercent / 100;
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

        public static void LoadDBSetting()
        {
            Accessor.LoadSetting();
        }
        public static void SaveDBSetting()
        {
            Accessor.SaveSetting();
        }

        public static void SaveLocalSetting()
        {
            Properties.Settings.Default.Save();
        }
    }
}
