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
using UIHotel.App.Attributes;
using UIHotel.App.Auth;
using UIHotel.App.Controller;

namespace UIHotel.App.Router
{
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
        private IResourceHandler result;

        public IResourceHandler GetResponse(IRequest request)
        {
            var Url = new Uri(request.Url);
            var Path = Url.AbsolutePath;
            var Controller = GetController(Path);
            var Action = GetAction(Path);
            var ClassName = Namespace + "." + Controller;

            if (IsMethodExists(ClassName, Action))
            {
                using (var worker = new BackgroundWorker())
                {
                    Type type = Type.GetType(ClassName);

                    resetEvent = new AutoResetEvent(false);

                    worker.DoWork += Worker_DoWork;
                    worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                    worker.RunWorkerAsync(new object[] { type, Action, request });
                    
                    resetEvent.WaitOne(-1);

                    worker.DoWork -= Worker_DoWork;
                    worker.RunWorkerCompleted -= Worker_RunWorkerCompleted;
                }

                return result;
            }

            return new ResourceHandler()
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                StatusText = "Not Found",
            };
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            result = e.Result as IResourceHandler;
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

        private bool IsAuthorize(Attribute[] attrs)
        {
            foreach (var attr in attrs)
            {
                if (attr is IAuthAttribute)
                {
                    var att = attr as IAuthAttribute;
                    // Check valid user
                    if (att.IsValidUser())
                        return true;
                }
            }
            
            return false;
        }

        private IResourceHandler CreateInstance(Type tipe, string Method, IRequest request)
        {
            ConstructorInfo constInfo = tipe.GetConstructor(new[] { typeof(IRequest) });
            MethodInfo method = tipe.GetMethod(Method);
            Object instance = constInfo.Invoke(new object[] { request });

            var controlAttr = Attribute.GetCustomAttributes(tipe);
            var methodAttr = Attribute.GetCustomAttributes(method);

            if (controlAttr.Length > 0 || methodAttr.Length > 0)
            {
                if (methodAttr.Length > 0)
                {
                    if (!IsAuthorize(methodAttr))
                    {
                        if (AuthState.IsLogin)
                            return new BaseController().Redirect("http://localhost.com/home/get/index");
                        else
                            return new BaseController().Redirect("http://localhost.com/home/get/login");
                    }
                        
                } else
                {
                    if (!IsAuthorize(controlAttr))
                    {
                        if (AuthState.IsLogin)
                            return new BaseController().Redirect("http://localhost.com/home/get/index");
                        else
                            return new BaseController().Redirect("http://localhost.com/home/get/login");
                    }
                }
            }

            var result = method.Invoke(instance, BindingFlags.Default, null, new object[] { }, null);

            constInfo = null;
            method = null;
            instance = null;
            GC.Collect();

            if (result != null)
                return (IResourceHandler)result;
            else
                return ErrorResponse(HttpStatusCode.NotFound, "Method '" + Method + "' Not Found");
        }

        private ResourceHandler ErrorResponse(HttpStatusCode status, string message)
        {
            return new ResourceHandler()
            {
                StatusCode = (int)status,
                StatusText = message
            };
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
