<template>
  <v-app>
    <div class="wrap-app d-flex flex-column justify-center dark var-5">
      <div class="container-app d-flex flex-row">
        <menu-lateral></menu-lateral>

        <transition mode="out-in">
          <router-view :key="$route.path"></router-view>
        </transition>
      </div>
    </div>
  </v-app>
</template>

<script>
import MenuLateral from '@/components/MenuLateral.vue'
export default {
  name: 'ViewGlobal',
  components: {
    MenuLateral,
  },
}
</script>

<style>
.wrap-app {
  width: 100%;
  height: 100vh;
}
.container-app {
  width: 100%;
  height: 100vh;
  /* background-color: #0F0E10; */
}

.dark{
background-color: #102030;
}
</style>
