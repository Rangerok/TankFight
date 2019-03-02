import VueRouter from "vue-router";
import auth from "../auth";
import Editor from "../components/Editor/Editor.vue";
import BattleViewer from "../components/BattleViewer/BattleViewer.vue";
import Login from "../components/Login.vue";
import NotFound from "../components/NotFound.vue";

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
      path: "/battle/:id",
      component: BattleViewer,
      props: true,
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
