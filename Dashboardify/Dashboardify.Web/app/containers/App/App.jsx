import React from 'react'
import { connect } from 'react-redux'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import injectTapEventPlugin from 'react-tap-event-plugin'

import { CheckIntervalsAPI } from 'api'
import { DashboardsActions, CheckIntervalsActions } from 'actions'

import { Navbar } from 'components'

// Needed for onTouchTap
// http://stackoverflow.com/a/34015469/988941
injectTapEventPlugin()

import 'style!css!sass!applicationStyles'
// import 'script!bootstrap-sass/assets/javascripts/bootstrap.min.js';

class App extends React.Component {
  componentWillMount () {
    const { dispatch } = this.props

    dispatch(DashboardsActions.fetchDashboards(1)) // TODO: Add user session
    dispatch(CheckIntervalsActions.addCheckIntervals(CheckIntervalsAPI.getCheckIntervals()))
  }

  render () {
    const style = {
      minHeight: '100vh'
    }

    let { isAuthenticated } = this.props

    let renderNavbar = () => {
      if (isAuthenticated) {
        return <Navbar />
      }
    }

    return (
      <MuiThemeProvider>
        <div style={style}>
          {renderNavbar()}
          {this.props.children}
        </div>
      </MuiThemeProvider>
    )
  }
}

export default connect(
  (state) => state.auth
)(App)
