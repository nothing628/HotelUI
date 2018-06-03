
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
        static void Main(string[] args)
        {
            var routine = new CalcPinalty();
            
            using (AppMain.Main = new AppMain())
            {
                AppMain.Main.Init();

                if (args.Length == 0)
                {
#if DEBUG
                    App.Auth.AuthState.CurrentUserId = 1;
                    AppMain.Main.Run("http://localhost.com/home/get/index");     //Open after finish configure
#else
                    AppMain.Main.Run("http://localhost.com/home/get/login");     //Open after finish configure
#endif
                }
                else if (args[0] == "--calc")
                {
                    routine.DoWork();
                }
                else if (args[0] == "--setup")
                {
                    AppMain.Main.Run("http://localhost.com/setup/index");   // Open Setup Dialog
                }

                Cef.Shutdown();
            }
        }
    }
}
