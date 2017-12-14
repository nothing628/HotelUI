using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

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

        public bool IsMatch(string Path, string route)
        {
            var pattern = Regex.Replace(route, @"{\w+}", @"([^\/\n]+)");

            return Regex.IsMatch(Path, pattern, RegexOptions.IgnoreCase);
        }
    }
}
