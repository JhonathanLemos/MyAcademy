<template>
  <div
    class="elevation-5 rounded-lg background-black d-flex flex-column"
    style="position: relative; max-height: 100%"
  >
    <div class="d-flex justify-center mb-3">
      <div class="d-flex flex-row">
      <v-menu left min-width="250px" rounded >
        <template v-slot:activator="{ on }">
          <v-btn icon x-large v-on="on">
            <v-avatar class="mt-5" color="#BFACFF" size="50">
              <span class="black--text">{{ userInitials }}</span>
            </v-avatar>
           </v-btn>
        </template>

         <v-card>
          <v-list-item-content class="justify-center">
            <div class="d-flex flex-column justify-center align-center">
              <h3 class="mt-4">
                {{ userName + " " + surname }}
              </h3>

              <p class="text-caption mt-1">
                {{ userEmail }}
              </p>

              <v-btn depressed rounded text @click="redirectToUserPage()">Editar Conta</v-btn>

              <v-btn depressed rounded text @click="setLogout()">Sair</v-btn>
            </div>
          </v-list-item-content>
        </v-card>
       </v-menu>
     </div>

      <v-btn
      style="background-color: #BFACFF;"
        icon
        @click.stop="expandMenu"
        class="btn-expand"
        small
        id="btn-expand"
      >
        <v-icon id="icon-expand" class="icon" color="background-black"
          >mdi-chevron-right</v-icon
        >
      </v-btn>
    </div>
    <div class="d-flex align-center justify-center flex-grow-1 background-black">
      <v-navigation-drawer
        v-model="drawer"
        :mini-variant="mini"
        hide-overlay
        class="rounded-xl "
        mobile-breakpoint="0"
        style="overflow: visible"
      >
        <div
          class="d-flex flex-column justify-space-between background-black"
          style="height: 100%"
        >
        <v-list flat class="mt-12">
            <v-list-item-group>
              <v-list-item link :to="{ name: 'ViewUsuarios' }" :class="$route.name == 'ViewUsuarios' ? background : null" >
                <v-list-item-icon>
                  <v-icon class="color-white">mdi mdi-view-dashboard</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title class="Academia-neutral--text"
                    >Painel</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>
              <v-list-item link :to="{ name: 'ViewTreinos' }" :class="$route.name == 'ViewTreinos' ? background : null">
                <v-list-item-icon>
                  <v-icon class="color-white">mdi-account-group</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title class="Academia-neutral--text"
                    >Treinos</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>
              <v-list-item link :to="{ name: 'ViewClientes' }" :class="$route.name == 'ViewVendas' ? background : null">
                <v-list-item-icon>
                  <v-icon class="color-white">mdi mdi-shopping</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title class="Academia-neutral--text"
                    >Vendas</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>
              <v-list-item link :to="{ name: 'ViewClientes' }">
                <v-list-item-icon>
                  <v-icon class="color-white">mdi mdi-calendar-month</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title class="Academia-neutral--text"
                    >Mensalidades</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </v-list>

          <v-list flat>
            <v-list-item-group color="Academia-neutral--text">
              <v-list-item link to="/home/administracao">
                <v-list-item-icon>
                  <v-icon class="color-white">mdi-cog</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                  <v-list-item-title class="Academia-neutral--text"
                    >Administração</v-list-item-title
                  >

                </v-list-item-content>
              </v-list-item>
              <!-- <div style="font-size: 12px;" class="d-flex justify-end background-black" v-if="!mini">Versão: {{ appVersion }}</div> -->
            </v-list-item-group>
          </v-list>
        </div>
      </v-navigation-drawer>
    </div>
  </div>
</template>

<script>
  import {version} from "../../package.json"
  import { mapActions } from "vuex"
export default {
  name: 'MenuLateral',
  data: () => ({
    appVersion: version,
    background: 'background',
    drawer: true,
    mini: true,
    userEmail: window.localStorage.getItem("userEmail"),
    userName: window.localStorage.getItem("userName"),
    userInitials: window.localStorage.getItem("userInitials"),
    surname: window.localStorage.getItem("userSurname"),
  }),

  methods: {
    expandMenu() {
      if (this.mini) {
        document.getElementById('btn-expand').classList.add('btn-expand-rotate')
      } else {
        document
          .getElementById('btn-expand')
          .classList.remove('btn-expand-rotate')
      }
      this.mini = !this.mini
    },
    ...mapActions("ModuleSession", { logout: "logout" }),

    setLogout(){
this.logout()
    }
  },
}
</script>

<style>
.img-logo {
  width: 45px;
}

.img-logo-expand {
  transform: scale(1.6);
}

.btn-expand {
  position: absolute !important;
  z-index: 50;
  right: -15px;
  top: 100px;
}

.btn-expand-rotate {
  transform: rotate(180deg);
}

.icon{
  color: black !important;
}

.v-list-item-group .v-icon {
  color: white !important; /* Defina a cor desejada para os ícones */
}

.background{
  background-color: #BFACFF;
}
</style>
