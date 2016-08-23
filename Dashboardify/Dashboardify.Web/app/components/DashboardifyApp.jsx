import React from 'react';

import Navbar from 'Navbar';
import ItemDashboard from 'ItemDashboard';

class DashboardifyApp extends React.Component {
  render() {
    return (
      <div>
        <Navbar/>
        <ItemDashboard/>
      </div>
    )
  }
}

export default DashboardifyApp;
