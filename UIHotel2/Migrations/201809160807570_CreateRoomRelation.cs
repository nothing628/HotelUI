namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRoomRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("Rooms", "RoomStateId", c => c.Long(nullable: false));
            CreateIndex("Rooms", "RoomStateId");
            AddForeignKey("Rooms", "RoomStateId", "RoomStates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Rooms", "RoomStateId", "RoomStates");
            DropIndex("Rooms", new[] { "RoomStateId" });
            DropColumn("Rooms", "RoomStateId");
        }
    }
}
