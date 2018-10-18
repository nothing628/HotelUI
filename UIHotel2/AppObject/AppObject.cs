using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chromium;
using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using UIHotel2.Data;
using UIHotel2.Misc;

namespace UIHotel2.AppObject
{
    public class AppObject : BaseObject
    {
        public string retStr;
        public string originalName;

        public override string ObjectName => "App";
        public const string baseUrl = "http://assets.app.local/upload/";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("OpenDialog").Execute += OpenDialogExecute;
            Self.AddFunction("SaveDialog").Execute += SaveDialogExecute;
            Self.AddFunction("GetUploadUrl").Execute += GetUploadUrl;
            Self.AddFunction("GetNewBookingNumber").Execute += GetBookingNumberExecute;
            Self.AddFunction("CalcTransaction").Execute += CalcTransaction;
            Self.AddFunction("CalcBooking").Execute += CalcBooking;
        }

        private void CalcTransaction(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            var callback = e.Arguments[0];
            var th = new Thread(TransactionHelper.CalculateSubtotal);
            th.Start();
            th.Join();

            ExecuteCallback(callback);
        }

        public void CalcBooking(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            var callback = e.Arguments[0];
            var bookId = "";

            if (e.Arguments.Length == 2)
            {
                bookId = e.Arguments[1].StringValue;
            }

            var th = new Thread(() => TransactionHelper.CalculateBooking(bookId));
            th.Start();
            th.Join();

            ExecuteCallback(callback);
        }

        private void ExecuteCallback(CfrV8Value callback)
        {
            var callbackArgs = CfrV8Value.CreateObject(new CfrV8Accessor());

            callback.ExecuteFunction(null, new CfrV8Value[] { callbackArgs });
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
