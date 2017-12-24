using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            string typeName = "UIHotel.Model." + InstanceName;
            Type typeD = Type.GetType(typeName);
            Type[] typeParams = new Type[s.Length];

            for (int i = 0; i < s.Length; i++)
                typeParams[i] = s[i].GetType();

            Object p = Activator.CreateInstance(typeD,
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding, null, null, null);
            MethodInfo method = typeD.GetMethod(Method, typeParams);

            if (method == null)
                return null;

            Object retVal = method.Invoke(p, s);

            return retVal;
        }

        public object GetObject(string InstanceName, string Method)
        {
            string typeName = "UIHotel.Model." + InstanceName;
            Type typeD = Type.GetType(typeName);

            Object p = Activator.CreateInstance(typeD,
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding, null, null, null);
            MethodInfo method = typeD.GetMethod(Method);
            Object retVal = method.Invoke(p, null);

            return retVal;
        }

        T CreateType<T>() where T : new()
        {
            return new T();
        }

        T GetInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        T GetInstance<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
