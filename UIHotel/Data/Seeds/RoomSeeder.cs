using System;
using System.Collections.Generic;
using System.Linq;
using UIHotel.Data.Table;

namespace UIHotel.Data.Seeds
{
    public class RoomSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            context.BookingType.Add(new BookingType() { TypeBooking = "Walk-In" });
            context.BookingType.Add(new BookingType() { TypeBooking = "Telephone" });
            context.BookingType.Add(new BookingType() { TypeBooking = "Online", IsOnline = true, OnlineProvider = "Traveloka" });
            context.BookingType.Add(new BookingType() { TypeBooking = "Online", IsOnline = true, OnlineProvider = "Agoda" });
            context.SaveChanges();

            context.Rooms.Add(new Room() { IdCategory = 1, RoomNumber = "201", IdStatus = 1 });
            context.Rooms.Add(new Room() { IdCategory = 1, RoomNumber = "202", IdStatus = 1 });
            context.Rooms.Add(new Room() { IdCategory = 1, RoomNumber = "203", IdStatus = 1 });
            context.Rooms.Add(new Room() { IdCategory = 2, RoomNumber = "501", IdStatus = 1 });
            context.Rooms.Add(new Room() { IdCategory = 2, RoomNumber = "502", IdStatus = 1 });
            context.Rooms.Add(new Room() { IdCategory = 2, RoomNumber = "503", IdStatus = 1 });
            context.Rooms.Add(new Room() { IdCategory = 3, RoomNumber = "301", IdStatus = 1 });
            context.Rooms.Add(new Room() { IdCategory = 3, RoomNumber = "302", IdStatus = 1 });
            context.SaveChanges();
        }
    }
}
