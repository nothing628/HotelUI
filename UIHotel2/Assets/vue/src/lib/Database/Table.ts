import { QueryBuilder } from "./SQLGrammar/QueryBuilder";
import { OrderType } from "./SQLGrammar/Enum";
import { THasAttribute } from "@/lib/Interface";

export class Table implements THasAttribute {
  public GetDirty(): any[] {
    throw new Error("Method not implemented.");
  }
  public GetAttributes(): object {
    throw new Error("Method not implemented.");
  }
  public OriginalEquivalent(): boolean {
    throw new Error("Method not implemented.");
  }

  protected table_name: string;
  protected primary_key: string;
  protected qry_builder: QueryBuilder;
  protected attributes?: any;
  protected original?: any;
  protected is_exists: boolean;

  constructor(exists: boolean = false) {
    this.qry_builder = new QueryBuilder();
    this.table_name = "";
    this.primary_key = "id";
    this.is_exists = exists;
  }

  protected InitQueryBuilder(): void {
    this.qry_builder.SetModel(Table.NewTableInstance);
    this.qry_builder.SetTable(this.table_name);
  }

  public Get(): Array<any> {
    return this.GetBuilder().Get();
  }

  public First(): any {
    return this.GetBuilder().First();
  }

  public Update(): boolean {
    return true;
  }

  public Delete(): boolean {
    return false;
  }

  public GetBuilder(): QueryBuilder {
    return this.qry_builder;
  }

  public static Get(): Array<any> {
    let table: any = this.NewTableInstance();
    return table.Get();
  }

  public static First(): any {
    let table: any = this.NewTableInstance();
    return table.First();
  }

  public static NewTableInstance(exists: boolean = false): any {
    return new this(exists);
  }

  public NewFromBuilder(data: object): Table {
    this.SetRawAttributes(data, true);
    return this;
  }

  public SetRawAttributes(data: object, sync: boolean = false): Table {
    this.attributes = data;

    if (sync) {
      this.SyncOriginal();
    }

    return this;
  }

  public SyncOriginal(): Table {
    this.original = this.attributes;

    return this;
  }
}
