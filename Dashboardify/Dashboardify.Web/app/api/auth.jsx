import axios from 'axios'
import Cookies from 'js-cookie'

const rootDomain = `http://${window.location.hostname}/api`

export default {
  login (email, password) {
    let data = {
      Email: email,
      Password: password
    }

    return axios.post(`${rootDomain}/login/index`, data)
  },

  logout (ticket) {
    return axios.get(`${rootDomain}/login/LogOut?ticket=${ticket}`)
  },

  isLoggedIn () {
    return !!Cookies.get('ticket')
  },

  register (name, email, password, invitationCode) {
    let data = {
      Name: name,
      Email: email,
      Password: password
    }

    return axios.post(`${rootDomain}/Users/Create?inviteCode=${invitationCode}`, data)
  }
}
