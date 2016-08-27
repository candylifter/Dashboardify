import React from 'react';
import ReactDOM from 'react-dom';
import {Router, IndexRoute, Route, hashHistory} from 'react-router';

import DashboardifyApp from 'DashboardifyApp';
import DashboardifyIndex from 'DashboardifyIndex';
import ItemDashboard from 'ItemDashboard';

import Login from 'Login';

import 'style!css!sass!applicationStyles';
import 'script!bootstrap-sass/assets/javascripts/bootstrap.min.js';

ReactDOM.render(
  <Router history={hashHistory}>
    <Route path="/" component={DashboardifyApp}>
      <IndexRoute component={DashboardifyIndex}/>
      <Route path="dashboard/:id" component={ItemDashboard}/>
      <Route path="login" component={Login}/>
    </Route>
  </Router>
, document.getElementById('app'));
