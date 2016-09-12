import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Router, hashHistory } from 'react-router';

import routes from 'routes';

import configureStore from 'configureStore';

const store = configureStore();

import 'style!css!sass!applicationStyles';
import 'script!bootstrap-sass/assets/javascripts/bootstrap.min.js';


store.subscribe(() => {
  var state = store.getState();

  console.log(state);
  // Note: send state changes to WebAPI
});



ReactDOM.render(
  <Provider store={store}>
    <Router history={hashHistory}>
      {routes}
    </Router>
  </Provider>
, document.getElementById('app'));
