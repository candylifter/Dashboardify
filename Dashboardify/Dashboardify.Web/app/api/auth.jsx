import axios from 'axios'
import Cookies from 'js-cookie'

const rootDomain = `http://${window.location.hostname}/api`

export default {
  login (email, password) {
    let data = {
      User: {
        Email: email,
        Password: password
      }
    }

    return axios.post(
      `${rootDomain}/login/index`, data
    )
  },

  logout (ticket) {
    let data = {
      Ticket: ticket
    }

    return axios.post(`${rootDomain}/login/LogOut`, data)
  },

  isLoggedIn () {
    return !!Cookies.get('ticket') // TODO: Add expiration check (dunno if it's necessary here)
  },

  register (name, email, password, invitationCode) {
    let data = {
      Username: name,
      Email: email,
      Password: password,
      InvitationCode: invitationCode
    }

    return axios.post(`${rootDomain}/Users/Create`, data)
  }
}
