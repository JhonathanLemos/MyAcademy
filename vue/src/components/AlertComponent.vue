<template>
  <div>
    <v-alert :type="typeMessage" transition="slide-y-transition" dismissible v-show="isActive" dense class="mt-1">{{
      textMessage }}</v-alert>
  </div>
</template>


<script>
export default{
  name: "AlertComponent",

  data : () => ({

  }),

  props: ["type", "active", "message"],


  computed:{
    typeMessage(){
      return this.type
    },

    isActive(){
      return this.active
    },

    textMessage(){
      return this.message
    }
  }
}
</script>
