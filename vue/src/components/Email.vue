<template>
  <v-dialog persistent v-model="display" max-width="550px">
    <div class="white pa-8 d-flex flex-column">
      <div class="d-flex flex-row justify-space-between mb-3">
        <h2>Recuperar senha</h2>
        <v-btn text small fab>
          <v-icon @click="display = false">mdi-close</v-icon>
        </v-btn>
      </div>

      <div class="mt-5">
        <v-form ref="form">
          <AlertComponent :type="type" :active="active" :message="message"></AlertComponent>
          <v-text-field label="Insira seu E-mail" type="email" :rules="[rules.email]" v-model="email"></v-text-field>
        </v-form>
      </div>

      <div class="mt-2" style="text-align: right">
        <v-btn class="gradient black--text" :loading="loadingBtnSave" @click="sendEmail">Enviar</v-btn>
      </div>
    </div>
  </v-dialog>
</template>

<script>
import AlertComponent from "./AlertComponent.vue";
import { mapActions } from "vuex";
import validationRules from "../mixins/validationRules"
export default {
  props: {
    value: null,
  },

  components: { AlertComponent },
  mixins: [validationRules],

  data: () => ({
    email: null,
    type: null,
    active: null,
    message: null,
    loadingBtnSave: false,
  }),

  methods: {
    ...mapActions("ModuleUsuarios", {
      recoveryPassword: "recoveryPassword"
    }),

    async sendEmail() {
      if (this.email == null || this.email == "") {
        this.setAlert('error', true, "Preencha o campo!")
        return
      }
      this.loadingBtnSave = true;
      await this.recoveryPassword(this.email)
      this.loadingBtnSave = false
      this.setAlert('success', true, "Um E-mail foi enviado para refinir sua senha!")
      setTimeout(() => {
        this.display = false
      }, 5000);
    },
  },

  computed: {
    display: {
      get() {
        this.email = ''
        return this.value;
      },
      set(val) {
        this.$emit("input", val);
      },
    },
  },
};
</script>
