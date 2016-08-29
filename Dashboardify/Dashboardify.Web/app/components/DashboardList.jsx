import React from 'react';
import { connect } from 'react-redux';

import DashboardListItem from 'DashboardListItem';

class DashboardList extends React.Component {
  render() {
    let {dashboards} = this.props;

    let renderDashboards = () => {
      if (dashboards.length === 0) {
        return (
          <p>Nothing to show</p>
        )
      }

      return dashboards.map((dashboard) => {
        return <DashboardListItem key={dashboard.id} {...dashboard} />
      });
    };

    return (
      <div>
        {renderDashboards()}
      </div>
    )
  }
}

export default connect(
  (state) => state
)(DashboardList);
