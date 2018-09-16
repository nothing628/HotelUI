namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UIHotel2.Data.Tables;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.HotelContext>
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
                new RoomState { StateName = "Vacant", StateColor = "32C787" },
                new RoomState { StateName = "Booked", StateColor = "FF9800" },
                new RoomState { StateName = "Occupied", StateColor = "FF5652" },
                new RoomState { StateName = "Cleaning", StateColor = "AB47BC" },
                new RoomState { StateName = "Maintance", StateColor = "757575" },
                new RoomState { StateName = "Late Checkout", StateColor = "2196F3" });
            context.RoomCategories.AddOrUpdate(x => x.CategoryName,
                new RoomCategory { CategoryName = "Big" },
                new RoomCategory { CategoryName = "Medium" },
                new RoomCategory { CategoryName = "Small" });
            context.SaveChanges();

            var big = context.RoomCategories.Where(x => x.CategoryName == "Big").Single();
            var vacant = context.RoomStates.Where(x => x.StateName == "Vacant").Single();

            context.Rooms.RemoveRange(context.Rooms.ToList());
            context.SaveChanges();
            context.Rooms.AddOrUpdate(x => x.RoomNumber,
                new Room { RoomNumber = "201", State = vacant, Category = big });
            context.SaveChanges();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
