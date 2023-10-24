/* eslint-disable */
import axios from 'axios'
import appconst from './appconst'
import store from '@/store'

const http = axios.create({
  baseURL: appconst.remoteServiceBaseUrl,
  timeout: 84000
})

http.interceptors.request.use(function (config) {

  if (!!window.abp.auth.getToken()) {
    config.headers.common["Authorization"] = "Bearer " + window.abp.auth.getToken();
  }

  config.headers.common[".AspNetCore.Culture"] = window.abp.utils.getCookieValue("Abp.Localization.CultureName");
  config.headers.common["Abp.TenantId"] = window.abp.multiTenancy.getTenantIdCookie();
 
  return config;
}, function (error) {

  return Promise.reject(error);

})

http.interceptors.response.use(function (response) {

  return response;
  
}, function (error) {

  if(error.response.status == 401){

    store.dispatch({type: 'ModuleSession/logout'})
  }

  return Promise.reject(error);

});

export default http;