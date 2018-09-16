namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCalendar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "RoomCalendars",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateAt = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("RoomPriceKinds", "KindName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("RoomPriceKinds", new[] { "KindName" });
            DropTable("RoomCalendars");
        }
    }
}
