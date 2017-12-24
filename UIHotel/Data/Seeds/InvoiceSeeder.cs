using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Seeds
{
    public class InvoiceSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
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
                AmmountIn = 100000,
                Description = "Deposito",
                CreateAt = DateTime.Now
            });

            context.InvoiceDetails.Add(new InvoiceDetail()
            {
                IdInvoice = "TEST_INVOICE",
                AmmountOut = 20000,
                Description = "Room Invoice",
                CreateAt = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}
