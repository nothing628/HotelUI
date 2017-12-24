using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data
{
    [Table("booking_type")]
    public class BookingType
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Required]
        [Column("type", Order = 1)]
        [StringLength(50)]
        public string TypeBooking { get; set; }

        [Column("is_online", Order = 2)]
        public bool IsOnline { get; set; }
    }
}
