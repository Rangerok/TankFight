import VueRouter from "vue-router";
import Editor from "../components/Editor/Editor.vue";
import BattleViewer from "../components/BattleViewer/BattleViewer.vue";
import NotFound from "../components/NotFound.vue";

export const router = new VueRouter({
  base: "app",
  routes: [
    { path: "/", component: Editor },
    { path: "/battle/:id", component: BattleViewer, props: true },
    { path: "*", component: NotFound }
  ]
});
