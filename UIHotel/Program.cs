using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;

namespace UIHotel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var startup = new App())
            {
                startup.IsShowDevTool = true;
                startup.Run("http://localhost.com/checkin/booking.cshtml");     //Open after finish configure
            }
        }
    }
}
