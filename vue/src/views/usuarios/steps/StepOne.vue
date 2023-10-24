<template>
  <div class="d-flex flex-column" style="width: 100%; height: 100%;">

    <div class="d-block background-black" style="width: 100%;">
      <v-form ref="form">
        <div class="d-flex justify-center">

          <alert-component style="width: 70%;" :type="type" :message="message" :active="active"></alert-component>
        </div>
        <div class="d-flex mx-auto" style="width: 70%;">

          <div style="width: 50%;" class="mr-6">

            <v-text-field color="#BFACFF" style="width: 100%;border-color: aqua !important;" outlined dark label="Nome"
              v-model="inputData.name" :rules="[rules.required]" validate-on-blur>
            </v-text-field>
            <v-text-field color="#BFACFF" clearable hint="DD/MM/AAAA" v-mask="'##/##/####'"
              prepend-inner-icon="mdi mdi-account-circle" dark label="Data de Nascimento"
              v-model="inputData.dataNascimento" outlined :rules="[rules.required, rules.date]"
              validate-on-blur></v-text-field>
            <v-text-field color="#BFACFF" style="width: 100%;" outlined dark :rules="[rules.email, rules.required]"
              label="E-mail" v-model="inputData.emailAddress" clearable validate-on-blur>
            </v-text-field>
            <v-select color="#BFACFF" style="width: 100%;" outlined dark label="Tipo plano" v-model="inputData.tipoPlano"
              clearable validate-on-blur>
            </v-select>
          </div>
          <div style="width: 50%;">
            <v-text-field color="#BFACFF" style="width: 100%;" outlined dark label="Sobrenome" v-model="inputData.surname"
              :rules="[rules.required]" validate-on-blur>
            </v-text-field>
            <v-text-field color="#BFACFF" persistent-hint hint="(##) #####-####" clearable
              prepend-inner-icon="mdi mdi-account-circle" dark label="Telefone"
              v-mask="['(##) #####-####', '(##) ####-####']" v-model="inputData.phoneNumber" outlined
              :rules="[rules.required, rules.telefone]" validate-on-blur></v-text-field>
            <v-select color="#BFACFF" :menu-props="{ bottom: true, offsetY: true }" open-on-clear clearable
              prepend-inner-icon="mdi mdi-account-circle" dark label="Sexo" :items="sexo" v-model="inputData.sexo"
              outlined :rules="[rules.required]" validate-on-blur></v-select>
            <v-text-field v-mask="'###.###.###-##'" color="#BFACFF" style="width: 100%;" outlined dark label="Cpf"
              v-model="inputData.cpf" :rules="[rules.required]" validate-on-blur>
            </v-text-field>
          </div>
        </div>
      </v-form>
      <div class="d-flex justify-center">

        <v-btn @click="stepBack" class="background-purple mr-4 white--text">Anterior</v-btn>
        <v-btn :loading="loading" @click="nextStep" class="background-purple mb-4 ml-2 white--text">Próximo</v-btn>
      </div>
    </div>
  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import { mapActions } from "vuex";
import informacoesGerais from "@/views/usuarios/informacoesGerais/informacoesGerais.vue";
import AlertComponent from "../../../components/AlertComponent.vue";
import validationRules from "../../../mixins/validationRules";
import { mask } from 'vue-the-mask'

export default {
  directives: { mask },
  props: ["userIdToGet"],
  data: () => ({
    inputData: {},
    sexo: [
      { value: 1, text: "Masculino" },
      { value: 2, text: "Feminino" }
    ],
    loading: false,
    type: null,
    active: null,
    message: null
  }),

  methods: {
    ...mapActions("ModuleUsuarios", {
      getById: "getById",
      createOrUpdate: "createOrUpdate"
    }),

    async nextStep() {
      if (!this.$refs.form.validate())
        return

      this.loading = true;
      this.inputData.Id = this.$route.params.id ? this.$route.params.id : 0
      this.inputData.UserName = this.inputData.name;
      this.inputData.Imagem = ''
      this.inputData.IsActive = true;

      var result = await this.createOrUpdate(this.inputData);
      if (result.status == 200 || result.success == true) {
        this.$emit('changeStep', 2)
        this.$emit('userId', result.data.result.id)
      }
      else {
        this.setAlert('error', true, result)
      }
      this.loading = false
    },

    stepBack() {
      this.$router.push({ name: 'ViewUsuarios' })
    }

  },
  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionUserEdit);
    },
  },

  mixins: [
    getRouterData,
    validationRules
  ],

  components: {
    TopSection,
    informacoesGerais,
    AlertComponent
  },

  async mounted() {
    if (this.userIdToGet != null) {
      var user = await this.getById(this.userIdToGet);
      this.inputData = user.data.result
    }

    if (this.$route.params.id != null)
    {
      var user = await this.getById(this.$route.params.id);
      this.inputData = user.data.result
    }
  },
};
</script>

<style scoped>
.back {
  background-color: #071422 !important;
  ;
}
</style>

