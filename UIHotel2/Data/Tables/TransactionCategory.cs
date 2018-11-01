using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    public class TransactionCategory
    {
        [Key]
        public long Id { get; set; }

        public bool IsIncome { get; set; } = true;

        [StringLength(40)]
        public string CategoryName { get; set; }

        [StringLength(40)]
        public string CategoryIcon { get; set; }

        [StringLength(6)]
        public string CategoryColor { get; set; }
    }
}
