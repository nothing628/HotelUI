using FluentMigrator;

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
            Delete.Table("booking_type");
            Delete.Table("guest");
        }

        public override void Up()
        {
            Create.Table("guest")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("id_number").AsString(50).NotNullable()
                .WithColumn("id_kind").AsString(20).WithDefaultValue("KTP")
                .WithColumn("fullname").AsString(50).NotNullable()
                .WithColumn("email").AsString(100).Nullable()
                .WithColumn("isVIP").AsBoolean().WithDefaultValue(false)
                .WithColumn("birth_place").AsString(50).Nullable()
                .WithColumn("birth_day").AsDate().NotNullable()
                .WithColumn("phone1").AsString(15).NotNullable()
                .WithColumn("phone2").AsString(15).Nullable()
                .WithColumn("address").AsString(255).Nullable()
                .WithColumn("city").AsString(50).Nullable()
                .WithColumn("province").AsString(50).Nullable()
                .WithColumn("state").AsString(50).Nullable()
                .WithColumn("postcode").AsString(10).Nullable()
                .WithColumn("photo_doc").AsString(50).NotNullable()
                .WithColumn("photo_guest").AsString(50).Nullable()
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();

            Create.UniqueConstraint("Cret")
                .OnTable("guest")
                .Columns("id_number", "id_kind");

            Create.Table("booking_type")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("type").AsString(50).NotNullable()
                .WithColumn("is_online").AsBoolean().WithDefaultValue(false)
                .WithColumn("online_provider").AsString(50).Nullable();

            Create.Table("booking")
                .WithColumn("id").AsString(25).PrimaryKey()
                .WithColumn("id_guest").AsInt64().NotNullable()
                .WithColumn("id_type").AsInt64().NotNullable()
                .WithColumn("count_child").AsInt16().WithDefaultValue(0)
                .WithColumn("count_adult").AsInt16().WithDefaultValue(0)
                .WithColumn("arrive_at").AsDate().NotNullable()
                .WithColumn("departure_at").AsDate().NotNullable()
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();

            Create.Table("booking_detail")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("id_booking").AsString(25).NotNullable()
                .WithColumn("id_room").AsInt64().NotNullable()
                .WithColumn("id_checkin").AsString(25).Nullable()
                .WithColumn("is_checkin").AsBoolean().WithDefaultValue(false);

            Create.Table("checkin")
                .WithColumn("id").AsString(25).PrimaryKey()
                .WithColumn("id_room").AsInt64().NotNullable()
                .WithColumn("id_guest").AsInt64().NotNullable()
                .WithColumn("id_booking").AsString(25).Nullable()
                .WithColumn("arrive_at").AsDate().NotNullable()
                .WithColumn("departure_at").AsDate().NotNullable()
                .WithColumn("count_child").AsInt16().WithDefaultValue(0)
                .WithColumn("count_adult").AsInt16().WithDefaultValue(1)
                .WithColumn("checkin_at").AsDateTime().NotNullable()
                .WithColumn("checkout_at").AsDateTime().Nullable()
                .WithColumn("note").AsString(255).Nullable();
        }
    }
}
