using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace UIHotel.Data.Table
{
    [Table("checkin")]
    public class Checkin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        [StringLength(25)]
        public string Id { get; set; }

        [Column("id_booking", Order = 1)]
        [StringLength(25)]
        public string IdBooking { get; set; }

        [Required]
        [Column("id_guest", Order = 2)]
        public long IdGuest { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("arrive_at", Order = 3, TypeName = "Date")]
        public DateTime ArriveAt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("depature_at", Order = 4, TypeName = "Date")]
        public DateTime DepartureAt { get; set; }

        [Required]
        [Column("checkin_at", Order = 5)]
        public DateTime CheckinAt { get; set; }

        [Column("checkout_at", Order = 6)]
        public DateTime CheckoutAt { get; set; }
    }
}
