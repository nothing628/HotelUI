using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using UIHotel.App;

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
            using (var startup = new AppMain())
            {
                startup.IsShowDevTool = true;
                startup.Run("http://localhost.com/checkin/checkout.cshtml");     //Open after finish configure
            }
        }
    }
}
