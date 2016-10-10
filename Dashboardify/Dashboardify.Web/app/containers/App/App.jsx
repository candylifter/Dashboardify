import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import injectTapEventPlugin from 'react-tap-event-plugin'

import { CheckIntervalsAPI } from 'api'
import { DashboardsActions, CheckIntervalsActions } from 'actions'

import { Navbar } from 'components'

// Needed for onTouchTap
// http://stackoverflow.com/a/34015469/988941
injectTapEventPlugin()

import 'applicationStyles'

class App extends React.Component {
  componentWillMount () {
    const { dispatch, isAuthenticated } = this.props

    if (isAuthenticated) {
      dispatch(DashboardsActions.fetchDashboards()) // TODO: Add user session
      dispatch(CheckIntervalsActions.addCheckIntervals(CheckIntervalsAPI.getCheckIntervals()))
    }
  }

  render () {
    const style = {
      minHeight: '100vh'
    }

    let { children, isAuthenticated } = this.props

    let renderNavbar = () => {
      if (isAuthenticated) {
        return <Navbar />
      }
    }

    return (
      <MuiThemeProvider>
        <div style={style}>
          {renderNavbar()}
          {children}
        </div>
      </MuiThemeProvider>
    )
  }
}

App.propTypes = {
  isAuthenticated: PropTypes.bool,
  children: PropTypes.node,
  dispatch: PropTypes.func
}

export default connect(
  (state) => state.auth
)(App)
