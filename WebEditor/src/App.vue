<template>
  <div id="app">
    <Header/>
    <transition name="fade" mode="out-in">
      <router-view></router-view>
    </transition>
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
  margin: 6px 8px;
  padding: 0.5% 0%;
  display: flex;
  align-items: stretch;
  align-content: center;
}

.menu {
  width: 15%;
  display: flex;
  flex-direction: column;
  align-items: center;
  align-content: center;
}

.main {
  text-align: left;
  margin-left: 2.5%;
}

.actions,
.main {
  width: 100%;
}

#app {
  padding: 60px;
  height: auto;
  text-align: center;
}
</style>
