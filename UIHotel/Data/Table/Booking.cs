using System;
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

        [Column("count_child", Order = 3)]
        public short CountChild { get; set; }

        [Column("count_adult", Order = 4)]
        public short CountAdult { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("arrive_at", Order = 5, TypeName = "Date")]
        public DateTime ArriveAt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("departure_at", Order = 6, TypeName = "Date")]
        public DateTime DepartureAt { get; set; }

        [Required]
        [Column("create_at", Order = 7)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 8)]
        public DateTime? UpdateAt { get; set; }

        List<BookingDetail> _details;
        Guest _guest;

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
        public ICollection<BookingDetail> Details
        {
            get
            {
                if (_details == null)
                {
                    using (var model = new DataContext())
                    {
                        try
                        {
                            _details = (from a in model.BookingDetails
                                        where a.IdBooking == Id
                                        select a).ToList();
                        } catch (Exception ex)
                        {
                            _details = new List<BookingDetail>();
                        }
                    }
                }

                return _details;
            }
        }

        [NotMapped]
        public ICollection<Room> Rooms
        {
            get
            {
                var details = Details;
                var rooms = Details.Select(x => x.Room).ToList();

                return rooms;
            }
        }

        [NotMapped]
        public string CheckinLink
        {
            get
            {
                return string.Format("http://localhost.com/chekin/get/index?bookid={0}", Id);
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
