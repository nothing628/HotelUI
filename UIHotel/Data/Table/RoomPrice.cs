﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.Data.Table
{
    [Table("room_price")]
    public class RoomPrice
    {
        [Key]
        [Column("id", Order = 0)]
        public long Id { get; set; }

        [Column("id_category", Order = 1)]
        public long IdCategory { get; set; }

        [Column("id_effect", Order = 2)]
        public int IdEffect { get; set; }

        [Column("price", Order = 3)]
        public decimal Price { get; set; }
    }
}
