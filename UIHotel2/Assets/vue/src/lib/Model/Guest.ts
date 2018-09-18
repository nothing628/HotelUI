import { Table } from "@/lib/Database/Table";

export class Guest extends Table {
  constructor(exists: boolean = false) {
    super(exists);
    this.table_name = "Guest";
    this.InitQueryBuilder();
  }

  protected InitQueryBuilder(): void {
    this.qry_builder.SetModel(Guest.NewTableInstance);
    this.qry_builder.SetTable(this.table_name);
  }

  public static NewTableInstance(exists: boolean = false): any {
    return new Guest(exists);
  }
}