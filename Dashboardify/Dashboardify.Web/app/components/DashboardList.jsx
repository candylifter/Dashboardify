import React from 'react';

import Dashboard from 'Dashboard';

class DashboardList extends React.Component {
  render() {
    let {dashboards} = this.props;

    let renderDashboards = () => {
      return dashboards.map((dashboard) => {
        return <Dashboard key={dashboard.id} {...dashboard} />
      });
    };

    return (
      <div>
        {renderDashboards()}
      </div>
    )
  }
}

export default DashboardList;
