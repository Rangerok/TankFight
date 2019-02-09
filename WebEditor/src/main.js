import Vue from "vue";
import VueRouter from "vue-router";
import App from "./App.vue";
import { store } from "./store";
import { router } from "./router";
import vSelect from "vue-select";

Vue.config.productionTip = false;

import VueMaterial from "vue-material";
import "vue-material/dist/vue-material.min.css";
import "vue-material/dist/theme/default.css";

Vue.component("v-select", vSelect);
Vue.use(VueRouter);
Vue.use(VueMaterial);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
