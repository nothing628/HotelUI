namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForeignKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Guests");
            AddColumn("Bookings", "TypeId", c => c.Long(nullable: false));
            DropColumn("Guests", "Id");
            AddColumn("Guests", "IdNumber", c => c.String(maxLength: 30, storeType: "nvarchar"));
            AddColumn("Guests", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("Guests", "Id");
            AlterColumn("Guests", "IdKind", c => c.String(maxLength: 10, storeType: "nvarchar"));
            AlterColumn("Guests", "Id", c => c.Long(nullable: false, identity: true));
            CreateIndex("BookingTypes", "TypeName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("BookingTypes", new[] { "TypeName" });
            DropPrimaryKey("Guests");
            AlterColumn("Guests", "IdKind", c => c.String(nullable: false, maxLength: 10, storeType: "nvarchar"));
            AlterColumn("Guests", "Id", c => c.String(nullable: false, maxLength: 30, storeType: "nvarchar"));
            DropColumn("Guests", "IdNumber");
            DropColumn("Bookings", "TypeId");
            AddPrimaryKey("Guests", new[] { "Id", "IdKind" });
        }
    }
}
