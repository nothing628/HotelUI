using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data
{
    public class RoomPrice
    {
        public int Id { get; set; }
        public int IdRoomCategory { get; set; }
        public int IdEffect { get; set; }
        public decimal Price { get; set; }
    }
}
