using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    class RoomState
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }
        
        [Index(IsUnique = true)]
        [StringLength(24)]
        public string StateName { get; set; }

        [StringLength(6)]
        public string StateColor { get; set; }
    }
}
