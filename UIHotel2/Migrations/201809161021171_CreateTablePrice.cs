namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTablePrice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "RoomPrices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoomCategoryId = c.Long(nullable: false),
                        RoomPriceKindId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("RoomPrices");
        }
    }
}
