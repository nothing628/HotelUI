using CefSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace UIHotel.ViewModel
{
    public class BaseModel
    {
        public string fileName;

        public object GetObjectParam(string InstanceName, string Method, params object[] s)
        {
            string typeName = "UIHotel.ViewModel." + InstanceName;
            Type typeD = Type.GetType(typeName);
            var typeParams = new Type[s.Length];

            for (int i = 0; i < s.Length; i++)
                typeParams[i] = s[i].GetType();

            Object p = Activator.CreateInstance(typeD,
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding, null, null, null);
            MethodInfo method = typeD.GetMethod(Method, typeParams);
            var ret = method?.Invoke(p, s);

            return JsonConvert.SerializeObject(ret);
        }

        public object GetObject(string InstanceName, string Method)
        {
            string typeName = "UIHotel.ViewModel." + InstanceName;
            Type typeD = Type.GetType(typeName);

            Object p = Activator.CreateInstance(typeD,
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding, null, null, null);
            MethodInfo method = typeD.GetMethod(Method, Type.EmptyTypes);
            var ret = method?.Invoke(p, null);
            
            return JsonConvert.SerializeObject(ret);
        }

        public object Close()
        {
            Application.Exit();

            return true;
        }

        public object Print()
        {
            var browser = App.AppMain.Main.Browser;

            browser.Print();

            return 0;
        }

        public object Export(params object[] datas)
        {
            var param = datas[0] as ExpandoObject;
            var dict = ((IDictionary<string, Object>)param);

            using (var grid = ReoGridControl.CreateMemoryWorkbook())
            {
                try
                {
                    var items = dict["items"] as List<object>;
                    var worksheet = grid.Worksheets[0];

                    if (items is null) return false;

                    foreach (var item in items)
                    {
                        ApplyRow(worksheet, item);
                    }

                    if (dict.ContainsKey("options"))
                    {
                        var options = dict["options"] as List<object>;

                        if (options != null)
                        {
                            foreach (var option in options)
                                ApplyOptions(worksheet, option);
                        }
                    }

                    var oThread = new Thread(() => {
                        var basePath = AppDomain.CurrentDomain.BaseDirectory;
                        var dialog = new SaveFileDialog();
                        dialog.Filter = "Excel | *.xlsx";

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            //save
                            grid.Save(dialog.FileName);
                        }
                    });

                    oThread.SetApartmentState(ApartmentState.STA);
                    oThread.Start();
                    var isSafe = oThread.Join(new TimeSpan(2, 0, 0));
                    if (isSafe)
                        oThread.Abort();
                    
                    return true;
                }
                catch (KeyNotFoundException ex)
                {
                    return false;
                }
            }
        }

        private void ApplyOptions(Worksheet sheet, object option)
        {
            var itemObj = ((IDictionary<string, Object>)option);

            if (itemObj.ContainsKey("col") && itemObj.ContainsKey("width"))
            {
                var col = (int)itemObj["col"];
                var width = Convert.ToUInt16((int)itemObj["width"]);

                sheet.SetColumnsWidth(col, 1, width);
            }

            if (itemObj.ContainsKey("row") && itemObj.ContainsKey("height"))
            {
                var row = (int)itemObj["row"];
                var height = Convert.ToUInt16((int)itemObj["height"]);

                sheet.SetRowsHeight(row, 1, height);
            }
        }

        private void ApplyRow(Worksheet sheet, object item)
        {
            var itemObj = ((IDictionary<string, Object>)item);

            if (!itemObj.ContainsKey("row") || !itemObj.ContainsKey("col") || !itemObj.ContainsKey("value"))
                return;

            var row = (int)itemObj["row"];
            var col = (int)itemObj["col"];

            AutoAppend(sheet, row, col);
            sheet[row, col] = itemObj["value"];
            sheet.Cells[row, col].Data = itemObj["value"];

            if (itemObj.ContainsKey("border"))
            {
                var borderStyle = new RangeBorderStyle(unvell.ReoGrid.Graphics.SolidColor.Black, BorderLineStyle.Solid);
                
                sheet.SetRangeBorders(row, col, 1, 1, BorderPositions.All, borderStyle);
            }
            
            if (itemObj.ContainsKey("fontsize"))
            {
                var fontSize = (int)itemObj["fontsize"];

                sheet.Cells[row, col].Style.FontSize = Convert.ToSingle(fontSize);
            }
        }

        private void AutoAppend(Worksheet sheet, int row, int col)
        {
            if (sheet.RowCount < row + 1)
                sheet.AppendRows(row - sheet.RowCount + 1);

            if (sheet.ColumnCount < col + 1)
                sheet.AppendCols(col - sheet.ColumnCount + 1);
        }
    }
}
