using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data.Table;

namespace UIHotel.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContext : DbContext
    {
        public DbSet<BookingType> BookingType { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Checkin> CheckIn { get; set; }
        public DbSet<DayCycle> DayCycles { get; set; }
        public DbSet<DayEffect> DayEffect { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<RoomCategory> RoomCategory { get; set; }
        public DbSet<RoomPrice> RoomPrice { get; set; }
        public DbSet<RoomStatus> RoomStatus { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DataContext()
          : base(new MySqlConnection(Properties.Settings.Default.MyDB), false)
        {
        }

        public DataContext(DbConnection existingConnection, bool contextOwnsConnection)
          : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Table Relation One to One
            modelBuilder.Entity<Invoice>().HasKey(x => x.Id);
            modelBuilder.Entity<InvoiceDetail>()
                .HasRequired<Invoice>(s => s.Invoice)
                .WithMany(g => g.Details)
                .HasForeignKey<string>(s => s.IdInvoice);
        }
    }
}
