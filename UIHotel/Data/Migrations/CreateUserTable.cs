using FluentMigrator;

namespace UIHotel.Data.Migrations
{
    [Migration(1)]
    public class CreateUserTable : Migration
    {
        public override void Down()
        {
            Delete.Table("room");
            Delete.Table("room_category");
            Delete.Table("room_status");
            Delete.Table("user");
        }

        public override void Up()
        {
            Create.Table("user")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("username").AsString(50).NotNullable()
                .WithColumn("password").AsString(255).NotNullable()
                .WithColumn("fullname").AsString(50).NotNullable()
                .WithColumn("permission").AsInt32().WithDefaultValue(0)
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();

            Create.Table("room_status")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("status").AsString(50).NotNullable()
                .WithColumn("status_color").AsString(6).NotNullable();

            Create.Table("room_category")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("category").AsString(50).NotNullable()
                .WithColumn("description").AsString(200).Nullable();

            Create.Table("room")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("id_category").AsInt64().NotNullable()
                .WithColumn("room_number").AsString(10).NotNullable().Unique()
                .WithColumn("room_floor").AsInt16().WithDefaultValue(1)
                .WithColumn("status").AsInt32().WithDefaultValue(0);
        }
    }
}
