import React from 'react'
import { connect } from 'react-redux'

import { Toolbar, ToolbarGroup, ToolbarSeparator, ToolbarTitle } from 'material-ui/Toolbar'
import Paper from 'material-ui/Paper'

import { Breadcrumb, Search, ItemList, ItemPanel } from 'components'
import { ItemsActions, ItemPanelActions } from 'actions'

class Dashboard extends React.Component {
  componentWillMount () {
    let { dashboardId } = this.props.routeParams
    let { dispatch } = this.props

    dispatch(ItemsActions.fetchItems(parseInt(dashboardId)))
  }

  componentWillUnmount() {
    let { dispatch } = this.props

    dispatch(ItemPanelActions.close())
  }

  render () {
    let { isFetching, error, isPanelOpen } = this.props
    let { dashboardId } = this.props.routeParams

    dashboardId = parseInt(dashboardId)

    let renderItemList = () => {
      if (isFetching) {
        return (
          <div>
            <p className='text-center'>Loading...</p>
          </div>
        )
      } else if (error === undefined) {
        return (
          <ItemList dashboardId={dashboardId} />
        )
      } else {
        return (
          <p className='text-center'>{error}</p>
        )
      }
    }

    const style = {
      dashboard: {
        maxWidth: 1200,
        width: '100%',
        margin: '40px auto 0'
      },
      ItemList: {
        display: 'flex',
        margin: '40px auto 0'
      },
      Toolbar: {
        backgroundColor: 'transparent'
      }
    }

    return (
      <div style={style.dashboard}>
        <Paper style={style} zDepth={1} rounded={false}>
          <Toolbar style={style.Toolbar}>
            <ToolbarGroup firstChild>
              <Breadcrumb dashboardId={dashboardId} />
            </ToolbarGroup>
            <ToolbarGroup>
              <Search />
            </ToolbarGroup>
          </Toolbar>
        </Paper>

        <div style={style.ItemList}>
          {renderItemList()}
        </div>
        <ItemPanel dashboardId={dashboardId} />
      </div>
    )
  }
}

export default connect(
  (state) => {
    return {
      isFetching: state.items.isFetching,
      error: state.items.error,
      isPanelOpen: state.itemPanel.open
    }
  }
)(Dashboard)
