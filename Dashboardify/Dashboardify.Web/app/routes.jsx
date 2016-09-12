import React from 'react';
import { Route, IndexRoute } from 'react-router';


import { App, Dashboards, Dashboard, Login } from 'containers';

export default (
  <Route path="/" component={App}>
    <IndexRoute component={Dashboards}/>
    <Route path="dashboard/:dashboardId" component={Dashboard}/>
    <Route path="login" component={Login}/>
  </Route>
)
