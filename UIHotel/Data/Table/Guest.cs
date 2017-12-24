using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data.Table
{
    [Table("guest")]
    public class Guest
    {
        [Key]
        public long Id { get; set; }

        public string IdNumber { get; set; }
        public string IdKind { get; set; }
        public string Fullname { get; set; }

        public DateTime BirthDay { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
