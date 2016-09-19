import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router'

class Breadcrumb extends React.Component {
  render () {
    let { dashboards, dashboardId } = this.props

    let dashboardName = (dashboardId) => {
      let currentDashboard = dashboards.data.filter((dashboard) => {
        return dashboard.id === dashboardId
      })

      if (dashboards.data.length !== 0) {
        return currentDashboard[0].name
      } else {
        return ''
      }
    }

    return (
      <ol className='breadcrumbs'>
        <li className='breadcrumbs__breadcrumb'>
          <Link className='breadcrumbs__breadcrumb__text' to='/'>Dashboards</Link>
        </li>
        <li className='breadcrumbs__breadcrumb'>
          <span className='breadcrumbs__breadcrumb__text breadcrumbs__breadcrumb__text--current'>{dashboardName(dashboardId)}</span>
        </li>
      </ol>
    )
  }
}

Breadcrumb.propTypes = {
  dashboards: PropTypes.object,
  dashboardId: PropTypes.number
}

export default connect(
  (state) => {
    return {
      dashboards: state.dashboards
    }
  }
)(Breadcrumb)
