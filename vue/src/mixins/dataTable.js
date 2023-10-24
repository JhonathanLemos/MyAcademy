import { createNamespacedHelpers } from "vuex";
import { mapState } from "vuex";

export default ({
  store = "",
  module = createNamespacedHelpers(store),
} = {}) => ({
  data: () => ({
    page: 1,
    totalItems: 0,
    options: {},
    tableLoading: false,
    setSearch: null,
    reload: null,
  }),

  computed: {
    ...mapState("ModuleGlobal", { search: "search", active: "active" }),
  },

  watch: {
    options: {
      handler() {
        this.loadItems(this.options, this.search);
      },
      deep: true,
    },
    search: {
      handler() {
        this.loadItems(this.options, this.search);
      },
      deep: true,
    },
    reload:{
      handler() {
        this.loadItems(this.options, this.search);
      },
      deep: true,
    }
  },

  methods: {
    ...module.mapActions(["getAll"]),

    async loadItems(options, search) {

      var page = null;
      var itemsPerPage = null;
      var pageNumber = null;

      page = options.page;
      itemsPerPage = options.itemsPerPage;
      pageNumber = page > 0 ? page - 1 : 0;

      this.tableLoading = true;
      let response = await this.getAll({ pageNumber, itemsPerPage, search });
      if (response.status == 200) {
        this.totalItems = response.data.result.totalCount;
        this.numberOfPages = Math.ceil(this.totalItems / itemsPerPage);
        this.buildRows(response.data.result.items);
        this.tableLoading = false;
      } else {
        this.tableLoading = false;
      }
      this.$route.params.isUpdate = false;
    },
  },
});
