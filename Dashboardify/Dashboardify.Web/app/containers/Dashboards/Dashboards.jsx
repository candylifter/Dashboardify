import React from 'react';
import { connect } from 'react-redux';

import { DashboardList } from 'components';

const Dashboards = (props) => {

  let { isFetching, error } = props;

  let renderDashboardList = () => {
    if (isFetching) {
      return (
        <p className="text-center">Loading...</p>
      )
    } else if (error === undefined) {
      return (
        <DashboardList/>
      )
    } else {
      return (
        <p className="text-center">{error}</p>
      )
    }
  };

  return (
    <div>
      {renderDashboardList()}
    </div>
  )
}

export default connect(
  (state) => {
    return {
      isFetching: state.dashboards.isFetching,
      error: state.dashboards.error,
    }
  }
)(Dashboards)
