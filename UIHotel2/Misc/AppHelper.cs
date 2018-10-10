using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel2.Misc
{
    static class AppHelper
    {
        public static string GenerateRandomStr(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static TimeSpan GetTimespan(string timespanstr)
        {
            return TimeSpan.ParseExact(timespanstr, "hh\\:mm\\:ss", CultureInfo.CurrentCulture);
        }
    }

    public class EventInputObject
    {
#pragma warning disable IDE1006 // Naming Styles
        public string title { get; set; }
        public string color { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public bool allDay => true;
#pragma warning restore IDE1006 // Naming Styles
    }
}
