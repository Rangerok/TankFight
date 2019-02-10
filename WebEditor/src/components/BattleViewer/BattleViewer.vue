<template>
  <div class="battle-viewer">
    <div class="battle-viewer_menu">
      <Actions/>
    </div>
    <iframe :src="battleUrl" scrolling="no"></iframe>
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
  },
  computed: {
    battleUrl: function() {
      return "/view-battle/?battleId=" + this.id;
    }
  }
};
</script>

<style>
iframe {
  width: 960px !important;
  height: 560px !important;
  overflow: hidden;
}
</style>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h2,
iframe {
  margin: 6px 8px;
}

.battle-viewer_menu {
  padding: 0.5% 0%;
  display: flex;
  align-items: center;
  align-content: center;
}
</style>
