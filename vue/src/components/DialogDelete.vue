<template>
  <v-dialog
    v-model="openDialogInternal"
    persistent
    max-width="450px"
    min-height="50px"
  >
    <div class="white pa-8 d-flex flex-column align-center text-center">
      <loading v-if="loading" :size="50" :width="5" />

      <template v-else>
        <v-icon style="font-size: 60px" color="Academia-purple"
          >mdi-alert-circle-outline</v-icon
        >

        <h4 class="Academia-neutral--text text--var-1 pb-5 pt-5">
          {{ message }}
        </h4>

        <div class="d-flex flex-row justify-end">
          <v-btn
            rounded
            large
            text
            class="mr-4"
            @click="openDialogInternal = false"
          >
            CANCELAR
          </v-btn>

          <v-btn
            v-if="!showBtnDelete"
            v-show="showButtonInactive"
            rounded
            large
            class="Academia-purple white--text"
            :loading="loadingButton"
            @click="
              openDialogInternal = false;
              $emit('inativar');
            "
          >
            INATIVAR
          </v-btn>

          <v-btn
            v-if="showBtnDelete"
            rounded
            large
            class="Academia-purple white--text"
            :loading="loadingButton"
            @click="
              openDialogInternal = false;
              $emit('excluir');
            "
          >
            EXCLUIR
          </v-btn>
        </div>
      </template>
    </div>
  </v-dialog>
</template>

<script>
import Loading from "@/components/Loading.vue";
export default {
  props: [
    "message",
    "showButtonInactive",
    "value",
    "loadingButton",
    "showBtnDelete",
    "loading",
  ],
  data: () => ({
    openDialogData: false,
  }),

  components: {
    Loading,
  },

  watch: {
    value(newValue) {
      this.openDialogData = newValue;
    },
  },
  computed: {
    openDialogInternal: {
      get() {
        return this.openDialogData;
      },
      set(val) {
        this.openDialogData = val;
        this.$emit("value", this.openDialogData);
      },
    },
  },
};
</script>
