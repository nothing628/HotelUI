﻿using Chromium;
using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using Dapper;
using Dapper.Contrib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.Data;
using UIHotel2.Data.Tables;
using UIHotel2.Misc;
using UIHotel2.Properties;

namespace UIHotel2.JsObject
{
    public class DBObject : BaseObject
    {
        public static string ConnectionString
        {
            get
            {
                return GetConnectionString();
            }
        }

        public override string ObjectName => "DB";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("Query").Execute += QueryExecute;
            Self.AddFunction("QueryScalar").Execute += QueryScalarExecute;
            Self.AddFunction("CalendarGet").Execute += GetCalendar;
            Self.AddFunction("CalendarSet").Execute += SetCalendar;
            Self.AddFunction("Test").Execute += TestExecute;
            Self.AddFunction("TestConnect").Execute += TestConnectExecute;
            Self.AddFunction("Migrate").Execute += MigrateExecute;
        }

        private async void MigrateExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            if (e.Arguments.Length != 6)
            {
                e.Exception = "6 parameters expected!";
                return;
            }

            var server = e.Arguments[0];
            var port = e.Arguments[1];
            var user = e.Arguments[2];
            var password = e.Arguments[3];
            var database = e.Arguments[4];
            var callback = e.Arguments[5];
            var context = CfrV8Context.GetCurrentContext();

            if (!server.IsString || !port.IsInt || !user.IsString || !password.IsString || !database.IsString || !callback.IsFunction)
            {
                //Invalid parameter;
                e.Exception = "Invalid parameter type";
                return;
            }

            Settings.Default.SQL_HOST = server.StringValue;
            Settings.Default.SQL_PORT = port.IntValue;
            Settings.Default.SQL_USER = user.StringValue;
            Settings.Default.SQL_PASSWORD = password.StringValue;
            Settings.Default.SQL_DATABASE = database.StringValue;
            Settings.Default.Save();

            await Task.Run(() =>
            {
                var is_success = DataHelper.MigrateDB(true);
                AppHelper.CreateStartup();

                CallCallback(callback, context, new KeyValuePair<string, object>("success", is_success));
            });
        }

        private void TestConnectExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            if (e.Arguments.Length != 5)
            {
                e.Exception = "5 parameters expected!";
                return;
            }

            var server = e.Arguments[0];
            var port = e.Arguments[1];
            var user = e.Arguments[2];
            var password = e.Arguments[3];
            var callback = e.Arguments[4];

            if (!server.IsString || !port.IsInt || !user.IsString || !password.IsString || !callback.IsFunction)
            {
                //Invalid parameter;
                e.Exception = "Invalid parameter type";
                return;
            }

            if (callback.IsFunction)
            {
                // Callback to result
                Settings.Default.SQL_HOST = server.StringValue;
                Settings.Default.SQL_PORT = port.IntValue;
                Settings.Default.SQL_USER = user.StringValue;
                Settings.Default.SQL_PASSWORD = password.StringValue;

                var is_connect = TestConnect();

                callback.ExecuteFunction(null, new CfrV8Value[] { is_connect });

                Settings.Default.Reload();
            }
        }

        private bool TestConnect()
        {
            var is_connect = true;

            using (var context = new HotelContext())
            {
                try
                {
                    context.Database.Connection.Open();
                }
                catch
                {
                    is_connect = false;
                }
            }

            return is_connect;
        }

        private bool TestDatabase()
        {
            var is_verified = true;

            using (var context = new HotelContext())
            {
                var connection = context.Database.Connection;

                try
                {
                    connection.Open();

                    var result = context.Database.SqlQuery<int>("SELECT COUNT(*) FROM __migrationhistory");
                    var arr_result = result.ToArray();

                    is_verified = arr_result.Length > 0 && arr_result[0] == 31;
                }
                catch
                {
                    is_verified = false;
                }
            }

            return is_verified;
        }

        private void TestExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            if (e.Arguments.Length != 6)
            {
                e.Exception = "6 parameters expected!";
                return;
            }

            var server = e.Arguments[0];
            var port = e.Arguments[1];
            var user = e.Arguments[2];
            var password = e.Arguments[3];
            var database = e.Arguments[4];
            var callback = e.Arguments[5];

            if (!server.IsString || !port.IsInt || !user.IsString || !password.IsString || !database.IsString || !callback.IsFunction)
            {
                //Invalid parameter;
                e.Exception = "Invalid parameter type";
                return;
            }

            if (callback.IsFunction)
            {
                // Callback to result
                Settings.Default.SQL_DATABASE = database.StringValue;
                Settings.Default.SQL_HOST = server.StringValue;
                Settings.Default.SQL_PORT = port.IntValue;
                Settings.Default.SQL_USER = user.StringValue;
                Settings.Default.SQL_PASSWORD = password.StringValue;

                var is_connect = TestConnect();
                var is_verify = TestDatabase();

                callback.ExecuteFunction(null, new CfrV8Value[] { is_connect && is_verify });

                Settings.Default.Reload();
            }
        }

        private CfrV8Value[] ParseToArray(CfrV8Value arguments)
        {
            if (!arguments.IsArray)
                return new CfrV8Value[] { };

            var result = new List<CfrV8Value>();

            for(int i = 0; i < arguments.ArrayLength; i++)
            {
                result.Add(arguments.GetValue(i));
            }

            return result.ToArray();
        }

        private DynamicParameters ParseParameter(CfrV8Value argumentsx)
        {
            var arguments = ParseToArray(argumentsx);
            var paramLength = arguments.Length;
            var paramObject = new DynamicParameters();

            if (paramLength > 0)
            {
                var paramStart = 0;

                for (int i = 0; i < arguments.Length; i++)
                {
                    var paramName = "@param" + paramStart;
                    var argData = arguments[i];

                    if (argData.IsString)
                    {
                        paramObject.Add(paramName, argData.StringValue, DbType.String);
                    }
                    else if (argData.IsInt)
                    {
                        paramObject.Add(paramName, argData.IntValue, DbType.Int32);
                    }
                    else if (argData.IsDouble)
                    {
                        paramObject.Add(paramName, argData.DoubleValue, DbType.Double);
                    }
                    else if (argData.IsBool)
                    {
                        paramObject.Add(paramName, argData.BoolValue, DbType.Boolean);
                    }
                    else if (argData.IsUint)
                    {
                        paramObject.Add(paramName, argData.UintValue, DbType.UInt32);
                    }
                    else if (argData.IsUndefined || argData.IsNull)
                    {
                        paramObject.Add(paramName, null);
                    }
                    else if (argData.IsObject)
                    {
                        var type = argData.GetValue("type").StringValue;
                        var values = argData.GetValue("value");

                        switch (type)
                        {
                            case "boolean":
                                var boolean_result = new List<bool>();
                                for (int j = 0; j < values.ArrayLength; j++)
                                {
                                    boolean_result.Add(values.GetValue(j).BoolValue);
                                }
                                paramObject.Add(paramName, boolean_result.ToArray());
                                break;
                            case "string":
                                var str_result = new List<string>();
                                for (int j = 0; j < values.ArrayLength; j++)
                                {
                                    str_result.Add(values.GetValue(j).StringValue);
                                }
                                paramObject.Add(paramName, str_result.ToArray());
                                break;
                            case "double":
                                var double_result = new List<double>();
                                for (int j = 0; j < values.ArrayLength; j++)
                                {
                                    double_result.Add(values.GetValue(j).DoubleValue);
                                }
                                paramObject.Add(paramName, double_result.ToArray());
                                break;
                            case "int":
                                var int_result = new List<int>();
                                for (int j = 0; j < values.ArrayLength; j++)
                                {
                                    int_result.Add(values.GetValue(j).IntValue);
                                }
                                paramObject.Add(paramName, int_result.ToArray());
                                break;
                            case "uint":
                                var uint_result = new List<uint>();
                                for (int j = 0; j < values.ArrayLength; j++)
                                {
                                    uint_result.Add(values.GetValue(j).UintValue);
                                }
                                paramObject.Add(paramName, uint_result.ToArray());
                                break;
                        }
                    }

                    paramStart++;
                }
            }

            return paramObject;
        }

        private void QueryExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var qryString = e.Arguments[0].StringValue;
                var paramObject = ParseParameter(e.Arguments[1]);
                var counter = 0;

                using (var connect = GetConnection())
                {
                    var reader = connect.Query(qryString, paramObject);
                    var arrObj = CfrV8Value.CreateArray(reader.Count());

                    foreach (var obj in reader)
                    {
                        var exp_obj = ToExpandoObject(obj);
                        arrObj.SetValue(counter, ConvertValue(exp_obj));

                        counter++;
                    }

                    e.SetReturnValue(arrObj);
                }
            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
                e.SetReturnValue(CfrV8Value.CreateUndefined());
            }
        }

        private void QueryScalarExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var qryString = e.Arguments[0].StringValue;
                var paramObject = ParseParameter(e.Arguments[1]);

                using (var connData = GetConnection())
                {
                    var result = connData.Execute(qryString, paramObject);
                    e.SetReturnValue(result);
                }
            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
                e.SetReturnValue(CfrV8Value.CreateUndefined());
            }
        }

        private void GetCalendar(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            using (var context = new HotelContext())
            {
                var year = e.Arguments[0].IntValue;
                var date_start = new DateTime(year, 1, 1);
                var date_end = new DateTime(year + 1, 1, 1).AddDays(-1);
                EventInputObject lastInput = null;
                var listOut = new List<EventInputObject>();
                var calendar = context.RoomCalendars
                    .Include("Kind")
                    .Where(c => c.DateAt >= date_start)
                    .Where(c => c.DateAt <= date_end)
                    .OrderBy(c => c.DateAt)
                    .ToList();

                foreach (var item in calendar)
                {
                    if (lastInput != null)
                    {
                        var dateNext = lastInput.end;
                        var dateDiff = item.DateAt - dateNext;
                        var title = lastInput.title;

                        if (title == item.Kind.KindName && dateDiff.TotalDays == 1)
                        {
                            lastInput.end = item.DateAt;
                            continue;
                        }
                    }

                    lastInput = new EventInputObject
                    {
                        start = item.DateAt,
                        end = item.DateAt,
                        title = item.Kind.KindName,
                        color = "#" + item.Kind.KindColor,
                    };

                    listOut.Add(lastInput);
                }

                foreach (var input in listOut)
                {
                    var diff = input.end - input.start;

                    if (diff.TotalDays > 0)
                    {
                        input.end = input.end.AddDays(1);
                    }
                }

                var callbackArgs = CfrV8Value.CreateObject(new CfrV8Accessor());
                var callback = e.Arguments[1];
                callbackArgs.SetValue("list", ConvertValue(listOut.ToArray()), CfxV8PropertyAttribute.ReadOnly);
                callback.ExecuteFunction(null, new CfrV8Value[] { callbackArgs });
            }
        }

        private void SetCalendar(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            using (var context = new HotelContext())
            {
                var list_data = e.Arguments[0];

                if (!list_data.IsArray) return;

                for (int i = 0; i < list_data.ArrayLength; i++)
                {
                    var elem_insert = list_data.GetValue(i);
                    var date = elem_insert.GetValue("DateAt").StringValue;
                    var kind = elem_insert.GetValue("RoomPriceKindId").IntValue;
                    var dateAt = DateTime.Parse(date);

                    var item = context.RoomCalendars.FirstOrDefault(c => c.DateAt == dateAt);

                    if (item == null)
                    {
                        item = new RoomCalendar
                        {
                            DateAt = dateAt,
                            RoomPriceKindId = kind
                        };

                        context.RoomCalendars.Add(item);
                        context.SaveChanges();
                    }
                    else if (item.RoomPriceKindId != kind)
                    {
                        item.RoomPriceKindId = kind;
                        context.Entry(item).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
        }

        private static string GetConnectionString()
        {
            var SQL_Host = Settings.Default.SQL_HOST;
            var SQL_Port = Settings.Default.SQL_PORT;
            var SQL_Database = Settings.Default.SQL_DATABASE;
            var SQL_User = Settings.Default.SQL_USER;
            var SQL_Password = Settings.Default.SQL_PASSWORD;
            var connTmp = Settings.Default.ConnStr;

            return string.Format(connTmp, SQL_Host, SQL_Port, SQL_Database, SQL_User, SQL_Password);
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }
    }
}
