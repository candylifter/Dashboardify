import React, { PropTypes } from 'react'

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'

import { AuthAPI } from 'api'

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
  }

  handleSubmit (e) {
    e.preventDefault()

    let { name, email, password, repeatPassword, invitationCode } = this.refs

    let validation = AuthAPI.validateRegisterForm(name.input.value, email.input.value, password.input.value, repeatPassword.input.value, invitationCode.input.value)

    console.log(validation)
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
          floatingLabelText='Name'
          hintText='E. g. Darth Vader'
          errorText={this.state.nameError}
          type='text'
          ref='name'
          fullWidth
        />
        <TextField
          floatingLabelText='Email'
          hintText='E. g. darth.vader@empire.gov'
          errorText={this.state.emailError}
          type='email'
          ref='email'
          fullWidth
        />
        <TextField
          floatingLabelText='Password'
          errorText={this.state.passwordError}
          type='password'
          ref='password'
          fullWidth
        />
        <TextField
          floatingLabelText='Repeat password'
          errorText={this.state.repeatPasswordError}
          type='password'
          ref='repeatPassword'
          fullWidth
        />
        <TextField
          floatingLabelText='Invitation code'
          errorText={this.state.invitationCodeError}
          type='text'
          ref='invitationCode'
          fullWidth
        />
        <RaisedButton
          style={style.button}
          label='Sign up'
          type='submit'
          primary
          fullWidth
        />
      </form>
    )
  }
}

RegisterForm.propTypes = {}

export default RegisterForm
