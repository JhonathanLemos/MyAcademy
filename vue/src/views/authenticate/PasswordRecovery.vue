<template>
  <v-app>
    <div class="d-flex mb-3">
      <div v-if="isTokenInvalid">
        <h2>O link que voce tentou acessar está indisponível!</h2>
        <h2>Por favor, faça uma nova solicitação!</h2>
      </div>
      <div v-else>
        <AlertComponent :type="type" :active="active" :message="message"></AlertComponent>
        <h1>Redefinição de Senha</h1>

        <v-form class="justify-center" @submit="submitPasswordReset" style="width: 50%;">
          <v-text-field outlined @blur="validatePasswords()" type="password" :rules="[rules.strongPassword]"
            label="Nova Senha" v-model="newPassword"></v-text-field>

          <v-text-field outlined @blur="validatePasswords()" type="password" :rules="[rules.strongPassword, validatePass]"
            label="Confirmar Senha" v-model="confirmPassword"></v-text-field>

          <v-btn class="Academia-purple white--text" :loading="loadingBtnSave" @click="submitPasswordReset">Enviar</v-btn>
        </v-form>
      </div>
    </div>
  </v-app>
</template>

<script>
import { mapActions } from 'vuex';
import validationRules from '@/mixins/validationRules';
import AlertComponent from '../../components/AlertComponent.vue';
export default {
  data() {
    return {
      newPassword: "",
      confirmPassword: "",
      validatePass: null,
      isTokenInvalid: null,
      active: false,
      type: null,
      message: null
    };
  },
  mixins: [validationRules],
  components: [AlertComponent],
  methods: {
    ...mapActions("ModuleUsuarios", {
      resetPassword: "resetPassword",
      validateRecoveryToken: "validateRecoveryToken",
    }),
    async submitPasswordReset() {
      var model = {
        UserId: this.$route.params.id,
        Code: this.$route.params.code,
        NewPassword: this.newPassword
      };
      var result = await this.resetPassword(model);
      if (result.result) {
        this.setAlert("success", true, "Senha redefinida, volte a pagina de login!");
        // setTimeout(() => {
        //   this.$router.push('/login')
        // }, 5000);
      }
    },
    validatePasswords() {
      if (this.newPassword != this.confirmPassword)
        this.validatePass = "Senhas não combinam";
      else
        this.validatePass = null;
    },
    async validateToken() {
      var model = { UserId: this.$route.params.id, Code: this.$route.params.code };
      var result = await this.validateRecoveryToken(model);
      if (!result.result)
        this.isTokenInvalid = true;
    },

  },
  mounted() {
    this.validateToken();
  },
  components: { AlertComponent }
};
</script>
