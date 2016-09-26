import React from 'react'
import { Route, IndexRoute } from 'react-router'

import { App, Dashboards, Dashboard, Login, Register, NoMatch } from 'containers'
import { AuthAPI } from 'api'

const requireAuth = (nextState, replace) => {
  if (!AuthAPI.isLoggedIn()) {
    replace({
      pathname: '/login',
      state: {nextPathName: nextState.location.pathname}
    })
  }
}

export default (
  <Route name='app' path='/' component={App}>
    <IndexRoute name='dashboards' component={Dashboards} onEnter={requireAuth} />
    <Route name='items' path='dashboard/:dashboardId' component={Dashboard} onEnter={requireAuth} />
    <Route name='login' path='login' component={Login} />
    <Route name='register' path='register' component={Register} />
    <Route name='noMatch' path='*' component={NoMatch} />
  </Route>
)
