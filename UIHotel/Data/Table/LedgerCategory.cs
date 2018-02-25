using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data.Table
{
    [Table("ledger_category")]
    public class LedgerCategory
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Column("description", Order = 1)]
        [StringLength(50)]
        public string Description { get; set; }

        [Column("icon", Order = 2)]
        [StringLength(20)]
        public string Icon { get; set; }

        [Column("color", Order = 3)]
        [StringLength(20)]
        public string Color { get; set; }

        [Column("is_expense", Order = 4)]
        public bool IsExpense { get; set; }
    }
}
