import Vue from "vue";
import Vuex from "vuex";
import {
  goTemplate,
  csharpTemplate,
  jsTemplate,
  pythonTemplate
} from "./templates";

Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    languages: [
      {
        id: "go",
        label: "go",
        template: goTemplate,
        mode: "text/x-go"
      },
      {
        id: "csharp",
        label: "С#",
        template: csharpTemplate,
        mode: "text/x-csharp"
      },
      {
        id: "python",
        label: "python",
        template: pythonTemplate,
        mode: "text/x-python"
      },
      {
        id: "javascript",
        label: "JavaScript",
        template: jsTemplate,
        mode: "text/javascript"
      }
    ],
    selectedLanguage: {
      id: "javascript",
      label: "JavaScript",
      template: jsTemplate,
      mode: "text/javascript"
    },
    code: null
  },
  getters: {
    LANGUAGES: state => {
      return state.languages;
    },
    LANGUAGE: state => {
      return state.selectedLanguage;
    },
    MODE: state => {
      return state.selectedLanguage.mode;
    },
    CODE: state => {
      if (state.code === null) {
        return state.selectedLanguage.template;
      }
      return state.code;
    }
  },
  mutations: {
    SET_LANGUAGE: (state, value) => {
      state.selectedLanguage = value;
      state.code = value.template;
    },
    SET_CODE: (state, value) => {
      state.code = value;
    }
  },
  actions: {
    SET_LANGUAGE: (context, value) => {
      context.commit("SET_LANGUAGE", value);
    },
    SET_CODE: (context, value) => {
      context.commit("SET_CODE", value);
    }
  },
  modules: {}
});
