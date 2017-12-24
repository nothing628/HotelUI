using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace UIHotel.Data.Table
{
    [Table("booking_detail")]
    public class BookingDetail
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Required]
        [Column("id_booking", Order = 1)]
        public string IdBooking { get; set; }

        [Column("id_room", Order = 2)]
        public long IdRoom { get; set; }
        
        [Column("is_checkin", Order = 3)]
        public bool IsCheckedIn { get; set; }
    }
}
