import React from 'react'
import ReactDOM from 'react-dom'
import { Provider } from 'react-redux'
import { Router, browserHistory } from 'react-router'
import { syncHistoryWithStore } from 'react-router-redux'

import routes from 'routes'
import configureStore from 'configureStore'
import { AuthAPI } from 'api'
import { AuthActions } from 'actions'

const store = configureStore()
const history = syncHistoryWithStore(browserHistory, store)

if (AuthAPI.isLoggedIn()) {
  store.dispatch(AuthActions.completeLogin())
}

ReactDOM.render(
  <Provider store={store}>
    <Router history={history}>
      {routes}
    </Router>
  </Provider>
, document.getElementById('app'))
