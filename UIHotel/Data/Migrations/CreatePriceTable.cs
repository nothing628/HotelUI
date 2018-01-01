using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Migrations
{
    [Migration(4)]
    public class CreatePriceTable : Migration
    {
        public override void Down()
        {
            Delete.Table("room_price");
            Delete.Table("daycycle");
            Delete.Table("dayeffect");
        }

        public override void Up()
        {
            Create.Table("dayeffect")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("effect").AsString(50).NotNullable()
                .WithColumn("effect_color").AsString(6).NotNullable()
                .WithColumn("can_delete").AsBoolean().WithDefaultValue(true);

            Create.Table("daycycle")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("id_effect").AsInt32().NotNullable()
                .WithColumn("date_at").AsDate().Unique();

            Create.Table("room_price")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("id_category").AsInt64().NotNullable()
                .WithColumn("id_effect").AsInt32().NotNullable()
                .WithColumn("price").AsCurrency().WithDefaultValue(0);

            Create.UniqueConstraint("PriceUnique")
                .OnTable("room_price")
                .Columns("id_category", "id_effect");
        }
    }
}
