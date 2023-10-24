import { createNamespacedHelpers } from "vuex";
export default ({
  store = "",
  module = createNamespacedHelpers(store),
} = {}) => ({
  data: () => ({
    displayModalErrorText: null,
    displayModalError: null,
    dialogDeleteConfirm: false,
    dataNewItem: { status: true, tripStatus: true },
    dataDeleteItem: null,
    loadingBtnSave: false,
    loadingBtnDelete: false,
    active: false,
    message: "",
    type: null,
    success: null,
    key: 1,
    valid: true,
    validationRules: { required: (value) => !!value || "Campo obrigatório" },
  }),

  methods: {
    ...module.mapActions(["getById", "create", "update", "delete"]),

    async addNewItem () {
        this.loadingBtnSave = true;
        if (!this.$refs.form || this.$refs.form.validate()) {
            let response = await this.create(this.dataNewItem);
            if (response.status == 200) {
              this.loadingBtnSave = false;
              this.key++;
              this.setAlert("Registro cadastrado com sucesso!", "success")
              setTimeout(() => {
                this.active = false;
              }, 10000);
            } else {
              this.loadingBtnSave = false;
             this.setAlert(response.error.message, "error")
            }

        }
        this.loadingBtnSave = false;
      },

    async updateItem () {
        this.loadingBtnSave = true;
        if (!this.$refs.form || this.$refs.form.validate()) {
          let response = await this.update(this.dataNewItem);
          if (response.status == 200) {
            this.loadingBtnSave = false;
            this.key++;
            this.setAlert("Registro alterado com sucesso!", "success")
          } else {
            this.loadingBtnSave = false;
            this.setAlert(response.error.message, "error")
          }
        } else {
          this.loadingBtnSave = false;
        }
      },

    async deleteItem (stage, id) {
      if (stage == 1) {
        this.dataDeleteItem = id;
        this.dialogDeleteConfirm = true;
      } else if (stage == 2) {
        this.loadingBtnDelete = true;
        var response = await this.delete(this.dataDeleteItem)
        if (response.status == 200){
          this.loadingBtnDelete = false;
              this.key++;
              this.dialogDeleteConfirm = false;
              this.dataDeleteItem = null;
              this.setAlert("Registro removido com sucesso!", "success")
            }
        else{
          this.setAlert(response.error.message, "error")
          this.loadingBtnDelete = false;
          this.dialogDeleteConfirm = false;
          this.dataDeleteItem = null;
        }
      }
    },

    async getItemById(id){
      var result = await this.getById(id);
      if (result.status == 200){
        return result.data.result
      }
    },

    setAlert(message, type){
      this.type = type,
      this.message = message
      this.active = true;

      setTimeout(() => {
        this.active = false
        if (type == "success")
        this.success = true
      }, 2000);
    }
  },
});
