using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Seeds
{
    public class RoomSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            context.Room.Add(new Room() { IdCategory = 1, RoomNumber = "201", Status = 1 });
            context.Room.Add(new Room() { IdCategory = 1, RoomNumber = "202", Status = 1 });
            context.Room.Add(new Room() { IdCategory = 1, RoomNumber = "203", Status = 1 });
            context.Room.Add(new Room() { IdCategory = 2, RoomNumber = "501", Status = 1 });
            context.Room.Add(new Room() { IdCategory = 2, RoomNumber = "502", Status = 1 });
            context.Room.Add(new Room() { IdCategory = 2, RoomNumber = "503", Status = 1 });
            context.Room.Add(new Room() { IdCategory = 3, RoomNumber = "301", Status = 1 });
            context.Room.Add(new Room() { IdCategory = 3, RoomNumber = "302", Status = 1 });
            context.SaveChanges();
        }
    }
}
