using CefSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace UIHotel.ViewModel
{
    public class BaseModel
    {
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
                    var items = dict["items"] as object[];
                    var worksheet = grid.Worksheets[0];

                    foreach (var row in items)
                    {
                        var cols = row as object[];
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

                    using (var dlg = new SaveFileDialog() {Filter = "Excel | *.xlsx" })
                    {
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            grid.Save(dlg.FileName);
                        }
                    }

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
