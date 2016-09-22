import React, { PropTypes } from 'react'

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'

import { AuthAPI } from 'api'

class LoginForm extends React.Component {
  constructor () {
    super()

    this.handleSubmit = this.handleSubmit.bind(this)
  }

  handleSubmit (e) {
    e.preventDefault()
    let { email, password } = this.refs

    AuthAPI.login(email.input.value, password.input.value)

    console.log(email.input.value, password.input.value)
  }

  render () {
    const style = {
      padding: '1em'
    }

    return (
      <form style={style} onSubmit={this.handleSubmit}>
        <TextField
          floatingLabelText='Email'
          hintText='darth.vader@empire.gov'
          type='text'
          fullWidth
          ref='email'
        />
        <TextField
          floatingLabelText='Password'
          hintText='IFeelTheForce'
          type='password'
          fullWidth
          ref='password'
        />
        <RaisedButton
          label='Sign in'
          type='submit'
          primary
          fullWidth
        />
      </form>
    )
  }
}

export default LoginForm
