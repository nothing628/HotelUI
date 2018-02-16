using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UIHotel.App.Controller;
using UIHotel.App.Router;

namespace UIHotel.App.Provider
{
    public class RouterProvider : ServiceProvider
    {
        private List<RouteModel> listRoute { get; set; } = new List<RouteModel>();

        public override void Register()
        {
            RouterControl.GetRoute(listRoute);
        }

        public override void Boot()
        {
        }

        public IResourceHandler GetResponse(IRequest request)
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

        public override string Provide()
        {
            return "route";
        }
    }
}
