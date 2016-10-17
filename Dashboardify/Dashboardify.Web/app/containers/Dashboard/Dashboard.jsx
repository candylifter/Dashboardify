import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { hashHistory } from 'react-router'

import CircularProgress from 'material-ui/CircularProgress'

import { CheckIntervalsAPI } from 'api'
import { ItemList, ItemPanel, Toolbar } from 'components'
import { ItemsActions, ItemPanelActions, CheckIntervalsActions, AuthActions } from 'actions'

class Dashboard extends React.Component {
  componentWillMount () {
    let { dashboardId } = this.props.routeParams
    let { dispatch } = this.props

    dispatch(ItemsActions.removeItemError())
    dispatch(CheckIntervalsActions.addCheckIntervals(CheckIntervalsAPI.getCheckIntervals()))
    dispatch(ItemsActions.fetchItems(parseInt(dashboardId)))
  }

  componentWillUnmount () {
    let { dispatch } = this.props

    dispatch(ItemPanelActions.close())
  }

  componentDidUpdate () {
    let { dispatch, error } = this.props

    if (error !== undefined) {
      if (error.status === 401) {
        dispatch(AuthActions.logout())
        hashHistory.push('/login')
      }
    }
  }

  render () {
    let { isFetching, error, routeParams: { dashboardId } } = this.props

    dashboardId = parseInt(dashboardId)

    let renderItemList = () => {
      if (isFetching) {
        return (
          <div className='spinner'>
            <CircularProgress size={1.5} />
          </div>
        )
      } else if (error === undefined) {
        return (
          <ItemList dashboardId={dashboardId} />
        )
      } else {
        return (
          <div className='flex-container flex-container--toolbar'>
            <div className='error'>
              <i className='error__icon material-icons'>&#xE000;</i>
              <p className='error__text'>{error.status}</p>
            </div>
          </div>
        )
      }
    }

    let renderToolbar = () => {
      if (!isFetching && error === undefined) {
        return (
          <Toolbar dashboardId={dashboardId} />
        )
      }
    }

    return (
      <div className='dashboard-container'>
        {renderToolbar()}
        {renderItemList()}
        <ItemPanel dashboardId={dashboardId} />
      </div>
    )
  }
}

Dashboard.propTypes = {
  dispatch: PropTypes.func,
  routeParams: PropTypes.object,
  isFetching: PropTypes.bool,
  error: PropTypes.object
}

export default connect(
  (state) => {
    return {
      isFetching: state.items.isFetching,
      error: state.items.error,
      isPanelOpen: state.itemPanel.open
    }
  }
)(Dashboard)
