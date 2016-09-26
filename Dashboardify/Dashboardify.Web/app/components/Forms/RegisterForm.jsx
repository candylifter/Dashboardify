import React, { PropTypes } from 'react'

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'

class RegisterForm extends React.Component {
  render () {
    const style = {
      padding: '0 1em 1em 1em',
      button: {
        margin: '2em 0 1em'
      }
    }

    return (
      <form style={style}>
        <TextField
          floatingLabelText='Name'
          hintText='E. g. Darth Vader'
          type='text'
          fullWidth
        />
        <TextField
          floatingLabelText='Email'
          hintText='E. g. darth.vader@empire.gov'
          type='email'
          fullWidth
        />
        <TextField
          floatingLabelText='Password'
          type='password'
          fullWidth
        />
        <TextField
          floatingLabelText='Repeat password'
          type='password'
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

export default RegisterForm
