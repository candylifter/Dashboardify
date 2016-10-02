import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { hashHistory } from 'react-router'

import { Card, CardTitle, CardActions } from 'material-ui/Card'
import Divider from 'material-ui/Divider'
import FlatButton from 'material-ui/FlatButton'
import CircularProgress from 'material-ui/CircularProgress'

import { RegisterForm } from 'components'

class Register extends React.Component {
  componentWillMount () {
    let { isAuthenticated } = this.props

    if (isAuthenticated) {
      hashHistory.push('/')
    }
  }

  componentWillUpdate (props) {
    if (props.isAuthenticated) {
      hashHistory.push('/')
    }
  }

  // Architecture:
  // 361687
  // Mountains:
  // 148982
  // Urban
  // 183254

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

    let { isRegistering, registerSuccess } = this.props

    let renderRegisterForm = () => {
      if (isRegistering) {
        return (
          <div style={style.spinnerContainer}>
            <div className='text-center'>
              <CircularProgress size={1.5} />
            </div>
          </div>
        )
      } else if (registerSuccess) {
        return (
          <div className='text-center'>
            <p style={{fontSize: '1.1em', padding: '1em'}}>You can now sign in to Dashboardify</p>
          </div>
        )
      } else {
        return <RegisterForm />
      }
    }

    return (
      <div style={style}>
        <Card style={style.card}>
          <CardTitle
            style={style.card.title}
            title={isRegistering ? 'Signing up' : registerSuccess ? 'Congratulations!' : 'Sign up'}
          />
          <Divider />
          {renderRegisterForm()}
          <Divider />
          <CardActions style={style.card.footer}>
            <FlatButton
              label='Sign in'
              disabled={isRegistering}
              onClick={() => hashHistory.push('/login')}
              style={style.card.footer.button}
            />
          </CardActions>
        </Card>
      </div>
    )
  }
}

Register.propTypes = {
  isAuthenticated: PropTypes.bool,
  isRegistering: PropTypes.bool,
  registerSuccess: PropTypes.bool
}

export default connect(
  (state) => state.auth
)(Register)
