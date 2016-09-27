import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import Dialog from 'material-ui/Dialog'
import FlatButton from 'material-ui/FlatButton'
import TextField from 'material-ui/TextField'

import { ModalsActions } from 'actions'

class CreateDashboardModal extends React.Component {
  constructor () {
    super()

    this.handleClose = this.handleClose.bind(this)
  }

  handleClose () {
    let { dispatch } = this.props

    dispatch(ModalsActions.closeCreateDashboardModal())
  }

  render () {
    const actions = [
      <FlatButton
        label='Cancel'
        primary={false}
        onTouchTap={this.handleClose}
      />,
      <FlatButton
        label='Create'
        primary
        onTouchTap={this.handleClose}
      />
    ]

    let { open } = this.props

    console.log(this.props)

    return (
      <Dialog
        title='Create a new dashboard'
        actions={actions}
        modal={false}
        open={open}
        onRequestClose={this.handleClose}
      >
        <form>
          <TextField
            floatingLabelText='Dashboard name'
            hintText='E. g. Rebel news'
            fullWidth
          />
        </form>
      </Dialog>
    )
  }
}

CreateDashboardModal.propTypes = {
  open: PropTypes.bool,
  dispatch: PropTypes.func
}

export default connect(
  (state) => state.modals.createDashboardModal
)(CreateDashboardModal)
