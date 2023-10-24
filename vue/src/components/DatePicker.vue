<template>
  <div>
    <v-menu v-model="menu" :disabled="readonly" :close-on-content-click="false" transition="scale-transition" offset-y
      min-width="auto">
      <template v-slot:activator="{ on, attrs }">
        <v-text-field class="text-field" v-mask="'##/##/####'" :label="label" prepend-inner-icon="mdi-calendar" v-bind="attrs" hide-details
          :dense="dense" :outlined="outlined" :disabled="disabled" :readonly="readonly" :clearable="!readonly"
          :solo="solo" v-on="on" :dark="dark" :validate-on-blur="validOnBlur ? validOnBlur : false" v-model="virtualDate"
          :rules="required ? [rules.required, rules.date] : [rules.date]"
          @blur="date = parseDate(dateFormatted)"></v-text-field>
      </template>
      <v-date-picker dark color="orange" :rules="[rules.date]" no-title locale="pt-br" v-model="date"
        @input="menu = false" :readonly="readonly"></v-date-picker>
    </v-menu>
  </div>
</template>

<script>
import { mask } from 'vue-the-mask'
import validationRules from '@/mixins/validationRules.js'

export default {
  props: ['value', 'label', 'disabled', 'required', 'validOnBlur', 'readonly', 'outlined', 'dark', 'dense', 'solo'],
  data: () => ({
    dateFormatted: '',
    menu: false,
  }),

  computed: {
    virtualDate: {
      get() {
        return this.formatDate(this.date)
      },
      set(val) {
        this.date = this.parseDate(val)
      },
    },
    date: {
      get() {
        return this.value
      },
      set(val) {
        this.$emit('input', val)
      },
    },
  },
  watch: {
    date() {
      this.dateFormatted = this.formatDate(this.date)
    },
  },
  methods: {
    formatDate(date) {
      if (!date) return null

      const [year, month, day] = date.substring(0, 10).split('-')
      return `${day}/${month}/${year}`
    },
    parseDate(date) {
      if (!date || date.length < 10) return null

      const [day, month, year] = date.substring(0, 10).split('/')
      return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    },
  },
  directives: { mask },
  mixins: [validationRules],
}
</script>
