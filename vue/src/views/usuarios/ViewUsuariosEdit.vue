<template>
  <div class="d-flex flex-column pa-6" style="width: 100%; height: 100%; overflow: auto">
    <div class="d-flex justify-center background-black" style="width: 100%;" ref="scrollContainer">
      <h2>Realize o cadastro</h2>

    </div>

    <div class="d-flex justify-center background-black">
      <v-stepper elevation="0" v-model="step" dark alt-labels style="width: 50%;" class="background-black">
        <v-stepper-header>
          <v-stepper-step step="1" :complete="step1Complete">
          </v-stepper-step>

          <v-divider></v-divider>

          <v-stepper-step step="2" :complete="step2Complete">
          </v-stepper-step>

          <v-divider></v-divider>

          <v-stepper-step step="3" :complete="step3Complete">
          </v-stepper-step>
        </v-stepper-header>
      </v-stepper>
    </div>

    <div>

      <step-one :userIdToGet="userIdToGet" @userId="setUserId" @changeStep="changeStep" v-if="step == 1"></step-one>
      <step-two :userIdToGet="userIdToGet" :userId="userId" @listBack="listBack" @changeStep="changeStep" v-if="step == 2"></step-two>
      <step-three :userIdToGet="userIdToGet" :userId="userId" @listBack="listBack"   @changeStep="changeStep" v-if="step == 3"></step-three>
      <step-four @finalizar="finalizar" @listBack="listBack" @changeStep="changeStep" v-if="step == 4"></step-four>

    </div>

  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import crudController from "@/mixins/crudController.js";
import { mapActions } from "vuex";
import informacoesGerais from "@/views/usuarios/informacoesGerais/informacoesGerais.vue";
import perfilUsuario from "./permissoes/perfilUsuario.vue";
import AlertComponent from "../../components/AlertComponent.vue";
import StepOne from "./steps/StepOne.vue";
import StepTwo from "./steps/StepTwo.vue";
import StepThree from "./steps/StepThree.vue";
import StepFour from "./steps/StepFour.vue";

export default {
  data: () => ({
    step: 1,
    step1Complete: false,
    step2Complete: false,
    step3Complete: false,
    userId: null,
  }),

  methods: {
    listBack(){
      this.userIdToGet = this.userId;
    },
    changeStep(v) {
      this.step = v;
      this.userIdToGet = null;
    },

    setUserId(v) {
      this.userId = v;
    },

    finalizar(){
      this.$router.push({name: `ViewUsuarios`})
    }
  },
  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionUserEdit);
    },
  },

  watch: {
    step() {
      if (this.step == 2)
        this.step1Complete = true;
      else if (this.step == 3) {
        this.step1Complete = true
        this.step2Complete = true
      }
      else if (this.step == 4) {
        this.step1Complete = true
        this.step2Complete = true
        this.step3Complete = true
      }
    }
  },

  mixins: [
    getRouterData,
    crudController({ store: "ModuleUsuarios" }),
  ],

  components: {
    TopSection,
    informacoesGerais,
    perfilUsuario,
    AlertComponent,
    StepOne,
    StepTwo,
    StepThree,
    StepFour
  },

  async mounted() {
    this.dataNewItem.isActive = true;
    var id = this.$route.params.id;
    if (id) {
      var result = await this.getItemById(id)
      this.dataNewItem = result

    }
  }
};
</script>

<style scoped>
.back {
  background-color: #071422 !important;
  ;
}
</style>

