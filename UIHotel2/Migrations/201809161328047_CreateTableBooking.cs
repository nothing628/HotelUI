namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableBooking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Bookings",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        BookingAt = c.DateTime(nullable: false, precision: 0),
                        CheckinAt = c.DateTime(precision: 0),
                        CheckoutAt = c.DateTime(precision: 0),
                        ArrivalDate = c.DateTime(nullable: false, storeType: "date"),
                        DepartureDate = c.DateTime(nullable: false, storeType: "date"),
                        CountAdult = c.Int(nullable: false),
                        CountChild = c.Int(nullable: false),
                        Note = c.String(maxLength: 1000, storeType: "nvarchar"),
                        GuestId = c.Long(nullable: false),
                        RoomId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BookingTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypeName = c.String(maxLength: 60, storeType: "nvarchar"),
                        IsLocal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("BookingTypes");
            DropTable("Bookings");
        }
    }
}
