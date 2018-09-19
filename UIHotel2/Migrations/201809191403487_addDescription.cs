namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("RoomCategories", "CategoryDescription", c => c.String(maxLength: 250, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("RoomCategories", "CategoryDescription");
        }
    }
}
