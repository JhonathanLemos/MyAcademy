<template>
  <div class="d-flex flex-column mx-10" style="overflow-y: hidden; width: 100%">
    <top-section></top-section>

    <div class="flex-grow-1 d-flex flex-column" style="overflow-y: hidden">
      <h1 class="Academia-neutral--text mb-5">Administração</h1>
      <div class="d-flex justify-space-between" style="height: 82%">
        <div class="rounded-lg pa-8 elevation-5 mb-2 " style="width: 30%; overflow-y: auto"
          v-for="bloco in EstruturaOperacoes.blocos" :key="EstruturaOperacoes.blocos[bloco]">
          <h2 class="pb-2 white--text ">{{ bloco.name }}</h2>

          <hr />

          <ul class="menu-list" style="overflow-y: auto">
            <li v-for="item in bloco.itens" :key="item[item]">
              <div v-if="hasPermissionGranted(item.router)">
                <router-link :to="{ name: item.router }">
                  <v-icon medium class="Academia-yellow--text mr-3">{{
                    item.icon
                  }}</v-icon> <span class="white--text">{{ item.name }}</span>
                </router-link>
              </div>
            </li>
          </ul>
        </div>
        <div style="width: 5%"></div>
      </div>
    </div>
  </div>
</template>

<script>
import TopSection from '@/components/TopSection.vue'
import EstruturaOperacoes from '@/config/EstruturaOperacoes.json'
import { mapActions } from "vuex";
export default {
  name: 'ViewMainOperacoes',
  data: () => ({
    EstruturaOperacoes,
  }),
  components: {
    TopSection,
  },

  methods: {
    ...mapActions("ModuleInformacao", {
      VerifyIfInformacaoIsEnded: "VerifyIfInformacaoIsEnded"
    }),
    hasPermissionGranted(value) {
      switch (value) {
        case 'ViewUsuarios':
          return this.hasPermission(this.$PermissionNames.permissionUser);
        case 'ViewClientes':
          return this.hasPermission(this.$PermissionNames.permissionCustomer);
      }
      return true;
    },

    async verifyPerfil() {
      var result = await this.VerifyIfInformacaoIsEnded()
      if (result.data.result == false)
        this.$router.push('/perfil-create')
    },
  },

  created() {
    this.verifyPerfil();
    this.$store.dispatch("ModuleGlobal/SET_SEARCH", null);
  },
}
</script>

<style>
a {
  text-decoration: none !important;
  color: inherit !important;
}

.v-application ul {
  padding: 0 10px !important;
}

.menu-list li {
  list-style: none;
  display: flex;
  flex-direction: row;
  align-items: center;
  font-weight: 400;
  color: #434343;
  font-size: 16px;
  margin: 10px 0;
  cursor: pointer;
}

.menu-list li:hover {
  font-weight: 500;
}
</style>
