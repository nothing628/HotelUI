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
        public List<ResourceModel> mapRoute = new List<ResourceModel>();
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
            mapRoute.Add(new ResourceModel()
            {
                RoutePath = urlPath,
                BasePath = pathLocation
            });
        }

        private string CombineUrl(params string[] param)
        {
            string cleanUrl = string.Join("/", param).Replace("//", "/");

            return cleanUrl[0] == '/' ? cleanUrl.Substring(1, cleanUrl.Length - 1) : cleanUrl; 
        }

        private ResourceModel SearchRouteModel(string pathUri)
        {
            return (from a in mapRoute
                    where a.IsMatch(pathUri)
                    where a.IsExists(pathUri)
                    select a).FirstOrDefault();
        }

        private string GetMimeType(string filename)
        {
            return MimeMapping.GetMimeMapping(filename);
        }

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            var res = new ResourceHandler();
            var uri = new Uri(request.Url);
            var path = CombineUrl(uri.AbsolutePath);
            var model = SearchRouteModel(path);

            if (_RouterProvider.HasMatch(request))
                return _RouterProvider.GetResponse(request);

            if (model == null)
            {
                res.StatusText = "404 File Not Found";
                res.StatusCode = (int)HttpStatusCode.NotFound;

                return res;
            } else
            {
                var stream = model.GetFilestream(path);
                var mimeType = GetMimeType(model.ResolveFile(path));

                res.StatusCode = (int)HttpStatusCode.OK;
                res.Stream = stream;
                res.ResponseLength = stream.Length;
                res.MimeType = mimeType;

                return res;
            }
        }
    }

    public class ResourceModel
    {
        private string _RoutePath;

        public string BasePath { get; set; }
        public string RoutePath
        {
            get => _RoutePath;
            set
            {
                var ret = value;

                if (value.EndsWith("/"))
                    ret = ret.Substring(0, ret.Length - 1);

                _RoutePath = ret;
            }
        }
        private bool IsRoot { get => RoutePath == ""; }

        public string ResolveFile(string url)
        {
            if (IsMatch(url))
            {
                var cleanPath = url;

                if (!IsRoot)
                    cleanPath = cleanPath.Replace(RoutePath, "").TrimStart('/');

                var baseName = cleanPath.Replace('/', Path.DirectorySeparatorChar);
                return Path.Combine(BasePath, baseName);
            }

            return "";
        }

        public FileStream GetFilestream(string Url)
        {
            var filename = ResolveFile(Url);

            if (filename != "" && File.Exists(filename))
                return new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            return null;
        }

        public bool IsMatch(string url)
        {
            if (IsRoot)
                return true;

            return url.StartsWith(RoutePath);
        }

        public bool IsExists(string url)
        {
            var file = ResolveFile(url);

            return File.Exists(file);
        }
    }
}
