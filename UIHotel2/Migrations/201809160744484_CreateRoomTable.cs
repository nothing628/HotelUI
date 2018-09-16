namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRoomTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Guests");
            CreateTable(
                "Rooms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoomNumber = c.String(maxLength: 60, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "RoomStates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StateName = c.String(maxLength: 24, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("Guests", "IdKind", c => c.String(nullable: false, maxLength: 10, storeType: "nvarchar"));
            AddPrimaryKey("Guests", new[] { "Id", "IdKind" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("Guests");
            AlterColumn("Guests", "IdKind", c => c.String(maxLength: 10, storeType: "nvarchar"));
            DropTable("RoomStates");
            DropTable("Rooms");
            AddPrimaryKey("Guests", "Id");
        }
    }
}
