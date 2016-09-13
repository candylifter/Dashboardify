import React from 'react';
import { connect } from 'react-redux';

import { DashboardListItem } from 'components';

class DashboardList extends React.Component {
  render() {
    let { dashboards } = this.props;

    const style = {
      display: 'flex',
      maxWidth: 1200,
      justifyContent: 'center',
      flexWrap: 'wrap',
      alignItems: 'center',
    };

    let renderDashboards = () => {
      if (dashboards.data.length === 0 ) {
        return (
          <p>Nothing to show</p>
        )
      }

      return dashboards.data.map((dashboard) => {
        return <DashboardListItem key={dashboard.id} {...dashboard} />
      });
    };

    return (
      <div style={style}>
        {renderDashboards()}
      </div>
    )
  }
}

export default connect(
  (state) => {
    return {
      dashboards: state.dashboards
    }
  }
)(DashboardList);
