<template>
  <div
    class="
      container-top-actions
      d-flex
      flex-row
      justify-space-between
      align-center
      mb-2
    "
  >
    <div class="d-flex flex-column flex-row">
      <template>
        <v-btn dark icon large class="elevation-0 mb-0" @click="setRoute"
          ><v-icon>mdi-arrow-left</v-icon></v-btn
        >
        <p
          style="font-size: 13px"
          class="Academia-neutral--text ml-1 mt-0"
        >
          Voltar
        </p>
      </template>
    </div>


  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  props: ["nameRoute"],
  name: "TopSection",
  data: () => ({
    userEmail: window.localStorage.getItem("userEmail"),
    userName: window.localStorage.getItem("userName"),
    userInitials: window.localStorage.getItem("userInitials"),
    surname: window.localStorage.getItem("userSurname"),
  }),

  methods: {
    ...mapActions("ModuleSession", { logout: "logout" }),
    ...mapActions("ModuleUsuarios", {
      getUserByEmail: "getUserByEmail",
    }),

    setLogout() {
      this.logout();
      return location.reload();
    },

    async redirectToUserPage(){
      var result = await this.getUserByEmail(this.userEmail);
      this.$router.push({ path: `/home/usuarios/edit/${result.data.result}` });
    },

    setRoute() {
      if (this.nameRoute) {
        this.$router.push({
          name: this.nameRoute,
          params: {
            isUpdate: true,
            options: this.$route.params.options,
          },
        });
      } else this.$router.back();
    },
  },
};
</script>

<style>
</style>
