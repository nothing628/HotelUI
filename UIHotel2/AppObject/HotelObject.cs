using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHotel2.Data;
using unvell.ReoGrid;

namespace UIHotel2.AppObject
{
    class HotelObject : BaseObject
    {
        public HotelObject(Form owner) : base(owner)
        {
        }

        public override string ObjectName => "Hotel";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("TransactionReportDownload").Execute += TransactionDownload; ;
        }

        private void TransactionDownload(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            if (e.Arguments.Length != 2 || !e.Arguments[0].IsDate || !e.Arguments[1].IsDate)
            {
                e.Exception = "Need to have 2 argument in date";
                return;
            }

            var time = new CfrTime();
            var start = time.ToUniversalTime(e.Arguments[0].DateValue);
            var end = time.ToUniversalTime(e.Arguments[1].DateValue);
            var oThread = new Thread(() => {
                var filename = GetFilename("Excel File|*.xlsx");
                var isSuccess = CreateTransactionReport(filename, start, end);
                Thread.Sleep(15000);
            });

            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
            var isSafe = oThread.Join(new TimeSpan(10, 0, 0));
            if (isSafe)
                oThread.Abort();
        }

        private bool CreateTransactionReport(string destination, DateTime start, DateTime end)
        {
            using (var workbook = ReoGridControl.CreateMemoryWorkbook())
            using (var context = new HotelContext())
            {
                //
            }

            return true;
        }

        private string GetFilename(string filter = "All Files|*.*")
        {
            var filename = "";

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filename = dialog.FileName;
                }
            }

            return filename;
        }
    }
}
