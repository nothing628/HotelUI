import { JoinType, OrderType } from "./Enum";

export class WhereClause {
  public JoinType: JoinType = JoinType.AND;
  public IsNested: boolean = false;
  public WhereColumn: string = "";
  public WhereOperator: string = "";
  public WhereCondition: string = "";
  public WhereNested: Array<WhereClause> = new Array();

  public AddNested(nested: WhereClause): void {
    this.WhereNested.push(nested);
  }

  public Compile(is_first: boolean = false): string {
    let query_str: string = "";

    if (!is_first) {
      query_str += this.JoinType == JoinType.AND ? "AND " : "OR ";
    }

    if (this.IsNested) {
      query_str += "( ";

      this.WhereNested.forEach((val, idx) => {
        query_str += val.Compile(idx == 0);
      });

      query_str += ") ";
    } else {
      query_str += this.WhereColumn + " ";
      query_str += this.WhereOperator + " ";
      query_str += "'" + this.WhereCondition + "' ";
    }

    return query_str;
  }
}

export class OrderClause {
  public OrderColumn: string = "";
  public OrderType: OrderType = OrderType.ASC;

  public Compile(): string {
    let order = "";

    if (this.OrderType == OrderType.ASC) {
      order = "ASC";
    } else {
      order = "DESC";
    }

    return this.OrderColumn + " " + order;
  }
}
