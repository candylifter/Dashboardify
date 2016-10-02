import React, { PropTypes } from 'react'

import { Toolbar as MUIToolbar, ToolbarGroup } from 'material-ui/Toolbar'
import Paper from 'material-ui/Paper'

import { Breadcrumb, Search } from 'components'

class Toolbar extends React.Component {
  render () {
    let { dashboardId } = this.props

    return (
      <Paper className='toolbar-paper' zDepth={1} rounded={false}>
        <MUIToolbar className='toolbar'>
          <ToolbarGroup firstChild>
            <Breadcrumb dashboardId={dashboardId} />
          </ToolbarGroup>
          <ToolbarGroup>
            <Search />
          </ToolbarGroup>
        </MUIToolbar>
      </Paper>
    )
  }
}

Toolbar.propTypes = {
  dashboardId: PropTypes.number
}

export default Toolbar
