using Chromium;
using Chromium.Remote;
using Chromium.WebBrowser;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UIHotel2.JsObject
{
    public interface IBaseObject
    {
        string ObjectName { get; }

        void Register(JSObject obj);
    }

    public abstract class BaseObject : IBaseObject
    {
        public abstract string ObjectName { get; }

        protected JSObject Self { get; set; }

        public virtual void Register(JSObject obj)
        {
            Self = obj.AddObject(ObjectName);
        }

        public dynamic ToExpandoObject(object value)
        {
            IDictionary<string, object> dapperRowProperties = value as IDictionary<string, object>;
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (KeyValuePair<string, object> property in dapperRowProperties)
                expando.Add(property.Key, property.Value);

            return expando as ExpandoObject;
        }

        protected void CallCallback(CfrV8Value callback, CfrV8Context v8Context, params KeyValuePair<string, object>[] par)
        {
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

                    var callbackArgs = CfrV8Value.CreateObject(new CfrV8Accessor());

                    foreach (var val in par)
                    {
                        var validValue = ConvertValue(val.Value);
                        callbackArgs.SetValue(val.Key, validValue, CfxV8PropertyAttribute.ReadOnly);
                    }

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
        }

        static BaseObject()
        {

        }

        public CfrV8Value ConvertValue(object data)
        {
            if (data == null) return CfrV8Value.CreateNull();

            var typObject = data.GetType();
            var retValue = CfrV8Value.CreateUndefined();

            switch (typObject.Name)
            {
                case "Boolean":
                    retValue = Convert.ToBoolean(data);
                    break;
                case "ExpandoObject":
                    IDictionary<string, object> keyValues = data as IDictionary<string, object>;
                    var resultx = CfrV8Value.CreateObject(new CfrV8Accessor(), new CfrV8Interceptor());

                    foreach (var keypair in keyValues)
                    {
                        resultx.SetValue(keypair.Key, ConvertValue(keypair.Value), CfxV8PropertyAttribute.ReadOnly);
                    }

                    retValue = resultx;
                    break;
                case "DBNull":
                    retValue = CfrV8Value.CreateNull();
                    break;
                case "DateTime":
                    var newValt = Convert.ToDateTime(data);
                    var univerTime = newValt.ToUniversalTime();
                    retValue = CfrV8Value.CreateDate(CfrTime.FromUniversalTime(univerTime));
                    break;
                case "Decimal":
                case "Double":
                case "Single":
                    retValue = Convert.ToDouble(data);
                    break;
                case "Byte":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                    retValue = Convert.ToUInt64(data);
                    break;
                case "SByte":
                case "Int16":
                case "Int32":
                case "Int64":
                    retValue = Convert.ToInt64(data);
                    break;
                case "String":
                    retValue = (string)data;
                    break;
                default:
                    if (typObject.IsEnum)
                    {
                        retValue = Enum.GetName(typObject, data);
                    }
                    else if (typObject.IsArray)
                    {
                        var pos = 0;

                        if (typObject.Name == "Byte[]")
                        {
                            var arr_byte = (byte[])data;
                            var result = CfrV8Value.CreateArray(arr_byte.Length);

                            foreach (var item in arr_byte)
                            {
                                result.SetValue(pos++, ConvertValue(item));
                            }

                            retValue = result;
                        }
                        else
                        {
                            var arr = (object[])data;
                            var result = CfrV8Value.CreateArray(arr.Length);

                            foreach (var item in arr)
                            {
                                result.SetValue(pos++, ConvertValue(item));
                            }

                            retValue = result;
                        }
                    }
                    else if (typObject.Name.StartsWith("Dictionary"))
                    {
                        var dictionary = (IDictionary<string, object>)data;
                        var result = CfrV8Value.CreateObject(new CfrV8Accessor(), new CfrV8Interceptor());

                        foreach (var keypair in dictionary)
                        {
                            result.SetValue(keypair.Key, ConvertValue(keypair.Value), CfxV8PropertyAttribute.ReadOnly);
                        }

                        retValue = result;
                    }
                    else if (typObject.IsClass)
                    {
                        var propertyInfoes = typObject.GetProperties();
                        var result = CfrV8Value.CreateObject(new CfrV8Accessor(), new CfrV8Interceptor());

                        foreach (var item in propertyInfoes)
                        {
                            result.SetValue(item.Name, ConvertValue(item.GetValue(data)), CfxV8PropertyAttribute.ReadOnly);
                        }

                        retValue = result;
                    }
                    break;
            }

            return retValue;
        }
    }
}
