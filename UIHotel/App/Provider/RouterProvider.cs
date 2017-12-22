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

namespace UIHotel.App.Provider
{
    public class RouterProvider : ServiceProvider
    {
        private List<RouteModel> listRoute { get; set; }
        private string controllerNamespace { get; set; } = "UIHotel.App.Controller";

        public override void Register()
        {
            listRoute = new List<RouteModel>();
            listRoute.Add(new RouteModel("home/get/{action}", "HomeController"));
            listRoute.Add(new RouteModel("home/post/{action}", "HomeController", Method: "POST"));
            listRoute.Add(new RouteModel("room/get/{action}", "RoomController"));
            listRoute.Add(new RouteModel("room/post/{action}", "RoomController", Method: "POST"));
            listRoute.Add(new RouteModel("setting/get/{action}", "SettingController"));
            listRoute.Add(new RouteModel("setting/post/{action}", "SettingController", Method: "POST"));
        }

        public override void Boot()
        {
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

        public override string Provide()
        {
            return "route";
        }
    }

    public class RouteModel
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public string Namespace { get; set; } = "UIHotel.App.Controller";
        public string Controller { get; set; }
        public string Action { get; set; }
        public string[] Params { get; set; }
        
        public RouteModel(string Path, string Controller, string Namespace = "UIHotel.App.Controller", string Action = "index", string Method = "GET")
        {
            this.Path = Path;
            this.Controller = Controller;
            this.Action = Action;
            this.Method = Method;
            this.Namespace = Namespace;
        }

        public bool IsMatch(IRequest request)
        {
            var Url = new Uri(request.Url);
            var Path = Url.AbsolutePath;
            var pattern = Regex.Replace(this.Path, @"{\w+}", @"([^\/\n]+)");

            return Regex.IsMatch(Path, pattern, RegexOptions.IgnoreCase) && request.Method == Method;
        }

        public string GetController(string Path)
        {
            var isControllerContextExists = Regex.IsMatch(this.Path, @"{controller}", RegexOptions.IgnoreCase);

            if (isControllerContextExists)
            {
                var pattern = Regex.Replace(this.Path, @"{controller}", @"([^\/\n]+)");
                var matchColl = Regex.Matches(Path, pattern, RegexOptions.IgnoreCase);
                var match = matchColl[0];

                if (match.Groups.Count == 2)
                    return match.Groups[1].Value;
            }
            
            return Controller;
        }

        public string GetAction(string Path)
        {
            var isActionContextExists = Regex.IsMatch(this.Path, @"{action}", RegexOptions.IgnoreCase);

            if (isActionContextExists)
            {
                var pattern = Regex.Replace(this.Path, @"{action}", @"([^\/\n]+)");
                var matchColl = Regex.Matches(Path, pattern, RegexOptions.IgnoreCase);
                var match = matchColl[0];

                if (match.Groups.Count == 2)
                    return match.Groups[1].Value;
            }

            return Action;
        }

        private AutoResetEvent resetEvent = new AutoResetEvent(false);
        private ResourceHandler result;

        public ResourceHandler GetResponse(IRequest request)
        {
            var Url = new Uri(request.Url);
            var Path = Url.AbsolutePath;
            var Controller = GetController(Path);
            var Action = GetAction(Path);
            var ClassName = Namespace + "." + Controller;
            
            if (IsMethodExists(ClassName, Action))
            {
                Type type = Type.GetType(ClassName);

                var worker = new BackgroundWorker();
                resetEvent = new AutoResetEvent(false);
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync(new object[] { type, Action, request });
                resetEvent.WaitOne();

                return result;
            }
            
            return new ResourceHandler() {
                StatusCode = (int)HttpStatusCode.NotFound,
                StatusText = "Not Found",
            };
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            result = e.Result as ResourceHandler;
            resetEvent.Set();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var argument = e.Argument as object[];
            var type = argument[0] as Type;
            var method = argument[1] as string;
            var request = argument[2] as IRequest;

            e.Result = CreateInstance(type, method, request);
        }

        private ResourceHandler CreateInstance(Type Type, string Method, IRequest request)
        {
            ConstructorInfo constInfo = Type.GetConstructor(new[] { typeof(IRequest) });
            MethodInfo method = Type.GetMethod(Method);
            Object instance = constInfo.Invoke(new object[] { request });
            
            var result = method.Invoke(instance, BindingFlags.Default, null, new object[] { }, null);

            constInfo = null;
            method = null;
            instance = null;
            GC.Collect();

            if (result != null)
                return (ResourceHandler)result;
            else
                return new ResourceHandler() { StatusCode = (int)HttpStatusCode.NotFound, StatusText = "Method '" + Method + "' Not Found" };
        }

        private bool IsClassExists(string ClassName)
        {
            Type type = Type.GetType(ClassName);

            if (type != null)
                return type.IsClass;

            return false;
        }

        private bool IsMethodExists(string ClassName, string Method)
        {
            if (IsClassExists(ClassName))
            {
                Type type = Type.GetType(ClassName);

                foreach (MethodInfo method in type.GetMethods())
                    if (method.Name.Equals(Method, StringComparison.OrdinalIgnoreCase) && method.ReturnType != typeof(void))
                        return true;
            }

            return false;
        }
    }
}
