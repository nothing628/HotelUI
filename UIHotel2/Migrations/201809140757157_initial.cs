namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Guests",
                c => new
                    {
                        id_number = c.String(nullable: false, maxLength: 30, storeType: "nvarchar"),
                        id_kind = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Name = c.String(maxLength: 60, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.id_number, t.id_kind });
            
        }
        
        public override void Down()
        {
            DropTable("Guests");
        }
    }
}
