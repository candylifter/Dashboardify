import React, { PropTypes } from 'react'

import RefreshIndicator from 'material-ui/RefreshIndicator'

class LoadingIndicator extends React.Component {
  render () {
    let { show } = this.props

    const style = {
      container: {
        display: 'block',
        position: 'absolute',
        left: '50%',
        transform: 'translateX(-50%)'
      },
      refresh: {
        display: 'block',
        position: 'relative'
      }
    }

    return (
      <div style={style.container}>
        <RefreshIndicator
          size={40}
          left={0}
          top={16}
          status={show ? 'loading' : 'hide'}
          style={style.refresh}
          />
      </div>
    )
  }
}

export default LoadingIndicator
