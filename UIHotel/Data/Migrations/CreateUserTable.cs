using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Migrations
{
    [Migration(1)]
    public class CreateUserTable : Migration
    {
        public override void Down()
        {
            Delete.Table("user");
        }

        public override void Up()
        {
            Create.Table("user")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("username").AsString(50).NotNullable()
                .WithColumn("password").AsString(50).NotNullable()
                .WithColumn("permission").AsInt32().WithDefaultValue(0)
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();

            Create.Table("room_status")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("status").AsString(50).NotNullable()
                .WithColumn("status_color").AsString(6).NotNullable();

            Create.Table("room_category")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("category").AsString(50).NotNullable();

            Create.Table("room")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("room_number").AsString(10).NotNullable()
                .WithColumn("status").AsInt32().WithDefaultValue(0);
        }
    }
}
