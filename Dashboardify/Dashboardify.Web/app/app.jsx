import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Router, IndexRoute, Route, hashHistory } from 'react-router';
//
import axios from 'axios';
//

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
  // Note: send state changes to WebAPI
});

store.dispatch(actions.addDashboards(DashboardifyAPI.getDashboards()));

store.dispatch(actions.addItems(DashboardifyAPI.getItems()));
// axios.get('//localhost/api/Items/GetItems/')
//   .then((res) => {
//     let apiItems = res.data.items;
//     let items = [];
//
//     for (var i = 0; i < apiItems.length; i++) {
//       let item = {
//         id: apiItems[i].Id,
//         dashboardId: apiItems[i].DashBoardId,
//         name: apiItems[i].Name,
//         img: 'https://www.placecage.com/gif/500/200',
//         url: apiItems[i].Website,
//         isActive: apiItems[i].isActive,
//         isSelected: false,
//         checkInterval: apiItems[i].CheckInterval,
//         lastChecked: apiItems[i].LastChecked,
//         lastModified: apiItems[i].Modified
//       };
//
//       items.push(item);
//     }
//
//     store.dispatch(actions.addItems(items));
//   })
//   .catch((err) => {
//     console.log(err);
//   })
store.dispatch(actions.addCheckIntervals(DashboardifyAPI.getCheckIntervals()));

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
