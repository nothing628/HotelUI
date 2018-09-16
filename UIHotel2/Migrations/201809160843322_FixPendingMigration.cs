namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPendingMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("Rooms", "RoomCategoryId", c => c.Long(nullable: false));
            CreateIndex("RoomCategories", "CategoryName", unique: true);
            CreateIndex("Rooms", "RoomCategoryId");
            AddForeignKey("Rooms", "RoomCategoryId", "RoomCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Rooms", "RoomCategoryId", "RoomCategories");
            DropIndex("Rooms", new[] { "RoomCategoryId" });
            DropIndex("RoomCategories", new[] { "CategoryName" });
            DropColumn("Rooms", "RoomCategoryId");
        }
    }
}
