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
            var settings = new CefSettings()
            {
                RemoteDebuggingPort = 55477,
                IgnoreCertificateErrors = true,
            };

            settings.RegisterScheme(new CefCustomScheme() {
                DomainName = "localhost.com",
                IsCorsEnabled = false,
                IsSecure = false,
                SchemeName = "http",
                SchemeHandlerFactory = new AppRequestHandler()
            });

            Cef.Initialize(settings);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Cef.Shutdown();
        }
    }
}
