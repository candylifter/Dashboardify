import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'

import { ValidationAPI } from 'api'
import { AuthActions } from 'actions'

class RegisterForm extends React.Component {
  constructor () {
    super()

    this.state = {
      nameError: '',
      emailError: '',
      passwordError: '',
      repeatPasswordError: '',
      invitationCodeError: ''
    }

    this.handleSubmit = this.handleSubmit.bind(this)
    this.handleNameChange = this.handleNameChange.bind(this)
    this.handleEmailChange = this.handleEmailChange.bind(this)
    this.handlePasswordChange = this.handlePasswordChange.bind(this)
    this.handleRepeatPasswordChange = this.handleRepeatPasswordChange.bind(this)
    this.handleInvitationCodeChange = this.handleInvitationCodeChange.bind(this)
  }

  handleSubmit (e) {
    e.preventDefault()

    let { name, email, password, repeatPassword, invitationCode } = this.refs

    let validation = ValidationAPI.validateRegisterForm(name.input.value, email.input.value, password.input.value, repeatPassword.input.value, invitationCode.input.value)
    let { nameError, emailError, passwordError, repeatPasswordError, invitationCodeError } = validation

    if (validation.hasErrors) {
      this.setState({nameError, emailError, passwordError, repeatPasswordError, invitationCodeError})
    } else {
      let { dispatch } = this.props
      dispatch(AuthActions.register(name.input.value, email.input.value, repeatPassword.input.value, invitationCode.input.value))
    }
  }

  handleNameChange () {
    let { name } = this.refs

    this.setState({
      nameError: ValidationAPI.validateName(name.input.value)
    })
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

  handleRepeatPasswordChange () {
    let { password, repeatPassword } = this.refs

    this.setState({
      repeatPasswordError: ValidationAPI.validateRepeatPassword(password.input.value, repeatPassword.input.value)
    })
  }

  handleInvitationCodeChange () {
    let { invitationCode } = this.refs

    this.setState({
      invitationCodeError: ValidationAPI.validateInvitationCode(invitationCode.input.value)
    })
  }

  render () {
    return (
      <form className='auth-form' onSubmit={this.handleSubmit}>
        <TextField
          floatingLabelText='Name'
          hintText='E. g. Darth Vader'
          onChange={this.handleNameChange}
          errorText={this.state.nameError}
          type='text'
          ref='name'
          fullWidth
          autoFocus
        />
        <TextField
          floatingLabelText='Email'
          hintText='E. g. darth.vader@empire.gov'
          noValidate
          onChange={this.handleEmailChange}
          errorText={this.state.emailError}
          type='email'
          ref='email'
          fullWidth
        />
        <TextField
          floatingLabelText='Password'
          onChange={this.handlePasswordChange}
          errorText={this.state.passwordError}
          type='password'
          ref='password'
          fullWidth
        />
        <TextField
          floatingLabelText='Repeat password'
          onChange={this.handleRepeatPasswordChange}
          errorText={this.state.repeatPasswordError}
          type='password'
          ref='repeatPassword'
          fullWidth
        />
        <TextField
          floatingLabelText='Invitation code'
          onChange={this.handleInvitationCodeChange}
          errorText={this.state.invitationCodeError}
          type='text'
          ref='invitationCode'
          fullWidth
        />
        <RaisedButton
          className='auth-form__button'
          label='Sign up'
          type='submit'
          primary
          fullWidth
        />
      </form>
    )
  }
}

RegisterForm.propTypes = {
  dispatch: PropTypes.func
}

export default connect()(RegisterForm)
