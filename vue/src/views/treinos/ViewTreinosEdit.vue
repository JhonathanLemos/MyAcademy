<template>
  <div class="d-flex flex-column mx-10 " style="width: 100%; overflow: auto">
    <v-dialog v-model="dialogDeleteConfirm" persistent max-width="450px">
      <div class="white pa-8 d-flex flex-column align-center text-center">
        <span style="font-size: 60px" class="mdi mdi-alert-circle-outline gradient-span"></span>
        <div class="d-flex align-start">
          <h4 class="Academia-neutral--text text--var-1 pb-5 pt-5">
            Deseja excluir este registro? <br>
            Esta ação não poderá ser desfeita. Deseja continuar?
          </h4>
        </div>

        <div class="d-flex flex-row justify-end">
          <v-btn rounded large text class="mr-4" @click="dialogDeleteConfirm = false">CANCELAR</v-btn>

          <v-btn rounded large class="gradient" @click="removeItem" :loading="loadingBtnDelete">PROSSEGUIR</v-btn>
        </div>
      </div>
    </v-dialog>
    <AlertComponentVue :type="type" :active="active" :message="message"></AlertComponentVue>


    <top-section></top-section>

    <div class="d-flex background-black" style="width: 100%;">

      <div class="d-flex" style="width: 92%;">
        <v-progress-circular style="margin-left: 180px; margin-right: 50px;" :size="200" color="#FFEC44"
          :indeterminate="loading">
          <v-avatar size="190">
            <v-img :src="imagemPerfil" alt="User" v-if="imagemPerfil != null"></v-img>
            <span v-else class="icone mdi mdi-camera"></span></v-avatar>
        </v-progress-circular>

        <div>
          <h1 class="white--text mt-3">{{ this.dataNewItem.name | capitalize }} <v-chip label
              :color="this.dataNewItem.isActive ? 'yellow' : ''" outlined style="font-weight: 500">
              {{ dataNewItem.isActive ? "Ativo" : "Inativo" }}
            </v-chip></h1>
          <h3 class="Academia-neutral--text text--var-3">{{ this.dataNewItem.emailAddress }}</h3>
          <h3 class="white--text">Matricula: {{ this.dataNewItem.id }}</h3>
          <h3 class="white--text">Objetivo: {{ this.objetivo | enumObjetivo }}</h3>
        </div>
      </div>

      <div class="d-flex justify-end">
        <v-btn dark class="background-purple mt-6">Imprimir</v-btn>
      </div>

    </div>
    <div class="background-black">

      <v-container fluid>
        <v-row justify="center">
          <v-carousel :continuous="false" style="width: 90%;height: 200px;" v-model="paginaAtual" :value="paginaAtual"
            :hide-delimiter-background="true" hide-delimiters>
            <v-carousel-item v-for="pagina in paginas" :key="pagina[0].toString()">
              <v-sheet class="teste" color="#071422" height="100%" tile>
                <v-row style="width: 90%;margin: auto; height: 200px;" class="fill-height" align="center">
                  <v-col @click="selecionarDia(dia)" class="white--text coluna" v-for="dia in pagina"
                    :key="dia.toString()">
                    {{ diaAtual }}
                    <div :class="setClass(dia)">
                      {{ dia | formatarDiaSemana }}<br>
                      {{ dia | formatarData }}
                      {{ dia | formatarMes }}
                      <p></p>
                    </div>
                  </v-col>
                </v-row>
              </v-sheet>
            </v-carousel-item>
          </v-carousel>
        </v-row>
      </v-container>
    </div>


    <div class="d-flex justify-end background-black">
      <v-btn @click="addNewItemOnList" dark class="mr-12 background-purple"> <v-icon>mdi mdi-plus</v-icon></v-btn>
    </div>
    <div class="background-black" style="width: 100%">
      <v-data-table @click:row="editTable" hide-default-footer style="width: 100%;margin: auto;" dark
        class="background-black" :key="key" disable-sort :options.sync="options" :headers="headers" :items="rows"
        :server-items-length="totalItems" noDataText="Nenhum registro encontrado" :loading="loadingTable"
        loading-text="Aguarde, carregando clientes..." :footer-props="{
          itemsPerPageOptions: [10, 20],
          itemsPerPageText: 'Itens por Página',
        }">


        <template  v-slot:[`item.col2`]="{ item }">
          <v-icon v-if="item.col2" color="green">mdi-check</v-icon>
          <v-icon v-else color="red">mdi-alert</v-icon>

        </template>
        <template  v-slot:[`item.col3`]="{ item }">
          <h3 v-if="!item.col6">{{ item.col3 }}</h3>
          <v-autocomplete :menu-props="{ bottom: true, offsetY: true }" open-on-clear clearable :items="exerciseList"
            style="width: 80%" v-else v-model="table.exercicioId"></v-autocomplete>
        </template>
        <!-- Icone Manutenção -->
        <template  v-slot:[`item.col4`]="{ item }">
          <h3 v-if="!item.col6">{{ item.col4.item1 + " séries de " + item.col4.item2 + " repetições" }}</h3>
          <div v-else class="d-flex">
            <v-text-field style="width: 40px" v-model="table.series"></v-text-field>
            <p style="margin: auto; margin-left: 5px;margin-right: 5px;">séries</p> <v-text-field style="width: 40px"
              v-model="table.repeticoes"></v-text-field>
            <p style="margin: auto;width: 100%;margin-right: 5px;">repetições</p>
          </div>
        </template>

        <template  v-slot:[`item.col5`]="{ item }" v-slot:activator="{ on, attrs }">
          <v-btn v-if='!item.col6' icon @click="editTable(item)">
            <v-icon> mdi-pencil </v-icon>
          </v-btn>


          <v-menu bottom left v-if='!item.col6'>
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon @click="removeItemFromList(item)" v-bind="attrs" v-on="on">
                <v-icon> mdi-delete </v-icon>
              </v-btn>
            </template>
          </v-menu>

          <v-btn v-if='item.col6' icon @click="saveItemFromTable(item)">
            <v-icon> mdi-check </v-icon>
          </v-btn>
          <v-menu bottom left v-if='item.col6'>
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon @click="item.col6 = false" v-bind="attrs" v-on="on">
                <v-icon> mdi-close </v-icon>
              </v-btn>
            </template>
          </v-menu>
        </template>

      </v-data-table>
    </div>
  </div>
</template>

<script>
import { format, addDays, subDays } from "date-fns";
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import crudController from "@/mixins/crudController.js";
import validationRules from "../../mixins/validationRules";
import { mask } from "vue-the-mask";
import AlertComponentVue from "../../components/AlertComponent.vue";
import { mapActions } from "vuex";
export default {
  directives: { mask },
  data: () => ({
    dialogConfirmSalvar: null,
    paginaAtual: 3,
    numeroDiasPorPagina: 7,
    numeroPaginas: 7,
    dataNewItem: {},
    table: {},
    colunaBackground: 'colunaBackground',
    colunaNormal: 'colunaNormal',
    totalItems: 10,
    rows: [],
    itemToRemove: null,
    loadingTable: false,
    dialogDeleteConfirm: false,
    loading: false,
    options: {},
    exerciseList: [],
    diaAtual: null,
    usersList: [],
    imagemPerfil: null,
    diaSelecionadoDateFormat: null,
    objetivo: null,
    diaSelecionado: null,
    listaDeTreinos: {},
    headers: [
      { text: "", value: "col1", width: "5%" },
      { text: "", value: "col2", width: "5%" },
      { text: "", value: "col3", width: "40%" },
      { text: "", value: "col4", width: "40%" },
      { text: "", value: "col5", width: "10%" },
    ],
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionCustomerEdit);
    },

    paginas() {
      const dias = [];
      let diaCorrente = new Date();
      diaCorrente = addDays(diaCorrente, -24)
      for (let i = 0; i < this.numeroPaginas; i++) {
        const pagina = [];

        for (let j = 0; j < this.numeroDiasPorPagina; j++) {
          pagina.push(diaCorrente);
          diaCorrente = addDays(diaCorrente, 1);
        }
        dias.push(pagina);
      }
      return dias;
    },
  },

  async mounted() {

    await this.getExercices()

    await this.listAll()

  },
  filters: {
    formatarData(data) {
      return format(data, "dd");
    },

    formatarMes(data) {
      const meses = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
      return ((meses[(data.getMonth())]));
    },

    formatarDiaSemana(data) {
      return data.toLocaleString('pt-BR', { weekday: 'short' });
    },
  },
  methods: {
    ...mapActions("ModuleTreinos", {
      getExercicesList: "getExercicesList",
      create: "create",
      updateTreino: "update",
      getTreinosByDataAndIdMethod: "getTreinosByDataAndId",
      removeItemMethod: "delete"
    }),

    ...mapActions("ModuleUsuarios", {
      getObjetivo: "getObjetivo",
    }),

    removeItemFromList(v) {
      this.itemToRemove = v;
      this.dialogDeleteConfirm = true;
    },

    async removeItem() {

      var list = this.listaDeTreinos.find(x => x.index == this.itemToRemove.col8)
      if (list.id != null) {
        await this.removeItemMethod(list.id)
        await this.getTreinosByDataAndId(this.diaSelecionadoDateFormat, this.$route.params.id);
      }
      else
        this.listaDeTreinos = this.listaDeTreinos.filter(x => x.index != this.itemToRemove.col8);

      this.buildRows(this.listaDeTreinos)
      this.dialogDeleteConfirm = false;
    },
    async listAll() {
      var id = this.$route.params.id;
      if (id) {
        var result = await this.getItemById(id)
        this.dataNewItem = result
        this.diaSelecionado = format(new Date(), "dd/MM")
        this.diaSelecionadoDateFormat = new Date();
        this.setImagem()
        await this.setObjetivo();
        await this.getTreinosByDataAndId(new Date(), this.$route.params.id);

      }
    },

    formatDate(date) {
      const ano = date.getFullYear();
      const mes = String(date.getMonth() + 1).padStart(2, '0')
      const dia = date.getDate();
      const hora = date.getHours();
      const minuto = String(date.getMinutes()).padStart(2, '0');
      const segundo = date.getSeconds();
      return `${ano}-${mes}-${dia}T${hora}:${minuto}:${segundo}`;
    },

    async getTreinosByDataAndId(date, id) {
      var criacao = this.formatDate(date);
      var result = await this.getTreinosByDataAndIdMethod({ criacao: criacao, id: this.$route.params.id });
      if (result.status == 200) {
        this.dataNewItem.treinos = result.data.result
        this.listaDeTreinos = result.data.result

        var index = 0;
        this.listaDeTreinos.forEach(element => {
          element.index = index;
          index++;
        });
      }
      this.buildRows(this.listaDeTreinos)
      this.loadingTable = false
      // this.selecionarDia(new Date())
    },

    setImagem() {
      if (this.dataNewItem.imagem != null && this.dataNewItem.imagem != "")
        this.imagemPerfil = "data:image/jpg;base64," + this.dataNewItem.imagem;
    },

    async setObjetivo() {

      var resultObjetivo = await this.getObjetivo(this.dataNewItem.id)
      if (resultObjetivo.status == 200)
        this.objetivo = resultObjetivo.data.result;
    },

    editTable(v) {
      v.col6 = true;
      this.table.exercicio = v.col3

    },
    selecionarDia(v) {
      this.loadingTable = true;
      this.diaSelecionado = format(v, 'dd/MM')
      this.diaSelecionadoDateFormat = v;
      this.getTreinosByDataAndId(v, this.$route.params.id)
    },

    async saveItemFromTable(v) {
      this.table.criacao = v.col7
      this.table.isCreated = true
      this.table.nome = ''
      this.table.userId = this.$route.params.id
      if (v.col5 != null) {
        this.table.id = v.col5;
        var resultCreate = await this.updateTreino(this.table)
      }
      else
        var resultCreate = await this.create(this.table)

      if (resultCreate.status == 200) {

        this.table = {}
        this.getTreinosByDataAndId(this.diaSelecionadoDateFormat, this.$route.params.id)
      }
    },

    setClass(dia) {
      var dia = format(dia, 'dd/MM')

      if (dia == this.diaSelecionado)
        return 'colunaBackground mb-1'
      else
        return 'colunaNormal'
    },

    async getExercices() {
      var result = await this.getExercicesList();
      result.data.result

      result.data.result.forEach(element => {
        this.exerciseList.push({ value: element.id, text: element.nome })
      });
    },


    addNewItemOnList() {
      let data = this.diaSelecionado.split('/')
      let newDate = new Date();
      const ano = newDate.getFullYear();
      const hora = newDate.getHours();
      const minuto = String(newDate.getMinutes()).padStart(2, '0');
      const segundo = newDate.getSeconds();

      let criacao = `${ano}-${data[1]}-${data[0]}T${hora}:${minuto}:${segundo}`;
      this.listaDeTreinos.push({ criacao: criacao, series: "_", repeticoes: "_", nome: "Exercicio", userId: this.$route.params.id, exercicioNome: "Exercicio" })

      let index = 0;
      this.listaDeTreinos.forEach(element => {
        element.index = index;
        index++;
      });
      this.buildRows(this.listaDeTreinos)
    },

    buildRows(data) {
      let dataRows = [];
      data.forEach((e) => {
        dataRows.push({
          col1: "",
          col2: e.isCreated,
          col3: e.exercicioNome,
          col4: { item1: e.series, item2: e.repeticoes },
          col5: e.id,
          col6: e.editando = false,
          col7: e.criacao,
          col8: e.index
        });
      });

      this.rows = dataRows;
    },
  },

  mixins: [
    getRouterData,
    crudController({ store: "ModuleUsuarios" }),
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

.v-carousel__controls {
  margin: 0;
  /* Remove a margem padrão dos controles do carrossel */
}

.colunaNormal {
  text-align: center;
  width: 45%;
  margin: auto;
  font-size: 17px;
}

.colunaBackground {
  text-align: center;
  width: 45%;
  background-color: #BFACFF;
  border-radius: 10px;
  padding-top: 5%;
  padding-bottom: 1%;
  font-size: 17px;
  margin: auto;
}

.coluna :hover {
  cursor: pointer;
}
</style>
