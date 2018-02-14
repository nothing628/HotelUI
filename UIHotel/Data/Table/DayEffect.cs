using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Table
{
    [Table("dayeffect")]
    public class DayEffect
    {
        [Key]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("effect", Order = 1)]
        [StringLength(50)]
        public string Effect { get; set; }
        
        [Required]
        [Column("effect_color", Order = 2)]
        [StringLength(25)]
        public string EffectColor { get; set; }

        [Column("can_delete", Order = 3)]
        public bool CanDelete { get; set; } = true;
    }
}
