<template>
  <v-app persistent max-width="600px">
    <div class="containerr">

      <div class="background-yellow">
        <v-stepper class="my-stepper" v-model="e1" vertical elevation="0">
          <v-stepper-step step="1" :complete="step1" color="black">
            Perfil
          </v-stepper-step>
          <v-stepper-content step="1"></v-stepper-content>

          <v-stepper-step step="2" :complete="step2" color="black">
            Avaliação
          </v-stepper-step>
          <v-stepper-content step="2"></v-stepper-content>

          <v-stepper-step step="3" :complete="step3" color="black">
            Concluir
          </v-stepper-step>

        </v-stepper>
      </div>
      <div class="background-black" ref="scrollContainer">

        <FirstStepComponent @concluido="step1Concluido" v-if="ifStep1"></FirstStepComponent>
        <SecondStepComponent @scrollToTop="scrollToTop" @concluido="step2Concluido" v-else-if="ifStep2"></SecondStepComponent>
        <ThirdStepComponent @scrollToTop="scrollToTop" @concluido="step3Concluido" v-if="ifStep3"></ThirdStepComponent>
      </div>
    </div>
  </v-app>
</template>

<script>
import { mask } from 'vue-the-mask'
import FirstStepComponent from "../../components/FirstStepComponent.vue"
import SecondStepComponent from "../../components/SecondStepComponent.vue"
import ThirdStepComponent from "../../components/ThirdStepComponent.vue"
import AlertComponent from "@/components/AlertComponent.vue"
export default {
  data: () => ({
    ifStep1: true,
    ifStep2: false,
    ifStep3: false,
    e1: 1,
    step1: false,
    step2: false,
    step3: false,
    loadBtnSave: null,
    type: null,
    active: null,
    message: null,
  }),
  methods: {
    step1Concluido(){
      this.step1 = true;
      this.e1 = 2;
      this.ifStep1 = false
      this.ifStep2 = true
    },

    step2Concluido(){
      this.step2 = true;
      this.e1 = 3;
      this.ifStep1 = false
      this.ifStep2 = false,
      this.ifStep3 = true;
    },

    step3Concluido(){
      this.step3 = true;
      this.$router.push("/home/administracao")
    },

    scrollToTop() {
      this.$refs.scrollContainer.scrollTo({
        top: 0,
        behavior: 'smooth', // Rolar para o topo suavemente
      });
    },
  },
  watch: {
    msgError() {
      this.setAlert('success', true, "Verifique seu email, após validar sua conta voce será redirecionado e logado")
    }
  },

  directives: { mask },
  components: { AlertComponent, FirstStepComponent, SecondStepComponent, ThirdStepComponent},
};
</script>

<style scoped>
.background-yellow {
  width: 20%;
  height: 100%;
  background-color: #FFEC44;
}

.my-stepper {
  background-color: #FFEC44 !important;
  border: none;
  margin-top: 30%;
  margin-left: 15%;
}



.containerr {
  height: 100vh;
  display: flex;
}


.background-black {
  width: 80%;
  height: 100vh;
  background-color: #0F0E10;
  overflow-y: scroll;
}



/* Estilos da barra de rolagem no Chrome, Edge e Safari */
.background-black::-webkit-scrollbar {
  width: 10px;
}

.background-black::-webkit-scrollbar-thumb {
  background-color: #FFEC44;
  border-radius: 5px;
}

.background-black::-webkit-scrollbar-thumb:hover {
  background-color: #FFEC44;
}

/* Estilos da barra de rolagem no Firefox */
.background-black {
  scrollbar-width: thin;
  scrollbar-color: #FFEC44 #eee;
}

.background-black::-webkit-scrollbar {
  width: 10px;
}

.background-black::-webkit-scrollbar-track {
  background-color: black;
}

.background-black::-webkit-scrollbar-thumb {
  background-color: #e0ce25;
  border-radius: 5px;
}

.background-black::-webkit-scrollbar-thumb:hover {
  background-color: #FFEC44;
}
</style>
