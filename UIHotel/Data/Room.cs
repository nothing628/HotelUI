using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data
{
    [Table("room")]
    public class Room
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Column("id_category", Order = 1)]
        [Required]
        public long IdCategory { get; set; }
        
        [Required]
        [StringLength(10)]
        [Column("room_number", Order = 2)]
        public string RoomNumber { get; set; }

        [Column("room_floor", Order = 3)]
        public short RoomFloor { get; set; }

        [Column("status", Order = 4)]
        public int Status { get; set; }
    }
}
