<template>
  <div class="battle-viewer content">
    <div class="battle-viewer_menu menu">
      <Actions
        @start-stop-clicked="startStopPlaying"
        @next-clicked="nextFrame"
        @previous-clicked="previousFrame"
      />
    </div>
    <div class="battle-viewer_main main">
      <unity
        src="../../unity/Build/Build.json"
        width="960"
        height="600"
        unityLoader="../../unity/Build/UnityLoader.js"
        :hideFooter="hideFooter"
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
  data() {
    return {
      hideFooter: true
    };
  },
  props: {
    battleId: String
  },
  methods: {
    startStopPlaying() {
      this.$refs.gameInstance.message("Main Camera", "StartStopPlaying", null);
    },
    nextFrame() {
      this.$refs.gameInstance.message("Main Camera", "GoToNextFrame", null);
    },
    previousFrame() {
      this.$refs.gameInstance.message("Main Camera", "GoToPreviousFrame", null);
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
