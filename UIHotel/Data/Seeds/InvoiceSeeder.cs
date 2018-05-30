using System;
using System.Collections.Generic;
using System.Linq;
using UIHotel.Data.Table;

namespace UIHotel.Data.Seeds
{
    public class InvoiceSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 1, Kind = "Room Invoice" });
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 2, Kind = "Room Move Charge" });
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 3, Kind = "Late Checkout" });
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 97, Kind = "Deposit" });
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 98, Kind = "Cashback" });
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 99, Kind = "Tax" });
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 100, Kind = "Pay" });
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 101, Kind = "Uncategorized In" });
            context.InvoiceKinds.Add(new InvoiceKind() { Id = 201, Kind = "Uncategorized Out" });

            context.Invoices.Add(new Invoice()
            {
                Id = "TEST_INVOICE",
                IdCheckin = "TEST_INVOICE",
                IdGuest = 123,
                CreateAt = DateTime.Now
            });

            context.InvoiceDetails.Add(new InvoiceDetail()
            {
                IdInvoice = "TEST_INVOICE",
                IdKind = 1,
                TransactionDate = DateTime.Now,
                AmmountIn = 100000,
                Description = "Deposito",
                CreateAt = DateTime.Now
            });
            context.InvoiceDetails.Add(new InvoiceDetail()
            {
                IdInvoice = "TEST_INVOICE",
                IdKind = 1,
                TransactionDate = DateTime.Now,
                AmmountOut = 20000,
                Description = "Room Invoice",
                CreateAt = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}
