<template>
  <v-dialog persistent v-model="display" max-width="600px">
    <div class="white pa-8 d-flex flex-column">
      <AlertComponent :type="type" :active="active" :message="message"></AlertComponent>
      <div class="d-flex flex-row justify-space-between mb-3">
        <h2>Verifique seu E-mail</h2>
        <v-btn text small fab>
          <v-icon @click="close()">mdi-close</v-icon>
        </v-btn>
      </div>

      <div class="mt-5">
        <v-form ref="form">
          <v-text-field :rules="[rules.emailCode]" v-mask="'######'" type="text" label="Código de verificação"
            v-model="code" />

        </v-form>
      </div>

      <div class="mt-2" style="text-align: right">
        <v-btn class="Academia-purple white--text" :loading="loadingBtnSave" @click="validateEmailCode()">Salvar</v-btn>
      </div>
    </div>
  </v-dialog>
</template>

<script>
import { mask } from 'vue-the-mask'
import { mapActions } from "vuex";
import validationRules from '@/mixins/validationRules';
import AlertComponent from "../../components/AlertComponent.vue"
import login from '@/mixins/login';
export default {

  props: ["value", "user", "msgError"],

  data: () => ({
    code: null,
    inputData: {},
    loadingBtnSave: false,
    loadBtnSave: null,
    emailCode: null,
    type: null,
    active: null,
    message: null,
  }),
  methods: {
    props: ['user'],
    ...mapActions("ModuleUsuarios", {
      create: "create",
      generateCodeToValidateUser: "generateCodeToValidateUser",
      verifyEmailAndCode: "verifyEmailAndCode",
      validateEmail: "validateEmail",
    }),

    close() {
      this.$emit('input', false)
    },

    async validateEmailCode() {
      this.loadingBtnSave = true;
      this.emailCode = { Code: this.code, UserId: this.userInfos.id }
      var result = await this.verifyEmailAndCode(this.emailCode)
      if (result.result) {
        this.setAlert('success', true, "Seu e-mail foi validado, em breve você será redirecionado")
        setTimeout(() => {
          this.loginUserAndValidateEmail()
        }, 5000);
      }
      else {
        this.loadingBtnSave = false;
        this.setAlert('error', true, "Código de verificação inválido")
      }
    },

    async loginUserAndValidateEmail() {
      this.inputData = { userNameOrEmailAddress: this.user.emailAddress, password: this.user.password, rememberClient: true }
      await this.validateEmailMethod();

      await this.tryAuthenticate(true)

    },

    async validateEmailMethod() {
      var userAndCode = { Code: this.code, UserId: this.user.id }
      await this.validateEmail(userAndCode);
    },
  },

  computed: {
    display() {
      return this.value;
    },

    userInfos() {
      return this.user;
    }
  },

  watch: {
    msgError() {
      this.setAlert('success', true, "Verifique seu email, após validar sua conta voce será redirecionado e logado")
    }
  },

  directives: { mask },
  components: { AlertComponent },
  mixins: [validationRules, login],
};
</script>

<style></style>
