using NetDimension.NanUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHotel2.Misc;

namespace UIHotel2
{
    static class Program
    {
        static System.Threading.Timer timer;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
            {
                var firstArgs = args[0];

                if (firstArgs == "--normalize")
                {
                    TransactionHelper.CalculateSubtotal(true);
                    return;
                }
                else if (firstArgs == "--daemon")
                {
                    timer = new System.Threading.Timer(new TimerCallback(TimerTick), null, 60*1000, 20*60*1000);
                    Application.Run();
                    return;
                }
                else if (firstArgs == "--setup")
                {
                    InitApp("http://assets.app.local/index.html#/setup/db");
                    return;
                }
            }

            InitApp();
        }

        public static void TimerTick(object state)
        {
            TransactionHelper.CalculateSubtotal();
            TransactionHelper.CalculateBooking();
        }

        public static void InitApp(string UrlToLoad = "http://assets.app.local/index.html")
        {
            if (Bootstrap.Load(options =>
            {
                options.RemoteDebuggingPort = 8010;
                options.SingleProcess = true;
            }))
            {
                Bootstrap.RegisterFolderResources("Assets");
                var form = new Form1();
                form.LoadUrl(UrlToLoad);
                Application.Run(form);
            }
        }
    }
}
