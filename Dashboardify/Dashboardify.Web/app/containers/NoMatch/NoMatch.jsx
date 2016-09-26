import React, { PropTypes } from 'react'

class NoMatch extends React.Component {
  render () {
    const randomInt = (Math.floor(Math.random() * (15 - 1 + 1)) + 1) + 256
    const backgroundUrl = `https://www.placecage.com/gif/${randomInt}/${randomInt}`

    const style = {
      minHeight: '100vh',
      width: '100vw',
      background: `url("${backgroundUrl}")`,
      backgroundPosition: 'center',
      backgroundSize: 'cover',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      flexDirection: 'column',
      color: 'white',
      textShadow: '2px 2px 2px rgba(0, 0, 0, 1)',
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

export default NoMatch
