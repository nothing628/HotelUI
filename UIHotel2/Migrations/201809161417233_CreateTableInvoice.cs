namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableInvoice : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Bookings");
            CreateTable(
                "InvoiceDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InvoiceId = c.String(maxLength: 16, storeType: "nvarchar"),
                        KindId = c.Short(nullable: false),
                        AmmountIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmmountOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionAt = c.DateTime(nullable: false, precision: 0),
                        IsSystem = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 1000, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "InvoiceDetailKinds",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        KindName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Invoices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 16, storeType: "nvarchar"),
                        BookingId = c.String(maxLength: 16, storeType: "nvarchar"),
                        CloseAt = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("Bookings", "Id", c => c.String(nullable: false, maxLength: 16, storeType: "nvarchar"));
            AddPrimaryKey("Bookings", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("Bookings");
            AlterColumn("Bookings", "Id", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            DropTable("Invoices");
            DropTable("InvoiceDetailKinds");
            DropTable("InvoiceDetails");
            AddPrimaryKey("Bookings", "Id");
        }
    }
}
