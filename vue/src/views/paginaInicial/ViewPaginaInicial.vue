<template>
  <v-app>
    <!-- <v-navigation-drawer v-model="drawer" app>
      <v-list>
        <v-list-item link to="/home">
          <v-list-item-icon>
            <v-icon>mdi-home</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Início</v-list-item-title>
        </v-list-item>
        <v-list-item link to="/Informacaos">
          <v-list-item-icon>
            <v-icon>mdi-dumbbell</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Informacaos</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-navigation-drawer> -->
    <v-app-bar dark app color="gradient">
      <!-- <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon> -->
      <v-toolbar-title>Hr Academias</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn text to="/login">Login</v-btn>
      <v-btn text @click="$router.push('/usuarios/register')">Cadastre-se</v-btn>
    </v-app-bar>
  </v-app>
</template>

<script>
import Register from "../../components/Register"
export default {
  data() {
    return {
      sliderValue: 0,
      drawer: false,
      openModalRegister: null,
      images: [
      "https://cdn.vuetifyjs.com/images/carousel/squirrel.jpg",
      "../../assets/img/image.png",
      "https://cdn.vuetifyjs.com/images/carousel/squirrel.jpg"
      ]
    };
  },
  methods: {
    getUserValue(value) {
      this.user = value;
    }
  },
  components: { Register }
}
</script>

<style>
/* Estilos personalizados */
</style>
