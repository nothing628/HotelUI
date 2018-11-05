using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public static void CreateStartup()
        {
            //Generate task schedule;
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
            var args = " --daemon";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("UIHotel2", path + args, Microsoft.Win32.RegistryValueKind.String);
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
