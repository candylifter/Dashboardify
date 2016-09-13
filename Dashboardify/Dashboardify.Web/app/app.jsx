import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { Router, hashHistory } from 'react-router';
import { syncHistoryWithStore } from 'react-router-redux';

import routes from 'routes';

import configureStore from 'configureStore';

const store = configureStore();

const history = syncHistoryWithStore(hashHistory, store);

store.subscribe(() => {
  var state = store.getState();

  console.log(state);
});

ReactDOM.render(
  <Provider store={store}>
    <Router history={history}>
      {routes}
    </Router>
  </Provider>
, document.getElementById('app'));
