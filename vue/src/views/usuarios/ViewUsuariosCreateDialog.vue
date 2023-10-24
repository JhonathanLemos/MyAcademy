<template>
  <v-dialog v-model="display" max-width="50%" persistent class="dialog">
    <alert-component :type="type" :message="message" :active="active"></alert-component>

    <div class="background-black">
      <div class="d-flex mt-5 mb-5">

        <h1 class="ma-auto mt-11 mb-11">Criar Usuário</h1>
        <v-btn class="ma-2 pa-2 mt-12" text small fab>
          <v-icon class="white--text" @click="display = false">mdi-close</v-icon>
        </v-btn>
      </div>
      <v-form ref="form">
        <div class="d-flex mx-auto" style="width: 90%;">

          <div style="width: 50%;" class="mr-6">

            <v-text-field color="#BFACFF" style="width: 100%;border-color: aqua !important;" outlined  dark  :disabled="readonly" :clearable="!readonly"
              label="Nome" v-model="inputData.name" :rules="[rules.required]" validate-on-blur>
            </v-text-field>
            <v-text-field color="#BFACFF" clearable hint="DD/MM/AAAA" v-mask="'##/##/####'"
              prepend-inner-icon="mdi mdi-account-circle" dark  label="Data de Nascimento"
              v-model="inputData.dataNascimento" outlined :rules="[rules.required, rules.date]"
              validate-on-blur></v-text-field>
            <v-text-field color="#BFACFF" style="width: 100%;" outlined dark  :rules="[rules.email, rules.required]"
              label="E-mail" v-model="inputData.emailAddress" clearable validate-on-blur>
            </v-text-field>
          </div>
          <div style="width: 50%;">
            <v-text-field color="#BFACFF" style="width: 100%;" outlined  dark  :disabled="readonly" :clearable="!readonly"
              label="Sobrenome" v-model="inputData.surname" :rules="[rules.required]" validate-on-blur>
            </v-text-field>
            <v-text-field color="#BFACFF" persistent-hint hint="(##) #####-####"  clearable
              prepend-inner-icon="mdi mdi-account-circle" dark  label="Telefone"
              v-mask="['(##) #####-####', '(##) ####-####']" v-model="inputData.phoneNumber" outlined
              :rules="[rules.required, rules.telefone]" validate-on-blur></v-text-field>
            <v-select color="#BFACFF" :menu-props="{ bottom: true, offsetY: true }" open-on-clear clearable
              prepend-inner-icon="mdi mdi-account-circle" dark label="Sexo"  :items="sexo" v-model="inputData.sexo"
              outlined :rules="[rules.required]" validate-on-blur></v-select>
          </div>
        </div>
      </v-form>
      <div class="d-flex mt-5 mb-12">

        <v-btn :loading="loading" @click="createOrUpdateUser" large  class="background-purple white--text ma-auto mb-12">{{ id != null ? "Editar Usuário" : "Criar Usuário"}}</v-btn>
      </div>
    </div>
  </v-dialog>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import { mapActions } from "vuex";
import informacoesGeraisDialog from "@/views/usuarios/informacoesGerais/informacoesGeraisDialog.vue";
import perfilUsuario from "./permissoes/perfilUsuario.vue";
import AlertComponent from "../../components/AlertComponent.vue";
import crudController from "@/mixins/crudController.js";
import validationRules from "../../mixins/validationRules";
import { mask } from "vue-the-mask";

export default {
  directives: { mask },
  props: [
    'value',
    'id',
    "user",
    "readonly",
  ],
  data: () => ({
    expandedPanel: null,
    status: true,
    loading: false,
    disabled: true,
    validaEdicao: null,
    search: "",
    reset: null,
    sexo: [
      { value: 1, text: "Masculino" },
      { value: 2, text: "Feminino" }
    ],
    Editar: false,
    inputData: {  }
  }),
  methods: {
    ...mapActions("ModuleUsuarios", {
      create: "create",
    }),
    async createOrUpdateUser() {
      if (!this.$refs.form.validate())
        return;
      this.loading = true;
      this.inputData.IsActive = true;
      if (this.id != null)
        var result = await this.update(this.inputData)
        else
        var result = await this.create(this.inputData)
      this.loading = false;
      if (result.success || result.status == 200)
      {
        this.display = false;
      this.$emit('reset', true)
      }
    }
  },
  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionUserEdit);
    },

    display: {
      get() {
        return this.value
      },
      set(val) {
        this.id = null;
        this.inputData = {}
        this.$emit('input', val)
      },
    },
  },
  watch: {
    active() {
      if (this.success) this.$router.push({ path: `/home/usuarios` });
    },
    async id() {
      if (this.id != null)
      {
        var result = await this.getItemById(this.id)
        this.inputData = result
      }
    },
  },

  mixins: [
    getRouterData,
    crudController({ store: "ModuleUsuarios" }),
    validationRules
  ],

  components: {
    TopSection,
    informacoesGeraisDialog,
    perfilUsuario,
    AlertComponent
  },

};
</script>
<style scoped>
.row {
  display: flex;
  align-items: center;
  flex-wrap: nowrap;
  gap: 40%;
}
</style>
