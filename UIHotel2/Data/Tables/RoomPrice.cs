using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    public class RoomPrice
    {
        [Key]
        public long Id { get; set; }
        public long RoomCategoryId { get; set; }
        public long RoomPriceKindId { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("RoomPriceKindId")]
        public RoomPriceKind Kind { get; set; }

        [ForeignKey("RoomCategoryId")]
        public RoomCategory Category { get; set; }
    }
}
