const initialState = {
  isFetching: false,
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
        error: action.err.message
      }
    default:
      return state
  }
}

export default dashboardsReducer
