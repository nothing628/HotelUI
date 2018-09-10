import { isArray } from "util";
import { OrderType, JoinType } from "./Enum";
import { WhereClause, WhereCompileParam, OrderClause } from "./Clause";
import { Table } from "./../Table";

export class QueryBuilder {
  private model_construct?: (exists: boolean) => any;
  private table_name: string = "";
  private where_arr: Array<WhereClause> = new Array();
  private select_arr: Array<string> = new Array();
  private order_arr: Array<OrderClause> = new Array();
  private skip?: number;
  private take?: number;

  constructor() {}

  public SetModel(constructors: (exists: boolean) => any): QueryBuilder {
    this.model_construct = constructors;
    return this;
  }

  public Where(
    column_name: string,
    condition: any,
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

  public OrWhere(
    column_name: string,
    condition: any,
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

  public NestedWhere(query_fn: (query: QueryBuilder) => void): QueryBuilder {
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

  public Select(column_name: string | Array<string>): QueryBuilder {
    if (isArray(column_name)) {
      column_name.forEach(item => this.select_arr.push(item));
    } else {
      this.select_arr.push(column_name);
    }

    return this;
  }

  public OrderBy(column_name: string, order_type?: OrderType): QueryBuilder {
    let order_cs = new OrderClause();
    order_cs.OrderColumn = column_name;
    // eslint-disable-next-line
    order_cs.OrderType = order_type === undefined ? OrderType.ASC : order_type;
    this.order_arr.push(order_cs);

    return this;
  }

  public SetTable(table_name: string): QueryBuilder {
    this.table_name = table_name;
    return this;
  }

  public Offset(count: number): QueryBuilder {
    this.skip = count;
    return this;
  }

  public Limit(count: number): QueryBuilder {
    this.take = count;
    return this;
  }

  private CompileSelect(query_str: string): string {
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

  private CompileFrom(query_str: string): string {
    let from_str = "FROM " + this.table_name + " ";

    return query_str + from_str;
  }

  private CompileWhere(query_str: string): string {
    if (this.where_arr.length == 0) {
      return query_str + "WHERE 1 = 1";
    }

    // eslint-disable-next-line
    let params = new WhereCompileParam();
    let where_str = "WHERE ";
    params.param_num = 0;

    this.where_arr.forEach((where, idx) => {
      params.is_first = idx == 0;
      where_str += where.Compile(params);
    });

    return query_str + where_str;
  }

  private CompileOrder(query_str: string): string {
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

  private CompileLimit(query_str: string): string {
    let limit_str: string = "";

    if (this.take !== undefined) {
      limit_str += "LIMIT " + this.take + " ";
    }

    if (this.skip !== undefined && this.take !== undefined) {
      limit_str += "OFFSET " + this.skip + " ";
    }

    return query_str + limit_str;
  }

  public CompileSql(): string {
    let result = "";

    result = this.CompileSelect(result);
    result = this.CompileFrom(result);
    result = this.CompileWhere(result);
    result = this.CompileOrder(result);
    result = this.CompileLimit(result);

    return result;
  }

  public GetParams(): Array<any> {
    let result = new Array();

    this.where_arr.forEach((where, idx) => {
      result = result.concat(where.Params());
    });

    return result;
  }

  public Get(): Array<any> {
    var compiledSql: string = this.CompileSql();
    var params: Array<any> = this.GetParams();
    var result: Array<any> = window.CS.DB.Query(compiledSql, params);
    var result_model: Array<any> = new Array();

    result.forEach(item => {
      let newInstance = this.model_construct!(true);
      let fromBuilder = newInstance.NewFromBuilder(item);
      result_model.push(fromBuilder);
    });

    return result_model;
  }

  public First(): any {
    try {
      return this.FirstOrFail();
    } catch (ex) {
      return null;
    }
  }

  public FirstOrFail(): any {
    this.skip = undefined;
    this.take = 1;
    var result = this.Get();

    if (result.length == 1) {
      return result[0];
    }

    throw new Error("The element not found!");
  }
}
