using System;
using UIHotel.App.Auth;

namespace UIHotel.App.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class Unauthorize : Attribute, IAuthAttribute
    {
        public bool IsValidUser()
        {
            return !AuthState.IsLogin;
        }
    }
}
