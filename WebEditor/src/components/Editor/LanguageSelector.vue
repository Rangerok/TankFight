<template>
  <div class="language-selector">
    <v-select
      @input="onInput"
      :options="LANGUAGES"
      :value="LANGUAGE"
      :searchable="false"
      :filterable="false"
      :clearable="false"
    ></v-select>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "LanguageSelector",
  computed: {
    ...mapGetters(["LANGUAGE", "LANGUAGES"])
  },
  methods: {
    onInput(value) {
      this.SET_LANGUAGE(value);
      this.$emit("language-selected");
    },
    ...mapActions(["SET_LANGUAGE"])
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.language-selector {
  width: 100%;
  font-size: larger;
  font-weight: bold;
}
</style>
