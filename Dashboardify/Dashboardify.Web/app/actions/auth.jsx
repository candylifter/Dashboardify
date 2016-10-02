import Cookies from 'js-cookie'
import moment from 'moment'

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
            Cookies.set('ticket', res.data.SessionId)
            // Cookies.set('ticket', res.data.SessionId, { expires: moment(res.data.ExpireDate).toDate() })

            dispatch(this.completeLogin(res.data))
          },
          (err) => {
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
  },

  startRegister () {
    return {
      type: 'START_REGISTER'
    }
  },

  completeRegister () {
    return {
      type: 'COMPLETE_REGISTER'
    }
  },

  failRegister (err) {
    return {
      type: 'FAIL_REGISTER',
      err
    }
  },

  register (name, email, password, invitationCode) {
    return (dispatch) => {
      dispatch(this.startRegister())

      return AuthAPI.register(name, email, password, invitationCode)
        .then(
          (res) => dispatch(this.completeRegister()),
          (err) => dispatch(this.failRegister(err))
        )
    }
  }
}
