import React, { PropTypes } from 'react'

import { Card, CardTitle, CardActions } from 'material-ui/Card'
import Divider from 'material-ui/Divider'
import CircularProgress from 'material-ui/CircularProgress'
import FlatButton from 'material-ui/FlatButton'
import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'

class Register extends React.Component {
  render () {
    const style = {
      width: '100%',
      height: '100vh',
      backgroundImage: 'url("https://source.unsplash.com/random/1920x1080")',
      backgroundPosition: 'center',
      backgroundSize: 'cover',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      card: {
        margin: '1em',
        width: '25em',
        title: {
          textAlign: 'center'
        },
        footer: {
          display: 'flex',
          justifyContent: 'space-between',
          button: {
            margin: '0 8px'
          }
        }
      },
      spinnerContainer: {
        padding: '2em'
      },
      form: {
        padding: '0 1em 1em 1em',
        button: {
          margin: '2em 0 1em'
        }
      }
    }

    return (
      <div style={style}>
        <Card style={style.card}>
          <CardTitle
            style={style.card.title}
            title='Sign up'
          />
          <Divider />
          <form style={style.form}>
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
              style={style.form.button}
              label='Sign up'
              type='submit'
              primary
              fullWidth
            />
          </form>
          <Divider />
          <CardActions style={style.card.footer}>
            <FlatButton
              label='Sign in'
              style={style.card.footer.button}
            />
          </CardActions>
        </Card>
      </div>
    )
  }
}

Register.propTypes = {
}

export default Register
