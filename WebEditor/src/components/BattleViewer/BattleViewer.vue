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
      <md-card class="bot-output">
        <md-card-header>
          <div class="md-subheading">Вывод бота</div>
        </md-card-header>
        <md-card-content class="md-scrollbar">
          <div class="bot-output_line" v-for="output in BOT_OUTPUT" :key="output">{{ output }}</div>
        </md-card-content>
      </md-card>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import Unity from "vue-unity-webgl";
import Actions from "./Actions.vue";
import { RegisterUnityListener } from "./unity-listener.js";

export default {
  name: "BattleViewer",
  components: {
    Unity,
    Actions
  },
  props: {
    battleId: String
  },
  data() {
    return {
      hideFooter: true
    };
  },
  computed: {
    ...mapGetters(["BOT_OUTPUT"])
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
    },
    setOutput(output) {
      this.SET_BOT_OUTPUT(output.split("\n"));
    },
    ...mapActions(["SET_BOT_OUTPUT"])
  },
  mounted() {
    RegisterUnityListener("SetBotOutput", this.setOutput.bind(this));
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.bot-output {
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  padding: 0px 24px;
  min-width: 300px;
}

.md-card-content {
  max-height: 520px;
  overflow: auto;
}
</style>
