using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIHotel.Data.Table;

namespace UIHotel.Data.Seeds
{
    public class LedgerSeeder : DBSeeder
    {
        public override void Run(DataContext context)
        {
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Cash", IsExpense = false, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Income", IsExpense = false, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Salary", IsExpense = false, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Food & Drinks", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Transportation", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Comunication", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Tax", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Utilities", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Insurance", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Loan", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Uncategorized", IsExpense = false, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Uncategorized", IsExpense = true, });
            context.SaveChanges();

            context.LedgerLogs.Add(new LedgerLog() {
                Id = LedgerLog.GenerateID(),
                IdCategory = 1,
                Date = DateTime.Now,
                Debit = 0,
                Kredit = 0,
                Description = "Initial Cash",
                UpdateAt = DateTime.Now,
                CreateAt = DateTime.Now
            });
        }
    }
}
