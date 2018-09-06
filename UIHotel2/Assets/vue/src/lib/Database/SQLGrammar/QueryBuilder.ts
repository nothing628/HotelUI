import { isArray } from "util";
import { OrderType, JoinType } from "./Enum";
import { WhereClause, OrderClause } from "./Clause";

export interface IQueryable {
  //
}

export class QueryBuilder {
  private table_name: string = "";
  private where_arr: Array<WhereClause> = new Array();
  private select_arr: Array<string> = new Array();
  private order_arr: Array<OrderClause> = new Array();
  private skip?: number;
  private take?: number;

  public where(
    column_name: string,
    condition: string,
    condional_operator?: string
  ): QueryBuilder {
    let where_cls = new WhereClause();

    where_cls.JoinType = JoinType.AND;
    where_cls.WhereColumn = column_name;
    // eslint-disable-next-line
    where_cls.WhereOperator = condional_operator === undefined ? "=" : condional_operator;
    where_cls.WhereCondition = condition;

    this.where_arr.push(where_cls);
    return this;
  }

  public orWhere(
    column_name: string,
    condition: string,
    condional_operator?: string
  ): QueryBuilder {
    let where_cls = new WhereClause();

    where_cls.JoinType = JoinType.OR;
    where_cls.WhereColumn = column_name;
    // eslint-disable-next-line
    where_cls.WhereOperator = condional_operator === undefined ? "=" : condional_operator;
    where_cls.WhereCondition = condition;

    this.where_arr.push(where_cls);
    return this;
  }

  public nestedWhere(query_fn: (query: QueryBuilder) => void): QueryBuilder {
    var newBuilder = new QueryBuilder();
    var where_cls = new WhereClause();

    query_fn(newBuilder);

    where_cls.IsNested = true;
    where_cls.JoinType = JoinType.AND;

    var nested_result = newBuilder.where_arr;
    nested_result.forEach(where => {
      where_cls.AddNested(where);
    });

    this.where_arr.push(where_cls);

    return this;
  }

  public select(column_name: string | Array<string>): QueryBuilder {
    if (isArray(column_name)) {
      column_name.forEach(item => this.select_arr.push(item));
    } else {
      this.select_arr.push(column_name);
    }

    return this;
  }

  public orderBy(column_name: string, order_type?: OrderType): QueryBuilder {
    let order_cs = new OrderClause();
    order_cs.OrderColumn = column_name;
    // eslint-disable-next-line
    order_cs.OrderType = order_type === undefined ? OrderType.ASC : order_type;
    this.order_arr.push(order_cs);

    return this;
  }

  public setTable(table_name: string): QueryBuilder {
    this.table_name = table_name;
    return this;
  }

  private compileSelect(query_str: string): string {
    let select_str = "SELECT ";
    let select_count = this.select_arr.length;

    if (select_count == 0) {
      select_str += "*";
    } else {
      this.select_arr.forEach((select, idx) => {
        // eslint-disable-next-line
        select_str += (idx > 0) ? ", " : "";
        select_str += select;
      });
    }

    select_str += " ";

    return select_str;
  }

  private compileFrom(query_str: string): string {
    let from_str = "FROM " + this.table_name + " ";

    return query_str + from_str;
  }

  private compileWhere(query_str: string): string {
    if (this.where_arr.length == 0) {
      return query_str + "WHERE 1 = 1";
    }

    let where_str = "WHERE ";

    this.where_arr.forEach((where, idx) => {
      where_str += where.Compile(idx == 0);
    });

    return query_str + where_str;
  }

  private compileOrder(query_str: string): string {
    if (this.order_arr.length == 0) {
      return query_str;
    }

    let order_str = "ORDER BY ";

    this.order_arr.forEach((order, idx) => {
      // eslint-disable-next-line
      order_str += idx > 0 ? ", " : "";
      order_str += order.Compile();
    });

    return query_str + order_str + " ";
  }

  public compile(): string {
    let result = "";

    result = this.compileSelect(result);
    result = this.compileFrom(result);
    result = this.compileWhere(result);
    result = this.compileOrder(result);

    return result;
  }
}
