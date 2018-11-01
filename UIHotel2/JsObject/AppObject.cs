using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chromium;
using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using NetDimension.NanUI;
using UIHotel2.Data;
using static UIHotel2.Form1;

namespace UIHotel2.JsObject
{
    public class AppObject : BaseObject
    {
        public string retStr;
        public string originalName;

        public override string ObjectName => "App";
        public const string baseUrl = "http://assets.app.local/upload/";
        private Form1 Form1;

        public AppObject(Form1 form): base()
        {
            this.Form1 = form;
        }

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("OpenDialog").Execute += OpenDialogExecute;
            Self.AddFunction("SaveDialog").Execute += SaveDialogExecute;
            Self.AddFunction("GetUploadUrl").Execute += GetUploadUrl;
            Self.AddFunction("GetNewBookingNumber").Execute += GetBookingNumberExecute;
            Self.AddFunction("Test").Execute += TestExecute;
            //add a function with async callback
            var asyncCallbackTestFunc = Self.AddFunction("asyncCallbackTest");
            asyncCallbackTestFunc.Execute += async (func, args) =>
            {
                //save current context
                var v8Context = CfrV8Context.GetCurrentContext();
                var callback = Array.Find(args.Arguments, p => p.IsFunction);

                //simulate async methods.
                await Task.Delay(5000).ConfigureAwait(false);

                if (callback != null)
                {
                    //get render process context
                    var rc = callback.CreateRemoteCallContext();

                    //enter render process
                    rc.Enter();

                    //create render task
                    var task = new CfrTask();
                    task.Execute += (_, taskArgs) =>
                    {
                        //enter saved context
                        v8Context.Enter();

                        //create callback argument
                        var callbackArgs = CfrV8Value.CreateObject(new CfrV8Accessor());
                        callbackArgs.SetValue("success", CfrV8Value.CreateBool(true), CfxV8PropertyAttribute.ReadOnly);
                        callbackArgs.SetValue("text", CfrV8Value.CreateString("Message from C#"), CfxV8PropertyAttribute.ReadOnly);

                        //execute callback
                        callback.ExecuteFunction(null, new CfrV8Value[] { callbackArgs });

                        v8Context.Exit();

                        //lock task from gc
                        lock (task)
                        {
                            Monitor.PulseAll(task);
                        }
                    };

                    lock (task)
                    {
                        //post task to render process
                        v8Context.TaskRunner.PostTask(task);
                    }

                    rc.Exit();

                    GC.KeepAlive(task);
                }
            };
        }

        private void TestExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            this.Form1.Invoke(Form1.CallJavascriptO);
        }

        private void GetBookingNumberExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            using (var context = new HotelContext())
            {
                var startnumber = 1;
                var today = DateTime.Today;
                var result = "BOK" + today.ToString("yyyyMMdd");
                var bookingToday = context
                    .Bookings
                    .Where(b => b.BookingAt >= today)
                    .Where(b => b.Id.Contains(result))
                    .OrderByDescending(b => b.BookingAt)
                    .FirstOrDefault();

                if (bookingToday != null)
                {
                    // Generate new
                    var subNumber = bookingToday.Id.Substring(11, 5);
                    startnumber = Convert.ToInt32(subNumber);
                    startnumber++;
                }

                result = string.Format("{0}{1:00000}", result, startnumber);
                e.SetReturnValue(result);
            }
        }

        private void SaveDialogExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            var is_callback = e.Arguments[0].IsFunction;

            Reset();

            if (is_callback)
            {
                //no-filter
                var callback = e.Arguments[0];

                SaveDialog(callback);
            }
            else
            {
                //with-filter
                var filter = e.Arguments[0].StringValue;
                var callback = e.Arguments[1];

                SaveDialog(callback, filter);
            }
        }

        private void OpenDialogExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            var is_callback = e.Arguments[0].IsFunction;

            Reset();

            if (is_callback)
            {
                //no-filter
                var callback = e.Arguments[0];

                OpenDialog(callback);
            } else
            {
                //with-filter
                var filter = e.Arguments[0].StringValue;
                var callback = e.Arguments[1];

                OpenDialog(callback, filter);
            }
        }

        private void GetUploadUrl(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            var filename = e.Arguments[0].StringValue;
            var retUrl = baseUrl + filename;

            e.SetReturnValue(retUrl);
        }

        private void Reset()
        {
            originalName = null;
            retStr = null;
        }

        private void OpenDialog(CfrV8Value callback)
        {
            OpenDialog(callback, "All Files|*.*");
        }

        private void OpenDialog(CfrV8Value callback, string Filter)
        {
            var oThread = new Thread(() => {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var dialog = new OpenFileDialog
                {
                    Filter = Filter
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var file = new FileInfo(dialog.FileName);
                    var ext = file.Extension;
                    var newName = Guid.NewGuid() + ext;
                    var newPath = Path.Combine(basePath, @"Assets\upload\", newName);

                    file.CopyTo(newPath);

                    originalName = file.Name;
                    retStr = newName;
                }
            });

            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
            var isSafe = oThread.Join(new TimeSpan(2, 0, 0));
            var callbackArgs = CfrV8Value.CreateObject(new CfrV8Accessor());
            if (isSafe)
                oThread.Abort();

            callbackArgs.SetValue("hashname", CfrV8Value.CreateString(retStr), CfxV8PropertyAttribute.ReadOnly);
            callbackArgs.SetValue("filename", CfrV8Value.CreateString(originalName), CfxV8PropertyAttribute.ReadOnly);
            callback.ExecuteFunction(null, new CfrV8Value[] { callbackArgs });
        }

        private void SaveDialog(CfrV8Value callback)
        {
            SaveDialog(callback, "All Files|*.*");
        }

        private void SaveDialog(CfrV8Value callback, string Filter)
        {
            var oThread = new Thread(() => {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var dialog = new SaveFileDialog
                {
                    Filter = Filter
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    originalName = dialog.FileName;
                    retStr = dialog.FileName;
                }
            });

            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
            var isSafe = oThread.Join(new TimeSpan(2, 0, 0));
            var callbackArgs = CfrV8Value.CreateObject(new CfrV8Accessor());
            if (isSafe)
                oThread.Abort();

            callbackArgs.SetValue("filename", CfrV8Value.CreateString(originalName), CfxV8PropertyAttribute.ReadOnly);
            callback.ExecuteFunction(null, new CfrV8Value[] { callbackArgs });
        }
    }
}
