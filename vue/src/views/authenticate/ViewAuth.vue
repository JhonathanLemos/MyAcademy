<template>
  <v-app>
    <div class="d-flex" style="height: 100vh;">
      <div class="imageBackGround">
      </div>


      <div class="containerForm" style="overflow: scroll;">
        <Email v-model="openModalEmail"></Email>
        <ValidateEmail @canLogin="loginUserAndValidateEmail" :user="user" v-model="openModalValidationEmail"
          :msgError="msgError">
        </ValidateEmail>
        <AlertComponent :type="typeAlert" :active="activeAlert" :message="textMessage"></AlertComponent>
        <a @click="$router.push('/')">
          <h1 class="h1">Here<span class="span">Gym</span></h1>
        </a>
        <p class="text"><span>Bem-vindo</span> de volta!</p>
        <p class="mt-12">Por favor <span class="span">faça o login</span> para continuar</p>
        <v-form ref="form" class="form-login mt-8">
          <v-text-field color="yellow" class="text-field-with-spacing" prepend-inner-icon="mdi mdi-account-circle" dark rounded filled dense label="Usuário ou e-mail"
            v-model="inputData.userNameOrEmailAddress" outlined :rules="[rules.required]" validate-on-blur></v-text-field>


          <v-text-field color="yellow" prepend-inner-icon="mdi mdi-lock" dark rounded filled dense label="Senha" v-model="inputData.password"
            :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'" :rules="[rules.required]" :type="show ? 'text' : 'password'"
            v-on:keyup.enter="tryAuthenticate(true)" @click:append="show = !show" outlined validate-on-blur
            class="mt-4"></v-text-field>

          <div class="d-flex">

            <p class="pa-2 ml-auto"><a @click="openModalEmail = true">Esqueçeu sua senha?</a></p>
          </div>
          <v-btn class="p-2 button" @click="tryAuthenticate(true)" :loading="loadingBtn">Entrar</v-btn>
        </v-form>
        <p class="mt-5">Não tem uma conta? Por favor <a @click="$router.push('/usuarios/register')"><span
              class="span">Crie um conta</span></a> primeiro</p>
        <div class="d-flex">



        </div>

      </div>
    </div>

  </v-app>
</template>

<script>
import login from "../../mixins/login";
import { mapActions } from "vuex";
import Email from "../../components/Email.vue";
import Register from "../../components/Register.vue";
import ValidateEmail from "../usuarios/ValidateEmail.vue";
import AlertComponent from "../../components/AlertComponent.vue"
export default {
  mixins: [login],
  name: "ViewLogin",

  data: () => ({
    typeAlert: null,
    activeAlert: null,
    textMessage: null,
    msgError: null,
    user: {},
    show: false,
    rules: {
      required: (value) => !!value || "Preencha este campo!",
    },
    openModalRegister: false,
    object: null,
    loadingBtn: false,
    openModalEmail: null,
    inputData: {
      rememberClient: true,
    },
    loadingBtnSave: false,
    openModalValidationEmail: false,
    password: null,
    code: null,
  }),

  methods: {
    ...mapActions("ModuleSession", {
      login: "login",
    }),

    ...mapActions("ModuleUsuarios", {
      create: "create",
      generateCodeToValidateUser: "generateCodeToValidateUser",
      validateEmail: "validateEmail",

    }),

    async setNewUser() {
      await this.create(this.inputData);
    },

    getUserValue(value) {
      this.user = value;
    },

    async loginUserAndValidateEmail(value) {
      this.code = value;
      this.inputData = { userNameOrEmailAddress: this.user.emailAddress, password: this.user.password }
      await this.validateEmailMethod()
      await this.tryAuthenticate(false)
      return location.reload();

    },

    async validateEmailMethod() {
      var userAndCode = { Code: this.code, UserId: this.user.id }
      await this.validateEmail(userAndCode);
    },

    setAlert(type, active, message) {
      this.typeAlert = type;
      this.activeAlert = active;
      this.textMessage = message;

      setTimeout(() => {
        this.activeAlert = false
      }, 5000);
    },

    resetParent(v) {
      this.loadingBtn = v;
      this.inputData = [];
    },
  },

  watch: {
    async user(value) {
      this.object = {
        userNameOrEmailAddress: value.userName,
        password: value.password,
        rememberClient: true,
      };
      this.generateCodeToValidateUser(value)
    }
  },


  components: {
    Email,
    Register, ValidateEmail, AlertComponent
  },
};
</script>

<style scoped>
.imageBackGround {
  width: 100%;
  height: 100vh;
  background-image: url("../../assets/img/image.png");
  background-repeat: no-repeat;
  background-size: cover;
  background-position: center center;
}
.container-login {
  width: 100%;
}

.text {
  font-size: 30px;
  font-weight: bold;
  color: white;
  margin-top: 70px;
}

.containerForm {
  text-align: center;
  width: 100%;
  padding: 20px;
  background-color: #0F0E10;
  height: 100vh;
}

.span {
  background: linear-gradient(90deg, #F6D74C ,#F6C449, #F6AB4C, #F6944C   );
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  font-weight: bold;
}

.form-login {
  width: 40%;
  margin: auto;
}

.button {
  width: 100%;
  border-radius: 20px;
  font-weight: bold;
  background: linear-gradient(90deg, #F6D74C ,#F6C449, #F6AB4C, #F6944C   );
}

h1 {
  font-size: 80px !important;
  color: white !important;
  margin-top: 100px;
  font-weight: 100 !important;
}

p {
  color: white;
}

.v-text-field {
  width: 100%;
  border-radius: 20px;
}

.v-text-field .v-messages__message {
  color: #F6C449;
}

.v-text-field .error__text {
  border-color: #F6C449;
  /* Altere a cor da borda aqui */
  color: #F6C449;
}
</style>
