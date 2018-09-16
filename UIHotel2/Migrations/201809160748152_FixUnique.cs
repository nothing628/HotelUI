namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUnique : DbMigration
    {
        public override void Up()
        {
            AddColumn("Rooms", "RoomFloor", c => c.String(maxLength: 3, storeType: "nvarchar"));
            CreateIndex("Rooms", "RoomNumber", unique: true);
            CreateIndex("RoomStates", "StateName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("RoomStates", new[] { "StateName" });
            DropIndex("Rooms", new[] { "RoomNumber" });
            DropColumn("Rooms", "RoomFloor");
        }
    }
}
