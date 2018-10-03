namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionField : DbMigration
    {
        public override void Up()
        {
            AddColumn("Transactions", "Description", c => c.String(nullable: false, maxLength: 200, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("Transactions", "Description");
        }
    }
}
