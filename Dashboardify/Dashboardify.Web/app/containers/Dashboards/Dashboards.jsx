import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import CircularProgress from 'material-ui/CircularProgress'

import { DashboardsActions } from 'actions'
import { DashboardList } from 'components'

class Dashboards extends React.Component {
  componentWillMount () {
    let { dispatch } = this.props

    dispatch(DashboardsActions.fetchDashboards())
  }

  render () {
    let { isFetching, error } = this.props

    const style = {
      display: 'flex',
      justifyContent: 'center'
    }

    let renderDashboardList = () => {
      if (isFetching) {
        return (
          <CircularProgress size={1.5} />
        )
      } else if (error === undefined) {
        return (
          <DashboardList />
        )
      } else {
        return (
          <p>{error}</p>
        )
      }
    }

    return (
      <div style={style}>
        {renderDashboardList()}
      </div>
    )
  }
}

Dashboards.propTypes = {
  dispatch: PropTypes.func,
  isFetching: PropTypes.bool,
  error: PropTypes.string
}

export default connect(
  (state) => {
    return {
      isFetching: state.dashboards.isFetching,
      error: state.dashboards.error
    }
  }
)(Dashboards)
