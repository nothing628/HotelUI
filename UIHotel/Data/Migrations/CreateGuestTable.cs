using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Migrations
{
    [Migration(2)]
    public class CreateGuestTable : Migration
    {
        public override void Down()
        {
            Delete.Table("checkin");
            Delete.Table("booking_detail");
            Delete.Table("booking");
            Delete.Table("guest");
        }

        public override void Up()
        {
            Create.Table("guest")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("fullname").AsString(50).NotNullable()
                .WithColumn("id_number").AsString(50).NotNullable().Unique()
                .WithColumn("id_kind").AsString(20).WithDefaultValue("KTP").Unique()
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();

            Create.Table("booking")
                .WithColumn("id").AsString(25).PrimaryKey()
                .WithColumn("id_guest").AsInt64().NotNullable()
                .WithColumn("count_child").AsInt16()
                .WithColumn("count_adult").AsInt16()
                .WithColumn("arrive_at").AsDate().NotNullable()
                .WithColumn("departure_at").AsDate().NotNullable()
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();

            Create.Table("booking_detail")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("id_booking").AsString(25).NotNullable()
                .WithColumn("id_room").AsInt64().NotNullable();

            Create.Table("checkin")
                .WithColumn("id").AsString(25).PrimaryKey()
                .WithColumn("id_booking").AsString(25).Nullable()
                .WithColumn("id_guest").AsInt64().NotNullable()
                .WithColumn("arrive_at").AsDate().NotNullable()
                .WithColumn("departure_at").AsDate().NotNullable()
                .WithColumn("checkin_at").AsDateTime().NotNullable()
                .WithColumn("checkout_at").AsDateTime().Nullable();
        }
    }
}
