import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import debounce from 'debounce'

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'

import { ValidationAPI } from 'api'
import { AuthActions } from 'actions'

class LoginForm extends React.Component {
  constructor () {
    super()

    this.state = {
      emailError: '',
      passwordError: ''
    }

    this.handleSubmit = this.handleSubmit.bind(this)
    this.handleEmailChange = debounce(this.handleEmailChange.bind(this), 1000)
    this.handlePasswordChange = this.handlePasswordChange.bind(this)
  }

  handleEmailChange () {
    let { email } = this.refs

    this.setState({
      emailError: ValidationAPI.validateEmail(email.input.value)
    })
  }

  handlePasswordChange () {
    let { password } = this.refs

    this.setState({
      passwordError: ValidationAPI.validatePassword(password.input.value)
    })
  }

  handleSubmit (e) {
    e.preventDefault()
    let { email, password } = this.refs

    let validation = ValidationAPI.validateLoginForm(email.input.value, password.input.value)

    if (validation.hasErrors) {
      this.setState({
        emailError: validation.emailError,
        passwordError: validation.passwordError
      })
    } else {
      let { dispatch } = this.props
      dispatch(AuthActions.login(email.input.value, password.input.value))
    }
  }

  render () {
    const style = {
      padding: '0 1em 1em 1em',
      button: {
        margin: '2em 0 1em'
      }
    }

    return (
      <form style={style} onSubmit={this.handleSubmit}>
        <TextField
          floatingLabelText='Email'
          hintText='E. g. darth.vader@empire.gov'
          fullWidth
          type='text'
          ref='email'
          errorText={this.state.emailError}
          onChange={this.handleEmailChange}
          autoFocus
        />
        <TextField
          floatingLabelText='Password'
          fullWidth
          type='password'
          ref='password'
          errorText={this.state.passwordError}
          onChange={this.handlePasswordChange}
        />
        <RaisedButton
          style={style.button}
          label='Sign in'
          type='submit'
          primary
          fullWidth
        />
      </form>
    )
  }
}

LoginForm.propTypes = {
  dispatch: PropTypes.func
}

export default connect()(LoginForm)
