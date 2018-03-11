using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace UIHotel.Data.Table
{
    [Table("booking")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(25)]
        [Column("id", Order = 0)]
        public string Id { get; set; }

        [Required]
        [Column("id_guest", Order = 1)]
        public long IdGuest { get; set; }

        [Required]
        [Column("id_type", Order = 2)]
        public long IdType { get; set; }

        [Column("id_room", Order = 3)]
        public long IdRoom { get; set; }

        [Column("id_checkin", Order = 4)]
        [StringLength(25)]
        public string IdCheckin { get; set; }

        [Column("is_checkin", Order = 5)]
        public bool IsCheckin { get; set; }

        [Column("count_child", Order = 6)]
        public short CountChild { get; set; }

        [Column("count_adult", Order = 7)]
        public short CountAdult { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("arrive_at", Order = 8, TypeName = "Date")]
        public DateTime ArriveAt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("departure_at", Order = 9, TypeName = "Date")]
        public DateTime DepartureAt { get; set; }
        
        [Required]
        [Column("create_at", Order = 10)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 11)]
        public DateTime? UpdateAt { get; set; }
        
        Guest _guest;
        Room _room;

        [NotMapped]
        public Guest Guest
        {
            get
            {
                if (_guest == null)
                {
                    using (var model = new DataContext())
                    {
                        try
                        {
                            _guest = (from a in model.Guests
                                      where a.Id == IdGuest
                                      select a).First();
                        } catch
                        {
                            _guest = new Guest();
                        }
                    }
                }

                return _guest;
            }
        }

        [NotMapped]
        public Room Room
        {
            get
            {
                if (_room == null)
                {
                    using (var model = new DataContext())
                    {
                        try
                        {
                            _room = (from a in model.Rooms.Include(x => x.Category).Include(x => x.Status)
                                     where a.Id == IdRoom
                                     select a).Single();
                        } catch
                        {
                            _room = new Room();
                        }
                    }
                }

                return _room;
            }
        }

        [NotMapped]
        public string CheckinLink
        {
            get
            {
                return string.Format("http://localhost.com/checkin/get/index?bookid={0}", Id);
            }
        }

        [NotMapped]
        public string EditLink
        {
            get
            {
                return string.Format("http://localhost.com/checkin/get/editBooking?id={0}", Id);
            }
        }

        [NotMapped]
        public string RemoveLink
        {
            get
            {
                return string.Format("http://localhost.com/checkin/post/cancelBooking?id={0}", Id);
            }
        }

        [NotMapped]
        public int LateLevel
        {
            get
            {
                var today = DateTime.Now;
                var arrive = ArriveAt.Add(new TimeSpan(12, 0, 0)); //TODO: Get Checkin Time in setting

                if (arrive.Date <= today.Date)
                {
                    if (arrive < today)
                        return 1;
                    else
                        return 0;
                }

                return -1;
            }
        }

        public static string GenerateID()
        {
            using (var context = new DataContext())
            {
                var CurrDate = DateTime.Now.ToString("yyyyMMdd");
                var Prefix = "BOK";
                var PrefixID = Prefix + CurrDate;
                var newId = 1;

                try
                {
                    var chk = (from a in context.Bookings
                               where a.Id.StartsWith(PrefixID)
                               select a.Id).ToList();
                    var trans = chk.Select(x => x.Replace(PrefixID, "")).Select(x => Convert.ToInt32(x)).Max();

                    newId = trans + 1;
                }
                catch
                {

                }

                return PrefixID + string.Format("{0:D5}", newId);
            }
        }
    }
}
