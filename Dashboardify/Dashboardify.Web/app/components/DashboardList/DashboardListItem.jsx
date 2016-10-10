import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router'
import moment from 'moment'

import Paper from 'material-ui/Paper'
import IconButton from 'material-ui/IconButton'

import { DashboardsActions } from 'actions'
import { ConfirmModal } from 'components'

class Dashboards extends React.Component {
  constructor () {
    super()

    this.state = {
      confirmOpen: false
    }

    this.handleOpenConfirm = this.handleOpenConfirm.bind(this)
    this.handleCloseConfirm = this.handleCloseConfirm.bind(this)
    this.handleDelete = this.handleDelete.bind(this)
  }

  componentWillUnmount () {
    let { dispatch } = this.props

    dispatch(DashboardsActions.removeError())
  }

  handleOpenConfirm (e) {
    e.preventDefault()
    this.setState({confirmOpen: true})
  }

  handleCloseConfirm () {
    this.setState({confirmOpen: false})
  }

  handleDelete () {
    let { dispatch, id } = this.props

    dispatch(DashboardsActions.deleteDashboard(id))
  }

  render () {
    let { id, name, dateCreated } = this.props

    return (
      <div className='dashboard'>
        <Link to={'/dashboard/' + id}>
          <Paper zDepth={1} className='dashboard__paper'>
            <div className='dashboard__paper__heading'>
              <span>{name}</span>
            </div>
            <div className='dashboard__paper__footer'>
              <div className='dashboard__paper__footer__left'>
                <span>Created {moment(dateCreated).fromNow()}</span>
              </div>
              <div className='dashboard__paper__footer__right'>
                <IconButton
                  onClick={this.handleOpenConfirm}
                  iconClassName='material-icons'
                  tooltip='Delete dashboard'
                  tooltipPosition='top-left'
                  className='dashboard__paper__footer__right__icon-button'
                >
                  delete
                </IconButton>
              </div>
            </div>
          </Paper>
        </Link>
        <ConfirmModal
          open={this.state.confirmOpen}
          onConfirm={() => {
            this.handleCloseConfirm()
            this.handleDelete()
          }}
          onCancel={this.handleCloseConfirm}
          title={`Delete ${name}`}
          text='Are you sure you want to delete this dashboard?'
          confirmLabel='Delete'
          cancelLabel='Cancel'
          />
      </div>
    )
  }
}

Dashboards.propTypes = {
  id: PropTypes.number,
  name: PropTypes.string,
  dateCreated: PropTypes.string,
  dispatch: PropTypes.func
}

export default connect()(Dashboards)
