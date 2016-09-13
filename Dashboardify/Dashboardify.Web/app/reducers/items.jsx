const itemsReducer = (state = {}, action) => {
  switch (action.type) {
    case 'ADD_ITEMS':
      return {
        ...state,
        data: action.items
      };
    case 'START_ITEMS_FETCH':
      return {
        ...state,
        isFetching: true,
      };
    case 'COMPLETE_ITEMS_FETCH':
      return {
        ...state,
        isFetching: false,
        error: undefined,
      }
    case 'FAIL_ITEMS_FETCH':
      return {
        ...state,
        isFetching: false,
        error: action.err.message,
      }
    case 'SELECT_ITEM':
      return {
        ...state,
        data: state.data.map((item) => {
          if (item.dashboardId === action.dashboardId ) {
            item.isSelected = item.id === action.id;
          }

          return item;
        }),
      };
    case 'TOGGLE_ITEM':
      return {
        ...state,
        data: state.data.map((item) => {
          if (item.id === action.id) {
            item.isActive = !item.isActive;
          }

          return item;
        }),
      };
    case 'SET_ITEM_CHECK_INTERVAL':
      return {
        ...state,
        data: state.data.map((item) => {
          if (item.id === action.id) {
            item.checkInterval = action.checkInterval;
          }

          return item;
        }),
      };
    default:
      return state;
  }
};

export default itemsReducer;
