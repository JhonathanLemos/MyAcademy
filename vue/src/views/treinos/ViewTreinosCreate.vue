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
        <v-btn dense rounded class="mr-3 white--text" text @click="$router.push(`/home/treinos`)">
          CANCELAR
        </v-btn>

        <v-btn rounded dense class="background-purple white--text" @click="submit()"
          :loading="loadingBtnSave">SALVAR</v-btn>
      </div>
    </div>


    <v-form ref="form" class="background-black">
      <div class="d-flex">
        <v-text-field dark solo outlined class="mr-4 mt-2" label="Nome" v-model="dataNewItem.nome" validate-on-blur
          :rules="[rules.required]">
        </v-text-field>
        <v-text-field dark solo outlined type="number" class="mr-4 mt-2" label="Repetições"
          v-model="dataNewItem.repeticoes" validate-on-blur :rules="[rules.required]">
        </v-text-field>
      </div>
      <div class="d-flex ">
        <v-text-field dark solo outlined class="mr-4 mt-2" label="Series" v-model="dataNewItem.series" validate-on-blur
          :rules="[rules.required]">
        </v-text-field>
        <v-autocomplete :items="exerciseList" class="mt-2 mr-4" :menu-props="{ bottom: true, offsetY: true }" open-on-clear
          clearable label="Exercicio" dark outlined solo v-model="dataNewItem.exercicioId"></v-autocomplete>
        <v-autocomplete :items="usersList" class="mt-2 mr-4" :menu-props="{ bottom: true, offsetY: true }" open-on-clear clearable
          label="Usuario" dark outlined solo v-model="dataNewItem.userId"></v-autocomplete>
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
import { mapActions } from "vuex";
export default {
  directives: { mask },
  data: () => ({
    dialogConfirmSalvar: null,
    dataNewItem: {},
    totalItems: 10,
    rows: [],
    tableLoading: false,
    options: {},
    exerciseList: [],
    usersList: [],
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionCustomerEdit);
    },
  },

  watch: {
    active() {
      if (this.success) this.$router.push({ path: `/home/treinos` });
    },
  },

  async mounted() {
    await this.getExercices()
    await this.getUsers()

    var id = this.$route.params.id;
    if (id) {
      var result = await this.getItemById(id)
      this.dataNewItem = result
    }

  },

  methods: {
    ...mapActions("ModuleTreinos", {
      getExercicesList: "getExercicesList",
      getUsersList: "getUsersList",
    }),



    async getExercices() {
      var result = await this.getExercicesList();
      result.data.result

      result.data.result.forEach(element => {
        this.exerciseList.push({ value: element.id, text: element.nome })
      });
    },
    async getUsers() {
      var result = await this.getUsersList();
      result.data.result

      result.data.result.forEach(element => {
        this.usersList.push({ value: element.id, text: element.name })
      });
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
    crudController({ store: "ModuleTreinos" }),
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
