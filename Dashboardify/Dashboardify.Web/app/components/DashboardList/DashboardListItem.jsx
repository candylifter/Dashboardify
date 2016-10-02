import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router'

import Paper from 'material-ui/Paper'
import FlatButton from 'material-ui/FlatButton'
import FontIcon from 'material-ui/FontIcon'
import IconButton from 'material-ui/IconButton'
import ActionHome from 'material-ui/svg-icons/action/home'

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
    let { id, name, img } = this.props

    const style = {
      link: {
        // margin: 20,
        // width: 300,
        // maxWidth: 'calc(100vw - 40px)'
      },
      paper: {
        // width: '100%',
        // height: 300,
        // textAlign: 'center',
        // display: 'inline-block'
      }
    }

    return (
      <div className='dashboard'>
        <Link to={'/dashboard/' + id}>
          <Paper zDepth={1} className='dashboard__paper'>
            <div className='dashboard__paper__heading'>
              <span>{name}</span>
            </div>
            <div className='dashboard__paper__footer'>
              <div className='dashboard__paper__footer__left'>
                <span>Created: last month</span>
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
  img: PropTypes.string,
  dispatch: PropTypes.func
}

export default connect()(Dashboards)
