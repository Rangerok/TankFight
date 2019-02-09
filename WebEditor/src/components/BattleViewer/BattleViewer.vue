<template>
  <div class="battle-viewer">
    <div class="battle-viewer_menu">
      <Actions/>
    </div>
  </div>
</template>

<script>
import Actions from "./Actions.vue";

export default {
  name: "BattleViewer",
  components: {
    Actions
  },
  props: {
    id: String
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h2 {
  margin: 6px 8px;
}

.battle-viewer_menu {
  padding: 0.5% 0%;
  display: flex;
  align-items: center;
  align-content: center;
}
</style>
