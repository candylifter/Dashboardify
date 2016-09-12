const dashboardsReducer = (state = {}, action) => {
  switch (action.type) {
    case 'ADD_DASHBOARDS':
      return {
        ...state,
        data: [
          ...state.data,
          ...action.dashboards, // TODO: Compare existing array items before spreading
        ],
      };
    case 'START_DASHBOARDS_FETCH':
      return {
        ...state,
        isFetching: true,
      };
    case 'COMPLETE_DASHBOARDS_FETCH':
      return {
        ...state,
        isFetching: false,
        error: undefined,
      }
    case 'FAIL_DASHBOARDS_FETCH':
      return {
        ...state,
        isFetching: false,
        error: action.err.message,
      }
    default:
      return state;
  }
};

export default dashboardsReducer;
