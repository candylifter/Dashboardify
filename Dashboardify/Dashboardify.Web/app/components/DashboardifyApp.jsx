import React from 'react';

import Navbar from 'Navbar';

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
