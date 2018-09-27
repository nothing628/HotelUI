<template>
  <div class="row">
    <div class="panel panel-inverse">
      <div class="panel-heading">
        <h4 class="panel-title">List Guest</h4>
      </div>
      <div class="panel-body">
        <div class="dataTables_wrapper form-inline dt-bootstrap no-footer">
          <div class="dt-buttons btn-group">
            <a class="btn btn-success buttons-copy buttons-html5 btn-sm" @click="addData">
              <i class="fa fa-plus"></i>
              <span> Add Room</span>
            </a>
          </div>
          <div class="dataTables_filter">
            <label>Search:<input type="search" class="form-control input-sm" placeholder="" ></label>
          </div>
          <table class="table table-striped table-bordered dataTable no-footer dtr-inline" role="grid">
            <thead>
                <tr role="row">
                  <th rowspan="1" colspan="1">ID Number</th>
                  <th rowspan="1" colspan="1">Fullname</th>
                  <th rowspan="1" colspan="1">Email</th>
                  <th rowspan="1" colspan="1">Telp</th>
                  <th rowspan="1" colspan="1">Address</th>
                  <th rowspan="1" colspan="1"></th>
                </tr>
            </thead>
            <tbody>
              <tr v-for="item in items" :key="item.Id">
                  <td>{{ item.IdNumber }}</td>
                  <td>{{ item.Fullname }}</td>
                  <td>{{ item.Email }}</td>
                  <td>{{ item.Phone1 }} / {{ item.Phone2 }}</td>
                  <td>{{ item.Address }}</td>
                  <td>
                    <div class="btn-group pull-right">
                      <button class="btn btn-sm btn-warning" @click="editData(item)"><i class="fa fa-pencil"></i> Edit</button>
                      <button class="btn btn-sm btn-danger" @click="deleteData(item)"><i class="fa fa-trash"></i> Delete</button>
                    </div>
                  </td>
              </tr>
            </tbody>
          </table>
          <counter :from.sync="from" :to.sync="to" :total.sync="max_item"></counter>
          <pagination :total-page.sync="totalPage" v-model="currentPage"></pagination>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { ss, sd, execute, executeScalar } from "@/lib/Test";
import Pagination from "@/components/Table/Pagination.vue";
import Counter from "@/components/Table/Counter.vue";

@Component({
  components: {
    Pagination,
    Counter
  }
})
export default class DataGuest extends Vue {
  private max_item: number = 0;
  private limit: number = 10;
  private currentPage: number = 1;
  private items: Array<any> = new Array();

  get from(): number {
    return (this.currentPage - 1) * this.limit + 1;
  }

  get to(): number {
    let rest = this.from + 9;

    if (rest > this.max_item) return this.max_item;
    return this.from + 9;
  }

  get totalPage(): number {
    return Math.ceil(this.max_item / this.limit);
  }

  get offset(): number {
    return (this.currentPage - 1) * this.limit;
  }

  addData() {
    this.$router.push({ name: "data.guest.create" });
  }

  editData(item: any) {
    this.$router.push({ name: "data.guest.edit", params: { id: item.Id }});
  }

  deleteData(item: any) {
    let Id = item.Id;
    let qry = sd()
      .from("guests")
      .where("Id = ?", Id);
    let result = executeScalar(qry);

    this.getMaxItem();
    this.getData();
  }

  getMaxItem() {
    let qry = ss()
      .from("guests")
      .field("COUNT(*) as cnt");
    let result = execute(qry);
    let first = result[0];

    this.max_item = Number(first.cnt);
  }

  getData() {
    let qry = ss()
      .from("guests")
      .limit(this.limit)
      .offset(this.offset);
    let result = execute(qry);

    this.items = [];
    result.forEach((item: any) => {
      this.items.push(item);
    });
  }

  mounted() {
    this.$store.commit("changeTitle", "List Guest");
    this.$store.commit("changeSubtitle", "");
    this.getMaxItem();
    this.getData();
  }

  @Watch("currentPage")
  currentPageChanged(newval: number, oldval: number) {
    this.getData();
  }
}
</script>
