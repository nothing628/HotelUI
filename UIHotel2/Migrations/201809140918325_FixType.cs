namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixType : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Guests");
            AlterColumn("Guests", "Id", c => c.String(nullable: false, maxLength: 30, storeType: "nvarchar"));
            AlterColumn("Guests", "IdKind", c => c.String(maxLength: 10, storeType: "nvarchar"));
            AddPrimaryKey("Guests", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("Guests");
            AlterColumn("Guests", "IdKind", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("Guests", "Id", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AddPrimaryKey("Guests", "Id");
        }
    }
}
