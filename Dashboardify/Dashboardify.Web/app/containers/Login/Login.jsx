import React from 'react'
import { browserHistory } from 'react-router'
import { connect } from 'react-redux'

import { Card, CardTitle, CardActions } from 'material-ui/Card'
import Divider from 'material-ui/Divider'
import CircularProgress from 'material-ui/CircularProgress'
import FlatButton from 'material-ui/FlatButton'

import { LoginForm } from 'components'

class Login extends React.Component {
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
      }
    }

    let { isPosting, isAuthenticated } = this.props

    if (isAuthenticated) {
      browserHistory.push('/')
    }

    let renderLoginForm = () => {
      if (isPosting || isAuthenticated) {
        return (
          <div style={style.spinnerContainer}>
            <div className='text-center'>
              <CircularProgress size={1.5} />
            </div>
          </div>
        )
      } else {
        return <LoginForm />
      }
    }

    return (
      <div style={style}>
        <Card style={style.card}>
          <CardTitle
            style={style.card.title}
            title='Sign in to Dashboardify'
          />
          <Divider />
          {renderLoginForm()}
          <Divider />
          <CardActions style={style.card.footer}>
            <FlatButton
              label='Forgot password'
              style={style.card.footer.button}
              disabled={isPosting}
            />
            <FlatButton
              label='Sign up'
              style={style.card.footer.button}
              disabled={isPosting}
            />
          </CardActions>
        </Card>
      </div>
    )
  }
}

export default connect(
  (state) => {
    return state.auth
  }
)(Login)
