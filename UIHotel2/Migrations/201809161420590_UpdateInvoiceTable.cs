namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoiceTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("InvoiceDetailKinds", "KindName", c => c.String(maxLength: 60, storeType: "nvarchar"));
            CreateIndex("InvoiceDetails", "InvoiceId");
            CreateIndex("InvoiceDetails", "KindId");
            CreateIndex("Invoices", "BookingId");
            CreateIndex("InvoiceDetailKinds", "KindName", unique: true);
            AddForeignKey("Invoices", "BookingId", "Bookings", "Id");
            AddForeignKey("InvoiceDetails", "InvoiceId", "Invoices", "Id");
            AddForeignKey("InvoiceDetails", "KindId", "InvoiceDetailKinds", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("InvoiceDetails", "KindId", "InvoiceDetailKinds");
            DropForeignKey("InvoiceDetails", "InvoiceId", "Invoices");
            DropForeignKey("Invoices", "BookingId", "Bookings");
            DropIndex("InvoiceDetailKinds", new[] { "KindName" });
            DropIndex("Invoices", new[] { "BookingId" });
            DropIndex("InvoiceDetails", new[] { "KindId" });
            DropIndex("InvoiceDetails", new[] { "InvoiceId" });
            AlterColumn("InvoiceDetailKinds", "KindName", c => c.String(unicode: false));
        }
    }
}
