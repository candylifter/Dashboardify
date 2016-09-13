import React from 'react';
import { connect } from 'react-redux';

import CircularProgress from 'material-ui/CircularProgress';

import { DashboardList } from 'components';

const Dashboards = (props) => {

  let { isFetching, error } = props;

  const style = {
    display: 'flex',
    justifyContent: 'center',
  };

  let renderDashboardList = () => {
    if (isFetching) {
      return (
        <CircularProgress size={1.5} />
      )
    } else if (error === undefined) {
      return (
        <DashboardList/>
      )
    } else {
      return (
        <p>{error}</p>
      )
    }
  };

  return (
    <div style={style}>
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
