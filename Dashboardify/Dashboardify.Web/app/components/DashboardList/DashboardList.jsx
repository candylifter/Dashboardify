import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import { DashboardListItem } from 'components'

class DashboardList extends React.Component {
  render () {
    let { dashboards } = this.props

    let renderDashboards = () => {
      if (dashboards.data.length === 0) {
        return (
          <div className='flex-container'>
            <div className='error'>
              <i className='error__icon material-icons'>dashboard</i>
              <p className='error__text'>No dashboards yet</p>
              <p className='error__subtext'>Click the button in the bottom right to add one</p>
            </div>
          </div>
        )
      }

      return dashboards.data.map((dashboard) => {
        return <DashboardListItem key={dashboard.id} {...dashboard} />
      })
    }

    return (
      <div className='dashboard-list'>
        {renderDashboards()}
      </div>
    )
  }
}

DashboardList.propTypes = {
  dashboards: PropTypes.object
}

export default connect(
  (state) => {
    return {
      dashboards: state.dashboards
    }
  }
)(DashboardList)
