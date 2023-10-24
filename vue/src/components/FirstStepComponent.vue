<template>
  <div>

    <h2>Criar Perfil</h2>
    <div class="d-flex justify-center">
      <v-progress-circular style="margin-left: 180px; margin-right: 50px;" :size="160" color="#FFEC44"
        :indeterminate="loading">
        <v-avatar size="150">
          <v-img :src="imagemPerfil" alt="User" v-if="imagemPerfil != null"></v-img>
          <span v-else class="icone mdi mdi-camera"></span></v-avatar></v-progress-circular>
      <div>

        <input style="position: absolute; left: -9999px; opacity: 0;" id="upload" type="file" ref="file"
          v-on:change="uploadImage($event)" />
        <v-btn text @click="$refs.file.click()"><span class="span-text mdi mdi-camera"></span> Fazer upload</v-btn>
      </div>

    </div>
    <div class="img-text">

      <p>Imagem de Perfil</p>

    </div>

    <AlertComponent style="margin: auto; width: 50%;" :type="type" :active="active" :message="message">
    </AlertComponent>

    <v-form ref="form">


      <div class="container-forms">
        <v-text-field class="teste" required prepend-inner-icon="mdi mdi-account-circle" dark rounded label="Nome"
          v-model="inputData.Name" outlined :rules="[rules.required]" validate-on-blur clearable></v-text-field>

        <v-text-field clearable required prepend-inner-icon="mdi mdi-account-circle" dark rounded label="Sobrenome"
          v-model="inputData.SurName" outlined :rules="[rules.required]" validate-on-blur></v-text-field>
      </div>
      <div class="container-forms">
        <v-text-field clearable hint="DD/MM/AAAA" v-mask="'##/##/####'" prepend-inner-icon="mdi mdi-account-circle" dark
          rounded label="Data de Nascimento" v-model="inputData.DataNascimento" outlined
          :rules="[rules.required, rules.date]" validate-on-blur></v-text-field>

        <v-text-field clearable prepend-inner-icon="mdi mdi-account-circle" dark rounded label="Telefone"
          v-mask="['(##) #####-####', '(##) ####-####']" v-model="inputData.PhoneNumber" outlined
          :rules="[rules.required, rules.telefone]" validate-on-blur></v-text-field>
      </div>
      <div class="container-forms">
        <v-text-field clearable prepend-inner-icon="mdi mdi-account-circle" dark rounded label="Cidade"
          v-model="inputData.Cidade" outlined :rules="[rules.required]" validate-on-blur></v-text-field>

        <v-select :menu-props="{ bottom: true, offsetY: true }" open-on-clear clearable
          prepend-inner-icon="mdi mdi-account-circle" dark label="Sexo" rounded :items="sexo" v-model="inputData.sexo"
          outlined :rules="[rules.required]" validate-on-blur></v-select>
      </div>
    </v-form>
    <div class="container-forms">
      <v-btn class="button" @click="nextStep()" :loading="loadingBtn">Próximo</v-btn>
    </div>
  </div>
</template>
<script>
import { toBase64 } from "@/lib/helpers/base64";
import { mask } from 'vue-the-mask'
import { mapActions } from "vuex";
import validationRules from '@/mixins/validationRules';
import AlertComponent from "./AlertComponent.vue"
import login from '@/mixins/login';
export default {
  data: () => ({
    inputData: {},
    e1: 1,
    step1: false,
    step2: false,
    loadBtnSave: null,
    type: null,
    active: null,
    message: null,
    loading: false,
    imagemPerfil: null,
    loadingBtn: false,
    sexo: [
      { id: 1, text: "Masculino" },
      { id: 2, text: "Feminino" }
    ]
  }),
  methods: {
    props: ['user'],
    ...mapActions("ModuleUsuarios", {
      create: "create",
      update: "update",
      generateCodeToValidateUser: "generateCodeToValidateUser",
      verifyEmailAndCode: "verifyEmailAndCode",
      validateEmail: "validateEmail",
      ObterFotoUsuarioLogado: "ObterFotoUsuarioLogado",
      UpdateFotoUsuarioLogado: "UpdateFotoUsuarioLogado",
    }),
    ...mapActions("ModuleSession", {
      init: "init"
    }),

    async nextStep() {
      if (!this.$refs.form.validate())
        return;
      this.loadBtnSave = true;
      this.inputData.Id = this.$store.state.ModuleSession.user.id
      this.inputData.UserName = this.inputData.Name;
      this.inputData.EmailAddress = this.$store.state.ModuleSession.user.emailAddress
      this.inputData.Imagem = this.$store.state.ModuleUsuarios.imagemBytes
      this.inputData.IsActive = true;
      var result = await this.update(this.inputData)
      if (result.status == 200) {
        this.loadBtnSave = false;
        this.$emit('concluido', true)

      }
      this.loadBtnSave = false;

    },

    async uploadImage(e) {
      if (e.target.files[0].size > 5000000) {
        e.preventDefault();
        this.alertError = true;
        this.alertErrorText = "Tamanho da imagem ultrapassa 5MB";
        setTimeout(() => {
          this.alertError = false;
        }, 5000);
        this.loading = false;
        return;
      }
      if (
        e.target.files[0].type === "image/png" ||
        e.target.files[0].type === "image/jpeg" ||
        e.target.files[0].type === "image/jpg"
      ) {
        this.loading = true;
        if (e.target.files.length > 0) {
          this.imagem = await toBase64(e.target.files[0]);
          var data = { UserId: this.$store.state.ModuleSession.user.id, Imagem: this.imagem };

          var result = await this.UpdateFotoUsuarioLogado(data)
          if (result.status == 200) {
            this.imagemPerfil = await this.ObterFotoUsuarioLogado();
            if (result.status == 200) {
            } else {
              this.alertError = true;
              this.alertErrorText = "Ocorreu um erro, ao alterar a imagem...";
              setTimeout(() => {
                this.alertError = false;
              }, 5000);
              this.loading = false;
            }
          };
          this.loading = false;

        }
      } else {
        this.setAlert('error', true, "Formato de arquivo inválido")
      }
    },
  },
  watch: {
    msgError() {
      this.setAlert('success', true, "Verifique seu email, após validar sua conta voce será redirecionado e logado")
    }
  },

  async mounted() {
    await this.init();
    this.imagemPerfil = await this.ObterFotoUsuarioLogado()

  },

  directives: { mask },
  components: { AlertComponent },
  mixins: [validationRules, login],
};
</script>

<style scoped>
.v-input__control {
  background-color: #F5F5F5 !important;
  /* Defina a cor de fundo desejada */
}


.button {
  width: 30%;
  height: 40px !important;
  margin-top: 20px !important;
  border-radius: 20px;
  font-weight: bold;
  background: linear-gradient(90deg, #F6D74C, #F6C449, #F6AB4C, #F6944C);
}

.container-forms {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 80%;
  margin: auto;
}


.container-forms .v-text-field {
  margin-top: 0px;
  margin-right: 30px;
}

.v-select {
  width: 25%;
  min-width: 30%;
}

.img-text {
  display: flex;
  justify-content: center;
  margin-top: 50px;
}

.img-text p {
  color: white;
  margin-top: 0 !important;
  font-size: 30px;
}

h2 {
  color: #FFEC44;
  padding: 20px;
}

.span-text {
  background-color: #fafafa;
  color: black;
  border-radius: 50%;
  padding: 8px;
  line-height: 1;
  font-size: 18px;
  margin-right: 10px;
}

.background-black .icone {
  color: #344150;
  font-size: 50px;
  background-color: #202934;
  border-radius: 50%;
  padding: 48px;
  line-height: 0.99;
}

.v-avatar {
  color: white;
  background-color: white;
}

.containerr {
  height: 100%;
  display: flex;
}

.background-black .v-btn {
  color: white;
  margin-top: 35%;
}
</style>

