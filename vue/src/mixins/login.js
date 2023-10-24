import { mapActions } from "vuex";
export default {
  methods: {
    ...mapActions("ModuleSession", {
      getCurrentLoginInformations: "getCurrentLoginInformation",
      login: "login",
      init: "init"
    }),

    ...mapActions("ModuleUsuarios", {
      getByEmailOrUserName: "getByEmailOrUserName"

    }),
    async tryAuthenticate(reload) {
      if (this.$refs.form.validate()) {
        this.alertError = false;
        this.loadingBtn = true;
        var response = await this.login(this.inputData);
        if (response.status == 200) {
          await this.setStorageUserInfo();
        if (reload == true)
        location.reload();
        this.$router.push('/perfil-create')
        }
        else if (response.error.code == 12)
        {
          var result = await this.getByEmailOrUserName(this.inputData.userNameOrEmailAddress)
          this.user = result.result;
          this.user.password = this.inputData.password;
          this.openModalValidationEmail = true;
          this.msgError = response.error.message;
        }
        else
          this.setAlert("error", true, response.error.details != null ? response.error.details : response.error.message)

        this.loadingBtn = false;

      }
    },

    async setStorageUserInfo() {
      let response = await this.getCurrentLoginInformations();
      let userInfo = response.data.result.user;
      let userInitials = (
        userInfo.name.charAt(0) + userInfo.surname.charAt(0)
      ).toUpperCase();
      localStorage.setItem("userEmail", userInfo.emailAddress);
      localStorage.setItem("userName", userInfo.name);
      localStorage.setItem("userSurname", userInfo.surname);
      localStorage.setItem("userInitials", userInitials);
    },
  },
};
