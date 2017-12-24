using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data.Table
{
    [Table("guest")]
    public class Guest
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Required]
        [Column("id_number", Order = 1)]
        [StringLength(50)]
        public string IdNumber { get; set; }

        [Required]
        [Column("id_kind", Order = 2)]
        [StringLength(20)]
        public string IdKind { get; set; }

        [Required]
        [Column("fullname", Order = 3)]
        [StringLength(50)]
        public string Fullname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("birth_day", Order = 4, TypeName = "Date")]
        public DateTime BirthDay { get; set; }

        [Required]
        [Column("create_at", Order = 5)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 6)]
        public DateTime? UpdateAt { get; set; }
    }
}
