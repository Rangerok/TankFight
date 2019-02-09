<template>
  <div class="editor_actions">
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
      this.loading = true;
      axios
        .post("/api/create", {
          language: this.LANGUAGE.id,
          code: this.CODE
        })
        .then(response => {
          this.$router.push("/battle/" + response.tag);
        })
        .catch(error => {
          this.error = "Не получилось создать образ";
          this.showError = true;
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.editor_actions {
  display: flex;
  align-items: center;
  align-content: center;
}
</style>
