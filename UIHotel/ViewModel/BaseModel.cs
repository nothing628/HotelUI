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
                    var row_pos = 14;
                    var items = dict["items"] as List<object>;
                    var worksheet = grid.Worksheets[0];

                    foreach (var row in items)
                    {
                        var cols = row as List<object>;
                        var col_pos = 0;

                        foreach (var col in cols)
                        {
                            // Grid cols
                            AutoAppend(worksheet, row_pos, col_pos);
                            worksheet[row_pos, col_pos] = col;
                            col_pos++;
                        }
                        row_pos++;
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

        private void AutoAppend(Worksheet sheet, int row, int col)
        {
            if (sheet.RowCount < row + 1)
                sheet.AppendRows(row - sheet.RowCount + 1);

            if (sheet.ColumnCount < col + 1)
                sheet.AppendCols(col - sheet.ColumnCount + 1);
        }
    }
}
