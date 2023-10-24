<template>
  <v-app class="white pa-8 d-flex flex-column" style="width: 500px;">
    <div class="d-flex justify-center mb-3">
      <h2>Cadastro de usuários</h2>
      <v-btn text small fab>
        <v-icon @click="close()">mdi-close</v-icon>
      </v-btn>
    </div>


    <AlertComponent :type="type" :active="active" :message="message"></AlertComponent>
    <ValidateEmail :user="user" v-model="openModalValidateEmail"> </ValidateEmail>

    <div class="mt-5">
      <v-form ref="form">
        <v-text-field :rules="[rules.email]" type="email" label="E-mail" v-model="inputData.EmailAddress" />

        <v-text-field :append-icon="showPass ? 'mdi-eye' : 'mdi-eye-off'" :type="showPass ? 'text' : 'password'"
          @blur="validatePasswords()" @click:append="showPass = !showPass" label="Insira a nova senha"
          class="mb-5 mt-4 pt-2 input-none-case" style="margin-top: -20px; font-size: 14px" v-model="inputData.Password"
          :rules="[rules.strongPassword]" hint="Deve conter no mínimo 6 caracteres, sendo ao menos um numérico"
          validate-on-blur clearable></v-text-field>

        <v-text-field class="mt-5 input-none-case" label="Confirme a nova senha"
          :append-icon="showConfirmPass ? 'mdi-eye' : 'mdi-eye-off'" :type="showConfirmPass ? 'text' : 'password'"
          @blur="validatePasswords()" @click:append="showConfirmPass = !showConfirmPass"
          style="margin-top: -20px; font-size: 14px" v-model="inputData.confirmPassword"
          :rules="[rules.strongPassword, validatePass]" validate-on-blur clearable></v-text-field>
      </v-form>
    </div>

    <div class="mt-2" style="text-align: right">
      <v-btn class="Academia-purple white--text" :loading="loadingBtnSave" @click="validateAndSavePassword">Salvar</v-btn>
    </div>
  </v-app>
</template>

<script>
import { mapActions } from "vuex";
import validationRules from '@/mixins/validationRules';
import AlertComponent from "../components/AlertComponent.vue"
import ValidateEmail from "../views/usuarios/ValidateEmail.vue";
export default {
  data: () => ({
    validatePass: null,
    inputData: {},
    showPass: false,
    showConfirmPass: false,
    newPassword: null,
    confirmPassword: null,
    loadingBtnSave: null,
    openModalValidateEmail: false,
    email: null,
    type: null,
    active: null,
    message: null,
    user: null
  }),
  methods: {
    ...mapActions("ModuleUsuarios", {
      create: "create",
      generateCodeToValidateUser: "generateCodeToValidateUser",
      validateEmail: "validateEmail",
      getByEmailOrUserName: "getByEmailOrUserName"
    }),
    ...mapActions("ModuleSession", {
      login: "login",
    }),
    close() {
      this.$emit("input", false);
    },
    async validateAndSavePassword() {
      this.inputData.Name = this.inputData.EmailAddress
      this.loadingBtnSave = true
      var result = await this.create(this.inputData);
      if (result.success == true) {
        result.result.password = this.inputData.Password
        this.user = result.result;
        this.generateCodeToValidateUser(result.result)
        this.openModalValidateEmail = true;
      }
      else {
        this.setAlert('error', true, result)
      }

      this.loadingBtnSave = false;
    },
    validatePasswords() {
      if (this.inputData.confirmPassword != this.inputData.Password)
        this.validatePass = "Senhas não combinam";
      else
        this.validatePass = null;
    }
  },
  computed: {
    display: {
      get() {
        return this.value;
      },
      set(val) {
        this.$emit("input", val);
      },
    },
  },

  watch: {
    async user(value) {

    }
  },
  mixins: [validationRules],
  components: { AlertComponent, ValidateEmail }
};
</script>

<style></style>
