import axios from "axios";

export default {
  logout() {
    axios.get("/auth/signout");
  },
  async loggedIn() {
    return await axios.get("/auth");
  }
};
