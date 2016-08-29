import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Router, IndexRoute, Route, hashHistory } from 'react-router';

import DashboardifyAPI from 'DashboardifyAPI';
let actions = require('actions');
let store = require('configureStore').configure();

import DashboardifyApp from 'DashboardifyApp';

import Login from 'Login';
import Dashboards from 'Dashboards';
import Dashboard from 'Dashboard';

import 'style!css!sass!applicationStyles';
import 'script!bootstrap-sass/assets/javascripts/bootstrap.min.js';


store.subscribe(() => {
  var state = store.getState();

  console.log(state);
});

store.dispatch(actions.addDashboards(DashboardifyAPI.getDashboards()));
store.dispatch(actions.addItems(DashboardifyAPI.getItems()));

ReactDOM.render(
  <Provider store={store}>
    <Router history={hashHistory}>
      <Route path="/" component={DashboardifyApp}>
        <IndexRoute component={Login}/>
        <Route path="dashboards" component={Dashboards}/>
        <Route path="dashboard/:id" component={Dashboard}/>
      </Route>
    </Router>
  </Provider>
, document.getElementById('app'));
