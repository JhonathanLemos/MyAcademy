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
    <AlertComponentVue :type="type" :active="active" :message="message"></AlertComponentVue>


    <top-section></top-section>

    <div class="d-flex justify-space-between align-end background-black">
      <h2 class="Academia-neutral--text">{{ $route.meta.title }}</h2>

      <div v-if="editPermission" class="d-flex flex-row justify-end align-end">
        <v-btn dense rounded class="mr-3 white--text" text @click="$router.push(`/home/clientes`)">
          CANCELAR
        </v-btn>

        <v-btn rounded dense class="Academia-purple white--text" @click="submit()" :loading="loadingBtnSave">SALVAR</v-btn>
      </div>
    </div>


    <v-form ref="form" class="background-black">
      <div class="d-flex">
        <v-text-field dark solo outlined class="mr-4 mt-2" label="Nome" v-model="dataNewItem.nome" validate-on-blur
          :rules="[rules.required]">
        </v-text-field>
        <v-text-field dark solo outlined type="number" class="mr-4 mt-2" label="Idade" v-model="dataNewItem.idade" validate-on-blur
          :rules="[rules.required]">
        </v-text-field>
      </div>
      <div class="d-flex ">
        <v-text-field dark solo outlined class="mr-4 mt-2" label="Telefone" v-model="dataNewItem.telefone" validate-on-blur
          v-mask="['(##) ####-####', '(##) #####-####']" :rules="[rules.telefone, rules.required]">
        </v-text-field>
        <v-text-field dark solo outlined class="mr-4 mt-2" label="Cnpj" v-model="dataNewItem.cnpj" validate-on-blur
          :rules="[rules.required, cnpj]">
        </v-text-field>
      </div>
    </v-form>

    <div v-if="false" class="background mt-3">
      <h2>Produtos</h2>
      <div class="flex-grow-1 rounded-lg pa-4 white elevation-5">
        <v-data-table :key="key" disable-sort :options.sync="options" :headers="headers" :items="rows"
          :server-items-length="totalItems" noDataText="Nenhum registro encontrado" :loading="tableLoading"
          loading-text="Aguarde, carregando produtos..." :footer-props="{
            itemsPerPageOptions: [10, 20],
            itemsPerPageText: 'Itens por Página',
          }">
          <!-- Icone Manutenção -->
          <!-- <template v-slot:[`item.col1`]="{ item }">
            <div v-if="editPermission" class="d-flex flex-nowrap">
              <v-btn icon @click="$router.push(`/home/produtos/edit/${item.col1}`)">
                <v-icon> mdi-pencil </v-icon>
              </v-btn>

              <v-btn icon @click="deleteItem(1, item.col1)">
                <v-icon> mdi-delete </v-icon>
              </v-btn>
            </div>

            <div v-else class="d-flex flex-nowrap">

            </div>
          </template> -->
        </v-data-table>
      </div>
    </div>
  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import crudController from "@/mixins/crudController.js";
import validationRules from "../../mixins/validationRules";
import { mask } from "vue-the-mask";
import AlertComponentVue from "../../components/AlertComponent.vue";
import { createNamespacedHelpers } from "vuex";
const moduleCustomer = createNamespacedHelpers("ModuleClientes");

export default {
  directives: { mask },
  data: () => ({
    dialogConfirmSalvar: null,
    dataNewItem: {},
    totalItems: 10,
    rows: [],
    tableLoading: false,
    options: {},
    headers: [{
      text: "",
      value: "",
      width: "10%"
    },
    {
      text: "Nome",
      value: "col2",
      width: "10%"
    },
    {
      text: "Código produto",
      value: "col3",
      width: "20%"
    },
    {
      text: "Quantidade",
      value: "col4",
      width: "20%"
    },
    ],
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionCustomerEdit);
    },
  },

  watch: {
    active() {
      if (this.success) this.$router.push({ path: `/home/clientes` });
    },

    options: {
      handler() {
        if (this.$route.params.id)
        this.getProdutos(this.options);
      },
      deep: true,
    },
  },

  async mounted() {
    var id = this.$route.params.id;
    if (id) {
      var result = await this.getItemById(id)
      this.dataNewItem = result
      this.getProdutos(this.options);
    }

  },

  methods: {
    ...moduleCustomer.mapActions({
      getProductsByCustomerId: "getProductsByCustomerId"
    }),

    async getProdutos(v) {
      v.customerId = parseInt(this.$route.params.id);
      var result = await this.getProductsByCustomerId(v);
      this.buildRows(result.data.result)
    },

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
    crudController({ store: "ModuleClientes" }),
    validationRules
  ],

  components: {
    TopSection,
    AlertComponentVue
  },

};
</script>

<style scoped>
.background {
  background-color: white;
  padding: 20px;
  border-end-start-radius: 6px;
}
</style>
