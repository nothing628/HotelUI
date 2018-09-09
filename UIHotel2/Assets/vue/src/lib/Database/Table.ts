import { QueryBuilder } from "./SQLGrammar/QueryBuilder";
import { OrderType } from "./SQLGrammar/Enum";

export class Table {
  protected table_name: string;
  protected qry_builder: QueryBuilder = new QueryBuilder();

  constructor() {
    this.table_name = "";
  }

  protected InitQueryBuilder(): void {
    this.qry_builder = new QueryBuilder();
    this.qry_builder.SetTable(this.table_name);
  }

  public Get(): Array<any> {
    return this.qry_builder.Get();
  }

  public GetBuilder(): QueryBuilder {
    return this.qry_builder;
  }
}
