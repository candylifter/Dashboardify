import React from 'react';
import ReactDOM from 'react-dom';
import {Router, IndexRoute, Route, hashHistory} from 'react-router';

import DashboardifyApp from 'DashboardifyApp';
// import DashboardifyIndex from 'DashboardifyIndex';
// import ItemDashboard from 'ItemDashboard';

import Login from 'Login';
import Dashboards from 'Dashboards';
import Dashboard from 'Dashboard';

import 'style!css!sass!applicationStyles';
import 'script!bootstrap-sass/assets/javascripts/bootstrap.min.js';

ReactDOM.render(
  <Router history={hashHistory}>
    <Route path="/" component={DashboardifyApp}>
      <IndexRoute component={Login}/>
      <Route path="dashboards" component={Dashboards}/>
      <Route path="dashboard/:id" component={Dashboard}/>
    </Route>
  </Router>
, document.getElementById('app'));
