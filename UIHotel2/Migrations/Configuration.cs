namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UIHotel2.Data;
    using UIHotel2.Data.Tables;
    using UIHotel2.Misc;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelContext>
    {
        public Configuration()
        {
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.HotelContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.RoomStates.AddOrUpdate(x => x.StateName,
                new RoomState { StateName = "Vacant", StateColor = "00ACAC", StateAllow = "YYNNY" },
                new RoomState { StateName = "Booked", StateColor = "F59C1A", StateAllow = "NYNNN" },
                new RoomState { StateName = "Occupied", StateColor = "FF5B57", StateAllow = "NNYNN" },
                new RoomState { StateName = "Cleaning", StateColor = "348FE2", StateAllow = "NNNYN" },
                new RoomState { StateName = "Maintance", StateColor = "929BA1", StateAllow = "NNNNY" },
                new RoomState { StateName = "Late Checkout", StateColor = "727CB6", StateAllow = "NNYNN" });
            context.RoomCategories.AddOrUpdate(x => x.CategoryName,
                new RoomCategory { CategoryName = "Big" },
                new RoomCategory { CategoryName = "Medium" },
                new RoomCategory { CategoryName = "Small" });
            context.RoomPriceKinds.AddOrUpdate(x => x.KindName,
                new RoomPriceKind { KindName = "WeekDay", KindColor = "43A047", KindDescription = "" },
                new RoomPriceKind { KindName = "WeekEnd", KindColor = "D32F2F", KindDescription = "" },
                new RoomPriceKind { KindName = "Holiday", KindColor = "00695C", KindDescription = "" });
            context.Settings.AddOrUpdate(x => x.Key,
                new Setting { Key = "app.name", Value = "Hotel Management System" },
                new Setting { Key = "hotel.name", Value = "Hotel Universal" },
                new Setting { Key = "hotel.address", Value = "Jl. Jalan Perum \nTangerang, 14000" },
                new Setting { Key = "hotel.logo", Value = "" },
                new Setting { Key = "hotel.phone", Value = "08965555555" },
                new Setting { Key = "hotel.email", Value = "home.test@hotel.com" },
                new Setting { Key = "time.checkin", Value = "12:00:00" },
                new Setting { Key = "time.checkout", Value = "13:00:00" },
                new Setting { Key = "time.fullcharge", Value = "18:00:00" },
                new Setting { Key = "penalty", Value = "20000" },
                new Setting { Key = "deposit", Value = "50000" });
            context.TransactionCategories.AddOrUpdate(x => x.CategoryName,
                new TransactionCategory { CategoryName = "Cash", CategoryColor = "558B2F", CategoryIcon = "fa-money", IsIncome = true },
                new TransactionCategory { CategoryName = "Income", CategoryColor = "1565C0", CategoryIcon = "fa-credit-card", IsIncome = true },
                new TransactionCategory { CategoryName = "Salary", CategoryColor = "F57F17", CategoryIcon = "fa-usd", IsIncome = true },
                new TransactionCategory { CategoryName = "Food & Drinks", CategoryColor = "00838F", CategoryIcon = "fa-cutlery", IsIncome = false },
                new TransactionCategory { CategoryName = "Transportation", CategoryColor = "BF360C", CategoryIcon = "fa-rocket", IsIncome = false },
                new TransactionCategory { CategoryName = "Comunication", CategoryColor = "311B92", CategoryIcon = "fa-phone", IsIncome = false },
                new TransactionCategory { CategoryName = "Tax", CategoryColor = "1B5E20", CategoryIcon = "fa-gavel", IsIncome = false },
                new TransactionCategory { CategoryName = "Utilities", CategoryColor = "FF8F00", CategoryIcon = "fa-cogs", IsIncome = false },
                new TransactionCategory { CategoryName = "Insurance", CategoryColor = "3E2723", CategoryIcon = "fa-heart", IsIncome = false },
                new TransactionCategory { CategoryName = "Loan", CategoryColor = "0097A7", CategoryIcon = "fa-university", IsIncome = false },
                new TransactionCategory { CategoryName = "Uncategorized Income", CategoryColor = "000000", CategoryIcon = "fa-asterisk", IsIncome = true },
                new TransactionCategory { CategoryName = "Uncategorized Outcome", CategoryColor = "000000", CategoryIcon = "fa-asterisk", IsIncome = false });
            context.BookingTypes.AddOrUpdate(x => x.TypeName,
                new BookingType { TypeName = "Walk-In", IsLocal = true },
                new BookingType { TypeName = "Telephone", IsLocal = true },
                new BookingType { TypeName = "Traveloka", IsLocal = false },
                new BookingType { TypeName = "Agoda", IsLocal = false });
            context.InvoiceKinds.AddOrUpdate(x => x.Id,
                new InvoiceDetailKind { Id = 1, KindName = "Room Invoice" },
                new InvoiceDetailKind { Id = 2, KindName = "Room Move Charge" },
                new InvoiceDetailKind { Id = 3, KindName = "Room Late Checkout" },
                new InvoiceDetailKind { Id = 4, KindName = "Room Price by Online" },
                new InvoiceDetailKind { Id = 97, KindName = "Deposit" },
                new InvoiceDetailKind { Id = 98, KindName = "Cashback" },
                new InvoiceDetailKind { Id = 99, KindName = "Pinalty" },
                new InvoiceDetailKind { Id = 100, KindName = "Pay Cash" },
                new InvoiceDetailKind { Id = 101, KindName = "Pay Card" },
                new InvoiceDetailKind { Id = 200, KindName = "Uncategorized In" },
                new InvoiceDetailKind { Id = 201, KindName = "Uncategorized Out" });
            context.SaveChanges();

            var is_exists = context.Settings.Where(x => x.Key == "app.key").Any();

            if (!is_exists)
            {
                context.Settings.Add(new Setting { Key = "app.key", Value = AppHelper.GenerateRandomStr(32) });
                context.SaveChanges();
            }
            
            var big = context.RoomCategories.Where(x => x.CategoryName == "Big").Single();
            var vacant = context.RoomStates.Where(x => x.StateName == "Vacant").Single();
            var weekday = context.RoomPriceKinds.Where(x => x.KindName == "WeekDay").Single();

            SettingHelper.Load();

            context.Users.AddOrUpdate(x => x.Username,
                new User {
                    Username = "admin",
                    Fullname = "Administrator",
                    Password = AuthHelper.HashText("admin", SettingHelper.AppKey),
                    Level = 0,
                    IsActive = true
                });

            context.RoomCalendars.AddOrUpdate(x => x.DateAt,
                    new RoomCalendar { DateAt = DateTime.Today, RoomPriceKindId = weekday.Id });
            context.Rooms.AddOrUpdate(x => x.RoomNumber,
                new Room { RoomNumber = "201", RoomStateId = vacant.Id, RoomCategoryId = big.Id });
            context.SaveChanges();
        }
    }
}
