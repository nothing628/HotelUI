using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data.Table
{
    [Table("room_category")]
    public class RoomCategory
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("category", Order = 1)]
        public string Category { get; set; }

        [StringLength(200)]
        [Column("description")]
        public string Description { get; set; }

        public decimal GetPrice()
        {
            decimal price = 0;
            var date = DateTime.Today;

            using (var model = new DataContext())
            {

                price = (from b in model.RoomPrice
                         join e in model.DayCycles on b.IdEffect equals e.IdEffect into f
                         from g in f
                         where g.DateAt == date
                         where b.IdCategory == Id
                         select b.Price).SingleOrDefault();
            }

            return price;
        }
    }
}
