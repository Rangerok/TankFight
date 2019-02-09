<template>
  <div class="header">
    <img alt="TankFight" src="../assets/tank.png">
    <h1>TankFight</h1>
  </div>
</template>

<script>
export default {
  name: "Header"
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
img,
h1 {
  margin: 6px 8px;
}

.header {
  display: flex;
  align-items: center;
  align-content: center;
}
</style>
