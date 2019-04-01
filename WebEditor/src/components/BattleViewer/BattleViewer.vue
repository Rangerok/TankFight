<template>
  <div class="battle-viewer content">
    <div class="battle-viewer_menu menu">
      <Actions/>
    </div>
    <div class="main">
      <unity
        src="../../unity/Build/Build.json"
        width="960"
        height="600"
        unityLoader="../../unity/Build/UnityLoader.js"
        hideFooter="true"
        ref="gameInstance"
      ></unity>
    </div>
  </div>
</template>

<script>
import Unity from "vue-unity-webgl";
import Actions from "./Actions.vue";

export default {
  name: "BattleViewer",
  components: {
    Unity,
    Actions
  },
  props: {
    battleId: String
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
