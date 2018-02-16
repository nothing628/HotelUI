using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class Unauthorize : Attribute, IAuthAttribute
    {
        public bool IsValidUser()
        {
            return true;
        }
    }
}
