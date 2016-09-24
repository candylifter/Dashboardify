import React from 'react'
import { Route, IndexRoute } from 'react-router'

import { App, Dashboards, Dashboard, Login } from 'containers'
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
  <Route path='/' component={App}>
    <IndexRoute component={Dashboards} onEnter={requireAuth} />
    <Route path='dashboard/:dashboardId' component={Dashboard} onEnter={requireAuth} />
    <Route path='login' component={Login} />
  </Route>
)
