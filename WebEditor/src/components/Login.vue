<template>
  <div class="login">
    <md-empty-state md-label="Чтобы играть авторизуйтесь" class="md-primary">
      <md-button href="/auth/signin?provider=GitHub" class="md-primary">через GitHub</md-button>
    </md-empty-state>
  </div>
</template>

<script>
export default {
  name: "Login"
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
