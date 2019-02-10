<template>
  <div class="editor">
    <div class="editor_menu">
      <LanguageSelector @language-selected="onLanguageSelected"/>
      <Actions/>
    </div>
    <codemirror ref="cm" v-model="code" :options="cmOptions"></codemirror>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import { codemirror } from "vue-codemirror";
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
    ...mapActions(["SET_CODE"])
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
.CodeMirror {
  border: 1px solid #eee;
  height: auto;
  margin: 6px 8px;
}
</style>

<style scoped>
.language-selector {
  width: 20%;
  display: inline-block;
}

.editor_actions {
  width: 75%;
}

.editor_menu {
  padding: 0.5% 0%;
  display: flex;
  align-items: center;
  align-content: center;
}
</style>
