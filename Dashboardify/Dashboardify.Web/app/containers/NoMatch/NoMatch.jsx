import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

class NoMatch extends React.Component {
  render () {
    const randomInt = (Math.floor(Math.random() * (15 - 1 + 1)) + 1) + 256
    const backgroundUrl = `https://www.placecage.com/gif/${randomInt}/${randomInt}`

    let { isAuthenticated } = this.props

    const style = {
      minHeight: isAuthenticated ? 'calc(100vh - 64px)' : '100vh',
      width: '100%',
      background: `url("${backgroundUrl}")`,
      backgroundPosition: 'center',
      backgroundSize: 'cover',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      flexDirection: 'column',
      color: 'white',
      textShadow: '2px 2px 5px rgba(0, 0, 0, 1)',
      header: {
        fontSize: '7em'
      },
      description: {

        fontSize: '2em'
      }
    }

    return (
      <div style={style}>
        <h1 style={style.header}>404</h1>
        <p style={style.description}>Oops, page not found!</p>
      </div>
    )
  }
}

export default connect(
  (state) => state.auth
)(NoMatch)
