using System;
using System.Collections.Generic;
using System.Linq;
using UIHotel.Data;
using UIHotel.Data.Table;

namespace UIHotel.App.Provider
{
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
                }
                catch
                {
                    //
                }
            }
        }

        public void LoadSetting()
        {
            GetSettings();
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
