using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data
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

        [Column("ammount_in", Order = 3)]
        public decimal AmmountIn { get; set; }

        [Column("ammount_out", Order = 4)]
        public decimal AmmountOut { get; set; }

        [Required]
        [Column("create_at", Order = 5)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 6)]
        public DateTime? UpdateAt { get; set; }
    }
}
