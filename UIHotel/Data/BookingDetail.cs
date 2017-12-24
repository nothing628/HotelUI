using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data
{
    [Table("booking_detail")]
    public class BookingDetail
    {
        public long Id { get; set; }
        public string IdBooking { get; set; }
        public long IdRoom { get; set; }
        public bool IsCheckedIn { get; set; }
    }
}
