using System;
using System.Collections.Generic;
using System.Linq;
using UIHotel.Data.Table;

namespace UIHotel.Data.Seeds
{
    public class RoomStatusSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            context.RoomCategory.Add(new RoomCategory() { Category = "Big" });
            context.RoomCategory.Add(new RoomCategory() { Category = "Medium" });
            context.RoomCategory.Add(new RoomCategory() { Category = "Small" });
            context.RoomStatus.Add(new RoomStatus() { Id = 1, Status = "Vacant", StatusColor = "32C787" });
            context.RoomStatus.Add(new RoomStatus() { Id = 2, Status = "Booked", StatusColor = "FF9800" });
            context.RoomStatus.Add(new RoomStatus() { Id = 3, Status = "Occupied", StatusColor = "FF5652" });
            context.RoomStatus.Add(new RoomStatus() { Id = 4, Status = "Cleaning", StatusColor = "AB47BC" });
            context.RoomStatus.Add(new RoomStatus() { Id = 5, Status = "Maintance", StatusColor = "757575" });
            context.RoomStatus.Add(new RoomStatus() { Id = 6, Status = "Late Checkout", StatusColor = "2196F3" });
            context.SaveChanges();
        }
    }
}
