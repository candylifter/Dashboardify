import React, { PropTypes } from 'react'
import { browserHistory } from 'react-router'
import { connect } from 'react-redux'

import { Card, CardTitle, CardActions } from 'material-ui/Card'
import Divider from 'material-ui/Divider'
import CircularProgress from 'material-ui/CircularProgress'
import FlatButton from 'material-ui/FlatButton'

import { LoginForm } from 'components'

class Login extends React.Component {
  componentWillMount () {
    let { isAuthenticated } = this.props

    if (isAuthenticated) {
      browserHistory.push('/')
    }
  }

  componentWillUpdate (props) {
    if (props.isAuthenticated) {
      browserHistory.push('/')
    }
  }

  render () {
    const style = {
      width: '100%',
      minHeight: '100vh',
      backgroundImage: 'url("https://source.unsplash.com/collection/183254/1920x1080")',
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

    let { isLoggingIn, error } = this.props

    let renderErrors = () => {
      if (error && !isLoggingIn) {
        return (
          <div className='text-center'>
            <p>Status: {error.status}</p>
            {error.data.Errors.map((err, index) => {
              return (
                <p key={index}>{err.Code}</p>
              )
            })}
          </div>
        )
      }
    }

    let renderLoginForm = () => {
      if (isLoggingIn) {
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
          {renderErrors()}
          {renderLoginForm()}
          <Divider />
          <CardActions style={style.card.footer}>
            <FlatButton
              label='Sign up'
              style={style.card.footer.button}
              onClick={() => browserHistory.push('/register')}
              disabled={isLoggingIn}
            />
          </CardActions>
        </Card>
      </div>
    )
  }
}

Login.propTypes = {
  isLoggingIn: PropTypes.bool,
  isAuthenticated: PropTypes.bool,
  error: PropTypes.object
}

export default connect(
  (state) => {
    return state.auth
  }
)(Login)
