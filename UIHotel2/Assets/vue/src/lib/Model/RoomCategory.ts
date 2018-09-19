import { Table } from "@/lib/Database/Table";

export interface IRoomCategory {
  CategoryName?: string,
  CategoryDescription?: string,
  Id: number
}

export class RoomCategory extends Table {
  constructor(exists: boolean = false) {
    super(exists);
    this.table_name = "roomcategories";
    this.InitQueryBuilder();
  }

  get CategoryName(): string | undefined {
    return this.attributes!.CategoryName;
  }

  set CategoryName(newCode: string | undefined) {
    this.attributes!.CategoryName = newCode;
  }

  protected attributes?: IRoomCategory;

  protected InitQueryBuilder(): void {
    this.qry_builder.SetModel(RoomCategory.NewTableInstance);
    this.qry_builder.SetTable(this.table_name);
  }

  public static NewTableInstance(exists: boolean = false): any {
    return new RoomCategory(exists);
  }
}
