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
import Header from "./components/Header.vue";

export default {
  name: "app",
  components: {
    Header
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
