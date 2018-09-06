import {
  QueryBuilder,
  IQueryable
} from "@/lib/Database/SQLGrammar/QueryBuilder";
import { OrderType } from '@/lib/Database/SQLGrammar/Enum';

export class Table implements IQueryable {
  protected table_name: string;
  protected qry_builder: QueryBuilder;

  constructor() {
    this.table_name = "";
    this.qry_builder = new QueryBuilder();
  }

  public Test(): void {
    this.qry_builder
      .setTable(this.table_name)
      .select(["A", "B"])
      .select("C")
      .nestedWhere(q => {
        q.where("x", "1").where("y", "2", ">");
      })
      .where("z", "12")
      .orderBy("A")
      .orderBy("B", OrderType.DESC)
      .compile();

    let result = this.qry_builder.compile();
    console.log(result);
  }
}
