import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    themes: {
      light: {
        primary: '#BFACFF',
        secondaryAction: '#4D4D4D',
        success: '#107154',
        error: '#C90C0F'
      },
    },
  },
});
