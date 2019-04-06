<template>
  <div class="editor_actions actions">
    <md-button class="md-raised" :disabled="loading" @click="toBattle">Тестовый бой</md-button>
    <md-button class="md-raised" :disabled="loading">Отправить решение</md-button>
    <div>
      <md-progress-spinner v-if="loading" :md-diameter="20" :md-stroke="2" md-mode="indeterminate"></md-progress-spinner>
    </div>
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
          this.$router.push({
            path: "/battle",
            query: { battleId: submitResponse.data }
          });
        })
        .catch(err => {
          if (err.response.data.error) {
            this.error = err.response.data.error;
          } else {
            this.error = "Не удалось создать бота, попробуйте еще раз.";
          }
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
</style>
