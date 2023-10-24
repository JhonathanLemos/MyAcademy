<template>
  <div>
    <h1 class="Academia-neutral--text">{{ setTituloView }}</h1>

    <div class="d-flex mb-6 mt-12">

      <div class="d-flex" style="width: 45%;">


        <v-select  v-if="showActiveFilter" v-model="active" @click:clear="resetActive" @change="activeSearch" :items="status"
          :menu-props="{ bottom: true, offsetY: true }" open-on-clear clearable style="width: 10px;" label="Status" dark
          outlined dense hide-details class="mr-6 text-field"></v-select>
        <DatePicker v-if="showDateFilter" @change="setDate" :solo="false" class="mr-6" v-model="date" :dense="true" :dark="true" :label="'Data'"
          :outlined="true"></DatePicker>
        <v-text-field class="text-field" color="yellow" dark  style="width: 5vw;"  clearable @click:clear="resetSearch"  hide-details
          :label="labelSearch" prepend-inner-icon="mdi-magnify" v-if="setBusca" @keyup.enter="searchText" v-model="search"
          outlined dense />

      </div>
      <div class="d-flex justify-end" style="width: 65%;">
        <v-btn small class="background-purple white--text elevation-0 ma-2 pa-4" @click="$emit('clickAdd')"
          v-if="!disableButton && setBtnInclude">
          {{ label }}
          <v-icon right>mdi mdi-account-plus</v-icon>
        </v-btn>
      </div>
    </div>
  </div>
</template>

<script>
import DatePicker from './DatePicker.vue';
export default {
  name: "ActionsTable",
  data: () => ({
    search: null,
    date: null,
    active: null,
    status: [
      { value: true, text: "Ativo" },
      { value: false, text: "Inativo" },

    ]
  }),
  props: ["params", "labelSearch", "setSearch", "disableButton", "label", "showActiveFilter", "showDateFilter"],
  methods: {

    searchText() {
      this.$store.dispatch("ModuleGlobal/SET_SEARCH", { search: this.search, active: this.active, date: this.date });
    },
    activeSearch() {
      this.$store.dispatch("ModuleGlobal/SET_SEARCH", { search: this.search, active: this.active, date: this.date });
    },
    setDate() {
      this.$store.dispatch("ModuleGlobal/SET_SEARCH", { search: this.search, active: this.active, date: this.date });
    },
    resetActive() {
      this.$store.dispatch("ModuleGlobal/SET_SEARCH", { search: this.search, active: this.null });
    },
    resetSearch() {
      this.$store.dispatch("ModuleGlobal/SET_SEARCH", { search: this.null, active: this.active });
    },
  },

  watch: {
    setSearch(v) {
      this.search = v;
    },

    date(v) {
      this.setDate()
    }
  },
  computed: {

    setTituloView() {
      return this.params.tituloView;
    },
    setBusca() {
      if (this.params.pesquisar == "true") {
        return true;
      }
      return false;
    },
    setBtnInclude() {
      if (this.params.btnInclude == "true") {
        return true;
      }
      return false;
    },
    setBtnAddViagens() {
      if (this.paramsAddViagens.btnIncludeViagens == "false") {
        return false;
      }
      return true;
    },
  },

  components: { DatePicker },
};
</script>
