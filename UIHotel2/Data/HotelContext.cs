using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.AppObject;
using UIHotel2.Data.Convention;
using UIHotel2.Data.Tables;

namespace UIHotel2.Data
{
    class HotelContext: DbContext
    {
        public HotelContext(): base(DBObject.ConnectionString)
        {
            //
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Guest>()
                .Property(x => x.BirthDay)
                .HasColumnType("Date");
            modelBuilder.Entity<RoomCalendar>()
                .Property(x => x.DateAt)
                .HasColumnType("Date");
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingType> BookingTypes { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomState> RoomStates { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<RoomPriceKind> RoomPriceKinds { get; set; }
        public DbSet<RoomPrice> RoomPrices { get; set; }
        public DbSet<RoomCalendar> RoomCalendars { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
