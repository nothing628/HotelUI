namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForeignKey1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Bookings", "GuestId");
            CreateIndex("Bookings", "RoomId");
            CreateIndex("Bookings", "TypeId");
            AddForeignKey("Bookings", "GuestId", "Guests", "Id", cascadeDelete: true);
            AddForeignKey("Bookings", "RoomId", "Rooms", "Id", cascadeDelete: true);
            AddForeignKey("Bookings", "TypeId", "BookingTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Bookings", "TypeId", "BookingTypes");
            DropForeignKey("Bookings", "RoomId", "Rooms");
            DropForeignKey("Bookings", "GuestId", "Guests");
            DropIndex("Bookings", new[] { "TypeId" });
            DropIndex("Bookings", new[] { "RoomId" });
            DropIndex("Bookings", new[] { "GuestId" });
        }
    }
}
