const initialState = {
  isFetching: false,
  isCreating: false,
  createError: undefined,
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
        isFetching: true
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
    case 'START_CREATE_DASHBOARD':
      return {
        ...state,
        isCreating: true
      }
    case 'COMPLETE_CREATE_DASHBOARD':
      return {
        ...state,
        isCreating: false,
        createError: undefined
      }
    case 'FAIL_CREATE_DASHBOARD':
      return {
        ...state,
        isCreating: false,
        createError: action.err.response
      }
    default:
      return state
  }
}

export default dashboardsReducer
