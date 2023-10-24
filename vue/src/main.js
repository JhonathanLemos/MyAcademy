import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import Filters from '@/lib/filters'
import Util from '@/lib/util'
import Http from '@/lib/http'
import Abp from './plugins/abp'
import AsyncComputed from 'vue-async-computed'
import PermissionNames from '@/constants/permission-names'
import Vue2Editor from 'vue2-editor';

Vue.use(Vue2Editor);

Vue.config.productionTip = false;
Vue.use(Abp);
Vue.use(Filters)
Vue.use(AsyncComputed)

Vue.prototype.$PermissionNames = PermissionNames

Http.get('/AbpUserConfiguration/GetAll').then((data) => {

  Util.abp = Util.extend(true, Util.abp, data.data.result);

  new Vue({
    router,
    store,
    vuetify,
    render: (h) => h(App),

    async mounted () {
      await this.$store.dispatch({ type: 'ModuleSession/init' })
    },
  }).$mount('#app')

})
