import { Table } from "@/lib/Database/Table";

export class Guest extends Table {
  constructor() {
    super();
    this.table_name = "Guest";
  }
}
