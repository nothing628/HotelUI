import { Table } from "@/lib/Database/Table";

export class User extends Table {
  constructor(exists: boolean = false) {
    super(exists);
    this.table_name = "User";
    this.InitQueryBuilder();
  }

  protected InitQueryBuilder(): void {
    this.qry_builder.SetModel(User.NewTableInstance);
    this.qry_builder.SetTable(this.table_name);
  }

  public static NewTableInstance(exists: boolean = false): any {
    return new User(exists);
  }
}
