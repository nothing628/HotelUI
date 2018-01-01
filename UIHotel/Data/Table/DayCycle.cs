using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Table
{
    [Table("daycycle")]
    public class DayCycle
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Column("id_effect", Order = 1)]
        [Required]
        public int IdEffect { get; set; }

        [Required]
        [Column("date_at", Order = 2, TypeName = "Date")]
        public DateTime DateAt { get; set; }
    }
}
