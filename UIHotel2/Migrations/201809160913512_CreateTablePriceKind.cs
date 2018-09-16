namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTablePriceKind : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "RoomPriceKinds",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        KindName = c.String(maxLength: 40, storeType: "nvarchar"),
                        KindColor = c.String(maxLength: 6, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("RoomPriceKinds");
        }
    }
}
