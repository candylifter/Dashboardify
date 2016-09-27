import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import { Toolbar, ToolbarGroup } from 'material-ui/Toolbar'
import Paper from 'material-ui/Paper'
import CircularProgress from 'material-ui/CircularProgress'

import { CheckIntervalsAPI } from 'api'
import { Breadcrumb, Search, ItemList, ItemPanel } from 'components'
import { ItemsActions, ItemPanelActions, CheckIntervalsActions } from 'actions'

class Dashboard extends React.Component {
  componentWillMount () {
    let { dashboardId } = this.props.routeParams
    let { dispatch } = this.props

    dispatch(CheckIntervalsActions.addCheckIntervals(CheckIntervalsAPI.getCheckIntervals()))
    dispatch(ItemsActions.fetchItems(parseInt(dashboardId)))
  }

  componentWillUnmount () {
    let { dispatch } = this.props

    dispatch(ItemPanelActions.close())
  }

  render () {
    let { isFetching, error, routeParams: { dashboardId } } = this.props

    const style = {
      dashboard: {
        maxWidth: 1200,
        width: '100%',
        margin: '0 auto'
      },
      ItemList: {
        display: 'flex',
        margin: '0 auto'
      },
      ToolbarContainer: {
        margin: '2em 0'
      },
      Toolbar: {
        backgroundColor: 'transparent'
      },
      spinner: {
        width: '100%',
        minHeight: 'calc(100vh - 64px)',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center'
      },
      error: {
        width: '100%',
        minHeight: 'calc(100vh - 64px)',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center',
        color: '#9E9E9E',
        icon: {
          fontSize: '8em'
        },
        text: {
          fontSize: '2em'
        }
      }
    }

    dashboardId = parseInt(dashboardId)

    let renderItemList = () => {
      if (isFetching) {
        return (
          <div style={style.spinner}>
            <CircularProgress size={1.5} />
          </div>
        )
      } else if (error === undefined) {
        return (
          <ItemList dashboardId={dashboardId} />
        )
      } else {
        return (
          <div style={style.error}>
            <i className='material-icons' style={style.error.icon}>&#xE000;</i>
            <p style={style.error.text}>{error}</p>
          </div>
        )
      }
    }

    let renderToolbar = () => {
      if (!isFetching && error === undefined) {
        return (
          <Paper style={style.ToolbarContainer} zDepth={1} rounded={false}>
            <Toolbar style={style.Toolbar}>
              <ToolbarGroup firstChild>
                <Breadcrumb dashboardId={dashboardId} />
              </ToolbarGroup>
              <ToolbarGroup>
                <Search />
              </ToolbarGroup>
            </Toolbar>
          </Paper>
        )
      }
    }

    return (
      <div style={style.dashboard}>
        {renderToolbar()}
        <div style={style.ItemList}>
          {renderItemList()}
        </div>
        <ItemPanel dashboardId={dashboardId} />
      </div>
    )
  }
}

Dashboard.propTypes = {
  dispatch: PropTypes.func,
  routeParams: PropTypes.object,
  isFetching: PropTypes.bool,
  error: PropTypes.string
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
