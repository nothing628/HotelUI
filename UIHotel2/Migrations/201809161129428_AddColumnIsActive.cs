namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Users", "IsActive");
        }
    }
}
