import http from "@/lib/http.js";
import actionWrapper from "@/lib/helpers/action-wrapper";
import axios from 'axios';
axios.defaults.baseURL = 'https://localhost:44311';
export default {
  namespaced: true,
  actions: {

    getAll: actionWrapper({
      async action(_, payload) {
          let qrSearch =
          payload.search && payload.search.search != null ? `&Keyword=${payload.search.search}` : "";
          let skipCount = payload.pageNumber * payload.itemsPerPage;
          let maxResultCount = payload.itemsPerPage;
          let active = payload.search != null && payload.search.active != null ? `&IsActive=${payload.search.active}` : ''
          let date = payload.search != null && payload.search.date != null ? `&Date=${payload.search.date}` : ''
        return await http.get(
          `/api/services/app/User/GetAll?SkipCount=${skipCount}&MaxResultCount=${maxResultCount}${qrSearch}${active}${date}`
        );
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    async create (_, payload){
      try {
      const response = await axios.post('/api/services/app/User/Create', payload); // Substitua "/api/users" pelo endpoint desejado
      return response.data
    } catch (error){
      return error.response.data.error.message
    }

    },

    ObterFotoUsuarioLogado: actionWrapper({
      async action(context, payload) {
        let response = await http.get(
          "/api/services/app/User/ObterFotoUsuarioLogado"
        );
        context.state.imagem =
        "data:image/jpg;base64," + response.data.result;
        context.state.imagemBytes = response.data.result;
       return "data:image/jpg;base64," + response.data.result;
      },
      async onError(e) {
        return e.response;
      },
    }),


    async generateCodeToValidateUser(_, payload){
      try {
        const response = await axios.post('/api/services/app/User/GenerateCodeToValidateUser', payload); // Substitua "/api/users" pelo endpoint desejado
        return response.data
      } catch (error){
        return error.message
      }
    },

    async verifyEmailAndCode(_, payload){
      try {
        const response = await axios.post('/api/services/app/User/VerifyUserAndCode', payload); // Substitua "/api/users" pelo endpoint desejado
        return response.data
      } catch (error){
        return error.message
      }
    },

    UpdateFotoUsuarioLogado: actionWrapper({
      async action(context, payload) {
        return await http.put(
          "/api/services/app/User/UpdateFotoUsuarioLogado",
          payload
        );
      },
      async onError(e) {
        return e.response;
      },
    }),

    validateEmail: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/User/ValidateEmail`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    recoveryPassword: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/User/RecoveryPassword?Email=${payload}`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    async validateRecoveryToken(_, payload){
      try {
        const response = await axios.post(`/api/services/app/User/ValidateRecoveryToken`, payload); // Substitua "/api/users" pelo endpoint desejado
        return response.data
      } catch (error){
        return error.message
      }
    },

    async getByEmailOrUserName (_, payload){
      try {
        const response = await axios.get(`/api/services/app/User/GetByEmailOrUserName?Email=${payload}`);
        return response.data
      } catch (error){
        return error.message
      }
    },

    async resetPassword(_, payload){
      try {
        const response = await axios.post('/api/services/app/User/ResetPassword', payload); // Substitua "/api/users" pelo endpoint desejado
        return response.data
      } catch (error){
        return error.message
      }
    },
    update: actionWrapper({
      async action(_, payload) {
        return await http.put(`/api/services/app/User/Update`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    createOrUpdate: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/User/CreateOrUpdate`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),
    delete: actionWrapper({
      async action(_, payload) {
        return await http.delete(`/api/services/app/User/Delete?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getById: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/User/Get?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),


    getObjetivo: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/User/GetObjetivo?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    checkUserEmail: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/User/CheckUserEmail?Email=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    setDefaultPassword: actionWrapper({
      async action(_, payload) {
        var result = await http.post(`/api/services/app/User/SetDefaultPassword?userId=${payload}`)
        return result;
      },
      async onError(e) {
        return e;
      }
    }),

    getUserByEmail: actionWrapper({
      async action(_, payload) {
        var result = await http.get(`/api/services/app/User/GetUserByEmail?email=${payload}`)
        return result;
      },
      async onError(e) {
        return e;
      }
    }),
  },
};
