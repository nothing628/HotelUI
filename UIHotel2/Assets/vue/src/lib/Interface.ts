export interface IDB {
  Query(qry: string, params: Array<any>): Array<any>;
  QueryScalar: any;
}

export interface ICS {
  DB: IDB;
}

export interface THasAttribute {
  GetDirty(): object;
  GetAttributes(): object;
  OriginalEquivalent(field: string, value: any): boolean;
}
