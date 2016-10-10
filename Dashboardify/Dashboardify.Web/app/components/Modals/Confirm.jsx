import React, { PropTypes } from 'react'

import Dialog from 'material-ui/Dialog'
import FlatButton from 'material-ui/FlatButton'

class Confirm extends React.Component {
  render () {
    let { open, onConfirm, onCancel, title = 'Confirm', text = 'Are you sure?', confirmLabel = 'Yes', cancelLabel = 'Cancel' } = this.props

    const actions = [
      <FlatButton
        label={cancelLabel}
        onTouchTap={onCancel}
      />,
      <FlatButton
        label={confirmLabel}
        primary
        onTouchTap={onConfirm}
      />
    ]
    return (
      <div>
        <Dialog
          title={title}
          actions={actions}
          modal={false}
          open={open}
          onRequestClose={onCancel}
          >
          {text}
        </Dialog>
      </div>
    )
  }
}

Confirm.propTypes = {
  open: PropTypes.bool,
  onConfirm: PropTypes.func,
  onCancel: PropTypes.func,
  title: PropTypes.string,
  text: PropTypes.string,
  confirmLabel: PropTypes.string,
  cancelLabel: PropTypes.string
}

export default Confirm
