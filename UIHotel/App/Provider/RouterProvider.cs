using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UIHotel.App.Provider
{
    public class RouterProvider : ServiceProvider
    {
        private List<RouteModel> listRoute { get; set; }

        public override void Register()
        {
            base.Register();

            listRoute = new List<RouteModel>();
            listRoute.Add(new RouteModel("home/get/{action}", "HomeController"));
            listRoute.Add(new RouteModel("home/post/{action}", "HomeController", Method: "POST"));
        }

        public override void Boot()
        {
            base.Boot();
            //
        }

        public ResourceHandler GetResponse(IRequest request)
        {
            foreach (RouteModel model in listRoute)
            {
                if (model.IsMatch(request))
                    return model.GetResponse(request);
            }

            return new ResourceHandler()
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                StatusText = "Not Found"
            };
        }

        public bool HasMatch(IRequest request)
        {
            foreach (RouteModel model in listRoute)
                if (model.IsMatch(request))
                    return true;

            return false;
        }
    }

    public class RouteModel
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string[] Params { get; set; }
        
        public RouteModel(string Path, string Controller, string Action = "index", string Method = "GET")
        {
            this.Path = Path;
            this.Controller = Controller;
            this.Action = Action;
            this.Method = Method;
        }

        public bool IsMatch(IRequest request)
        {
            var Url = new Uri(request.Url);
            var Path = Url.AbsolutePath;
            var pattern = Regex.Replace(this.Path, @"{\w+}", @"([^\/\n]+)");

            return Regex.IsMatch(Path, pattern, RegexOptions.IgnoreCase) && request.Method == Method;
        }

        public ResourceHandler GetResponse(IRequest request)
        {
            return ResourceHandler.FromString("A", Encoding.UTF8);
        }
    }
}
