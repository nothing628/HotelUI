using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data;
using UIHotel.Data.Table;

namespace UIHotel.App.Auth
{
    public static class AuthState
    {
        public static User User()
        {
            if (CurrentUserId.HasValue)
            {
                using (var data = new DataContext())
                {
                    var user = (from a in data.Users
                                where a.Id == CurrentUserId.Value
                                select a).SingleOrDefault();

                    return user;
                }
            }

            return null;
        }

        public static long? CurrentUserId { get; set; }

        public static bool IsLogin
        {
            get => CurrentUserId.HasValue;
        }
    }
}
