namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConst : DbMigration
    {
        public override void Up()
        {
            DropIndex("Users", new[] { "Username" });
            AlterColumn("Users", "Username", c => c.String(nullable: false, maxLength: 40, storeType: "nvarchar"));
            AlterColumn("Users", "Fullname", c => c.String(nullable: false, maxLength: 40, storeType: "nvarchar"));
            CreateIndex("Users", "Username", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("Users", new[] { "Username" });
            AlterColumn("Users", "Fullname", c => c.String(maxLength: 40, storeType: "nvarchar"));
            AlterColumn("Users", "Username", c => c.String(maxLength: 40, storeType: "nvarchar"));
            CreateIndex("Users", "Username", unique: true);
        }
    }
}
