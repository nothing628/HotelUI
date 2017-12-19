using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Seeds
{
    public class RoomStatusSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            context.RoomCategory.Add(new RoomCategory() { Category = "Big" });
            context.RoomCategory.Add(new RoomCategory() { Category = "Medium" });
            context.RoomCategory.Add(new RoomCategory() { Category = "Small" });
            context.RoomStatus.Add(new RoomStatus() { Id = 1, Status = "Vacant", StatusColor = "00FF00" });
            context.RoomStatus.Add(new RoomStatus() { Id = 2, Status = "Booked", StatusColor = "FF9900" });
            context.RoomStatus.Add(new RoomStatus() { Id = 3, Status = "Occupied", StatusColor = "FF0000" });
            context.RoomStatus.Add(new RoomStatus() { Id = 4, Status = "Cleaning", StatusColor = "0000FF" });
            context.RoomStatus.Add(new RoomStatus() { Id = 5, Status = "Maintance", StatusColor = "999999" });
            context.RoomStatus.Add(new RoomStatus() { Id = 6, Status = "LateCheckout", StatusColor = "FFFF00" });
            context.SaveChanges();
        }
    }
}
