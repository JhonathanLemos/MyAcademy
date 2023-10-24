import http from "@/lib/http.js";
import actionWrapper from "@/lib/helpers/action-wrapper";

export default {
  namespaced: true,
  actions: {

    getAll: actionWrapper({
      async action(_, payload) {
        let qrSearch =
          payload.search != null ? `&Keyword=${payload.search}` : "";
        let skipCount = payload.pageNumber * payload.itemsPerPage;
        let maxResultCount = payload.itemsPerPage;
        return await http.get(
          `/api/services/app/Informacao/GetAll?SkipCount=${skipCount}&MaxResultCount=${maxResultCount}${qrSearch}`
        );
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    create: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/Informacao/Create`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    createOrUpdate: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/Informacao/CreateOrUpdate`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    update: actionWrapper({
      async action(_, payload) {
        return await http.put(`/api/services/app/Informacao/Update`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    delete: actionWrapper({
      async action(_, payload) {
        return await http.delete(`/api/services/app/Informacao/Delete?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getById: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/Informacao/Get?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getByUserId: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/Informacao/GetByUserId?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    VerifyIfInformacaoIsEnded: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/Informacao/VerifyIfInformacaoIsEnded`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    setAsFinalizado: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/Informacao/setAsFinalizado`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),
  },
};
