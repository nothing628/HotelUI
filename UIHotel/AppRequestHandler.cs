using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UIHotel
{
    public class AppRequestHandler : ISchemeHandlerFactory
    {
        public AppRequestHandler()
        {
        }

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            var res = new ResourceHandler();
            var url = new Uri(request.Url);
            var absUrl = url.AbsolutePath;

            res.StatusCode = (int)HttpStatusCode.OK;
            res.StatusText = "OK";
            res.ResponseLength = 4;
            res.AutoDisposeStream = true;
            res.Stream = new FileStream("test", FileMode.Open);
            res.MimeType = MimeMapping.GetMimeMapping(absUrl);
            
            return res;
        }
    }
}
