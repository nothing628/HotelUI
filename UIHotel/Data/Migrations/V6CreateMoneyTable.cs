using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Migrations
{
    [Migration(6)]
    public class V6CreateMoneyTable : Migration
    {
        public override void Down()
        {
            Delete.Table("ledger_log");
            Delete.Table("ledger_category");
        }

        public override void Up()
        {
            Create.Table("ledger_category")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("description").AsString(50).NotNullable()
                .WithColumn("icon").AsString(20).Nullable()
                .WithColumn("color").AsString(20).Nullable()
                .WithColumn("is_expense").AsBoolean().WithDefaultValue(false);

            Create.Table("ledger_log")
                .WithColumn("id").AsString(16).PrimaryKey()
                .WithColumn("id_category").AsInt32().NotNullable()
                .WithColumn("date_transaction").AsDateTime().NotNullable()
                .WithColumn("description").AsString(512).NotNullable()
                .WithColumn("debit").AsDecimal().WithDefaultValue(0)
                .WithColumn("kredit").AsDecimal().WithDefaultValue(0)
                .WithColumn("is_close").AsBoolean().WithDefaultValue(false)
                .WithColumn("create_at").AsDateTime().NotNullable()
                .WithColumn("update_at").AsDateTime().Nullable();
        }
    }
}
