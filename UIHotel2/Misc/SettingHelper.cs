using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.Data;

namespace UIHotel2.Misc
{
    static class SettingHelper
    {
        private static IDictionary<string, string> DatabaseSetting { get; set; }

        static SettingHelper()
        {
            LoadDatabaseSetting();
        }

        private static void LoadDatabaseSetting()
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

        private static void SaveDatabaseSetting()
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
                }
                catch { }
            }
        }

        private static string GetDatabaseSetting(string Key, string Default = "")
        {
            if (DatabaseSetting.ContainsKey(Key))
                return DatabaseSetting[Key];
            return Default;
        }

        private static void SetDatabaseSetting(string Key, string Value)
        {
            if (DatabaseSetting.ContainsKey(Key))
                DatabaseSetting[Key] = Value;
        }

        public static void Save() => SaveDatabaseSetting();
        public static void Load() => LoadDatabaseSetting();

        public static string AppKey
        {
            get => GetDatabaseSetting("app.key");
        }
        public static string AppName
        {
            get => GetDatabaseSetting("app.name");
        }
        public static string HotelName
        {
            get => GetDatabaseSetting("hotel.name");
            set => SetDatabaseSetting("hotel.name", value);
        }
        public static string HotelLogo
        {
            get => GetDatabaseSetting("hotel.logo");
            set => SetDatabaseSetting("hotel.logo", value);
        }
        public static string HotelAddress
        {
            get => GetDatabaseSetting("hotel.address");
            set => SetDatabaseSetting("hotel.address", value);
        }
        public static string HotelPhone
        {
            get => GetDatabaseSetting("hotel.phone");
            set => SetDatabaseSetting("hotel.phone", value);
        }
        public static string HotelEmail
        {
            get => GetDatabaseSetting("hotel.email");
            set => SetDatabaseSetting("hotel.email", value);
        }
        public static int Deposit
        {
            get
            {
                var deposit = GetDatabaseSetting("deposit");
                return Convert.ToInt32(deposit);
            }
            set => SetDatabaseSetting("deposit", value.ToString());
        }
        public static int Penalty
        {
            get
            {
                var penalty = GetDatabaseSetting("penalty");
                return Convert.ToInt32(penalty);
            }
            set => SetDatabaseSetting("penalty", value.ToString());
        }
        public static string TimeCheckout
        {
            get => GetDatabaseSetting("time.checkout");
            set => SetDatabaseSetting("time.checkout", value);
        }
        public static string TimeCheckin
        {
            get => GetDatabaseSetting("time.checkin");
            set => SetDatabaseSetting("time.checkin", value);
        }
        public static string TimeFullcharge
        {
            get => GetDatabaseSetting("time.fullcharge");
            set => SetDatabaseSetting("time.fullcharge", value);
        }
    }
}
