<template>
  <div
    class="d-flex flex-column justify-space-between mx-10"
    style="width: 100%; height: 100%; overflow: auto"
  >
    <!-- DIALOG CONFIRM SALVAR -->
    <v-dialog v-model="dialogConfirmSalvar" persistent max-width="450px">
      <div class="white pa-8 d-flex flex-column align-center text-center">
        <v-icon style="font-size: 60px; border-radius: 15px" color="purple"
          >mdi-alert</v-icon
        >

        <div class="d-flex align-start">
          <h4 class="Academia-neutral--text text--var-1 pb-5 pt-5 mb-4">
            Após salvar não será possível alterar <br />
            o endereço de e-mail do cadastro. <br />
            Deseja continuar?
          </h4>
        </div>

        <div class="d-flex flex-row justify-end">
          <v-btn
            rounded
            large
            text
            class="mr-8"
            @click="dialogConfirmSalvar = false"
            >CANCELAR</v-btn
          >

          <v-btn
            rounded
            large
            class="white--text"
            style="background: purple"
            @click="submit"
            :loading="loadingBtnDelete"
            >SALVAR</v-btn
          >
        </div>
      </div>
    </v-dialog>
    <!-- FIM -->



    <top-section></top-section>

    <div class="d-flex justify-space-between align-end mb-2">
      <h2 class="Academia-neutral--text text--var-1">{{ $route.meta.title }}</h2>

      <div v-if="editPermission" class="d-flex flex-row justify-end align-end">
        <v-btn
          dense
          rounded
          class="mr-3"
          text
          dark
          @click="$router.back()"
        >
          CANCELAR
        </v-btn>

        <v-btn
          rounded
          dense
          class="background-purple white--text"
          @click="checkAndSave()"
          :loading="loadingBtnSave"
          >SALVAR</v-btn
        >
      </div>
    </div>

    <alert-component :type="type" :message="message" :active="active"></alert-component>

    <div class=" rounded-lg flex-grow-1 background-black">
      <div class="d-flex flex-column" style="width: 100%">
        <v-tabs dark background-color="blue" v-model="tabs">
          <v-tab href="#tab-1">Informações Gerais</v-tab>
          <v-tab href="#tab-2">Permissões</v-tab>
        </v-tabs>

        <v-tabs-items  touchless class="pa-4 back" v-model="tabs">
          <v-tab-item value="tab-1">
            <informacoes-gerais
              ref="informacoesGerais"
              :user="dataNewItem"
              :readonly="!editPermission"
              @mudarSenha="dialogConfirMudarSenha = true"
            />
          </v-tab-item>

            <v-tab-item value="tab-2" :eager="true">
            <perfil-usuario
              ref="permissions"
              v-model="dataNewItem.roleNames"
              :readonly="!editPermission"
              @mudarSenha="dialogConfirMudarSenha = true"
            />
          </v-tab-item>
        </v-tabs-items>
      </div>
    </div>
  </div>
</template>

<script>
import TopSection from "@/components/TopSection.vue";
import getRouterData from "@/mixins/getRouterData.js";
import crudController from "@/mixins/crudController.js";
import { mapActions } from "vuex";
import informacoesGerais from "@/views/usuarios/informacoesGerais/informacoesGerais.vue";
import perfilUsuario from "./permissoes/perfilUsuario.vue";
import AlertComponent from "../../components/AlertComponent.vue";

export default {
  data: () => ({
    tabs: null,
    dialogConfirmSalvar: false,
    dialogConfirMudarSenha: false,
    perfil: [],
    dataNewItem: {
      id: 0,
      userProfile: null,
      roleProfile: null,
      isCustomized: false,
      isActive: null,
    },
    trip: {
      name: "",
      location: null,
      start: null,
      end: null,
    },
    loadingPermissions: false,
    openExpansionPanel: null,
    hideList: null,
  }),

  computed: {
    editPermission() {
      return this.hasPermission(this.$PermissionNames.permissionUserEdit);
    },
  },

  mixins: [
    getRouterData,
    crudController({ store: "ModuleUsuarios" }),
  ],

  components: {
    TopSection,
    informacoesGerais,
    perfilUsuario,
    AlertComponent
},

  watch: {
    active() {
      if (this.success) this.$router.push({ path: `/home/usuarios` });
    },
  },

  methods: {
    ...mapActions("ModuleUsuarios", {
      setDefaultPassword: "setDefaultPassword",
      checkUserEmail: "checkUserEmail",
    }),
    async checkAndSave() {
      if (!this.$route.params.id) {
        if (!this.$refs.informacoesGerais.validateForm) return;
        var response = await this.checkUserEmail(this.dataNewItem.emailAddress);
        if (response.status != 200) {
          this.errorAlert = true;
          this.errorAlertText = response.error.message;
          setTimeout(() => {
            this.errorAlert = false;
          }, 5000);
          return;
        }
      }
      if (!this.dataNewItem.id && this.validateForms())
        this.dialogConfirmSalvar = true;
      else this.submit();
    },

    validateForms() {
      if (!this.$refs.informacoesGerais.validateForm) {
        if (this.tabs != "tab-1")
          this.showMessageValidateForm("Informações Gerais");
        return false;
      } else if (!this.$refs.permissions.validateForm) {
        if (this.tabs != "tab-2") this.showMessageValidateForm("Permissões");
        return false;
      }

      return true
    },

    showMessageValidateForm(abaNome) {
      this.errorAlert = true;
      this.errorAlertText = `A aba ${abaNome} contém campos não preenchidos.`;
      setTimeout(() => {
        this.errorAlert = false;
      }, 5000);
    },

    async submit() {
      if (this.validateForms()) {
        var [name, surname] = this.dataNewItem.name.split(/(?<=^\S+)\s/);
        this.dataNewItem.name = name;

        if (!surname) this.dataNewItem.surname = name;
        else this.dataNewItem.surname = name;

        if(!Array.isArray(this.dataNewItem.roleNames))
        {
          this.dataNewItem.roleNames = [this.dataNewItem.roleNames]
        }

        if (this.$route.params.id) {
          this.updateItem();
        } else {
          this.addNewItem();
        }
      }
    },

    async substituirSenha() {
      var id = this.$route.params.id;
      var response = await this.setDefaultPassword(id);
      if (response.status == 200) {
        this.dialogConfirMudarSenha = false;
        this.active = true;
        this.message = "Senha alterada com sucesso!";
        this.type= "success"
        setTimeout(() => {
          this.active = false;
        }, 5000);
      }
    },
  },
  async mounted() {
    this.dataNewItem.isActive = true;
    var id = this.$route.params.id;
    if (id) {
      var result = await this.getItemById(id)
        this.dataNewItem = result

  }
}
};
</script>

<style scoped>
.back{
  background-color: #071422  !important;;
}</style>

