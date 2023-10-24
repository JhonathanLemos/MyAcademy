<template>
  <div class="d-flex flex-column justify-space-between mx-10" style="width: 100%; height: 98vh">
    <alert-component></alert-component>
    <!-- DIALOG CONFIRM DELETE -->
    <v-dialog v-model="dialogDeleteConfirm" persistent max-width="450px">
      <div class="white pa-8 d-flex flex-column align-center text-center">
        <span  style="font-size: 60px" class="mdi mdi-alert-circle-outline gradient-span"></span>
        <div class="d-flex align-start">
          <h4 class="Academia-neutral--text text--var-1 pb-5 pt-5">
            Deseja excluir este registro? <br>
            Esta ação não poderá ser desfeita. Deseja continuar?
          </h4>
        </div>

        <div class="d-flex flex-row justify-end">
          <v-btn rounded large text class="mr-4" @click="dialogDeleteConfirm = false">CANCELAR</v-btn>

          <v-btn rounded large class="gradient" @click="deleteItem(2)"
            :loading="loadingBtnDelete">PROSSEGUIR</v-btn>
        </div>
      </div>
    </v-dialog>
    <ViewUsuariosCreateDialog @reset="reset" :id="id" v-model="openDialogUser"></ViewUsuariosCreateDialog>
    <top-section></top-section>

    <actions-table :showActiveFilter="true" :showDateFilter="true" :label="label" :params="getRouterData"
      :disableButton="!editPermission" v-on:clickAdd="$router.push({path: 'usuarios/Add'})"
      labelSearch="Pesquisar por Nome"></actions-table>

    <div class="flex-grow-1 rounded-lg pa-4 background-black elevation-5">
      <v-data-table dark class="background-black" :key="key" disable-sort :headers="headers" @click:row="clickRow"
        :items="rows" :options.sync="options" :page="page" :server-items-length="totalItems"
        noDataText="Nenhum registro encontrado" :loading="tableLoading" loading-text="Aguarde, carregando usuários..."
        :footer-props="{
          itemsPerPageOptions: [10, 50, 100],
          itemsPerPageText: 'Itens por Página',
        }">
        <!-- Icone Manutenção -->
        <template v-slot:[`item.col4`]="{ item }" v-slot:activator="{ on, attrs }">
            <v-btn  icon @click="$router.push((`/home/usuarios/edit/${item.col4}`))">
              <v-icon> mdi-pencil </v-icon>
            </v-btn>
            <v-menu bottom left>
            <template v-slot:activator="{ on, attrs }">
              <v-btn @click="deleteItem(1, item.col4)" v-bind="attrs" v-on="on" icon>
                <v-icon color="#DD3434"> mdi-delete </v-icon>
              </v-btn>
            </template>
          </v-menu>
        </template>

        <template v-slot:[`item.col1`]="{ item }">
          <v-chip :color="item.col1.color" outlined style="font-weight: 500">
            {{ item.col1.text }}
          </v-chip>
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
import ViewUsuariosCreateDialog from "./ViewUsuariosCreateDialog.vue";
export default {
  data: () => ({
    headers: [
      { text: "Status", value: "col1", width: "10%" },
      { text: "Nome", value: "col2", width: "10%" },
      { text: "E-mail", value: "col3", width: "20%" },
      { text: "", value: "col4", width: "10%" },

    ],
    id: null,
    rows: [],
    openDialogUser: false,
    label: "Criar Usuário"
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionUserEdit);
    },
  },

  watch: {
    openDialogUser() {
      if (!this.openDialogUser)
        this.id = null
    }
  },

  methods: {
    buildRows(data) {
      let dataRows = [];
      data.forEach((e) => {
        dataRows.push({
          col1: {
            text: e.isActive === true ? "Ativo" : "Inativo",
            color: e.isActive === true ? "#f5d234" : "#DD3434",
          },
          col2: e.name + " " + e.surname,
          col3: e.emailAddress,
          col4: e.id,

        });
      });

      this.rows = dataRows;
    },

    clickRow(item) {
      this.$router.push({path: 'usuarios/Add'})
      this.id = item.col4;
    },

    reset() {
      this.loadItems(this.options, null)
    }
  },

  mixins: [
    validationRules,
    getRouterData,
    dataTable({ store: "ModuleUsuarios" }),
    crudController({ store: "ModuleUsuarios" }),
  ],
  components: { TopSection, ActionsTable, AlertComponent, ViewUsuariosCreateDialog },
};
</script>

