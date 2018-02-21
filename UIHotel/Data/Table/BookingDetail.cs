using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace UIHotel.Data.Table
{
    [Table("booking_detail")]
    public class BookingDetail
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Required]
        [Column("id_booking", Order = 1)]
        public string IdBooking { get; set; }

        [Column("id_room", Order = 2)]
        public long IdRoom { get; set; }
        
        [Column("id_checkin", Order = 3)]
        [StringLength(25)]
        public string IdCheckin { get; set; }

        [Column("is_checkin", Order = 4)]
        public bool IsCheckedIn { get; set; }

        Room _Room;

        [NotMapped]
        public Room Room
        {
            get
            {
                if (_Room == null)
                {
                    using (var model = new DataContext())
                    {
                        try
                        {
                            _Room = (from a in model.Rooms.Include(x => x.Status).Include(x => x.Category)
                                     where a.Id == IdRoom
                                     select a).First();
                        } catch
                        {
                            _Room = new Room();
                        }
                    }
                }

                return _Room;
            }
        }
    }
}
