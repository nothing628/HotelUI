using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    class Booking
    {
        [Key]
        public string Id { get; set; }
        public DateTime BookingAt { get; set; }
        public DateTime? CheckinAt { get; set; }
        public DateTime? CheckoutAt { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ArrivalDate { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DepartureDate { get; set; }

        public int CountAdult { get; set; }
        public int CountChild { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        public long GuestId { get; set; }
        public long RoomId { get; set; }
        public long TypeId { get; set; }

        [ForeignKey("GuestId")]
        public Guest Guest { get; set; }

        [ForeignKey("RoomId")]
        public Room Room { get; set; }

        [ForeignKey("TypeId")]
        public BookingType Type { get; set; }

        [NotMapped]
        public bool IsCheckin
        {
            get
            {
                return CheckinAt.HasValue;
            }
        }

        [NotMapped]
        public bool IsCheckout
        {
            get
            {
                return CheckoutAt.HasValue;
            }
        }

        public static string GenerateId()
        {
            using (var context = new HotelContext())
            {
                var CurrDate = DateTime.Now.ToString("yyyyMMdd");
                var Prefix = "BOK";
                var PrefixID = Prefix + CurrDate;
                var newId = 1;

                try
                {
                    var chk = (from a in context.Bookings
                               where a.Id.StartsWith(PrefixID)
                               select a.Id).ToList();
                    var trans = chk.Select(x => x.Replace(PrefixID, "")).Select(x => Convert.ToInt32(x)).Max();

                    newId = trans + 1;
                }
                catch
                {

                }

                return PrefixID + string.Format("{0:D5}", newId);
            }
        }
    }
}
