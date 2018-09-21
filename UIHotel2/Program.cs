using NetDimension.NanUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIHotel2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Bootstrap.Load(options => options.RemoteDebuggingPort = 8010))
            {
                Bootstrap.RegisterFolderResources("Assets");

                Application.Run(new Form1());
            }
        }

        static void TestDatabase()
        {
            Properties.Settings.Default.Reset();
            using (var context = new Data.HotelContext())
            {
                var room = context.Rooms.Include("State").First();

            }
        }
    }
}
