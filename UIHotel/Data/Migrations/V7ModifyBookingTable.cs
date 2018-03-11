using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Migrations
{
    [Migration(7)]
    public class V7ModifyBookingTable : Migration
    {
        public override void Down()
        {
            Create.Table("booking")
                .WithColumn("id").AsString(25).PrimaryKey()
                .WithColumn("id_guest").AsInt64().NotNullable()
                .WithColumn("id_type").AsInt64().NotNullable()
                .WithColumn("count_child").AsInt16().WithDefaultValue(0)
                .WithColumn("count_adult").AsInt16().WithDefaultValue(0)
                .WithColumn("arrive_at").AsDate().NotNullable()
                .WithColumn("departure_at").AsDate().NotNullable()
                .WithColumn("is_closed").AsBoolean().WithDefaultValue(false)
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();

            Create.Table("booking_detail")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("id_booking").AsString(25).NotNullable()
                .WithColumn("id_room").AsInt64().NotNullable()
                .WithColumn("id_checkin").AsString(25).Nullable()
                .WithColumn("is_checkin").AsBoolean().WithDefaultValue(false);
        }

        public override void Up()
        {
            Delete.Table("booking_detail");
            Delete.Table("booking");

            Create.Table("booking")
                .WithColumn("id").AsString(25).PrimaryKey()
                .WithColumn("id_guest").AsInt64().NotNullable()
                .WithColumn("id_type").AsInt64().NotNullable()
                .WithColumn("id_room").AsInt64().NotNullable()
                .WithColumn("id_checkin").AsString(25).Nullable()
                .WithColumn("is_checkin").AsBoolean().WithDefaultValue(false)
                .WithColumn("count_child").AsInt16().WithDefaultValue(0)
                .WithColumn("count_adult").AsInt16().WithDefaultValue(0)
                .WithColumn("arrive_at").AsDate().NotNullable()
                .WithColumn("departure_at").AsDate().NotNullable()
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();
        }
    }
}
