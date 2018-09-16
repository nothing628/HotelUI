namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("RoomPrices", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("RoomPrices", "Price");
        }
    }
}
