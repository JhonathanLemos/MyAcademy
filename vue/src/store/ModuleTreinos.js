import http from "@/lib/http.js";
import actionWrapper from "@/lib/helpers/action-wrapper";

export default {
  namespaced: true,
  actions: {

    getAll: actionWrapper({
      async action(_, payload) {

        let qrSearch =
        payload.search && payload.search.search != null ? `&Keyword=${payload.search.search}` : "";
        let skipCount = payload.pageNumber * payload.itemsPerPage;
        let maxResultCount = payload.itemsPerPage;
        return await http.get(
          `/api/services/app/Treino/GetAll?SkipCount=${skipCount}&MaxResultCount=${maxResultCount}${qrSearch}`
        );
      },
      async onError(e) {
        return e.response.data;
      },
    }),


    getTreinosByDataAndId: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/Treino/GetTreinosByDataAndId?Criacao=${payload.criacao}&Id=${payload.id}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    create: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/Treino/Create`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    update: actionWrapper({
      async action(_, payload) {
        return await http.put(`/api/services/app/Treino/Update`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    delete: actionWrapper({
      async action(_, payload) {
        return await http.delete(`/api/services/app/Treino/Delete?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getById: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/Treino/Get?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getExercicesList: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/Treino/GetExercicesList`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getUsersList: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/Treino/GetUsersList`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),
  },
};
