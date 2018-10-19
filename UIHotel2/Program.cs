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
            }

            if (Bootstrap.Load(options => options.RemoteDebuggingPort = 8010))
            {
                Bootstrap.RegisterFolderResources("Assets");

                Application.Run(new Form1());
            }
        }
    }
}
