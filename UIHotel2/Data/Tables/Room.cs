using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    public class Room
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }
        
        [Index(IsUnique = true)]
        [StringLength(60)]
        public string RoomNumber { get; set; }

        [StringLength(3)]
        public string RoomFloor { get; set; }

        public long RoomStateId { get; set; }
        [ForeignKey("RoomStateId")]
        public RoomState State { get; set; }

        public long RoomCategoryId { get; set; }
        [ForeignKey("RoomCategoryId")]
        public RoomCategory Category { get; set; }
    }
}
