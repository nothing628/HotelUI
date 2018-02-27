using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UIHotel.Data.Table
{
    [Table("ledger_log")]
    public class LedgerLog
    {
        [Column("id", Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(16)]
        public string Id { get; set; }

        [Column("id_category", Order = 1)]
        public long IdCategory { get; set; }

        [Column("description", Order = 3)]
        [StringLength(512)]
        public string Description { get; set; }

        [Column("date_transaction", Order = 2)]
        public DateTime Date { get; set; }

        [Column("debit", Order = 4)]
        public decimal Debit { get; set; }

        [Column("kredit", Order = 5)]
        public decimal Kredit { get; set; }

        [Column("create_at", Order = 6)]
        public DateTime CreateAt { get; set; }

        [Column("update_at", Order = 7)]
        public DateTime? UpdateAt { get; set; }

        [NotMapped]
        public bool IsExpense
        {
            get => Kredit > Debit;
        }

        private LedgerCategory _Category;

        [NotMapped]
        public LedgerCategory Category
        {
            get
            {
                if (_Category == null)
                {
                    using (var model = new DataContext())
                    {
                        var category = (from a in model.LedgerCategories
                                        where a.Id == IdCategory
                                        select a).FirstOrDefault();

                        if (category != null)
                            _Category = category;
                        else
                            _Category = new LedgerCategory();
                    }
                }

                return _Category;
            }
        }

        public static string GenerateID()
        {
            using (var context = new DataContext())
            {
                var CurrDate = DateTime.Now.ToString("yyyyMMdd");
                var Prefix = "ACT";
                var PrefixID = Prefix + CurrDate;
                var newId = 1;

                try
                {
                    var chk = (from a in context.LedgerLogs
                               where a.Id.StartsWith(PrefixID)
                               select a.Id).ToList();
                    var trans = chk.Select(x => x.Replace(PrefixID, "")).Select(x => Convert.ToInt32(x)).Max();

                    newId = trans + 1;
                }
                catch
                {

                }

                return PrefixID + string.Format("{0:D5}", newId);
            }
        }
    }
}
