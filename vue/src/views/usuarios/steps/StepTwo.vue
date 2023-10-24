<template>
  <div class="d-flex flex-column" style="width: 100%; height: 100%;">
    <alert-component :type="type" :message="message" :active="active"></alert-component>

    <div class="d-block background-black" style="width: 100%;">
      <v-form ref="form">
        <div class="d-flex mx-auto" style="width: 70%;">

          <div style="width: 50%;" class="mr-6">

            <v-text-field v-mask="'#####-###'" @blur="consultaComViaCep" color="#BFACFF" style="width: 100%;border-color: aqua !important;" outlined dark label="CEP"
              v-model="inputData.cep" :rules="[rules.required]" validate-on-blur>
            </v-text-field>
            <v-text-field color="#BFACFF" clearable prepend-inner-icon="mdi mdi-account-circle" dark label="Estado"
              v-model="inputData.uf" outlined :rules="[rules.required]" validate-on-blur></v-text-field>
            <v-text-field color="#BFACFF" style="width: 100%;" outlined dark :rules="[rules.required]" label="Rua"
              v-model="inputData.logradouro" clearable validate-on-blur>
            </v-text-field>
          </div>
          <div style="width: 50%;">
            <v-text-field color="#BFACFF" style="width: 100%;" outlined dark label="Cidade" v-model="inputData.localidade"
              :rules="[rules.required]" validate-on-blur>
            </v-text-field>
            <v-text-field color="#BFACFF" clearable prepend-inner-icon="mdi mdi-account-circle" dark label="Bairro"
              v-model="inputData.bairro" outlined :rules="[rules.required]" validate-on-blur></v-text-field>
            <v-text-field color="#BFACFF" :menu-props="{ bottom: true, offsetY: true }" open-on-clear clearable
              prepend-inner-icon="mdi mdi-account-circle" dark label="Complemento" v-model="inputData.complemento"
              outlined :rules="[rules.required]" validate-on-blur></v-text-field>

          </div>
        </div>
      </v-form>
      <div class="d-flex justify-center mb-6">

        <v-btn @click="stepBack" class="background-purple mr-4 white--text">Anterior</v-btn>
        <v-btn :loading="loading" @click="nextStep" class="background-purple ml-2 white--text">Próximo</v-btn>
      </div>
    </div>
  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import crudController from "@/mixins/crudController.js";
import { mapActions } from "vuex";
import informacoesGerais from "@/views/usuarios/informacoesGerais/informacoesGerais.vue";
import AlertComponent from "../../../components/AlertComponent.vue";
import validationRules from "../../../mixins/validationRules";
import buscarEnderecoViaCep from "../../../lib/helpers/viaCepApi";
import { mask } from 'vue-the-mask'

export default {
  directives: { mask },

  data: () => ({
    inputData: {},
    loading: false,
  }),
  props: ["userId", "userIdToGet"],

  methods: {
    ...mapActions("ModuleUsuarioDados", {
      createOrUpdate: "createOrUpdate",
      getByUserId: "getByUserId",
    }),

    async consultaComViaCep() {
      const response = await buscarEnderecoViaCep(this.inputData.cep);
      if (response.error) {
        this.setAlert('error', true, "Cep não encontrado")
        return;
      }
      if (response != null && !response.error) {
        this.inputData = response
      }

    },

    async nextStep() {
      if (!this.$refs.form.validate())
        return
      this.loading = true;
      this.inputData.userId = this.userId;
      let result = await this.createOrUpdate(this.inputData)
      if (result.status == 200)
        this.$emit('changeStep', 3)
      else
        this.setAlert('error', true, result)

      this.loading = false;
    },

    stepBack() {
      this.$emit('changeStep', 1)
      this.$emit('listBack', "")
    }
  },
  async mounted() {
    if (this.userIdToGet != null) {
      var user = await this.getByUserId(this.userIdToGet);
      this.inputData = user.data.result
    }

    if (this.$route.params.id != null)
    {
      var user = await this.getByUserId(this.$route.params.id);
      this.inputData = user.data.result
    }
  },
  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionUserEdit);
    },
  },

  mixins: [
    getRouterData,
    crudController({ store: "ModuleUsuarios" }),
    validationRules
  ],

  components: {
    TopSection,
    informacoesGerais,
    AlertComponent
  },

  watch: {
    active() {
      if (this.success) this.$router.push({ path: `/home/usuarios` });
    },
  },
};
</script>

<style scoped>
.back {
  background-color: #071422 !important;
  ;
}
</style>

