<template>
  <div class="d-flex flex-column justify-space-between mx-10" style="width: 100%; height: 98vh">

    <alert-component :type="type" :message="message" :active="active"></alert-component>

    <v-dialog v-model="dialogDeleteConfirm" persistent max-width="450px">
      <div class="white pa-8 d-flex flex-column align-center text-center">
        <v-icon style="font-size: 60px" color="Academia-purple">mdi-alert-circle-outline</v-icon>

        <div class="d-flex align-start">
          <h4 class="Academia-neutral--text text--var-1 pb-5 pt-5">
            Deseja excluir este registro? <br> Esta ação não poderá ser desfeita. Deseja continuar?
          </h4>
        </div>

        <div class="d-flex flex-row justify-end">
          <v-btn rounded large text class="mr-4" @click="dialogDeleteConfirm = false">CANCELAR</v-btn>

          <v-btn rounded large class="Academia-purple white--text" @click="deleteItem(2)"
            :loading="loadingBtnDelete">PROSSEGUIR</v-btn>
        </div>
      </div>
    </v-dialog>

    <top-section></top-section>

    <actions-table :params="getRouterData" :disableButton="!editPermission" v-on:clickAdd="$router.push(`clientes/Add`)"
      labelSearch="Pesquisar por Nome"></actions-table>

    <div class="flex-grow-1 rounded-lg pa-4 elevation-5 background-black">
      <v-data-table dark class="background-black" :key="key" disable-sort :options.sync="options" :headers="headers" :items="rows"
        :server-items-length="totalItems" noDataText="Nenhum registro encontrado" :loading="tableLoading"
        loading-text="Aguarde, carregando clientes..." :footer-props="{
          itemsPerPageOptions: [1, 20],
          itemsPerPageText: 'Itens por Página',
        }">
        <!-- Icone Manutenção -->
        <template v-slot:[`item.col1`]="{ item }">
          <div v-if="editPermission" class="d-flex flex-nowrap">
            <v-btn icon @click="$router.push(`/home/clientes/edit/${item.col1}`)">
              <v-icon> mdi-pencil </v-icon>
            </v-btn>

            <v-btn icon @click="deleteItem(1, item.col1)">
              <v-icon> mdi-delete </v-icon>
            </v-btn>
          </div>

          <div v-else class="d-flex flex-nowrap">

          </div>
        </template>
      </v-data-table>
    </div>
  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import ActionsTable from "@/components/ActionsTable.vue";
import getRouterData from "@/mixins/getRouterData.js";
import dataTable from "@/mixins/dataTable.js";
import crudController from "@/mixins/crudController.js";
import validationRules from "../../mixins/validationRules";
import AlertComponent from "../../components/AlertComponent.vue";

export default {
  data: () => ({
    alertSuccess: false,
    type: null,
    headers: [{
      text: "",
      value: "col1",
      width: "10%"
    },
    {
      text: "Nome",
      value: "col2",
      width: "10%"
    },
    {
      text: "Idade",
      value: "col3",
      width: "20%"
    },
    {
      text: "Telefone",
      value: "col4",
      width: "10%"
    },
    {
      text: "Cnpj",
      value: "col4",
      width: "10%"
    },
    ],
    rows: [],
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionCustomerEdit);
    },
  },

  methods: {

    buildRows(data) {
      let dataRows = [];

      data.forEach((e) => {
        dataRows.push({
          col1: e.id,
          col2: e.nome,
          col3: e.idade,
          col4: e.telefone,
          col5: e.cnpj,
        });
      });

      this.rows = dataRows;
    },
  },

  mixins: [
    validationRules,
    getRouterData,
    dataTable({
      store: "ModuleClientes"
    }),
    crudController({
      store: "ModuleClientes"
    }),
  ],

  components: {
    TopSection,
    ActionsTable,
    AlertComponent
},
};
</script>
