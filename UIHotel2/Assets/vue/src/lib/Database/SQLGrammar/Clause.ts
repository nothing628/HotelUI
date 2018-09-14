import { JoinType, OrderType } from "./Enum";

export class WhereCompileParam {
  public param_num: number = 0;
  public is_first: boolean = false;
}

export class WhereClause {
  public JoinType: JoinType = JoinType.AND;
  public IsNested: boolean = false;
  public WhereColumn: string = "";
  public WhereOperator: string = "";
  public WhereCondition: any = "";
  public WhereNested: Array<WhereClause> = new Array();

  public AddNested(nested: WhereClause): void {
    this.WhereNested.push(nested);
  }

  public Compile(params: WhereCompileParam): string {
    let query_str: string = "";
    let param_num: number = params.param_num;
    let is_first: boolean = params.is_first;
    let param_name: string = "@param" + param_num;

    if (!is_first) {
      query_str += this.JoinType === JoinType.AND ? "AND " : "OR ";
    }

    if (this.IsNested) {
      query_str += "( ";

      this.WhereNested.forEach((val, idx) => {
        var new_param: WhereCompileParam = new WhereCompileParam();
        new_param.is_first = idx === 0;
        new_param.param_num = params.param_num;

        query_str += val.Compile(new_param);
        params.param_num = new_param.param_num;
      });

      query_str += ") ";
    } else {
      query_str += this.WhereColumn + " ";
      query_str += this.WhereOperator + " ";
      query_str += param_name + " ";
      params.param_num++;
    }

    return query_str;
  }

  public Params(): Array<any> {
    let return_val: Array<any> = new Array();

    if (this.IsNested) {
      this.WhereNested.forEach((val, idx) => {
        return_val = return_val.concat(val.Params());
      });
    } else {
      return_val.push(this.WhereCondition);
    }

    return return_val;
  }
}

export class OrderClause {
  public OrderColumn: string = "";
  public OrderType: OrderType = OrderType.ASC;

  public Compile(): string {
    let order: string = "";

    if (this.OrderType === OrderType.ASC) {
      order = "ASC";
    } else {
      order = "DESC";
    }

    return this.OrderColumn + " " + order;
  }
}
