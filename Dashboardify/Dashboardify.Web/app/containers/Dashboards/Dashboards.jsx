import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import CircularProgress from 'material-ui/CircularProgress'
import FloatingActionButton from 'material-ui/FloatingActionButton'
import ContentAdd from 'material-ui/svg-icons/content/add'

import { DashboardsActions, ModalsActions } from 'actions'
import { DashboardList, CreateDashboardModal } from 'components'

class Dashboards extends React.Component {
  constructor () {
    super()

    this.handleFabClick = this.handleFabClick.bind(this)
  }

  componentWillMount () {
    let { dispatch } = this.props

    dispatch(DashboardsActions.fetchDashboards())
  }

  handleFabClick () {
    let { dispatch } = this.props

    dispatch(ModalsActions.openCreateDashboardModal())
  }

  render () {
    let { isFetching, error } = this.props

    const style = {
      display: 'flex',
      justifyContent: 'center',
      spinner: {
        width: '100%',
        minHeight: 'calc(100vh - 64px)',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center'
      },
      fab: {
        display: isFetching ? 'none' : 'block',
        position: 'fixed',
        bottom: '1.5em',
        right: '2.25em'
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

    let renderDashboardList = () => {
      if (isFetching) {
        return (
          <div style={style.spinner}>
            <CircularProgress size={1.5} />
          </div>
        )
      } else if (error === undefined) {
        return (
          <DashboardList />
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

    return (
      <div style={style}>
        {renderDashboardList()}
        <FloatingActionButton
          style={style.fab}
          onClick={this.handleFabClick}
          secondary
        >
          <ContentAdd />
        </FloatingActionButton>
        <CreateDashboardModal />
      </div>
    )
  }
}

Dashboards.propTypes = {
  dispatch: PropTypes.func,
  isFetching: PropTypes.bool,
  error: PropTypes.string
}

export default connect(
  (state) => {
    return {
      isFetching: state.dashboards.isFetching,
      error: state.dashboards.error
    }
  }
)(Dashboards)
