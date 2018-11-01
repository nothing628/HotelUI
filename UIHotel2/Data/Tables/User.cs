using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.Misc;

namespace UIHotel2.Data.Tables
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(40)]
        [Required]
        public string Username { get; set; }

        [StringLength(40)]
        [Required]
        public string Fullname { get; set; }

        [StringLength(1024)]
        public string Password { get; set; }

        public byte Level { get; set; } = 0;
        //Level 0 = Administrator, 1 = Manager, 2 = Receptionist, 3 = Maintenance/OB

        public bool IsActive { get; set; } = true;

        public void UpdatePassword(string password)
        {
            var appkey = SettingHelper.AppKey;
            Password = AuthHelper.HashText(password, appkey);
        }
    }
}
