import Cookies from 'js-cookie'

import { AuthAPI } from 'api'

export default {
  startLogin () {
    return {
      type: 'START_LOGIN'
    }
  },

  completeLogin (res) {
    Cookies.set('ticket', res.SessionId)
    console.log('Logged in with ticket:', res.SessionId)
    return {
      type: 'COMPLETE_LOGIN'
    }
  },

  failLogin (err) {
    console.error('Failed to logged:', err.message)
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
          (res) => dispatch(this.completeLogin(res.data)),
          (err) => dispatch(this.failLogin(err))
        )
    }
  }
}
