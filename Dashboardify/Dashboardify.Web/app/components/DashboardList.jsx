import React from 'react';

import DashboardListItem from 'DashboardListItem';

class DashboardList extends React.Component {
  render() {
    let {dashboards} = this.props;

    let renderDashboards = () => {
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

export default DashboardList;
