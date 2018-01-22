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
        public string IdKind { get; set; } = "KTP";

        [Required]
        [Column("fullname", Order = 3)]
        [StringLength(50)]
        public string Fullname { get; set; }

        [Column("email", Order = 4)]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("isVIP", Order = 5)]
        public bool IsVIP { get; set; }

        [Column("birth_place", Order = 6)]
        [StringLength(50)]
        public string BirthPlace { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("birth_day", Order = 7, TypeName = "Date")]
        public DateTime BirthDay { get; set; }

        [Required]
        [Column("phone1", Order = 8)]
        [StringLength(15)]
        public string Phone1 { get; set; }

        [Column("phone2", Order = 9)]
        [StringLength(15)]
        public string Phone2 { get; set; }

        [Column("address", Order = 10)]
        [StringLength(255)]
        public string Address { get; set; }

        [Column("city", Order = 11)]
        [StringLength(50)]
        public string City { get; set; }

        [Column("province", Order = 12)]
        [StringLength(50)]
        public string Province { get; set; }

        [Column("state", Order = 13)]
        [StringLength(50)]
        public string State { get; set; }

        [Column("postcode", Order = 14)]
        [StringLength(10)]
        public string PostCode { get; set; }

        [Required]
        [Column("photo_doc", Order = 15)]
        [StringLength(50)]
        public string PhotoDoc { get; set; }

        [Column("photo_guest", Order = 16)]
        [StringLength(50)]
        public string PhotoGuest { get; set; }

        [Required]
        [Column("create_at", Order = 17)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 18)]
        public DateTime? UpdateAt { get; set; }

        [NotMapped]
        public string FullAddress
        {
            get
            {
                return this.Address + ", " + this.City + ", " + this.Province + ", " + this.State;
            }
        }

        [NotMapped]
        public string DetailLink
        {
            get
            {
                return "http://localhost.com/guest/get/detail?id=" + this.Id;
            }
        }

        [NotMapped]
        public string EditLink
        {
            get
            {
                return "http://localhost.com/guest/get/edit?id=" + this.Id;
            }
        }

        [NotMapped]
        public string PhotoUrl
        {
            get
            {
                return "http://localhost.com/Upload/" + this.PhotoGuest;
            }
        }

        [NotMapped]
        public string DocumentUrl
        {
            get
            {
                return "http://localhost.com/Upload/" + this.PhotoDoc;
            }
        }
    }
}
