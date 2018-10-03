using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    class Transaction
    {
        [Key]
        public long Id { get; set; }
        
        public DateTime TransactionAt { get; set; }

        public bool IsClosed { get; set; } = false;

        public decimal AmmountIn { get; set; } = 0;

        public decimal AmmountOut { get; set; } = 0;

        public decimal Subtotal { get; set; } = 0;

        public long UserId { get; set; }

        public long CategoryId { get; set; }

        [StringLength(200)]
        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public TransactionCategory Category { get; set; }

        [ForeignKey("UserId")]
        public User ChangedBy { get; set; }
    }
}
