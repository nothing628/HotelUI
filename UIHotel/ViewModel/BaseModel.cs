using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace UIHotel.ViewModel
{
    public class BaseModel
    {
        public string GetHelloMessage()
        {
            return "Hello world";
        }

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
    }
}
