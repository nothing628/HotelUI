using NetDimension.NanUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHotel2.Misc;

namespace UIHotel2
{
    static class Program
    {
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

                if (firstArgs == "--calc-all-transaction")
                {
                    TransactionHelper.CalculateSubtotal(true);
                    return;
                }
                else if (firstArgs == "--calc-transaction")
                {
                    TransactionHelper.CalculateSubtotal();
                    TransactionHelper.ClosingTransaction();
                    return;
                }
                else if (firstArgs == "--calc-booking")
                {
                    TransactionHelper.CalculateBooking();
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
