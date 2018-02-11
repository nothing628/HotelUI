using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                return new TimeSpan();
            }
            set
            {
                //
            }
        }
        public static TimeSpan ChekoutTime
        {
            get
            {
                return new TimeSpan();
            }
            set
            {

            }
        }
        public static void SaveSetting() => Accessor.SaveSetting();
    }

    public class SettingAccessor
    {
        private Dictionary<string, string> SettingData = new Dictionary<string, string>();
        private string[] Keys
        {
            get
            {
                return SettingData.Keys.ToArray();
            }
        }

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
            return this.SettingData[key];
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
