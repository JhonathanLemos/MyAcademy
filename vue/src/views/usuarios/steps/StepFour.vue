<template>
  <div>
    <div class="text1 background-black">
      <div>
        <h1>Tudo <span>Pronto!</span></h1>
        <h4>Você está pronto para começar sua jornada!</h4>
        <div class="icon-container">
          <span class="icon mdi mdi-check"></span>
        </div>

        <p>Você já pode prosseguir até a <span>tela inicial</span></p>
      </div>
    </div>

    <div class="container-button background-black">
      <v-btn style="width: 200px;" class="background-purple mr-2" @click="stepBack()" :loading="loadingBtn">Anterior</v-btn>
      <v-btn style="width: 200px;" class="background-purple ml-2" @click="nextStep()" :loading="loadingBtn">Concluir</v-btn>
    </div>
  </div>
</template>
<script>
import { mapActions } from "vuex";
export default {
  data: () => ({
    type: null,
    active: null,
    message: null,
    loadingBtn: false,
  }),
  methods: {
    ...mapActions("ModuleInformacao", {
      setAsFinalizado: "setAsFinalizado"
    }),
    async nextStep() {
      this.loadingBtn = true
      await this.setAsFinalizado();
      this.$emit('finalizar', true)
      this.loadingBtn = false

    },

    async stepBack() {
      this.$emit('changeStep', 3);
      this.$emit('listBack', "")

    },
  },
};
</script>

<style scoped>
.button {
  width: 30%;
  height: 40px !important;
  margin-top: 20px !important;
  font-weight: bold;
  background-color: #BFACFF;
}

.icon-container {
  margin-top: 15%;

  display: inline-block;
  line-height: 1;
  border-radius: 100%;
  border: 2px solid #BFACFF;
  /* Cor da borda (substitua #000 pela cor desejada) */
  padding: 4px;
  /* Ajuste o valor de acordo com a largura da borda */
}

p {
  color: white;
  margin-top: 10%;
}

.container-button {
  width: 100%;
  height: auto;
  display: flex;
  justify-content: center;
}

.container-button .v-btn {
  width: 30%;
  margin-bottom: 100px;
  border-radius: 15px;

}

.icon {
  font-size: 90px;
}

h1 {
  color: white;
  padding: 50% 0px 0px 0px;
  font-size: 45px;
}

span {
 background-color: #BFACFF;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  font-weight: bold;
}

h4 {
  color: white;
  font-weight: 1000;
  font-size: 17px;

}

.text1 {
  margin: auto;
  justify-content: center;
  display: flex;
  text-align: center;
}
</style>

