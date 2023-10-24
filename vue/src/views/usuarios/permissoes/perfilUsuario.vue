<template>
  <div style="overflow: auto; width: 100%">
    <v-form ref="form">
      <div class="d-flex align-center mb-6">
        <v-select :disabled="readonly" v-model="userRoleName" class="mr-8" :items="listRoles" label="Perfil de Usuário"
          :rules="[rules.required]" validate-on-blur>
        </v-select>

      </div>
    </v-form>


  </div>
</template>

<script>
import validationRules from "@/mixins/validationRules.js";
import { createNamespacedHelpers } from "vuex";
const moduleRoles = createNamespacedHelpers("ModuleRoles");
export default {
  props: ["userId", "value", "readonly"],

  components: {
  },

  computed: {
    validateForm() {
      return this.$refs.form.validate();
    },
    userRoleName: {
      get() {
        if (this.value && this.value.length == 1)
        return this.value[0];
        else
        return this.value
      },
      set(value) {
        this.$emit('input', value)
      },
    },
  },

  created(){
    this.getAllRoles()
  },

  data: () => ({
    loading: false,
    listRoles: []
  }),

  methods: {
    ...moduleRoles.mapActions({ getAllRolesMethod: "getAllRoles" }),

    async getAllRoles() {
      var result = await this.getAllRolesMethod();
      result.items.map(x => {
        this.listRoles.push({ value: x.name.toUpperCase(), text: x.displayName })
      })
    }
  },

  mixins: [validationRules],
};
</script>
