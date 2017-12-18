using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using UIHotel.App;
using MySql.Data.MySqlClient;
using UIHotel.Data;
using UIHotel.Data.Migrations;

namespace UIHotel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            var migrator = new Migrator();
            migrator.Run();

            /*
            using (AppMain.Main = new AppMain())
            {
                AppMain.Main.Init();
                AppMain.Main.IsShowDevTool = true;
                AppMain.Main.Run("http://localhost.com/home/get/test?foo=2&data=test%20data");     //Open after finish configure
            }
            */
        }
    }
}
