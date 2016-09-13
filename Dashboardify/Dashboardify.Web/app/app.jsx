import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Router, hashHistory } from 'react-router';
import { syncHistoryWithStore } from 'react-router-redux';

import routes from 'routes';

import configureStore from 'configureStore';

const store = configureStore();

import 'style!css!sass!applicationStyles';
import 'script!bootstrap-sass/assets/javascripts/bootstrap.min.js';


const history = syncHistoryWithStore(hashHistory, store);

store.subscribe(() => {
  var state = store.getState();

  console.log(state);
  // Note: send state changes to WebAPI
});



ReactDOM.render(
  <Provider store={store}>
    <Router history={history}>
      {routes}
    </Router>
  </Provider>
, document.getElementById('app'));
