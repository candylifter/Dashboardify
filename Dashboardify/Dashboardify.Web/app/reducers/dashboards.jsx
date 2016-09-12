const dashboardsReducer = (state = [], action) => {
  switch (action.type) {
    case 'ADD_DASHBOARDS':
      return [
        ...state,
        ...action.dashboards
      ];
    default:
      return state;
  }
};

export default dashboardsReducer;
