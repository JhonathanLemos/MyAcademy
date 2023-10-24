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
          `/api/services/app/Product/GetAll?SkipCount=${skipCount}&MaxResultCount=${maxResultCount}${qrSearch}`
        );
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    create: actionWrapper({
      async action(_, payload) {
        return await http.post(`/api/services/app/Product/Create`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    update: actionWrapper({
      async action(_, payload) {
        return await http.put(`/api/services/app/Product/Update`, payload);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    delete: actionWrapper({
      async action(_, payload) {
        return await http.delete(`/api/services/app/Product/Delete?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getById: actionWrapper({
      async action(_, payload) {
        return await http.get(`/api/services/app/Product/Get?Id=${payload}`);
      },
      async onError(e) {
        return e.response.data;
      },
    }),
  },
};
