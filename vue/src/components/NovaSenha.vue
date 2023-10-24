<template>
  <v-dialog persistent v-model="display" max-width="450px">
    <div class="white pa-8 d-flex flex-column">
      <div class="d-flex flex-row justify-space-between mb-3">
        <h2>Redefinir uma nova senha</h2>
        <v-btn text small fab>
          <v-icon @click="close()">mdi-close</v-icon>
        </v-btn>
      </div>

      <v-alert
        dismissible
        dense
        type="warning"
        transition="scroll-y-transition"
        icon="mdi-alert-outline"
        v-show="alertWarning"
        >{{ alertWarningText }}</v-alert
      >

      <div class="mt-5">
        <v-form ref="form">
          <v-text-field
            :append-icon="showPass ? 'mdi-eye' : 'mdi-eye-off'"
            :type="showPass ? 'text' : 'password'"
            @click:append="showPass = !showPass"
            label="Insira a nova senha"
            class="mb-5 pt-2 input-none-case"
            style="margin-top: -20px; font-size: 14px"
            v-model="newPassword"
            :rules="[rules.strongPassword]"
            hint="Deve conter no mínimo 6 caracteres, sendo ao menos um numérico"
            validate-on-blur
            clearable
          ></v-text-field>

          <v-text-field
            class="mt-5 input-none-case"
            label="Confirme a nova senha"
            :append-icon="showConfirmPass ? 'mdi-eye' : 'mdi-eye-off'"
            :type="showConfirmPass ? 'text' : 'password'"
            @click:append="showConfirmPass = !showConfirmPass"
            style="margin-top: -20px; font-size: 14px"
            v-model="confirmPassword"
            :rules="[rules.strongPassword]"
            validate-on-blur
            clearable
          ></v-text-field>
        </v-form>
      </div>

      <div class="mt-2" style="text-align: right">
        <v-btn
          class="Academia-purple white--text"
          :loading="loadingBtnSave"
          @click="validateAndSavePassword"
          >Salvar</v-btn
        >
      </div>
    </div>
  </v-dialog>
</template>

<script>
import { mapActions } from "vuex";
import validationRules from "../mixins/validationRules"
export default {
  props: {
    value: null,
    object: null,
  },

  mixins: [validationRules],

  data: () => ({
    showPass: false,
    showConfirmPass: false,
    newPassword: null,
    confirmPassword: null,
    alertWarning: null,
    alertWarningText: null,
    loadingBtnSave: null,
    loadBtnSave: null,
  }),

  methods: {
    ...mapActions("ModuleUsuarios", {
      resetPassword: "resetPassword",
    }),

    ...mapActions("ModuleSession", {
      login: "login",
      logout: "logout",
    }),

    close() {
      this.display = false;
      this.$emit('closeModal');
    },

    async validateAndSavePassword() {
      if (this.newPassword != this.confirmPassword) {
        this.alertWarning = true;
        this.alertWarningText = "Senhas não combinam!";
        setTimeout(() => {
          this.alertWarning = false;
        }, 5000);
      } else {
        var teste = {
          CurrentPassword: this.object.password,
          NewPassword: this.newPassword,
        };
        var responseLogin = await this.login(this.object);
        if (responseLogin.status == 200) {
          this.loadingBtnSave = true;
          var response = await this.resetPassword(teste);
          if (response.status == 200) {
            this.display = false;
            this.$emit('success');
          } else {
            await this.logout();
            this.alertWarning = true;
            this.alertWarningText = response.response.data.error.message;
            setTimeout(() => {
              this.alertWarning = false;
            }, 5000);
            this.$emit("resetParent", false);
          }
        } else {
          this.alertWarning = true;
          this.alertWarningText = "Erro ao autenticar!";
          setTimeout(() => {
            this.alertWarning = false;
          }, 5000);
          this.$emit("resetParent", false);
        }
        this.loadingBtnSave = false;
      }
    },
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
    value() {
      this.newPassword = null;
      this.confirmPassword = null;
    },
  },
};
</script>
