namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldFullname : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users", "Fullname", c => c.String(maxLength: 40, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("Users", "Fullname");
        }
    }
}
