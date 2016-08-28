import React, { PropTypes } from 'react'

import DashboardList from 'DashboardList';
import DashboardifyAPI from 'DashboardifyAPI';

class Dashboards extends React.Component {
  constructor(props, context) {
    super(props, context);

    this.state = {
      dashboards: DashboardifyAPI.getDashboards()
    }
  }

  render () {
    return (
      <div>
        <DashboardList dashboards={this.state.dashboards}/>
      </div>
    )
  }
}

export default Dashboards;
