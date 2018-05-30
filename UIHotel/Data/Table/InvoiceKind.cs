using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Table
{
    [Table("invoice_kind")]
    public class InvoiceKind
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [StringLength(50)]
        [Column("kind", Order = 1)]
        public string Kind { get; set; }
    }
}
