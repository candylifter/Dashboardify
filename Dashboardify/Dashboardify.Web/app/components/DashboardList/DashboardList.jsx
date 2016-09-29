import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import { DashboardListItem } from 'components'

class DashboardList extends React.Component {
  render () {
    let { dashboards } = this.props

    const style = {
      display: 'flex',
      maxWidth: 1200,
      justifyContent: 'center',
      flexWrap: 'wrap',
      alignItems: 'center',
      error: {
        width: '100%',
        minHeight: 'calc(100vh - 64px)',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center',
        color: '#9E9E9E',
        icon: {
          fontSize: '8em'
        },
        text: {
          fontSize: '2em'
        },
        subtext: {
          margin: 0,
          fontSize: '1.5em'
        }
      }
    }

    let renderDashboards = () => {
      if (dashboards.data.length === 0) {
        return (
          <div style={style.error}>
            <i className='material-icons' style={style.error.icon}>dashboard</i>
            <p style={style.error.text}>No dashboards yet</p>
            <p style={style.error.subtext}>Click the button in the bottom right to add one</p>
          </div>
        )
      }

      return dashboards.data.map((dashboard) => {
        return <DashboardListItem key={dashboard.id} {...dashboard} />
      })
    }

    return (
      <div style={style}>
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
