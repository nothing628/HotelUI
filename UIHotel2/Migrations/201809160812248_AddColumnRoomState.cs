namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnRoomState : DbMigration
    {
        public override void Up()
        {
            AddColumn("RoomStates", "StateColor", c => c.String(maxLength: 6, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("RoomStates", "StateColor");
        }
    }
}
