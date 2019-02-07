<template>
  <div id="app">
    <Header/>
    <Editor/>
    <Actions/>
  </div>
</template>

<script>
import Header from "./components/Header.vue";
import Editor from "./components/Editor.vue";
import Actions from "./components/Actions.vue";

export default {
  name: "app",
  components: {
    Header,
    Editor,
    Actions
  }
};
</script>
<style>
#app {
  padding: 60px;
  height: auto;
  text-align: left;
}

.editor,
.actions,
.header {
  width: 100%;
  padding: 0.1% 2%;
}
</style>
