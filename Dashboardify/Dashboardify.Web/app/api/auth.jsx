import axios from 'axios'

const rootDomain = `http://${window.location.hostname}/api`

export default {
  login (email, password) {
    let Data = {
      User: {
        Email: email,
        Password: password
      }
    }

    return axios.post(
      `${rootDomain}/login/index`, Data
    )
  },

  register () {
  }
}
