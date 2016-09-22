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

    console.log(JSON.stringify(Data))

    return axios.post(
      `${rootDomain}/login/index`, Data
    )
    .then((res) => console.log(res.data))
    .catch((err) => console.log(err))
  },

  register () {
  }
}
