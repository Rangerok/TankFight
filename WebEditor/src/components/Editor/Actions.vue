<template>
  <div class="editor_actions">
    <md-button class="md-raised md-dense" @click="toBattle">Тестовый бой</md-button>
    <md-button class="md-raised md-dense">Отправить решение</md-button>
  </div>
</template>

<script>
import axios from "axios";
import { mapGetters } from "vuex";

export default {
  name: "Actions",
  computed: {
    ...mapGetters(["LANGUAGE", "CODE"])
  },
  methods: {
    toBattle() {
      axios
        .post("api/create", {
          language: this.LANGUAGE.id,
          code: this.CODE
        })
        .then(response => {
          //
        })
        .catch(error => {
          console.log(error);
        });

      this.$router.push("/battle/1");
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
