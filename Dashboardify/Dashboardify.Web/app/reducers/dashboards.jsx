const initialState = {
  isFetching: false,
  isPosting: false,
  postError: undefined,
  userNotified: false,
  data: [],
  error: undefined
}

const dashboardsReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'ADD_DASHBOARDS':
      return {
        ...state,
        data: action.dashboards
      }
    case 'ADD_DASHBOARD':
      return {
        ...state,
        data: [
          ...state.data,
          action.dashboard
        ]
      }
    case 'START_DASHBOARDS_FETCH':
      return {
        ...state,
        isFetching: true,
        error: undefined
      }
    case 'COMPLETE_DASHBOARDS_FETCH':
      return {
        ...state,
        isFetching: false,
        error: undefined
      }
    case 'FAIL_DASHBOARDS_FETCH':
      return {
        ...state,
        isFetching: false,
        error: action.err.response
      }
    case 'REMOVE_DASHBOARDS_ERROR':
      return {
        ...state,
        error: undefined
      }
    case 'START_CREATE_DASHBOARD':
      return {
        ...state,
        isPosting: true,
        postError: undefined
      }
    case 'COMPLETE_CREATE_DASHBOARD':
      return {
        ...state,
        isPosting: false,
        postError: undefined
      }
    case 'FAIL_CREATE_DASHBOARD':
      return {
        ...state,
        isPosting: false,
        userNotified: false,
        postError: action.err.response
      }
    case 'START_DELETE_DASHBOARD':
      return {
        ...state,
        isPosting: true,
        postError: undefined
      }
    case 'COMPLETE_DELETE_DASHBOARD':
      return {
        ...state,
        isPosting: false,
        postError: undefined,
        data: state.data.filter((dashboard) => {
          return dashboard.id !== action.id
        })
      }
    case 'FAIL_DELETE_DASHBOARD':
      return {
        ...state,
        isPosting: false,
        postError: action.err.response
      }
    default:
      return state
  }
}

export default dashboardsReducer
