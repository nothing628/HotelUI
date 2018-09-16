namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUnique2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("RoomPrices", "RoomCategoryId");
            CreateIndex("RoomPrices", "RoomPriceKindId");
            AddForeignKey("RoomPrices", "RoomCategoryId", "RoomCategories", "Id", cascadeDelete: true);
            AddForeignKey("RoomPrices", "RoomPriceKindId", "RoomPriceKinds", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("RoomPrices", "RoomPriceKindId", "RoomPriceKinds");
            DropForeignKey("RoomPrices", "RoomCategoryId", "RoomCategories");
            DropIndex("RoomPrices", new[] { "RoomPriceKindId" });
            DropIndex("RoomPrices", new[] { "RoomCategoryId" });
        }
    }
}
