using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Table
{
    [Table("booking")]
    public class Booking
    {
        public string Id { get; set; }
        public long IdGuest { get; set; }
        public long IdType { get; set; }
        public short CountChild { get; set; }
        public short CountAdult { get; set; }
        public DateTime ArriveAt { get; set; }
        public DateTime DepartureAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
