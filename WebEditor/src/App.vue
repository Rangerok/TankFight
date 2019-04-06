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
  flex-direction: row;
}

.menu {
  flex-basis: 15%;
}

.main {
  text-align: left;
  margin-left: 2.5%;
  flex-basis: 80%;
}

.actions {
  display: flex;
  flex-direction: column;
  align-items: stretch;
  align-content: stretch;
}

.md-button {
  margin-left: 0px;
  margin-right: 0px;
}

#app {
  padding: 60px;
  height: auto;
  text-align: center;
}
</style>
