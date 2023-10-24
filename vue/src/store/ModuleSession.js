import Http from "@/lib/http.js";
import Util from "@/lib/util.js";
import appconst from "@/lib/appconst.js";
import actionWrapper from "@/lib/helpers/action-wrapper";

export default {
  namespaced: true,

  state: {
    application: null,
    user: null,
    tenant: null,
  },

  getters: {
    version: (state) => state.application && state.application.version,
    userFullName: (state) => (state.user ? `${state.user.name}` : ""),
  },

  actions: {
    login: actionWrapper({
      async action(_, payload) {
        let response = await Http.post("/api/TokenAuth/Authenticate", payload);
        let tokenExpireDate = payload.rememberClient
          ? new Date(
            new Date().getTime() + 1000 * response.data.result.expireInSeconds
          )
          : undefined;
        Util.abp.auth.setToken(
          response.data.result.accessToken,
          tokenExpireDate
        );
        Util.abp.utils.setCookieValue(
          appconst.authorization.encrptedAuthTokenName,
          response.data.result.encryptedAccessToken,
          tokenExpireDate,
          Util.abp.appPath
        );
        return response
      },
      async onError(e) {
        return e.response.data;
      },
    }),


    logout: actionWrapper({
      async action() {
        Util.abp.auth.clearToken();
        window.localStorage.clear()
      },
      async onError(e) {
        return e.response.data;
      },
    }),

    getCurrentLoginInformation: actionWrapper({
      async action() {
        return await Http.get('/api/services/app/Session/GetCurrentLoginInformations')
      },
      async onError(e) {
        return e.response.data
      }
    }),

    init: actionWrapper({
      async action(context) {
        var tenant = Util.abp.multiTenancy.getTenantIdCookie();

        const rep = await Http.get(
          "/api/services/app/Session/GetCurrentLoginInformations",
          {
            headers: {
              "Abp.TenantId": tenant,
            },
          }
        );
        context.state.application = rep.data.result.application;
        context.state.user = rep.data.result.user;
        context.state.tenant = rep.data.result.tenant;
      },
      async onError(e) {
        return e;
      },
    }),
  },
};
