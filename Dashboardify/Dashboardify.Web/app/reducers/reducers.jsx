export let dashboardsReducer = (state = [], action) => {
  switch (action.type) {
    case 'ADD_DASHBOARDS':
      return [
        ...state,
        ...action.dashboards
      ];
    default:
      return state;
  }
}

export let itemsReducer = (state = [], action) => {
  switch (action.type) {
    case 'ADD_ITEMS':
      return [
        ...state,
        ...action.items
      ]
    default:
      return state;
  }
}

export let searchTextReducer = (state = '', action) => {
  switch (action.type) {
    case 'SET_SEARCH_TEXT':
      return action.searchText;
    default:
      return state;
  }
}
