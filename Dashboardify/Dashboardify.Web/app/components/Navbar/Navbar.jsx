import React from 'react'
import { browserHistory } from 'react-router'

import AppBar from 'material-ui/AppBar'
import Drawer from 'material-ui/Drawer'
import MenuItem from 'material-ui/MenuItem'

import { AuthActions } from 'actions'

class Navbar extends React.Component {
  constructor (props) {
    super(props)

    this.state = {
      open: false
    }

    this.handleLogout = this.handleLogout.bind(this)
  }

  handleLogout () {
    AuthActions.logout()
    this.setState({open: false})
  }

  navigateTo (url) {
    browserHistory.push(url)
    this.setState({open: false})
  }

  render () {
    const styles = {
      title: {
        cursor: 'pointer'
      }
    }

    return (
      <div>
        <AppBar
          title={<span style={styles.title}>Dashboardify</span>}
          onTitleTouchTap={() => this.navigateTo('/')}
          onLeftIconButtonTouchTap={() => this.setState({open: true})}
        />
        <Drawer
          docked={false}
          width={200}
          open={this.state.open}
          onRequestChange={(open) => this.setState({open})}
            >
          <MenuItem onTouchTap={() => this.navigateTo('/')}>Home</MenuItem>
          <MenuItem onTouchTap={() => this.navigateTo('/logout')}>Logout</MenuItem>
        </Drawer>
      </div>
    )
  }
}

export default Navbar
