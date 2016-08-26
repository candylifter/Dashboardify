import React from 'react';
import ReactDOM from 'react-dom';
import {Router, IndexRoute, Route, hashHistory} from 'react-router';

import DashboardifyApp from 'DashboardifyApp';
import DashboardifyIndex from 'DashboardifyIndex';
import ItemDashboard from 'ItemDashboard';

import 'style!css!sass!applicationStyles';

ReactDOM.render(
  <Router history={hashHistory}>
    <Route path="/" component={DashboardifyApp}>
      <IndexRoute component={DashboardifyIndex}/>
      <Route path="dashboard/:id" component={ItemDashboard}/>
    </Route>
  </Router>
, document.getElementById('app'));
