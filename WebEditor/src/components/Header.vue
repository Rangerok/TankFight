<template>
  <div class="header">
    <md-toolbar md-elevation="0" class="md-transparent no-padding">
      <div class="md-toolbar-row">
        <img alt="TankFight" src="../assets/tank.png">
        <h1>TankFight</h1>
      </div>
    </md-toolbar>
  </div>
</template>

<script>
export default {
  name: "Header"
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1 {
  margin: 0% 2%;
}

.no-padding {
  padding: 0%;
}
</style>
