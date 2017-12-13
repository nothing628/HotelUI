using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data
{
    [Table("room")]
    public class Room
    {
        public int id { get; set; }
        public string room_number { get; set; }
    }
}
