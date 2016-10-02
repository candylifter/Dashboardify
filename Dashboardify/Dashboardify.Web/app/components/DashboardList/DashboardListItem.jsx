import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router'
import moment from 'moment'

import Paper from 'material-ui/Paper'
import IconButton from 'material-ui/IconButton'

import { DashboardsActions } from 'actions'

class Dashboards extends React.Component {
  constructor () {
    super()

    this.handleDelete = this.handleDelete.bind(this)
  }

  handleDelete (e) {
    e.preventDefault()

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
                  onClick={this.handleDelete}
                  iconClassName='material-icons'
                  tooltip='Delete dashboard'
                  className='dashboard__paper__footer__right__icon-button'
                >
                  delete
                </IconButton>
              </div>
            </div>
          </Paper>
        </Link>
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
