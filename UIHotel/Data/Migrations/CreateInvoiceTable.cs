using FluentMigrator;

namespace UIHotel.Data.Migrations
{
    [Migration(3)]
    public class CreateInvoiceTable : Migration
    {
        public override void Down()
        {
            Delete.Table("invoice_detail");
            Delete.Table("invoice");
        }

        public override void Up()
        {
            Create.Table("invoice")
                .WithColumn("id").AsString(25).PrimaryKey()
                .WithColumn("id_checkin").AsString(25).NotNullable()
                .WithColumn("id_guest").AsInt64().NotNullable()
                .WithColumn("is_closed").AsBoolean().WithDefaultValue(false)
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();
            
            Create.Table("invoice_detail")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("id_invoice").AsString(25).NotNullable()
                .WithColumn("description").AsString(500).Nullable()
                .WithColumn("is_system").AsBoolean().WithDefaultValue(false)
                .WithColumn("date_transaction").AsDate().NotNullable()
                .WithColumn("ammount_in").AsDecimal().WithDefaultValue(0)
                .WithColumn("ammount_out").AsDecimal().WithDefaultValue(0)
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();
        }
    }
}
