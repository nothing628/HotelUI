using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data
{
    [Table("room_status")]
    public class RoomStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Column("status", Order = 1)]
        [StringLength(50)]
        [Required]
        public string Status { get; set; }

        [Column("status_color", Order = 2)]
        [StringLength(6)]
        [Required]
        public string StatusColor { get; set; }
    }
}
