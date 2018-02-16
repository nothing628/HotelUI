using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.App.Auth;

namespace UIHotel.App.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class Authorize : Attribute, IAuthAttribute
    {
        private AuthLevel Level = AuthLevel.User;
        private bool IsSet;

        public Authorize()
        {
            //
        }

        public Authorize(AuthLevel auth)
        {
            Level = auth;
            IsSet = true;
        }

        public bool IsValidUser()
        {
            if (AuthState.IsLogin)
            {
                if (IsSet)
                    return Level == (AuthLevel)AuthState.User().Permission;
                else
                    return true;
            }

            return false;
        }
    }
}
