using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UIHotel.App.Provider;

namespace UIHotel.App
{
    public class AppRequestHandler : ISchemeHandlerFactory
    {
        private string DomainName;
        private RouterProvider _RouterProvider;
        private CefCustomScheme _Scheme;
        public List<RouteModel> mapRoute = new List<RouteModel>();
        private string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

        public CefCustomScheme Scheme
        {
            get => _Scheme;
        }

        public AppRequestHandler(string DomainName)
        {
            this.DomainName = DomainName;
            this._RouterProvider = new RouterProvider();
            this._Scheme = new CefCustomScheme()
            {
                DomainName = DomainName,
                IsCorsEnabled = false,
                IsSecure = false,
                SchemeName = "http",
                SchemeHandlerFactory = this
            };

            this._RouterProvider.Register();
        }

        public void RegisterPath(string pathLocation, string urlPath)
        {
            try
            {
                var dirInfo = new DirectoryInfo(pathLocation);
                var lstDir = dirInfo.GetDirectories();
                var lstFile = dirInfo.GetFiles();

                ScanDirectories(lstDir, urlPath);
                ScanFiles(lstFile, urlPath);
            } catch
            {
                throw;
            }
        }

        public void RegisterRoute(string url, FileInfo file)
        {
            if (file.Exists)
            {
                mapRoute.Add(new RouteModel()
                {
                    RoutePath = url,
                    Filename = file.FullName,
                });
            }
        }

        private void ScanDirectories(DirectoryInfo[] listDir, string urlPath)
        {
            foreach (var info in listDir)
            {
                var lstDir = info.GetDirectories();
                var lstFile = info.GetFiles();

                ScanDirectories(lstDir, CombineUrl(urlPath, info.Name));
                ScanFiles(lstFile, CombineUrl(urlPath, info.Name));
            }
        }

        private void ScanFiles(FileInfo[] listFile, string urlPath)
        {
            foreach (var info in listFile)
            {
                if (info.Exists)
                    RegisterRoute(CombineUrl(urlPath, info.Name), info);
            }
        }

        private string ConvertToPathUri(string localPath)
        {
            var cleanPath = localPath.Replace(BaseDir, "");

            return cleanPath.Replace(Path.DirectorySeparatorChar, '/');
        }

        private string ConvertToPathLocal(string uriPath)
        {
            var convertSlash = uriPath.Replace('/', Path.DirectorySeparatorChar);

            if (convertSlash.StartsWith(Path.DirectorySeparatorChar.ToString()))
                convertSlash = convertSlash.Substring(1);
            
            return Path.Combine(BaseDir, convertSlash);
        }

        private string CombineUrl(params string[] param)
        {
            string cleanUrl = string.Join("/", param).Replace("//", "/");

            return cleanUrl[0] == '/' ? cleanUrl.Substring(1, cleanUrl.Length - 1) : cleanUrl; 
        }

        private RouteModel SearchRouteModel(string pathUri)
        {
            var result = (from a in mapRoute
                          where pathUri.Equals(a.RoutePath, StringComparison.OrdinalIgnoreCase)
                          select a).FirstOrDefault();

            return result;
        }

        private string GetMimeType(string filename)
        {
            return MimeMapping.GetMimeMapping(filename);
        }

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            var res = new ResourceHandler();
            var uri = new Uri(request.Url);
            var model = SearchRouteModel(CombineUrl(uri.AbsolutePath));

            if (_RouterProvider.HasMatch(request))
                return _RouterProvider.GetResponse(request);

            if (model == null)
            {
                res.StatusText = "404 File Not Found";
                res.StatusCode = (int)HttpStatusCode.NotFound;

                return res;
            } else
            {
                var stream = model.GetFilestream();
                var mimeType = GetMimeType(model.Filename);

                res.StatusCode = (int)HttpStatusCode.OK;
                res.Stream = stream;
                res.ResponseLength = stream.Length;
                res.MimeType = mimeType;

                return res;
            }
        }
    }

    public class RouteModel
    {
        public string Filename { get; set; }
        public string RoutePath { get; set; }

        public FileStream GetFilestream()
        {
            var info = new FileInfo(Filename);

            if (info.Exists)
            {
                return new FileStream(info.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }

            return null;
        }
    }
}
