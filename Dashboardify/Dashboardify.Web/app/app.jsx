import React from 'react';
import ReactDOM from 'react-dom';
import {Router, Route} from 'react-router';

import DashboardifyApp from 'DashboardifyApp';

require('style!css!sass!applicationStyles');

ReactDOM.render(
  <DashboardifyApp/>
, document.getElementById('app'));
