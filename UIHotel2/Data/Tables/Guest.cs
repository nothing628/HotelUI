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
        [Column("id_number", Order = 0)]
        [StringLength(30)]
        public string Id { get; set; }

        [Key]
        [Column("id_kind", Order = 1)]
        [StringLength(10)]
        public string IdKind { get; set; }

        [StringLength(60)]
        public string Name { get; set; }
    }
}
