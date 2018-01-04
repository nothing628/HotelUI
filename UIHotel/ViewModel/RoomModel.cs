using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data.Table;

namespace UIHotel.ViewModel
{
    public class RoomModel
    {
        public long Id { get; set; }
        public string RoomNumber { get; set; }
        public short RoomFloor { get; set; }
        public string RoomCategory { get; set; }
        public RoomLink[] RoomLinks {
            get {
                var ls = new List<RoomLink>();

                ls.Add(new RoomLink()
                {
                    Icon = "zmdi zmdi-apps",
                    Href = string.Format("http://localhost.com/room/get/detail?roomId={0}", Id),
                    Name = "Detail",
                });

                if (Status == "Vacant" || Status == "Booked")
                {
                    ls.Add(new RoomLink()
                    {
                        Icon = "zmdi zmdi-download",
                        Href =string.Format("http://localhost.com/home/get/checkin?roomId={0}", Id),
                        Name = "Checkin",
                    });
                }

                if (Status == "Late Checkout" || Status == "Occupied")
                {
                    ls.Add(new RoomLink()
                    {
                        Icon = "zmdi zmdi-upload",
                        Href = string.Format("http://localhost.com/home/get/checkout?roomId={0}", Id),
                        Name = "Checkout",
                    });
                }

                if (Status == "Vacant")
                {
                    ls.Add(new RoomLink()
                    {
                        Icon = "zmdi zmdi-bookmark",
                        Href = string.Format("http://localhost.com/home/get/booking?roomId={0}", Id),
                        Name = "Booking",
                    });
                }
                
                if (Status == "Occupied")
                {
                    ls.Add(new RoomLink()
                    {
                        Icon = "zmdi zmdi-refresh-sync",
                        Href = string.Format("http://localhost.com/home/get/change?roomId={0}", Id),
                        Name = "Change Room",
                    });
                }

                return ls.ToArray();
            }
        }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
    }

    public class RoomLink
    {
        public string Icon { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
    }

    public class RoomPriceModel
    {
        public int IdEffect { get; set; }
        public long IdCategory { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }

    public class RoomContainer
    {
        public string Category { get; set; }
        public List<RoomModel> Rooms { get; set; }
        [JsonIgnore]
        public List<RoomStatus> Status { get; set; }
        public Dictionary<string, int> StatusCount
        {
            get
            {
                var list = new Dictionary<string, int>();

                foreach (var status in Status)
                {
                    var room = (from a in Rooms
                                where a.StatusID == status.Id
                                select a).Count();

                    list.Add(status.Status, room);
                }

                return list; 
            }
        }
    }
}
