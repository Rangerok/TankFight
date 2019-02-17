<template>
  <div class="editor_actions actions">
    <md-button class="md-raised md-dense" @click="toBattle">Тестовый бой</md-button>
    <md-button class="md-raised md-dense">Отправить решение</md-button>

    <md-progress-spinner v-if="loading" :md-diameter="20" :md-stroke="2" md-mode="indeterminate"></md-progress-spinner>
    <md-dialog-alert :md-active.sync="showError" :md-content="error" md-confirm-text="ОК"/>
  </div>
</template>

<script>
import axios from "axios";
import { mapGetters } from "vuex";

export default {
  name: "Actions",
  data: () => ({
    showError: false,
    error: "",
    loading: false
  }),
  computed: {
    ...mapGetters(["LANGUAGE", "CODE"])
  },
  methods: {
    toBattle() {
      this.$emit("submit-started");
      this.loading = true;
      axios
        .post("/api/submit", {
          language: this.LANGUAGE.id,
          code: this.CODE
        })
        .then(submitResponse => {
          this.$router.push("/battle/" + submitResponse.data);
        })
        .catch(err => {
          this.error = err.response.data.error;
          this.showError = true;
        })
        .finally(() => {
          this.loading = false;
          this.$emit("submit-ended");
        });
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
