using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data.Table
{
    [Table("invoice_detail")]
    public class InvoiceDetail
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Required]
        [Column("id_invoice", Order = 1)]
        public string IdInvoice { get; set; }

        [Column("description", Order = 2)]
        [StringLength(500)]
        public string Description { get; set; }

        [Column("is_system", Order = 3)]
        public bool IsSystem { get; set; }

        [Required]
        [Column("date_transaction", Order = 4)]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Column("ammount_in", Order = 5)]
        public decimal AmmountIn { get; set; }

        [Column("ammount_out", Order = 6)]
        public decimal AmmountOut { get; set; }

        [Required]
        [Column("create_at", Order = 7)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 8)]
        public DateTime? UpdateAt { get; set; }
        
        public virtual Invoice Invoice { get; set; }
    }
}
