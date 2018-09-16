namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUnique4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Guests", new[] { "IdNumber", "IdKind" }, unique: true, name: "IFX_IdNumber");
        }
        
        public override void Down()
        {
            DropIndex("Guests", "IFX_IdNumber");
        }
    }
}
