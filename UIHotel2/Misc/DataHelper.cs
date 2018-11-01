using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.JsObject;
using UIHotel2.Data;
using UIHotel2.Data.Tables;
using UIHotel2.Migrations;

namespace UIHotel2.Misc
{
    public static class DataHelper
    {
        public static bool MigrateDB(bool recreateDB)
        {
            var connStr = DBObject.ConnectionString;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HotelContext, Configuration>());

            if (Database.Exists(connStr) && recreateDB)
            {
                Database.Delete(connStr);
            }

            try
            {
                using (var ctx = new HotelContext())
                {
                    ctx.Database.Initialize(true);
                }

                return true;
            } catch {
                return false;
            }
        }

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
            var price = prices.SingleOrDefault(p => p.RoomCategoryId == IdCategory);

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
