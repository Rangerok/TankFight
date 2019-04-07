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
  display: flex;
}

.actions {
  display: flex;
  flex-direction: column;
  align-items: stretch;
  align-content: stretch;
}

.actions > div,
.actions > .md-button,
.language-selector {
  margin: 0px 0px 8px 0px;
}

.md-button {
  margin: 0px;
  flex-grow: 1;
}

#app {
  padding: 60px;
  height: auto;
  text-align: center;
}
</style>
