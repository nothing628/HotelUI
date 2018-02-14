using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data.Table;

namespace UIHotel.Data.Seeds
{
    public class RoomPriceSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            var currDate = DateTime.Today;
            var currYear = currDate.Year;
            

            var WeekDay = new DayEffect()
            {
                CanDelete = false,
                Effect = "WEEKDAY",
                EffectColor = "green darken-1",
            };

            var WeekEnd = new DayEffect()
            {
                CanDelete = false,
                Effect = "WEEKEND",
                EffectColor = "red darken-2",
            };

            var Holiday = new DayEffect()
            {
                CanDelete = false,
                Effect = "HOLIDAY",
                EffectColor = "teal darken-4",
            };

            context.DayEffect.Add(WeekDay);
            context.DayEffect.Add(WeekEnd);
            context.DayEffect.Add(Holiday);
            context.SaveChanges();

            for (var i = new DateTime(currYear, 1, 1); i < new DateTime(currYear + 1, 1, 1);)
            {
                var cycle = new DayCycle()
                {
                    DateAt = i,
                    IdEffect = (i.DayOfWeek == DayOfWeek.Sunday) ? WeekEnd.Id : WeekDay.Id
                };

                context.DayCycles.Add(cycle);

                i = i.AddDays(1.0d);
            }
            context.SaveChanges();

            var categories = (from a in context.RoomCategory select a).ToList();
            var effects = (from a in context.DayEffect select a).ToList();

            foreach (var effect in effects)
            {
                foreach (var category in categories)
                {
                    var price = new RoomPrice()
                    {
                        IdCategory = category.Id,
                        IdEffect = effect.Id,
                        Price = (new Random().Next(100, 200)) * 1000
                    };

                    context.RoomPrice.Add(price);
                }
            }
            context.SaveChanges();
        }
    }
}
