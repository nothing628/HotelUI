using System;
using System.Linq;
using UIHotel.App.Auth;
using UIHotel.App.Provider;
using UIHotel.Data.Table;

namespace UIHotel.Data.Seeds
{
    public class SettingSeeder : DBSeeder
    {
        private string appKey()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, 32)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public override void Run(DataContext context)
        {
            context.Settings.Add(new Setting() { Key = "app.key", Value = appKey() });
            context.Settings.Add(new Setting() { Key = "checkin.time", Value = "12:00:00" });
            context.Settings.Add(new Setting() { Key = "checkout.time", Value = "13:00:00" });
            context.Settings.Add(new Setting() { Key = "deposit", Value = "50000" });
            context.Settings.Add(new Setting() { Key = "penalty", Value = "20000" });
            context.Settings.Add(new Setting() { Key = "tax", Value = "5" });
            context.Settings.Add(new Setting() { Key = "hotel.name", Value = "Hotel Test" });
            context.Settings.Add(new Setting() { Key = "hotel.address", Value = "Tangerang 15122" });
            context.Settings.Add(new Setting() { Key = "hotel.logo", Value = "" });
            context.SaveChanges();

            SettingProvider.LoadDBSetting();

            context.Users.Add(new User() {
                Fullname = "Administrator",
                Permission = (int)AuthLevel.Administrator,
                Username = "admin",
                Password = AuthHelper.HashText("123456", SettingProvider.AppKey),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            });
            context.SaveChanges();
        }
    }
}
