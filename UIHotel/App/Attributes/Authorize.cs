using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Auth;

namespace UIHotel.App.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class Authorize : Attribute
    {
        private AuthLevel Level = AuthLevel.User;

        public Authorize()
        {
            //
        }

        public Authorize(AuthLevel auth)
        {
            Level = auth;
        }
    }
}
