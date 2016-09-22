import React, { PropTypes } from 'react'
import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'

class LoginForm extends React.Component {
  render () {
    const style = {
      padding: '1em'
    }

    return (
      <div style={style}>
        <TextField
          floatingLabelText='Email'
          hintText='darth.vader@empire.gov'
          type='text'
          fullWidth
        />
        <TextField
          floatingLabelText='Password'
          hintText='IFeelTheForce'
          type='password'
          fullWidth
        />
        <RaisedButton
          label='Sign in'
          primary
          fullWidth
        />
      </div>
    )
  }
}

export default LoginForm;
