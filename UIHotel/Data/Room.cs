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
        public int Id { get; set; }
        public string IdCategory { get; set; }
        public int RoomFloor { get; set; }
        public string RoomNumber { get; set; }
        public string RoomCard { get; set; }
        public int Status { get; set; }
    }
}
