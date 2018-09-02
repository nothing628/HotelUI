using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;
using Dapper;
using Dapper.Contrib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.Properties;

namespace UIHotel2.AppObject
{
    class DBObject : BaseObject
    {
        public override string ObjectName => "DB";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("Query").Execute += QueryExecute;
            Self.AddFunction("QueryScalar").Execute += QueryScalarExecute;
        }

        private void QueryExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            try
            {
                var qryString = e.Arguments[0].StringValue;
                var counter = 0;

                using (var connect = GetConnection())
                {
                    var reader = connect.Query(qryString);
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

                using (var connData = GetConnection())
                {
                    var result = connData.Execute(qryString);
                    e.SetReturnValue(result);
                }

            }
            catch (Exception ex)
            {
                e.Exception = ex.Message;
                e.SetReturnValue(CfrV8Value.CreateUndefined());
            }
        }

        private string GetConnectionString()
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
