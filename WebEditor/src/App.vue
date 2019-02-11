<template>
  <div id="app">
    <Header/>
    <div class="content">
      <transition name="fade" mode="out-in">
        <router-view></router-view>
      </transition>
    </div>
  </div>
</template>

<script>
import { mapActions } from "vuex";
import Header from "./components/Header.vue";

export default {
  name: "app",
  components: {
    Header
  },
  methods: {
    ...mapActions(["GET_LANGUAGES"])
  },
  mounted() {
    this.GET_LANGUAGES();
  }
};
</script>
<style>
.content {
  width: 100%;
}

#app {
  padding: 60px;
  height: auto;
  text-align: left;
}
</style>
