using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    class RoomCalendar
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(Order = 1, TypeName = "Date")]
        [Index(IsUnique = true)]
        public DateTime DateAt { get; set; }

        public long RoomPriceKindId { get; set; }
        [ForeignKey("RoomPriceKindId")]
        public RoomPriceKind Kind { get; set; }
    }
}
