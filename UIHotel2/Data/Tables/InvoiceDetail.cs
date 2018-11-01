using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    public class InvoiceDetail
    {
        [Key]
        public long Id { get; set; }

        [StringLength(16)]
        public string InvoiceId { get; set; }

        public short KindId { get; set; }
        public decimal AmmountIn { get; set; } = 0;
        public decimal AmmountOut { get; set; } = 0;
        public DateTime TransactionAt { get; set; }
        public bool IsSystem { get; set; } = true;

        [StringLength(1000)]
        public string Description { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }

        [ForeignKey("KindId")]
        public InvoiceDetailKind Kind { get; set; }
    }
}
