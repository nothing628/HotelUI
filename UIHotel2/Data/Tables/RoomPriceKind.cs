using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    public class RoomPriceKind
    {
        [Key]
        public long Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(40)]
        public string KindName { get; set; }

        [StringLength(100)]
        public string KindDescription { get; set; }

        [StringLength(6)]
        public string KindColor { get; set; }
    }
}
