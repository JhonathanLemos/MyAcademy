import moment from "moment";

const filters = [
  {
    name: "capitalize",
    handler: function (value) {
      if (typeof value !== "string") return "";
      return value.charAt(0).toUpperCase() + value.slice(1);
    },
  },
  {
    name: "truncate",
    handler: function (value, lenght) {
      if (!value) return "";
      return value.length <= lenght
        ? value
        : String(value).slice(0, lenght).concat(["..."]);
    },
  },
  {
    name: "quantityUsd",
    handler: function (value) {
      if (!value) return '';
      return `USD ${value.toFixed(3)}`
    },
  },
  {
    name: "currencyFormat",
    handler: function (value) {
      var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
      });
      let formatedValue = formatter.format(value);
      return formatedValue.replace('$', 'USD ')
    }

  },
  {
    name: "currencyFormatReal",
    handler: function (value) {
      if (!value) return;
      value = value.toString().replace(',', ".")
      var formatter = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
      });
      let formatedValue = formatter.format(value);
      return formatedValue;
    }

  },

  {
    name: "currencyFormatRealWithoutCurrency",
    handler: function (value) {
      if (!value) return;
      value = value.toString().replace(',', ".")
      var formatter = new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
      });
      let formatedValue = formatter.format(value);
      var a = formatedValue.split("$")
      return a[1];
    }

  },
  {
    name: "enumObjetivo",
    handler: function (value) {
      if (!value) return '';

      switch (value) {
        case 1:
        return 'Perder Peso';
          case 2:
          return 'Ganhar Massa';
            case 3:
            return 'Melhorar Condicionamento';
              case 4:
              return 'TonificarOCorpo';
                  default:
                  return '';
      }
    },
  },
  {
    name: "time",
    handler: function (value) {
      if (!value) return "";
      let dateTime = new Date(value);
      return moment(dateTime).format("HH:mm");
    },
  },
  {
    name: "date",
    handler: function (value) {
      if (!value) return "";
      let dateTime = new Date(value);
      return moment(dateTime).format("DD/MM/YYYY");
    },
  },
  {
    name: "formattedDate",
    handler: function (date) {
      if (!date) return null;

      const [year, month, day] = date.split("-");
      return `${day}/${month}/${year}`;
    },
  },
  {
    name: "dateFormat",
    handler: function (value) {
      if (!value) return "";
      //let dateTime = new Date(value);
      return moment(value, "YYYY-MM-DD").format("DD/MM/YYYY");
    },
  },
  {
    name: "longDate",
    handler: function (value) {
      moment.locale("pt");
      if (!value) return "";
      let dateTime = new Date(value);
      return moment(dateTime).format("DD [de] MMMM [de] YYYY");
    },
  },
  {
    name: "dateTime",
    handler: function (value) {
      if (!value) return "";
      let dateTime = new Date(value);
      return moment(dateTime).format("DD/MM/YYYY HH:mm");
    },
  },
  {
    name: "formatDate",
    handler: function (value) {
      if (value) {
        const [day, month, year,] = value.split('/')
        const yearFormat = year.split(' ');
        return `${day}/${month}/${yearFormat[0]}`
      }
    }
  },
  {
    name: "longDateTime",
    handler: function (value) {
      moment.locale("pt");
      if (!value) return "";
      let dateTime = new Date(value);
      return moment(dateTime).format("DD [de] MMMM [de] YYYY [às] HH:mm");
    },
  },
  {
    name: "somenteNumeros",
    handler: function (value) {
      if (value) {
        return value.replace(/\D/g, "");
      } else {
        return "";
      }
    },
  },
  {
    name: "AtivoInativo",
    handler: function (value) {
      return value ? "Ativo" : "Inativo";
    },
  },

  {
    name: "Telefone",
    handler: function (value) {
      if (value) {
        value = value.replace(/\D/g, "");
        value = value.replace(/^(\d{2})(\d)/g, "($1) $2");

        if (value.lenght === 11) {
          value = value.replace(/(\d)(\d{5})$/, "$1-$2");
        } else {
          value = value.replace(/(\d)(\d{4})$/, "$1-$2");
        }

        return value;
      } else {
        return null;
      }
    },
  },
];

export default {
  install(Vue) {
    filters.forEach((filter) => {
      Vue.filter(filter.name, filter.handler);
    });
  },
};
