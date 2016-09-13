import React from 'react';
import { connect } from 'react-redux';

import { ItemsAPI, DashboardsAPI, CheckIntervalsAPI } from 'api';
import { DashboardsActions, ItemsActions, CheckIntervalsActions } from 'actions';

import { Navbar } from 'components';

class App extends React.Component {
  componentWillMount() {
    const { dispatch } = this.props;

    dispatch(DashboardsActions.fetchDashboards(1));

    dispatch(CheckIntervalsActions.addCheckIntervals(CheckIntervalsAPI.getCheckIntervals()));
  }

  render() {
    return (
      <div>
        <Navbar/>
        {this.props.children}
      </div>
    )
  }
}

export default connect()(App);
