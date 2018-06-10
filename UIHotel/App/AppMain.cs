using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using UIHotel.App.Provider;
using UIHotel.ViewModel;
using System.Threading;
using UIHotel.App.Routine;

namespace UIHotel.App
{
    public class AppMain : IDisposable
    {
        private ChromiumWebBrowser browser;
        private Form mainForm;
        private AppRequestHandler RequestHandler;
        private const string Domain = "localhost.com";
        private System.Threading.Timer timer;
        
        private List<ServiceProvider> listServiceProvider = new List<ServiceProvider>();
        public static AppMain Main { get; set; }

        public void RegisterProvider()
        {
            foreach (ServiceProvider provider in listServiceProvider)
            {
                provider.RegisterContainer(this);
                provider.Register();
            }
        }

        public void BootProvider()
        {
            foreach (ServiceProvider provider in listServiceProvider)
                provider.Boot();
        }
        
        public string BaseDir { get => AppDomain.CurrentDomain.BaseDirectory; }
        public string ViewPath { get => Path.Combine(BaseDir, "View"); }
        public string AssetsPath { get => Path.Combine(BaseDir, "Assets"); }
        public string UploadPath { get => Path.Combine(BaseDir, "Upload"); }

        public ServiceProvider this[string provide]
        {
            get
            {
                foreach (ServiceProvider provider in listServiceProvider)
                    if (provider.Provide() == provide)
                        return provider;

                return null;
            }
        }

        public ChromiumWebBrowser Browser
        {
            get
            {
                return browser;
            }
        }

        public AppMain()
        {
            var callback = new TimerCallback(TimerRoutine);
            timer = new System.Threading.Timer(callback, null, 0, 10 * 60 * 10000);     //Every 10 Minutes
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private void TimerRoutine(object state)
        {
            var routine = new CalcPinalty();
            routine.DoWork();
        }

        public void Init()
        {
            RequestHandler = new AppRequestHandler(Domain);

            ConfigureSetting();
            ConfigureRoute();

            listServiceProvider.Add(new RouterProvider());
            listServiceProvider.Add(new ViewProvider());

            RegisterProvider();
            BootProvider();

            browser = new ChromiumWebBrowser("about:blank");
            browser.RegisterAsyncJsObject("CS", new BaseModel());
            browser.Dock = DockStyle.Fill;
            browser.FrameLoadStart += Browser_FrameLoadStart;
            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            browser.TitleChanged += Browser_TitleChanged;
            browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;

            mainForm = new Form();
            mainForm.WindowState = FormWindowState.Maximized;
            mainForm.Controls.Add(browser);
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Action changeTitle = () =>
            {
                mainForm.Text = "Hotel Management - " + e.Title;
            };

            mainForm.Invoke(changeTitle);
        }

        private void ConfigureSetting()
        {
            var settings = new CefSettings()
            {
                // Settings cef
                IgnoreCertificateErrors = true,
            };
#if DEBUG
            settings.RemoteDebuggingPort = 54477;
#endif
            settings.RegisterScheme(RequestHandler.Scheme);

            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Cef.Initialize(settings);
        }

        private void ConfigureRoute()
        {
            RequestHandler.RegisterPath(Path.Combine(BaseDir, @"vue\dist"), "");
            RequestHandler.RegisterPath(AssetsPath, "");
            RequestHandler.RegisterPath(ViewPath, "");
            RequestHandler.RegisterPath(UploadPath, "Upload/");
        }

        private void Browser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
#if DEBUG
            if (e.IsBrowserInitialized)
                browser.ShowDevTools();
#endif
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void Run(string startUpUrl)
        {
            browser.Load(startUpUrl);

            Run();
        }

        public void Run()
        {
            Application.Run(mainForm);
            Cef.Shutdown();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~App() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
