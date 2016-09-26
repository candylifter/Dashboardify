import Cookies from 'js-cookie'

import { AuthAPI } from 'api'

export default {
  startLogin () {
    return {
      type: 'START_LOGIN'
    }
  },

  completeLogin () {
    return {
      type: 'COMPLETE_LOGIN'
    }
  },

  failLogin (err) {
    return {
      type: 'FAIL_LOGIN',
      err
    }
  },

  login (email, password) {
    return (dispatch) => {
      dispatch(this.startLogin())

      return AuthAPI.login(email, password)
        .then(
          (res) => {
            Cookies.set('ticket', res.data.SessionId) // TODO: add expiration date and replace SessionId with Ticket
            dispatch(this.completeLogin(res.data))
          },
          (err) => {
            console.error('Failed to logged:', err.message)
            dispatch(this.failLogin(err))
          }
        )
    }
  },

  logout () {
    let ticket = Cookies.get('ticket')

    if (ticket !== undefined) {
      AuthAPI.logout(ticket)
      Cookies.remove('ticket')
    }

    return {
      type: 'LOGOUT'
    }
  }
}