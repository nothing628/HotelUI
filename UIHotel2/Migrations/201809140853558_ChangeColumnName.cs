namespace UIHotel2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "Guests", name: "id_number", newName: "Id", anonymousArguments: new { columnType = "nvarchar" });
            RenameColumn(table: "Guests", name: "id_kind", newName: "IdKind", anonymousArguments: new { columnType = "nvarchar" });
            DropPrimaryKey("Guests");
            AddColumn("Guests", "BirthDay", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("Guests", "Fullname", c => c.String(nullable: false, maxLength: 60, storeType: "nvarchar"));
            AddColumn("Guests", "Email", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AddColumn("Guests", "IsVIP", c => c.Boolean(nullable: false));
            AddColumn("Guests", "BirthPlace", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("Guests", "Phone1", c => c.String(nullable: false, maxLength: 15, storeType: "nvarchar"));
            AddColumn("Guests", "Phone2", c => c.String(maxLength: 15, storeType: "nvarchar"));
            AddColumn("Guests", "Address", c => c.String(maxLength: 255, storeType: "nvarchar"));
            AddColumn("Guests", "City", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("Guests", "Province", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("Guests", "State", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("Guests", "PostCode", c => c.String(maxLength: 10, storeType: "nvarchar"));
            AddColumn("Guests", "PhotoDoc", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
            AddColumn("Guests", "PhotoGuest", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AddColumn("Guests", "CreateAt", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("Guests", "UpdateAt", c => c.DateTime(precision: 0));
            AlterColumn("Guests", "Id", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("Guests", "IdKind", c => c.String(maxLength: 10, unicode: false));
            AddPrimaryKey("Guests", new string[] { "Id", "IdKind" });
            DropColumn("Guests", "Name");
        }
        
        public override void Down()
        {
            AddColumn("Guests", "Name", c => c.String(maxLength: 60, storeType: "nvarchar"));
            DropPrimaryKey("Guests");
            AlterColumn("Guests", "IdKind", c => c.String(nullable: false, maxLength: 10, storeType: "nvarchar"));
            AlterColumn("Guests", "Id", c => c.String(nullable: false, maxLength: 30, storeType: "nvarchar"));
            DropColumn("Guests", "UpdateAt");
            DropColumn("Guests", "CreateAt");
            DropColumn("Guests", "PhotoGuest");
            DropColumn("Guests", "PhotoDoc");
            DropColumn("Guests", "PostCode");
            DropColumn("Guests", "State");
            DropColumn("Guests", "Province");
            DropColumn("Guests", "City");
            DropColumn("Guests", "Address");
            DropColumn("Guests", "Phone2");
            DropColumn("Guests", "Phone1");
            DropColumn("Guests", "BirthPlace");
            DropColumn("Guests", "IsVIP");
            DropColumn("Guests", "Email");
            DropColumn("Guests", "Fullname");
            DropColumn("Guests", "BirthDay");
            AddPrimaryKey("Guests", new[] { "id_number", "id_kind" });
            RenameColumn(table: "Guests", name: "IdKind", newName: "id_kind");
            RenameColumn(table: "Guests", name: "Id", newName: "id_number");
        }
    }
}
