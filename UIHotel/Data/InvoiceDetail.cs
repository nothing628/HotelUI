using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public string IdInvoice { get; set; }
        public string IdBill { get; set; }
        public decimal InAmmount { get; set; }
        public decimal OutAmmount { get; set; }
    }
}
