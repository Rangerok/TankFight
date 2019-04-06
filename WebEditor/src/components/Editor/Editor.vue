<template>
  <div class="editor">
    <div v-if="code" class="editor_content content">
      <div class="editor_menu menu">
        <LanguageSelector @language-selected="onLanguageSelected"/>
        <Actions @submit-started="onSubmitStarted" @submit-ended="onSubmitEnded"/>
      </div>
      <div class="editor_main main">
        <codemirror ref="cm" v-model="code" :options="cmOptions"></codemirror>
      </div>
    </div>
    <md-empty-state v-if="loading" md-label="Загрузка...">
      <md-progress-spinner :md-diameter="20" :md-stroke="2" md-mode="indeterminate"></md-progress-spinner>
    </md-empty-state>
    <md-empty-state v-if="error" :md-label="error"></md-empty-state>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import { codemirror } from "vue-codemirror";
import axios from "axios";
import LanguageSelector from "./LanguageSelector.vue";
import Actions from "./Actions.vue";

// languages
import "codemirror/mode/clike/clike.js";
import "codemirror/mode/go/go.js";
import "codemirror/mode/python/python.js";
import "codemirror/mode/javascript/javascript.js";
// theme css
import "codemirror/lib/codemirror.css";
import "codemirror/theme/base16-dark.css";

export default {
  name: "Editor",
  components: {
    codemirror,
    LanguageSelector,
    Actions
  },
  data() {
    return {
      loading: false,
      error: null,
      cmOptions: {
        autoCloseBrackets: true,
        lineWrapping: true,
        scrollbarStyle: null,
        tabSize: 4,
        mode: "text/javascript",
        theme: "base16-dark",
        lineNumbers: true,
        line: true,
        viewportMargin: Infinity
      }
    };
  },
  computed: {
    code: {
      get: function() {
        return this.CODE;
      },
      set: function(val) {
        this.SET_CODE(val);
      }
    },
    ...mapGetters(["CODE", "MODE"])
  },
  methods: {
    onLanguageSelected() {
      this.$refs.cm.codemirror.setOption("mode", this.MODE);
    },
    onSubmitStarted() {
      this.$refs.cm.codemirror.setOption("readOnly", true);
    },
    onSubmitEnded() {
      this.$refs.cm.codemirror.setOption("readOnly", false);
    },
    ...mapActions(["SET_CODE", "SET_LANGUAGES"])
  },
  mounted() {
    if (!this.code) {
      this.loading = true;
      axios
        .get("/api/language")
        .then(response => {
          this.SET_LANGUAGES(response.data);
        })
        .catch(() => {
          this.error = "Не удалось соединиться с сервером";
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
.CodeMirror {
  border: 1px solid #eee;
  height: auto;
  text-align: left;
}
</style>

<style scoped>
</style>
