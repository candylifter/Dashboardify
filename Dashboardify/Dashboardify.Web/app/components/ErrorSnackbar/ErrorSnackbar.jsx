import React, { PropTypes } from 'react'
import Snackbar from 'material-ui/Snackbar'

class ErrorSnackbar extends React.Component {
  render () {
    let { open, message } = this.props

    return (
      <Snackbar
        open={open}
        message={message}
        autoHideDuration={4000}
      />
    )
  }
}

ErrorSnackbar.propTypes = {
  open: PropTypes.bool,
  message: PropTypes.string
}

export default ErrorSnackbar
