import {
  QueryBuilder,
  IQueryable
} from "@/lib/Database/SQLGrammar/QueryBuilder";

export class Table implements IQueryable {
  protected table_name: string;
  protected qry_builder: QueryBuilder;

  constructor() {
    this.table_name = "";
    this.qry_builder = new QueryBuilder();
  }

  public Test(): void {
    this.qry_builder
      .select(["A", "B"])
      .select("C")
      .setTable(this.table_name)
      .nestedWhere(q => {
        q.where("x", "1").where("y", "2");
      })
      .where("z", "12")
      .compile();

    let result = this.qry_builder.compile();
    console.log(result);
  }
}
