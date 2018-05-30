using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Migrations
{
    [Migration(7)]
    public class V8ModifyInvoiceTable : Migration
    {
        public override void Down()
        {
            Delete.Table("invoice_detail");
            Delete.Table("invoice_kind");

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

        public override void Up()
        {
            Delete.Table("invoice_detail");

            Create.Table("invoice_detail")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("id_invoice").AsString(25).NotNullable()
                .WithColumn("id_room").AsInt64().Nullable()
                .WithColumn("id_kind").AsInt64().NotNullable()
                .WithColumn("description").AsString(500).Nullable()
                .WithColumn("is_system").AsBoolean().WithDefaultValue(false)
                .WithColumn("date_transaction").AsDate().NotNullable()
                .WithColumn("ammount_in").AsDecimal().WithDefaultValue(0)
                .WithColumn("ammount_out").AsDecimal().WithDefaultValue(0)
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();

            Create.Table("invoice_kind")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("kind").AsString(50).NotNullable();
        }
    }
}
