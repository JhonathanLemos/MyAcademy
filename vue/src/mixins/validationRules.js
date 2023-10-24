export default {
  data: () => ({
    rules: {
      required: (value) => !!value || "Campo obrigatório",
      requiredSelect: (value) => !value || "Campo obrigatório",
      selectRequired: (value) => value.length == 0 || "Campo obrigatório",
      email: (value) => {
        const pattern =
          /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return pattern.test(value) || "O e-mail informado é inválido.";
      },
      emailNotRequired: (value) => {
        if (!value)
        return;
        const pattern =
          /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return pattern.test(value) ||"O e-mail informado é inválido.";
      },
      cep: (value) => {
        const pattern = /^[0-9]{5}[0-9]{3}$/;
        return pattern.test(value) || "O CEP informado é inválido";
      },
      time: (value) => {
        const pattern = /^(?:[01]\d|2[0123]):(?:[012345]\d)$/;
        return pattern.test(value);
      },
      date: (stringData) => {
        if (!stringData) return true
        let dadosData = stringData.split('/')

        if (dadosData.length !== 3) return 'Data inválida'

        let ano = Number(dadosData[2])
        let mes = Number(dadosData[1])
        let dia = Number(dadosData[0])

        if (!Date.parse(ano + '-' + mes + '-' + dia)) return 'Data inválida'
        else if (mes === 2 && dia > 29) return 'Data inválida'
        else if ((mes === 4 || mes === 6 || mes === 9 || mes === 11) && dia > 30)
          return 'Data inválida'
        else if (dia > 31) return 'Data inválida'
        else return true
      },
      strongPassword: (value) => {
        const pattern = /^(?=.*\d)(?=.*[a-z])[0-9a-zA-Z!@#$&()-`.+,/\\]{6,}$/
        return pattern.test(value) || "Deve conter no mínimo 6 caracteres, sendo ao menos um numérico";
      },
      notZero: (value) => {
        if (!value) return 'Valor inválido'
        let valueCleaned = value.replace(/[^1-9 ]/g, '');
        if (!valueCleaned) return 'Valor inválido'
        return true
      },
      notZeroOther: (value) => {
        if (!value) return 'Campo Obrigatório'
        let valueCleaned = value.replace(/[^1-9 ]/g, '');
        if (!valueCleaned) return 'Campo Obrigatório'
        return true
      },
      emailCode: (value) => {
      if (!value) return 'Campo obrigatório!'

      const pattern = /^[0-9]{6}$/;
      return pattern.test(value) || "O código é inválido";
      },
      telefone: value => {
          if (value) {
            const pattern = /^\(([1-9]{2})\) (?:[2-8]|9[1-9])[0-9]{4}-[0-9]{4}$/;
            const pattern2 = /^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}-[0-9]{4}$/;
            return pattern.test(value) || pattern2.test(value) || 'O telefone informado é inválido'
        }
        else
            return null;
    },

    },
  }),
  methods: {

    setAlert(type, active, message) {
      this.type = type;
      this.active = active;
      this.success = true;
      this.message = message
      setTimeout(() => {
        this.active = false
      this.success = false;

      }, 5000);
    },

    cnpj (cnpj) {
      if (cnpj) {
        let cnpjInitial = cnpj;

        cnpj = cnpj.replace(/[^\d]+/g, "");
        // Elimina CNPJs invalidos conhecidos
        if (
          cnpj == "00000000000000" ||
          cnpj == "11111111111111" ||
          cnpj == "22222222222222" ||
          cnpj == "33333333333333" ||
          cnpj == "44444444444444" ||
          cnpj == "55555555555555" ||
          cnpj == "66666666666666" ||
          cnpj == "77777777777777" ||
          cnpj == "88888888888888" ||
          cnpj == "99999999999999"
        )
          return "O CNPJ informado é inválido";
        // Valida DVs
        let tamanho = cnpj.length - 2;
        let numeros = cnpj.substring(0, tamanho);
        let digitos = cnpj.substring(tamanho);
        let soma = 0;
        let pos = tamanho - 7;
        for (var i = tamanho; i >= 1; i--) {
          soma += numeros.charAt(tamanho - i) * pos--;
          if (pos < 2) pos = 9;
        }
        let resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
        if (resultado != digitos.charAt(0)) return "O CNPJ informado é inválido";
        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
          soma += numeros.charAt(tamanho - i) * pos--;
          if (pos < 2) pos = 9;
        }
        const pattern = /^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$/;

        resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
        if (resultado != digitos.charAt(1)) return "O CNPJ informado é inválido";
        else if (!pattern.test(cnpjInitial))
          return "O CNPJ informado é inválido";
        else return true;
      }
    },
  },
};
