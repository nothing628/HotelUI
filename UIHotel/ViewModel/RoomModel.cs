using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data;
using UIHotel.Data.Table;

namespace UIHotel.ViewModel
{
    public class RoomModel
    {
        [JsonIgnore]
        public Room DataRoom { get; set; }
        [JsonIgnore]
        public RoomStatus DataStatus { get; set; }
        [JsonIgnore]
        public Checkin Checkin {
            get
            {
                if (!is_checkin)
                {
                    using (var model = new DataContext())
                    {
                        try
                        {
                            _checkin = (from a in model.CheckIn
                                        where a.CheckoutAt == null
                                        where a.IdRoom == Id
                                        select a).SingleOrDefault();
                        }
                        catch
                        {
                            _checkin = null;
                        }
                    }

                    is_checkin = true;
                }

                return _checkin;
            }
        }
        private Checkin _checkin;
        private bool is_checkin;

        public long Id { get => DataRoom.Id; }
        public string RoomNumber { get => DataRoom.RoomNumber; }
        public short RoomFloor { get => DataRoom.RoomFloor; }
        public string RoomCategory { get; set; }
        public RoomLink[] RoomLinks {
            get {
                var ls = new List<RoomLink>();

                ls.Add(new RoomLink()
                {
                    Icon = "zmdi zmdi-apps",
                    Href = string.Format("http://localhost.com/room/get/detail?roomId={0}", Id),
                    Name = "Detail",
                    Color = "757575",
                });

                if (Status == "Vacant" || Status == "Booked")
                {
                    ls.Add(new RoomLink()
                    {
                        Icon = "zmdi zmdi-download",
                        Href =string.Format("http://localhost.com/checkin/get/index?roomId={0}", Id),
                        Name = "Checkin",
                        Color = "66BB6A",
                    });
                }

                if (Status == "Late Checkout" || Status == "Occupied")
                {
                    ls.Add(new RoomLink()
                    {
                        Icon = "zmdi zmdi-upload",
                        Href = string.Format("http://localhost.com/checkin/get/checkout?roomId={0}", Id),
                        Name = "Checkout",
                        Color = "D32F2F",
                    });
                }

                if (Status == "Vacant")
                {
                    ls.Add(new RoomLink()
                    {
                        Icon = "zmdi zmdi-bookmark",
                        Href = string.Format("http://localhost.com/checkin/get/booking?roomId={0}", Id),
                        Name = "Booking",
                        Color = "039BE5",
                    });
                }

                if (Status == "Occupied" && Checkin != null && Checkin.CheckinAt.AddMinutes(30) > DateTime.Now)
                {
                    ls.Add(new RoomLink()
                    {
                        Icon = "zmdi zmdi-refresh-sync",
                        Href = string.Format("http://localhost.com/room/get/change?roomId={0}", Id),
                        Name = "Change Room",
                        Color = "F9A825",
                    });
                }

                if (Status == "Cleaning")
                {
                    ls.Add(new RoomLink() {
                        Icon = "zmdi zmdi-flag",
                        Href = string.Format("http://localhost.com/room/get/finishClean?roomId={0}", Id),
                        Name = "Finish Cleaning",
                        Color = "AB47BC",
                    });
                }

                return ls.ToArray();
            }
        }
        public int StatusID { get => DataStatus.Id; }
        public string Status { get => DataStatus.Status; }
        public string StatusColor { get => DataStatus.StatusColor; }
    }

    public class RoomLink
    {
        public string Icon { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
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
