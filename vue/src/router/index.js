import Vue from "vue";
import VueRouter from "vue-router";
import Util from "@/lib/util";
import ViewAuth from "@/views/authenticate/ViewAuth.vue"
import ViewGlobal from "@/views/globalViews/ViewGlobal.vue"
import ViewUsuariosNovo from "@/views/usuarios/ViewUsuariosNovo.vue"
import Register from "../components/Register.vue"
import ViewUsuariosCreate from "@/views/usuarios/ViewUsuariosCreate.vue"
import ViewOperacoes from "@/views/operacoes/ViewOperacoes.vue"
import ViewClientes from "../views/clientes/ViewClientes.vue"
import ViewExercicios from "../views/exercicios/ViewExercicios.vue"
import ViewExerciciosCreate from "../views/exercicios/ViewExerciciosCreate.vue"
import ViewClientesCreate from "../views/clientes/ViewClientesCreate.vue"
import ViewPaginaInicial from "../views/paginaInicial/ViewPaginaInicial.vue"
import PasswordRecovery from "../views/authenticate/PasswordRecovery.vue"
import PerfilCreateView from "../views/perfil/PerfilCreateView.vue"
import ViewTreinos from "../views/treinos/ViewTreinos.vue"
import ViewTreinosEdit from "../views/treinos/ViewTreinosEdit.vue"
import ViewTreinosCreate from "../views/treinos/ViewTreinosCreate.vue"
import ViewUsuariosEdit from "../views/usuarios/ViewUsuariosEdit.vue"

Vue.use(VueRouter);

export const defaultRoute = {
  path: "/",
  name: "ViewPaginaInicial",
  meta: {
    title: "Pagina Inicial",
  },
  component: ViewPaginaInicial
};

export const registerRoute = {
  path: "/usuarios/register",
  name: "Register",
  meta: {
    title: "Register",
  },
  component: Register
};

export const loginRoute = {
  path: "/login",
  name: "ViewAuth",
  meta: {
    title: "Autenticação",
  },
  component: ViewAuth,
};

export const appRoutes = [
  {
    path: "/home",
    name: "home",
    permission: "",
    meta: { title: "Home" },
    component: ViewGlobal,
    children: [
      {
        path: "administracao",
        permission: "",
        meta: { title: "Administração" },
        name: "ViewOperacoes",
        component: ViewOperacoes,
      },
      {
        path: "usuarios",
        props: true,
        meta: { title: "Usuários" },
        // permission: "Administrative.User.See",
        name: "ViewUsuarios",
        nome: "usuarios",
        component: ViewUsuariosNovo,
      },
      {
        path: "usuarios/Add",
        props: true,
        // permission: "Administrative.User.Edit",
        meta: { title: "Novo Usuário", editar: false },
        name: "ViewUsuariosEdit",
        component: ViewUsuariosEdit,
      },
      {
        path: "usuarios/edit/:id",
        props: true,
        permission: "Administrative.User.Edit",
        meta: { title: "Editar Usuário", editar: true },
        name: "ViewUsuariosEdit",
        component: ViewUsuariosEdit,
      },
      {
        path: "clientes",
        props: true,
        meta: { title: "Clientes" },
        name: "ViewClientes",
        component: ViewClientes,
      },
      {
        path: "clientes/Add",
        props: true,
        permission: "Administrative.Customer.Edit",
        meta: { title: "Novo Cliente", editar: false },
        name: "ViewClientesCreate",
        component: ViewClientesCreate,
      },
      {
        path: "clientes/edit/:id",
        props: true,
        permission: "Administrative.Customer.Edit",
        meta: { title: "Editar Cliente", editar: true },
        name: "ViewClientesCreate",
        component: ViewClientesCreate,
      },
      {
        path: "exercicios",
        props: true,
        meta: { title: "Exercicios"},
        name: "ViewExercicios",
        component: ViewExercicios,
      },
      {
        path: "exercicios/Add",
        props: true,
        meta: { title: "Novo Exercicio", editar: false },
        name: "ViewExerciciosCreate",
        component: ViewExerciciosCreate,
      },
      {
        path: "exercicios/edit/:id",
        props: true,
        meta: { title: "Editar Exercicio", editar: true },
        name: "ViewExercicios",
        component: ViewExerciciosCreate,
      },
      {
        path: "treinos",
        props: true,
        meta: { title: "treinos"},
        name: "ViewTreinos",
        component: ViewTreinos,
      },
      {
        path: "treinos/Add",
        props: true,
        meta: { title: "Novo Treino", editar: false },
        name: "ViewTreinosCreate",
        component: ViewTreinosCreate,
      },
      {
        path: "treinos/edit/:id",
        props: true,
        meta: { title: "Editar Treino", editar: true },
        name: "ViewTreinosEdit",
        component: ViewTreinosEdit,
      },
    ],
  },
];

const publicRoutes = [
  {
    path: "/public/recovery/:id/:code",
    name: "PasswordRecovery",
    meta: {
      title: "Página Pública",
      requiresAuth: false,
    },
    component: PasswordRecovery,
    props: true,
  }
]

const authRoutesOffMenu = [
  {
    permission: "Administrative.Informacao.Edit",
    path: "/perfil-create",
    name: "CriarPerfil",
    meta: {
      title: "Página Pública",
      requiresAuth: false,
    },
    component: PerfilCreateView,
    props: true,
  }
]

export const errorRoutes = [
  {
    path: "/forbbiden",
    name: "ViewErrorPermission",
    meta: {
      title: "Acesso Negado",
    },
    component: () => "@/views/authenticate/ViewAuth.vue", // SUBSTITUIR POR PAGINA DE ERRO
  },
];

const routes = [defaultRoute, registerRoute, ...publicRoutes, ...authRoutesOffMenu, ...appRoutes, ...errorRoutes, loginRoute];

const router = new VueRouter({
  mode: "hash",
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  Util.title(to.meta.title);
  if (to.name === 'PasswordRecovery') {
    next();
  }
  else if (to.name === 'ViewPaginaInicial') {
    next();
  }
  else if (to.path === '/usuarios/register' && Util.abp.session.userId == null) {
    next();
  }

  else if (!!Util.abp.session.userId && to.name === "Register") {
    Util.title(to.meta.title);
next({ name: "ViewOperacoes" }) }
  else if (!Util.abp.session.userId && to.name !== "ViewAuth") {
    next({ name: "ViewAuth" });
  } else if (!!Util.abp.session.userId && to.name === "ViewAuth") {
    Util.title(to.meta.title);

    next({ name: "ViewOperacoes" });
  } else {
    const curRouterObj = Util.getRouterObjByName([...appRoutes, ...authRoutesOffMenu], to.name);
    if (curRouterObj && curRouterObj.permission) {
      if (window.abp.auth.isGranted(curRouterObj.permission)) {
        Util.toDefaultPage([...appRoutes, ...authRoutesOffMenu], to.name, router, next);
      } else {
        next({ replace: true, name: "ViewErrorPermission" });
      }
    } else {
      Util.toDefaultPage([...routes], to.name, router, next);
    }
  }
});

export default router;
