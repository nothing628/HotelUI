
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
using UIHotel.Data.Seeds;
using UIHotel.App.Routine;

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
#if SEED
            var migrator = new Migrator();
            migrator.RunDown();
            migrator.Run();
            DBSeeder.Seed();
#else
#if DEBUG
            App.Auth.AuthState.CurrentUserId = 1;
#endif
            var routine = new CalcPrice();
            routine.DoWork();

            using (AppMain.Main = new AppMain())
            {
                AppMain.Main.Init();
                AppMain.Main.IsShowDevTool = true;
                AppMain.Main.Run("http://localhost.com/home/get/login");     //Open after finish configure
            }
#endif
        }
    }
}
