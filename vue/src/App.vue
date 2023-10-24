<template>
  <transition mode="out-in">
    <router-view></router-view>
  </transition>
</template>

<script>
export default {
  name: "App",
  watch:{
    /*
    $route (){
      this.$store.commit("ModuleGlobal/UNSET_FILTER");
    }
    */
  },
};
</script>

<style>
::-webkit-scrollbar
{
width: 0px;
}


.theme--dark {
    color: #FFFFFF;
}
</style>
