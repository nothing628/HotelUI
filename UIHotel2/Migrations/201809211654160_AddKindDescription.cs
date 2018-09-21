namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddKindDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("RoomPriceKinds", "KindDescription", c => c.String(maxLength: 100, storeType: "nvarchar"));
        }

        public override void Down()
        {
            DropColumn("RoomPriceKinds", "KindDescription");
        }
    }
}
