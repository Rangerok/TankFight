<template>
  <div class="battle-viewer content">
    <div class="battle-viewer_menu menu">
      <Actions/>
    </div>
    <div class="main">
      <iframe :src="battleUrl" scrolling="no"></iframe>
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
</style>
