import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";

Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    languages: [],
    selectedLanguage: {},
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
    SET_LANGUAGES: (state, value) => {
      state.languages = value;
      state.selectedLanguage = value[0];
    },
    SET_LANGUAGE: (state, value) => {
      state.selectedLanguage = value;
      state.code = value.template;
    },
    SET_CODE: (state, value) => {
      state.code = value;
    }
  },
  actions: {
    GET_LANGUAGES: context => {
      axios.get("/api/language").then(response => {
        context.commit("SET_LANGUAGES", response.data);
      });
    },
    SET_LANGUAGE: (context, value) => {
      if (context.state.selectedLanguage != value) {
        context.commit("SET_LANGUAGE", value);
      }
    },
    SET_CODE: (context, value) => {
      context.commit("SET_CODE", value);
    }
  },
  modules: {}
});
