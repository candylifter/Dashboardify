import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import CircularProgress from 'material-ui/CircularProgress'
import FloatingActionButton from 'material-ui/FloatingActionButton'
import ContentAdd from 'material-ui/svg-icons/content/add'

import { ErrorsAPI } from 'api'
import { DashboardsActions, ModalsActions } from 'actions'
import { DashboardList, CreateDashboardModal, LoadingIndicator, ErrorSnackbar } from 'components'

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
    let { isFetching, isPosting, error, postError } = this.props

    const style = {
      // display: 'flex',
      // justifyContent: 'center',
      error: {
        // width: '100%',
        // minHeight: 'calc(100vh - 64px)',
        // display: 'flex',
        // flexDirection: 'column',
        // justifyContent: 'center',
        // alignItems: 'center',
        // textAlign: 'center',
        // color: '#9E9E9E',
        icon: {
          // fontSize: '8em'
        },
        text: {
          // fontSize: '2em'
        }
      }
    }

    let renderDashboardList = () => {
      if (isFetching) {
        return (
          <div className='spinner'>
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
            <p style={style.error.text}>{error.status}</p>
          </div>
        )
      }
    }

    let renderErrorSnackbar = () => {
      if (postError) {
        return (
          postError.data.Errors.map((resError, index) => {
            return <ErrorSnackbar key={index} open message={ErrorsAPI.translate(resError.Code)} />
          })
        )
      }
    }

    return (
      <div style={style} className='dashboards-container'>
        <LoadingIndicator show={isPosting} />
        {renderDashboardList()}
        <FloatingActionButton
          className='fab'
          onClick={this.handleFabClick}
          secondary
        >
          <ContentAdd />
        </FloatingActionButton>
        <CreateDashboardModal />
        {renderErrorSnackbar()}
      </div>
    )
  }
}

Dashboards.propTypes = {
  dispatch: PropTypes.func,
  isFetching: PropTypes.bool,
  isPosting: PropTypes.bool,
  postError: PropTypes.object,
  error: PropTypes.object
}

export default connect(
  (state) => {
    return {
      isPosting: state.dashboards.isPosting,
      isFetching: state.dashboards.isFetching,
      postError: state.dashboards.postError,
      error: state.dashboards.error
    }
  }
)(Dashboards)
