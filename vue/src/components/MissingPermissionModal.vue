<template>
  <v-dialog persistent v-model="dialog" max-width="400px">
    <div class="white pa-8 d-flex flex-column align-center text-center">
      <v-icon style="font-size: 60px" color="purple">mdi-alert</v-icon>

      <span class="aapecan-neutral--text text--var-1 pt-5">
        Este cadastro <b>não pode</b> ser exibido pois
        <b>não está preenchido</b> por completo.
      </span>

      <b class="aapecan-neutral--text text--var-1 pt-5"
        >Contate o administrador.</b
      >

      <div class="d-flex flex-row justify-center mt-6">
        <v-btn
          class="aapecan-neutral--text elevation-0"
          color="purple"
          rounded
          large
          @click="dialog = false"
          ><b style="color: white">FECHAR MENSAGEM</b></v-btn
        >
      </div>
    </div>
  </v-dialog>
</template>

<script>
export default {
  data: () => ({
    dialog: false,
  }),

  methods: {
    open() {
      this.dialog = true;
    },
  },
};
</script>
