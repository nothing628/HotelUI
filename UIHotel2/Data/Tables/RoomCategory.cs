using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    class RoomCategory
    {
        [Key]
        public long Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(40)]
        public string CategoryName { get; set; }
    }
}
