using System;
using UIHotel.App.Auth;
using UIHotel.Data.Table;

namespace UIHotel.Data.Seeds
{
    public class SettingSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            context.Settings.Add(new Setting() { Key = "checkin.time", Value = "12:00:00" });
            context.Settings.Add(new Setting() { Key = "checkout.time", Value = "13:00:00" });
            context.Settings.Add(new Setting() { Key = "deposit", Value = "50000" });
            context.Settings.Add(new Setting() { Key = "penalty", Value = "20000" });
            context.Settings.Add(new Setting() { Key = "hotel.name", Value = "Hotel Test" });
            context.Settings.Add(new Setting() { Key = "hotel.address", Value = "Tangerang 15122" });
            context.Settings.Add(new Setting() { Key = "hotel.logo", Value = "" });
            context.SaveChanges();

            context.Users.Add(new User() {
                Fullname = "Administrator",
                Permission = (int)AuthLevel.Administrator,
                Username = "admin",
                Password = AuthHelper.HashText("123456", "appkey"),  //TODO Provide app key
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            });
            context.SaveChanges();
        }
    }
}
