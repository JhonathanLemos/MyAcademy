<template>
  <div>
    <div class="text1 background-black" ref="scrollContainer">
      <div>
        <h2>Queremos te conhecer melhor!</h2>
        <h4>Por favor preencha o formulário com a maior precisão possível</h4>
      </div>
    </div>


    <v-form ref="form" class="background-black pt-12">
      <AlertComponent ref="alert" style="margin: auto;width: 50%;" :type="type" :active="active" :message="message">
      </AlertComponent>
      <div class="d-flex justify-center">
        <div class="container-forms-initial">
          <div>
            <p>Há quanto tempo você pratica exercícios físicos regularmente?</p>
            <v-radio-group v-model="inputData.anosInformacao">
              <v-radio color="#FFEC44" dark :label="option" :value="index + 1" v-for="(option, index) in options"
                :key="index"></v-radio>
            </v-radio-group>
          </div>
        </div>

        <div class="container-forms">
          <p>Como você descreveria seu nível atual de condicionamento físico?</p>
          <v-radio-group v-model="inputData.condicionamentoInformacao">
            <v-radio color="#FFEC44" dark :label="option" :value="index + 1" v-for="(option, index) in options2"
              :key="index"></v-radio>
          </v-radio-group>
        </div>
      </div>

      <div class="d-flex justify-center" style="margin-top: 50px;">
        <div class="container-forms-initial">
          <div>
            <p>Quantos dias da semana você deseja treinar ?</p>
            <v-radio-group v-model="inputData.diasInformacao">
              <v-radio color="#FFEC44" dark :label="option" :value="index + 1" v-for="(option, index) in options3"
                :key="index"></v-radio>
            </v-radio-group>
          </div>
        </div>

        <div class="container-forms">
          <p>Quais são os melhores horários para você treinar na academia?</p>

          <v-radio-group v-model="inputData.horariosInformacao">
            <v-radio color="#FFEC44" dark :label="option" :value="index + 1" v-for="(option, index) in options4"
              :key="index"></v-radio>
          </v-radio-group>
        </div>
      </div>

      <div class="d-flex justify-center" style="margin-top: 50px;">
        <div class="container-forms-initial">
          <div>
            <p>Qual é o seu objetivo na academia ?</p>
            <v-radio-group v-model="inputData.objetivoInformacao">
              <v-radio color="#FFEC44" dark :label="option" :value="index + 1" v-for="(option, index) in options5"
                :key="index"></v-radio>
            </v-radio-group>
          </div>
        </div>

        <div class="container-forms-textfield">
          <div>
            <p>Você tem alguma restrição alimentar? Se sim, por favor especifique.</p>
            <v-textarea :rules="[rules.required]" clearable label="Digite aqui sua resposta..."
              v-model="inputData.restricaoAlimentar" dark color="yellow" outlined filled></v-textarea>
            <p style="width: 50%;">Há mais alguma informação relevante que você gostaria de compartilhar sobre sua saúde
              ou histórico de
              exercícios físicos ? </p>
            <v-text-field :rules="[rules.required]" clearable label="Digite aqui sua resposta..."
              v-model="inputData.outraInformacao" dark color="yellow" outlined filled></v-text-field>
          </div>
        </div>
      </div>
    </v-form>

    <div class="d-flex justify-center background-black pb-5">
      <v-btn class="background-purple white--text mr-4" @click="stepBack()">Anterior</v-btn>
      <v-btn class="background-purple white--text mr-4" @click="nextStep()" :loading="loadingBtn">Próximo</v-btn>
    </div>
  </div>
</template>
<script>
import { mapActions } from "vuex";
import AlertComponent from "../../../components/AlertComponent.vue"
import validationRules from "../../../mixins/validationRules"
export default {
  props: ["userId", "userIdToGet"],
  data: () => ({
    inputData: {
      objetivoInformacao: null,
      horariosInformacao: null,
      diasInformacao: null,
      condicionamentoInformacao: null,
      anosInformacao: null
    },
    type: null,
    active: null,
    message: null,
    loadingBtn: false,
    options: ['menos de 6 meses', 'menos de 1 ano', 'mais de 1 ano', 'estou parado'],
    options2: ['Baixo', 'Médio', 'Alto'],
    options3: ['2 dias', '3 dias', '5 dias', 'Livre'],
    options4: ['Manhã', 'Tarde', 'Noite',],
    options5: ['Perder peso', 'Ganhar massa muscular', 'Melhorar condicionamento físico', 'Tonificar o corpo'],

  }),
  methods: {
    ...mapActions("ModuleInformacao", {
      createOrUpdate: "createOrUpdate",
      getByUserId: "getByUserId"
    }),

    stepBack() {
      this.$emit('changeStep', 2)
      this.$emit('listBack', "")
    },
    async nextStep() {
      if (!this.$refs.form.validate())
        return
      if (!this.validate()) {
        const alertElement = this.$refs.scrollContainer;
        alertElement.scrollIntoView({ behavior: 'smooth' });
        this.setAlert('#BFACFF', true, "Preencha todos os campos!")
        return
      }
      this.loadingBtn = true;
      this.inputData.UserId = this.userId;
      var result = await this.createOrUpdate(this.inputData);
      if (result.status == 200)
        this.$emit('changeStep', 4);
      this.loadingBtn = false;
    },

    validate() {
      if (
        this.inputData.AnosInformacao === null ||
        this.inputData.CondicionamentoInformacao === null ||
        this.inputData.DiasInformacao === null ||
        this.inputData.HorariosInformacao === null ||
        this.inputData.ObjetivoInformacao === null
      ) {
        return false;
      }
      return true
    }
  },
  async mounted() {
    if (this.userIdToGet != null) {
      var user = await this.getByUserId(this.userIdToGet);
      this.inputData = user.data.result
    }
  },

  mixins: [validationRules],

  components: { AlertComponent },
};
</script>

<style scoped>
.button {
  width: 30%;
  height: 40px !important;
  margin-top: 20px !important;
  font-weight: bold;
  background: linear-gradient(90deg, #F6D74C, #F6C449, #F6AB4C, #F6944C);
}

.container-forms {
  width: 50%;
  height: auto;
}

.container-button {
  width: 95%;
  height: auto;
  display: flex;
  justify-content: center;
}

.container-button .v-btn {
  width: 20%;
  margin-bottom: 100px;
}

.container-forms-initial {
  width: 50%;
  padding-left: 15%;
}

.container-forms-textfield {
  color: white;
}

.container-forms-textfield .v-text-field {
  width: 80%;
}

.container-forms p {
  color: white;
}

.container-forms-initial p {
  color: white;
}


h2 {
  color: white;
  padding: 40px 0px 0px 0px;
  font-size: 35px;
}

h4 {
  color: white;
  font-weight: normal;
  font-size: 17px;
}

.text1 {
  margin: auto;
  justify-content: center;
  display: flex;
  text-align: center;
}
</style>

