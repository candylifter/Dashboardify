const itemsReducer = (state = [], action) => {
  switch (action.type) {
    case 'ADD_ITEMS':
      return [
        ...state,
        ...action.items
      ];
    case 'SELECT_ITEM':
      return [
        ...state.map((item) => {
          if (item.dashboardId === action.dashboardId ) {
            item.isSelected = item.id === action.id;
          }

          return item;
        })
      ];
    case 'TOGGLE_ITEM':
      return [
        ...state.map((item) => {
          if (item.id === action.id) {
            item.isActive = !item.isActive;
          }

          return item;
        })
      ];
    case 'SET_ITEM_CHECK_INTERVAL':
      return [
        ...state.map((item) => {
          if (item.id === action.id) {
            item.checkInterval = action.checkInterval;
          }

          return item;
        })
      ]
    default:
      return state;
  }
};

export default itemsReducer;
