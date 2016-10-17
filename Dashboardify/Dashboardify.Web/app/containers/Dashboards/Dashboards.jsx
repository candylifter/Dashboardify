import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { hashHistory } from 'react-router'

import CircularProgress from 'material-ui/CircularProgress'
import FloatingActionButton from 'material-ui/FloatingActionButton'
import ContentAdd from 'material-ui/svg-icons/content/add'

import { ErrorsAPI } from 'api'
import { DashboardsActions, ModalsActions, AuthActions } from 'actions'
import { DashboardList, CreateDashboardModal, LoadingIndicator, ErrorSnackbar } from 'components'

class Dashboards extends React.Component {
  constructor () {
    super()

    this.handleFabClick = this.handleFabClick.bind(this)
  }

  componentWillMount () {
    let { dispatch } = this.props

    dispatch(DashboardsActions.removeError())
    dispatch(DashboardsActions.fetchDashboards())
  }

  componentDidUpdate () {
    let { dispatch, error } = this.props

    if (error !== undefined) {
      if (error.status === 401) {
        dispatch(AuthActions.logout())
        hashHistory.push('/login')
      }
    }
  }

  handleFabClick () {
    let { dispatch } = this.props

    dispatch(ModalsActions.openCreateDashboardModal())
  }

  render () {
    let { isFetching, isPosting, error, postError } = this.props

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
          <div className='flex-container'>
            <div className='error'>
              <i className='error__icon material-icons'>&#xE000;</i>
              <p className='error__text'>{error.status}</p>
            </div>
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

    let renderCreateDashboard = () => {
      if (!isFetching && !error) {
        return (
          <div>
            <FloatingActionButton
              className='fab'
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

    return (
      <div className='dashboards-container'>
        <LoadingIndicator show={isPosting} />
        {renderDashboardList()}
        {renderCreateDashboard()}
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
