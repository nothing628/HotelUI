using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.Data;
using UIHotel2.Data.Tables;

namespace UIHotel2.Misc
{
    public static class DataHelper
    {
        public static decimal GetRoomPrice(long IdRoom, DateTime DateAt)
        {
            decimal price = 0;

            using (var context = new HotelContext())
            {
                try
                {
                    var room = context.Rooms.Where(r => r.Id == IdRoom).SingleOrDefault();

                    price = GetRoomPrice(room, DateAt);
                } catch { }
            }

            return price;
        }

        public static decimal GetRoomPrice(Room room, DateTime DateAt)
        {
            return GetCategoryPrice(room.RoomCategoryId, DateAt);
        }

        public static decimal GetCategoryPrice(long IdCategory, DateTime DateAt)
        {
            var prices = GetPrices(DateAt);
            var price = prices.Where(p => p.RoomCategoryId == IdCategory).SingleOrDefault();

            if (price == null)
                return 0;

            return price.Price;
        }

        public static List<RoomPrice> GetPrices(DateTime DateAt)
        {
            var result = new List<RoomPrice>();

            using (var context = new HotelContext())
            {
                try
                {
                    var calendar = (from a in context.RoomCalendars.Include(p => p.Kind)
                                    where a.DateAt == DateAt.Date
                                    select a).FirstOrDefault();

                    if (calendar != null)
                    {
                        var prices = (from a in context.RoomPrices
                                      where a.RoomPriceKindId == calendar.Kind.Id
                                      select a);

                        result.AddRange(prices.ToList());
                    }
                } catch { }
            }

            return result;
        }
    }
}
