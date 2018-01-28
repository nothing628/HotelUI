using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;

namespace UIHotel.Data.Table
{
    [Table("invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id", Order = 0)]
        public string Id { get; set; }

        [Required]
        [Column("id_checkin", Order = 1)]
        [StringLength(25)]
        public string IdCheckin { get; set; }

        [Required]
        [Column("id_guest", Order = 2)]
        public long IdGuest { get; set; }

        [Column("is_closed", Order = 3)]
        public bool IsClosed { get; set; }

        [Required]
        [Column("create_at", Order = 4)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 5)]
        public DateTime? UpdateAt { get; set; }
        
        public virtual ICollection<InvoiceDetail> Details { get; set; }
        
        public virtual Checkin CheckinInfo { get; set; }

        public static string GenerateID()
        {
            var CurrDate = DateTime.Now.ToString("yyyyMMdd");
            var Prefix = "INV";
            var PrefixID = Prefix + CurrDate;
            var newId = 1;

            using (var context = new DataContext())
            {
                try
                {
                    var chk = (from a in context.Invoices
                               where a.Id.StartsWith(PrefixID)
                               select a.Id).ToList();
                    var transform = (from a in chk
                                     select a.Replace(PrefixID, "")).Select(x => Convert.ToInt32(x)).Max();

                    newId = transform + 1;
                } catch
                {
                    //
                }
            }

            return PrefixID + string.Format("{0:D5}", newId);
        }
    }
}
