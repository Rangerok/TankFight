import Vue from "vue";
import App from "./App.vue";
import { store } from "./store";
import vSelect from "vue-select";

Vue.config.productionTip = false;

import VueMaterial from "vue-material";
import "vue-material/dist/vue-material.min.css";
import "vue-material/dist/theme/default.css";

Vue.component("v-select", vSelect);
Vue.use(VueMaterial);

new Vue({
  store,
  render: h => h(App)
}).$mount("#app");
