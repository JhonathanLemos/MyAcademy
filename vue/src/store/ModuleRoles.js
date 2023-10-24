import Http from "@/lib/http";
import actionWrapper from "@/lib/helpers/action-wrapper";

export default {
    namespaced: true,
    actions: {
        create: actionWrapper({
            action: async (context, payload) => {
                let retorno = await Http.post("/api/services/app/Role/Create", payload.data);
                return retorno
            },
            onError(error, defaultValue, context) {
                context.state.loading = false
                return error.response.data
            },
        }),
        update: actionWrapper({
            action: async (context, payload) => {
                let retorno = await Http.put("/api/services/app/Role/Update", payload.data);
                return retorno
            },
            onError(error, defaultValue, context) {
                context.state.loading = false
                return error.response.data
            },
        }),
        get: actionWrapper({
            action: async (context, payload) => {
                let reponse = await Http.get(
                    "/api/services/app/Role/Get?Id=" + payload.data.id
                );
                return reponse.data.result
            },
            onError(error, defaultValue, context) {
                context.state.loading = false
            },
        }),

        getAllRoles: actionWrapper({
          action: async (context, payload) => {
              let reponse = await Http.get(
                  `/api/services/app/Role/GetAllRoles`
              );
              return reponse.data.result
          },
          onError(error, defaultValue, context) {
              context.state.loading = false
          },
      }),
    },
}

