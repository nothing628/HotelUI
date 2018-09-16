using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Data.Tables
{
    class Setting
    {
        [Key]
        [StringLength(20)]
        public string Key { get; set; }
        
        public string Value { get; set; }
    }
}
