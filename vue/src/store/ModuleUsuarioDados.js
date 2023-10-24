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
          `/api/services/app/UsuarioDado/GetAll?SkipCount=${skipCount}&MaxResultCount=${maxResultCount}${qrSearch}${active}${date}`
        );
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    createOrUpdate: actionWrapper ({
      async action(_, payload) {
        return await http.post(`/api/services/app/UsuarioDado/CreateOrUpdate`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    create: actionWrapper({
      async action(_, payload) {
        return await http.put(`/api/services/app/UsuarioDado/Create`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    update: actionWrapper({
      async action(_, payload) {
        return await http.put(`/api/services/app/UsuarioDado/Update`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    delete: actionWrapper({
      async action(_, payload) {
        return await http.delete(`/api/services/app/UsuarioDado/Delete?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getById: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/UsuarioDado/Get?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getByUserId: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/UsuarioDado/GetByUserId?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),
  },
};
