using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data
{
    [Table("checkin")]
    public class Checkin
    {
        public string Id { get; set; }
        public string IdBooking { get; set; }
        public long IdGuest { get; set; }
        public DateTime ArriveAt { get; set; }
        public DateTime DepartureAt { get; set; }
        public DateTime CheckinAt { get; set; }
        public DateTime CheckoutAt { get; set; }
    }
}
