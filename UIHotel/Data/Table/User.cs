using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data.Table
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Required]
        [Column("username", Order = 1)]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [Column("password", Order = 2)]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [Column("fullname", Order = 3)]
        [StringLength(50)]
        public string Fullname { get; set; }

        [Column("permission", Order = 4)]
        public int Permission { get; set; }

        [Required]
        [Column("create_at", Order = 5)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 6)]
        public DateTime? UpdateAt { get; set; }
    }
}
