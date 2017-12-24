using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace UIHotel.Data.Table
{
    [Table("booking")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(25)]
        [Column("id", Order = 0)]
        public string Id { get; set; }

        [Required]
        [Column("id_guest", Order = 1)]
        public long IdGuest { get; set; }

        [Required]
        [Column("id_type", Order = 2)]
        public long IdType { get; set; }

        [Column("count_child", Order = 3)]
        public short CountChild { get; set; }

        [Column("count_adult", Order = 4)]
        public short CountAdult { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("arrive_at", Order = 5, TypeName = "Date")]
        public DateTime ArriveAt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("departure_at", Order = 6, TypeName = "Date")]
        public DateTime DepartureAt { get; set; }

        [Required]
        [Column("create_at", Order = 7)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 8)]
        public DateTime? UpdateAt { get; set; }
    }
}
