using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data
{
    public class Invoice
    {
        public string Id { get; set; }
        public string IdGuest { get; set; }
        public bool IsClosed { get; set; }
    }
}
