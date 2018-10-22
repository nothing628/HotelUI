using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHotel2.Data;
using UIHotel2.Data.Tables;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;

namespace UIHotel2.AppObject
{
    class HotelObject : BaseObject
    {
        public HotelObject(Form owner)
        {
        }

        public override string ObjectName => "Hotel";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("TransactionReportDownload").Execute += TransactionDownload;
            Self.AddFunction("BookingReportDownload").Execute += BookingDownload;
        }

        private void BookingDownload(object sender, CfrV8HandlerExecuteEventArgs e)
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
                var isSuccess = CreateBookingReport(filename, start, end.AddDays(1));

                if (isSuccess)
                {
                    Process.Start(filename);
                }
            });

            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
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
                var isSuccess = CreateTransactionReport(filename, start, end.AddDays(1));

                if (isSuccess)
                {
                    Process.Start(filename);
                }
            });

            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
        }

        private bool CreateBookingReport(string destination, DateTime start, DateTime end)
        {
            var result = true;
            var start_row = 0;

            using (var workbook = ReoGridControl.CreateMemoryWorkbook())
            using (var context = new HotelContext())
            {
                if (string.IsNullOrEmpty(destination)) return false;
                var worksheet = workbook.Worksheets[0];
            }

            return result;
        }

        private bool CreateTransactionReport(string destination, DateTime start, DateTime end)
        {
            var result = true;
            var start_row = 0;

            using (var workbook = ReoGridControl.CreateMemoryWorkbook())
            using (var context = new HotelContext())
            {
                if (string.IsNullOrEmpty(destination)) return false;
                var worksheet = workbook.Worksheets[0];
                var transactionList = new List<Transaction>();

                try
                {
                    transactionList = context.Transactions
                        .Where(t => t.TransactionAt >= start.Date)
                        .Where(t => t.TransactionAt < end.Date)
                        .ToList();
                } catch
                {
                    result = false;
                }

                try
                {
                    worksheet.Cells[start_row, 0].Data = "Date";
                    worksheet.Cells[start_row, 1].Data = "Description";
                    worksheet.Cells[start_row, 2].Data = "In";
                    worksheet.Cells[start_row, 3].Data = "Out";

                    for (int i = 1; i <= transactionList.Count;i++)
                    {
                        if (start_row + i >= worksheet.RowCount - 1) worksheet.RowCount = start_row + i + 2;

                        worksheet.Cells[start_row + i, 0].Data = transactionList[i - 1].TransactionAt;
                        worksheet.Cells[start_row + i, 1].Data = transactionList[i - 1].Description;
                        worksheet.Cells[start_row + i, 2].Data = transactionList[i - 1].AmmountIn;
                        worksheet.Cells[start_row + i, 3].Data = transactionList[i - 1].AmmountOut;

                        worksheet.Cells[start_row + i, 0].DataFormat = CellDataFormatFlag.DateTime;
                    }

                    workbook.Save(destination);
                } catch {
                    result = false;
                }
            }

            return result;
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
