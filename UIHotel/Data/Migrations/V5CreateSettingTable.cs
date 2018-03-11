using FluentMigrator;

namespace UIHotel.Data.Migrations
{
    [Migration(5)]
    public class V5CreateSettingTable : Migration
    {
        public override void Down()
        {
            Delete.Table("setting");
        }

        public override void Up()
        {
            Create.Table("setting")
                .WithColumn("key").AsString(50).PrimaryKey()
                .WithColumn("value").AsString();
        }
    }
}
