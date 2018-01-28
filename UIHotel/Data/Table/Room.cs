using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data.Table
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
        public short RoomFloor { get; set; } = 1;

        [Column("status", Order = 4)]
        public int IdStatus { get; set; }

        [ForeignKey("IdCategory")]
        public RoomCategory Category { get; set; }

        [ForeignKey("IdStatus")]
        public RoomStatus Status { get; set; }
        
        [NotMapped]
        public string RoomStatus { get => Status.Status; }

        [NotMapped]
        public string RoomCategory { get => Category.Category; }

        [NotMapped]
        public string RoomName
        {
            get
            {
                return RoomCategory + ": " + RoomNumber + ", Floor " + RoomFloor;
            }
        }

        [NotMapped]
        public string DetailLink
        {
            get
            {
                return string.Format("http://localhost.com/room/get/detail?roomId={0}", Id);
            }
        }
    }
}
