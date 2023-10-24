<template>
  <div class="d-flex flex-column mx-10 " style="width: 100%; overflow: auto">
    <!-- DIALOG CONFIRM SALVAR -->
    <v-dialog v-model="dialogConfirmSalvar" persistent max-width="450px">
      <div class="white pa-8 d-flex flex-column align-center text-center">
        <v-icon style="font-size: 60px; border-radius: 15px" color="purple">mdi-alert</v-icon>

        <div class="d-flex align-start">
          <h4 class="Academia-neutral--text text--var-1 pb-5 pt-5 mb-4">
            Após salvar não será possível alterar <br />
            o endereço de e-mail do cadastro. <br />
            Deseja continuar?
          </h4>
        </div>

        <div class="d-flex flex-row justify-end">
          <v-btn rounded large text class="mr-8" @click="dialogConfirmSalvar = false">CANCELAR</v-btn>

          <v-btn rounded large class="white--text" style="background: purple" @click="submit"
            :loading="loadingBtnDelete">SALVAR</v-btn>
        </div>
      </div>
    </v-dialog>
    <!-- FIM -->
    <AlertComponent :type="type" :active="active" :message="message"></AlertComponent>


    <top-section></top-section>

    <div class="d-flex justify-space-between align-end background-black">
      <h2 class="Academia-neutral--text ">{{ $route.meta.title }}</h2>

      <div v-if="editPermission" class="d-flex flex-row justify-end align-end">
        <v-btn dense dark rounded class="mr-3" text @click="$router.push(`/home/exercicios`)">
          CANCELAR
        </v-btn>

        <v-btn rounded dense class="Academia-purple white--text" @click="submit()" :loading="loadingBtnSave">SALVAR</v-btn>
      </div>
    </div>


    <v-form ref="form" class="background-black">
      <div class="d-flex">
        <v-text-field class="mr-4 mt-2" solo outlined dark label="Nome" v-model="dataNewItem.nome" validate-on-blur
          :rules="[rules.required]">
        </v-text-field>
      </div>
    </v-form>
  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import crudController from "@/mixins/crudController.js";
import validationRules from "../../mixins/validationRules";
import { mask } from "vue-the-mask";
import AlertComponent from "../../components/AlertComponent.vue";

export default {
  directives: { mask },
  data: () => ({
    dialogConfirmSalvar: null,
    dataNewItem: {},
    type: null,
    message: "",
    active: false,
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionCustomerEdit);
    },
  },

  watch: {
    active() {
      if (this.success) this.$router.push({ path: `/home/exercicios` });
    },
  },

  async mounted() {
    var id = this.$route.params.id;
    if (id) {
      var result = await this.getItemById(id)
      this.dataNewItem = result
    }

  },

  methods: {

    async submit() {
      var id = this.$route.params.id;
      if (!id) {
        this.addNewItem();
      }
      else
        this.updateItem(id);
    },

    buildRows(data) {
      let dataRows = [];

      data.forEach((e) => {
        dataRows.push({
          col1: e.id,
          col2: e.nomeProduto,
          col3: e.codigoProduto,
          col4: e.quantidade,
        });
      });

      this.rows = dataRows;
    },
  },

  mixins: [
    getRouterData,
    crudController({ store: "ModuleExercicios" }),
    validationRules
  ],

  components: {
    TopSection,
    AlertComponent
  },

};
</script>

<style scoped>
.background {
  padding: 20px;
  border-end-start-radius: 6px;
}
</style>
