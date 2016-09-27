import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { browserHistory } from 'react-router'

import { Card, CardTitle, CardActions } from 'material-ui/Card'
import Divider from 'material-ui/Divider'
import FlatButton from 'material-ui/FlatButton'
import CircularProgress from 'material-ui/CircularProgress'

import { RegisterForm } from 'components'

class Register extends React.Component {
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

    let { isRegistering } = this.props

    let renderRegisterForm = () => {
      if (isRegistering) {
        return (
          <div style={style.spinnerContainer}>
            <div className='text-center'>
              <CircularProgress size={1.5} />
            </div>
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
            title='Sign up'
          />
          <Divider />
          {renderRegisterForm()}
          <Divider />
          <CardActions style={style.card.footer}>
            <FlatButton
              label='Sign in'
              onClick={() => browserHistory.push('/login')}
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
  isRegistering: PropTypes.bool
}

export default connect(
  (state) => state.auth
)(Register)
