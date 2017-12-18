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
            Create.Table("user_test")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Username").AsString(50).NotNullable();
        }
    }
}
