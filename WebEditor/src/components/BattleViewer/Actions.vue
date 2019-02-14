<template>
  <div class="battle-viewer_actions actions">
    <md-button class="md-raised md-dense" @click="toEditor">Назад</md-button>
  </div>
</template>

<script>
export default {
  name: "Actions",
  methods: {
    toEditor() {
      this.$router.push("/");
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.md-button {
  margin: 6px 0px;
  width: 100%;
}
</style>
