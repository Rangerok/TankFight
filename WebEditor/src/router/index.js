import VueRouter from "vue-router";
import auth from "../auth";
const Editor = () => import("../components/Editor/Editor.vue");
const Login = () => import("../components/Login.vue");
const NotFound = () => import("../components/NotFound.vue");
const BattleViewer = () =>
  import("../components/BattleViewer/BattleViewer.vue");

function requireAuth(to, from, next) {
  auth
    .loggedIn()
    .then(() => {
      next();
    })
    .catch(() => {
      next({
        path: "/login"
      });
    });
}

export const router = new VueRouter({
  base: "app",
  routes: [
    { path: "/", component: Editor, beforeEnter: requireAuth },
    { path: "/login", component: Login },
    {
      path: "/battle",
      component: BattleViewer,
      props: route => ({ battleId: route.query.battleId }),
      beforeEnter: requireAuth
    },
    {
      path: "/logout",
      beforeEnter(to, from, next) {
        auth.logout();
        next("/");
      }
    },
    { path: "*", component: NotFound }
  ]
});
