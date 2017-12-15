using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Reflection;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var var = new bool[10];

            var[0] = IsMatch(@"index/path/test/12", @"index/{controller}/test/{method}");
            var[1] = IsMatch(@"index/path/test/", @"index/{controller}/test/{method}");
            var[2] = IsMatch(@"index/path/test", @"index/{controller}/test/{method}");
            var[3] = IsMatch(@"index/path/", @"index/{controller}/test/{method}");
            var[4] = IsMatch(@"index/path/test/12/tiga", @"index/{controller}/test/{method}");
            var[5] = true;
        }

        [TestMethod]
        public void TestMethod2()
        {
            var var = new bool[5];

            var[0] = IsMethodExists("UnitTest.TestCore", "index");
            var[1] = IsMethodExists("UnitTest.TestCore", "Index");
            var[2] = IsMethodExists("UnitTest.TestCore", "Test");
            var[3] = IsMethodExists("UnitTest.UnitTest1", "TestMethod2");
        }

        [TestMethod]
        public void TestInstance()
        {
            var result = CreateInstance(typeof(TestCore), "test");

            Assert.AreEqual("Hellow world", result);
        }

        public bool IsMatch(string Path, string route)
        {
            var pattern = Regex.Replace(route, @"{\w+}", @"([^\/\n]+)");

            return Regex.IsMatch(Path, pattern, RegexOptions.IgnoreCase);
        }

        public bool IsClassExists(string ClassName)
        {
            Type type = Type.GetType(ClassName);

            if (type != null)
                return type.IsClass;

            return false;
        }

        public bool IsMethodExists(string ClassName, string Method)
        {
            if (IsClassExists(ClassName))
            {
                Type type = Type.GetType(ClassName);
                MethodInfo[] methods = type.GetMethods();

                foreach (MethodInfo method in methods)
                    if (method.Name.Equals(Method, StringComparison.OrdinalIgnoreCase))
                        return true;
            }

            return false;
        }

        private string CreateInstance(Type Type, string Method)
        {
            ConstructorInfo constInfo = Type.GetConstructor(new Type[] { });
            MethodInfo method = Type.GetMethod(Method);
            Object instance = constInfo.Invoke(new object[] { });

            var result = method?.Invoke(instance, new object[] { });

            return result.ToString();
        }
    }

    public class TestCore
    {
        public void Index()
        {

        }

        public string test()
        {
            return "Hellow world";
        }
    }
}
