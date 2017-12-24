using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data
{
    [Table("invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public string Id { get; set; }

        [Required]
        [Column("id_checkin", Order = 1)]
        [StringLength(25)]
        public string IdCheckin { get; set; }

        [Required]
        [Column("id_guest", Order = 2)]
        public long IdGuest { get; set; }

        [Column("is_closed", Order = 3)]
        public bool IsClosed { get; set; }

        [Required]
        [Column("create_at", Order = 4)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 5)]
        public DateTime? UpdateAt { get; set; }
    }
}
