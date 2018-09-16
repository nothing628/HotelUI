namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "TransactionCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsIncome = c.Boolean(nullable: false),
                        CategoryName = c.String(maxLength: 40, storeType: "nvarchar"),
                        CategoryIcon = c.String(maxLength: 40, storeType: "nvarchar"),
                        CategoryColor = c.String(maxLength: 6, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Transactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TransactionAt = c.DateTime(nullable: false, precision: 0),
                        IsClosed = c.Boolean(nullable: false),
                        AmmountIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmmountOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.String(maxLength: 40, storeType: "nvarchar"),
                        Password = c.String(maxLength: 1024, storeType: "nvarchar"),
                        Level = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("Users");
            DropTable("Transactions");
            DropTable("TransactionCategories");
        }
    }
}
