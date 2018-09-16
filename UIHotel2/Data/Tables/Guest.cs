using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    class Guest
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string IdKind { get; set; }

        [Required]
        [StringLength(60)]
        public string Fullname { get; set; }
        
        [StringLength(100)]
        public string Email { get; set; }
        
        public bool IsVIP { get; set; }
        
        [StringLength(50)]
        public string BirthPlace { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(Order = 7, TypeName = "Date")]
        public DateTime BirthDay { get; set; }

        [Required]
        [StringLength(15)]
        public string Phone1 { get; set; }
        
        [StringLength(15)]
        public string Phone2 { get; set; }
        
        [StringLength(255)]
        public string Address { get; set; }
        
        [StringLength(50)]
        public string City { get; set; }
        
        [StringLength(50)]
        public string Province { get; set; }
        
        [StringLength(50)]
        public string State { get; set; }
        
        [StringLength(10)]
        public string PostCode { get; set; }

        [Required]
        [StringLength(50)]
        public string PhotoDoc { get; set; }
        
        [StringLength(50)]
        public string PhotoGuest { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdateAt { get; set; }
    }
}
