import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router';

class Breadcrumb extends React.Component {
  render () {
    let { dashboards, dashboardId } = this.props;

    let dashboardName = (dashboardId) => {
      let currentDashboard =  dashboards.filter((dashboard) => {
        return dashboard.id == dashboardId ? true : false;
      });

      return currentDashboard[0].name;
    }

    return (
      <ol className="breadcrumb">
        <li><Link to="/">Dashboards</Link></li>
        <li className="active">{dashboardName(dashboardId)}</li>
      </ol>
    )
  }
}

export default connect(
  (state) => {
    return {
      dashboards: state.dashboards
    }
  }
)(Breadcrumb);
