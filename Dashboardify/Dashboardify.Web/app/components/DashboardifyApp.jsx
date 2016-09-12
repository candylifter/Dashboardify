import React from 'react';

import { Navbar } from 'components';

class DashboardifyApp extends React.Component {
  render() {
    return (
      <div>
        <Navbar/>
        {this.props.children}
      </div>
    )
  }
}

export default DashboardifyApp;
