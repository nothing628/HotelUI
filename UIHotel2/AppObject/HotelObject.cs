using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using UIHotel2.Data;
using UIHotel2.Data.Tables;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;
using Chromium;
using UIHotel2.Misc;

namespace UIHotel2.AppObject
{
    public class HotelObject : BaseObject
    {
        public override string ObjectName => "Hotel";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("TransactionReportDownload").Execute += TransactionDownload;
            Self.AddFunction("BookingReportDownload").Execute += BookingDownload;
            Self.AddFunction("CalcTransaction").Execute += CalcTransaction;
            Self.AddFunction("CalcBooking").Execute += CalcBooking;
            var visitor = Self.AddDynamicProperty("Visitor");
            var room = Self.AddDynamicProperty("Room");
            var unique_visitor = Self.AddDynamicProperty("UniqueVisitor");
            var balance = Self.AddDynamicProperty("Balance");

            visitor.PropertyGet += Visitor_PropertyGet;
            room.PropertyGet += Room_PropertyGet;
            unique_visitor.PropertyGet += Unique_visitor_PropertyGet;
            balance.PropertyGet += Balance_PropertyGet;
        }

        private void ExecuteCallback(CfrV8Value callback)
        {
            var callbackArgs = CfrV8Value.CreateObject(new CfrV8Accessor());

            callback.ExecuteFunction(null, new CfrV8Value[] { callbackArgs });
        }

        public void CalcBooking(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                e.Exception = ex.Message;
            }
        }

        private void CalcTransaction(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var callback = e.Arguments[0];
                var th = new Thread(() => TransactionHelper.CalculateSubtotal(false));
                th.Start();
                th.Join();

                ExecuteCallback(callback);
            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
            }
        }

        private void Balance_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            using (var context = new HotelContext())
            {
                try
                {
                    var result = context.Transactions.OrderByDescending(p => p.TransactionAt).Select(p => p.Subtotal).First();
                    var castVal = Convert.ToUInt32(result);
                    e.Retval = castVal;
                } catch
                {
                    e.Retval = -1;
                }
            }

            e.SetReturnValue(true);
        }

        private void Unique_visitor_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            using (var context = new HotelContext())
            {
                try
                {
                    var today = DateTime.Today;
                    var monthago = today.AddMonths(-1);
                    var result = context.Bookings.Include(p => p.Guest).Where(p => p.Guest.CreateAt > monthago).ToList();
                    var guest = result.Select(p => p.Guest.Id).Distinct();
                    var count = guest.Count();

                    e.Retval = count;
                } catch
                {
                    e.Retval = -1;
                }
            }

            e.SetReturnValue(true);
        }

        private void Room_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            var total = 0;
            var used = 0;
            var C = CfrV8Value.CreateObject(new CfrV8Accessor());

            using (var context = new HotelContext())
            {
                try
                {
                    total = context.Rooms.Count();
                    var roomAvail = context.Rooms.Where(p => p.RoomStateId == 1 || p.RoomStateId == 2).Count();
                    used = total - roomAvail;
                } catch { }
            }

            C.SetValue("Used", used, CfxV8PropertyAttribute.ReadOnly);
            C.SetValue("Total", total, CfxV8PropertyAttribute.ReadOnly);
            e.Retval = C;
            e.SetReturnValue(true);
        }

        private void Visitor_PropertyGet(object sender, CfrV8AccessorGetEventArgs e)
        {
            using (var context = new HotelContext())
            {
                try
                {
                    var result = context.Bookings.Where(p => !p.CheckoutAt.HasValue).Count();
                    e.Retval = result;
                } catch
                {
                    e.Retval = -1;
                }
            }

            e.SetReturnValue(true);
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
            const int start_row = 5;

            using (var workbook = ReoGridControl.CreateMemoryWorkbook())
            using (var context = new HotelContext())
            {
                if (string.IsNullOrEmpty(destination)) return false;
                var worksheet = workbook.Worksheets[0];
                var transactionList = new List<Booking>();

                try
                {
                    transactionList = context.Bookings
                        .Include(t => t.Type)
                        .Include(t => t.Room)
                        .Include(t => t.Guest)
                        .Where(t => t.BookingAt >= start.Date)
                        .Where(t => t.BookingAt < end.Date)
                        .Where(t => t.CheckoutAt.HasValue)
                        .ToList();
                } catch
                {
                    result = false;
                }

                try
                {
                    worksheet.Cells[start_row, 0].Data = "Booking Date";
                    worksheet.Cells[start_row, 1].Data = "Arrival Date";
                    worksheet.Cells[start_row, 2].Data = "Departure Date";
                    worksheet.Cells[start_row, 3].Data = "Checkin Date";
                    worksheet.Cells[start_row, 4].Data = "Checkout Date";
                    worksheet.Cells[start_row, 5].Data = "Booking Type";
                    worksheet.Cells[start_row, 6].Data = "Room Number";
                    worksheet.Cells[start_row, 7].Data = "Guest Id";
                    worksheet.Cells[start_row, 8].Data = "Guest Name";
                    worksheet.Cells[start_row, 9].Data = "Total Adult";
                    worksheet.Cells[start_row, 10].Data = "Total Child";
                    worksheet.Cells[start_row, 11].Data = "Total Price";
                    worksheet.Cells[start_row, 12].Data = "Total Pay";

                    for (int i = 1; i <= transactionList.Count; i++)
                    {
                        var bookingData = transactionList[i - 1];
                        var rowNum = start_row + i;

                        if (rowNum >= worksheet.RowCount - 1) worksheet.RowCount = rowNum + 2;

                        worksheet.Cells[rowNum, 0].Data = bookingData.BookingAt;
                        worksheet.Cells[rowNum, 1].Data = bookingData.ArrivalDate;
                        worksheet.Cells[rowNum, 2].Data = bookingData.DepartureDate;
                        worksheet.Cells[rowNum, 3].Data = bookingData.CheckinAt;
                        worksheet.Cells[rowNum, 4].Data = bookingData.CheckoutAt;
                        worksheet.Cells[rowNum, 5].Data = bookingData.Type.TypeName;
                        worksheet.Cells[rowNum, 6].Data = bookingData.Room.RoomNumber;
                        worksheet.Cells[rowNum, 7].Data = bookingData.Guest.IdNumber;
                        worksheet.Cells[rowNum, 8].Data = bookingData.Guest.Fullname;
                        worksheet.Cells[rowNum, 9].Data = bookingData.CountAdult;
                        worksheet.Cells[rowNum, 10].Data = bookingData.CountChild;
                        worksheet.Cells[rowNum, 11].Data = GetPrice(bookingData.Id);
                        worksheet.Cells[rowNum, 12].Data = GetPay(bookingData.Id);

                        worksheet.Cells[rowNum, 0].DataFormat = CellDataFormatFlag.DateTime;
                        worksheet.Cells[rowNum, 1].DataFormat = CellDataFormatFlag.DateTime;
                        worksheet.Cells[rowNum, 2].DataFormat = CellDataFormatFlag.DateTime;
                        worksheet.Cells[rowNum, 3].DataFormat = CellDataFormatFlag.DateTime;
                        worksheet.Cells[rowNum, 4].DataFormat = CellDataFormatFlag.DateTime;
                        worksheet.Cells[rowNum, 6].DataFormat = CellDataFormatFlag.Text;
                        worksheet.Cells[rowNum, 7].DataFormat = CellDataFormatFlag.Text;
                    }

                    workbook.Save(destination);
                } catch
                {
                    result = false;
                }
            }

            return result;
        }

        private bool CreateTransactionReport(string destination, DateTime start, DateTime end)
        {
            var result = true;
            const int start_row = 5;

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
                        var rowNum = start_row + i;
                        var transaction = transactionList[i - 1];

                        if (rowNum >= worksheet.RowCount - 1) worksheet.RowCount = rowNum + 2;

                        worksheet.Cells[rowNum, 0].Data = transaction.TransactionAt;
                        worksheet.Cells[rowNum, 1].Data = transaction.Description;
                        worksheet.Cells[rowNum, 2].Data = transaction.AmmountIn;
                        worksheet.Cells[rowNum, 3].Data = transaction.AmmountOut;

                        worksheet.Cells[rowNum, 0].DataFormat = CellDataFormatFlag.DateTime;
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

        private decimal GetPrice(string invoiceId)
        {
            decimal result = 0;

            using (var context = new HotelContext())
            {
                var details = GetInvoiceDetail(invoiceId);

                result = details.Sum(t => t.AmmountOut);
            }

            return result;
        }

        private decimal GetPay(string invoiceId)
        {
            decimal result = 0;

            using (var context = new HotelContext())
            {
                var details = GetInvoiceDetail(invoiceId);

                result = details.Sum(t => t.AmmountIn);
            }

            return result;
        }

        private List<InvoiceDetail> GetInvoiceDetail(string invoiceId)
        {
            var result = new List<InvoiceDetail>();

            if (string.IsNullOrEmpty(invoiceId)) return result;

            using (var context = new HotelContext())
            {
                try
                {
                    result = context.InvoiceDetails
                        .Where(t => t.InvoiceId == invoiceId)
                        .ToList();
                } catch { }
            }

            return result;
        }
    }
}
