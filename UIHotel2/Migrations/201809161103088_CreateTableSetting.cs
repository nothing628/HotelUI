namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Settings",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Value = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("Settings");
        }
    }
}
