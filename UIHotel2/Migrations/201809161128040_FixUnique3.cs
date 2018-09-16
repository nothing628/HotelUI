namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUnique3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("Transactions", "UserId");
            CreateIndex("Transactions", "CategoryId");
            CreateIndex("Users", "Username", unique: true);
            AddForeignKey("Transactions", "CategoryId", "TransactionCategories", "Id", cascadeDelete: true);
            AddForeignKey("Transactions", "UserId", "Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Transactions", "UserId", "Users");
            DropForeignKey("Transactions", "CategoryId", "TransactionCategories");
            DropIndex("Users", new[] { "Username" });
            DropIndex("Transactions", new[] { "CategoryId" });
            DropIndex("Transactions", new[] { "UserId" });
        }
    }
}
