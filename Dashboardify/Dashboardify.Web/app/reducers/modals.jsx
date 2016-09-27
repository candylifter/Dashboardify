const initialState = {
  createDashboardModal: {
    open: false
  }
}

const modalsReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'OPEN_CREATE_DASHBOARD_MODAL':
      return {
        ...state,
        createDashboardModal: {
          ...state.createDashboardModal,
          open: true
        }
      }
    case 'CLOSE_CREATE_DASHBOARD_MODAL':
      return {
        ...state,
        createDashboardModal: {
          ...state.createDashboardModal,
          open: false
        }
      }
    default:
      return state
  }
}

export default modalsReducer
