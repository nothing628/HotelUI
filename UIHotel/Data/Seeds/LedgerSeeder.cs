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
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Cash", Icon = "local_atm", Color = "light-green darken-3", IsExpense = false, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Income", Icon = "get_app", Color = "blue darken-3", IsExpense = false, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Salary", Icon = "group", Color = "yellow darken-4", IsExpense = false, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Food & Drinks", Icon = "restaurant", Color = "cyan darken-3", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Transportation", Icon = "local_airport", Color = "deep-orange darken-4", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Comunication", Icon = "local_phone", Color = "deep-purple darken-4", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Tax", Icon = "public", Color = "green darken-4", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Utilities", Icon = "build", Color = "amber darken-3", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Insurance", Icon = "business_center", Color = "brown darken-4", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Loan", Icon = "card_giftcard", Color = "cyan darken-2", IsExpense = true, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Uncategorized", Icon = "all_inclusive", Color = "black", IsExpense = false, });
            context.LedgerCategories.Add(new LedgerCategory() { Description = "Uncategorized", Icon = "all_inclusive", Color = "black", IsExpense = true, });
            context.SaveChanges();

            context.LedgerLogs.Add(new LedgerLog() {
                Id = LedgerLog.GenerateID(),
                IdCategory = 1,
                Date = DateTime.Now,
                Debit = 0,
                Kredit = 0,
                IsClose = true,
                Description = "Initial Cash",
                UpdateAt = DateTime.Now,
                CreateAt = DateTime.Now
            });
        }
    }
}
