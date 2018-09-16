using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    class Invoice
    {
        [Key]
        [StringLength(16)]
        public string Id { get; set; }

        [StringLength(16)]
        public string BookingId { get; set; }

        public DateTime? CloseAt { get; set; }

        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }

        [NotMapped]
        public bool IsClosed
        {
            get => CloseAt.HasValue;
        }
    }
}
