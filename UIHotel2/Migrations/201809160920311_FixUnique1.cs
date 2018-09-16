namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUnique1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("RoomCalendars", "RoomPriceKindId", c => c.Long(nullable: false));
            CreateIndex("RoomCalendars", "DateAt", unique: true);
            CreateIndex("RoomCalendars", "RoomPriceKindId");
            AddForeignKey("RoomCalendars", "RoomPriceKindId", "RoomPriceKinds", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("RoomCalendars", "RoomPriceKindId", "RoomPriceKinds");
            DropIndex("RoomCalendars", new[] { "RoomPriceKindId" });
            DropIndex("RoomCalendars", new[] { "DateAt" });
            DropColumn("RoomCalendars", "RoomPriceKindId");
        }
    }
}
