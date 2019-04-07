<template>
  <div class="battle-viewer_actions actions">
    <div class="battle-viewer_playing-menu">
      <md-button class="md-raised" @click="previousClicked">
        <md-icon>skip_previous</md-icon>
      </md-button>
      <md-button class="md-raised" @click="startStopClicked">
        <md-icon>play_circle_filled</md-icon>
      </md-button>
      <md-button class="md-raised" @click="nextClicked">
        <md-icon>skip_next</md-icon>
      </md-button>
    </div>
    <md-button class="md-raised" @click="toEditor">Назад</md-button>
  </div>
</template>

<script>
export default {
  name: "Actions",
  methods: {
    toEditor() {
      this.$router.push("/");
    },
    startStopClicked() {
      this.$emit("start-stop-clicked");
    },
    nextClicked() {
      this.$emit("next-clicked");
    },
    previousClicked() {
      this.$emit("previous-clicked");
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.battle-viewer_playing-menu {
  display: flex;
  flex-direction: row;
}
</style>
