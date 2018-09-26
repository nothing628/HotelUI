namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStateAllow : DbMigration
    {
        public override void Up()
        {
            AddColumn("RoomStates", "StateAllow", c => c.String(maxLength: 12, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("RoomStates", "StateAllow");
        }
    }
}
