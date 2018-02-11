using System;
using UIHotel.Data.Table;

namespace UIHotel.Data.Seeds
{
    public class GuestSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            context.Guests.Add(new Guest() {
                IdKind = "KTP",
                IdNumber = "2017435000119",
                Fullname = "Nothing To Write Here",
                BirthPlace = "Tangerang",
                IsVIP = true,
                Phone1 = "082715543425",
                Phone2 = "02155524487",
                PhotoDoc = "421ec398-1adf-4e8b-9d27-3fde61a1e667.pdf",
                PhotoGuest = "4e775d10-5c82-4630-9bef-c3b79b94ade8.jpg",
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            });
            context.SaveChanges();
        }
    }
}
