import React from 'react';
import { Link } from 'react-router';

import DashboardifyAPI from 'DashboardifyAPI';

class Breadcrumb extends React.Component {
  render () {
    let { dashboardId } = this.props;

    let dashboardName = (dashboardId) => {
      let currentDashboard =  DashboardifyAPI.getDashboards().filter((dashboard) => {
        return dashboard.id == dashboardId ? true : false;
      });

      return currentDashboard[0].name;
    }

    return (
      <ol className="breadcrumb">
        <li><Link to="/dashboards">Dashboards</Link></li>
        <li className="active">{dashboardName(dashboardId)}</li>
      </ol>
    )
  }
}

export default Breadcrumb;
