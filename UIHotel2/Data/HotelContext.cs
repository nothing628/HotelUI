using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel2.AppObject;
using UIHotel2.Data.Tables;

namespace UIHotel2.Data
{
    class HotelContext: DbContext
    {
        public HotelContext(): base(DBObject.ConnectionString)
        {
            //
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Guest> Guests { get; set; }
    }
}
