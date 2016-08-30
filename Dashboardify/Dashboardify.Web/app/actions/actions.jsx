export let addDashboards = (dashboards) => {
  return {
    type: 'ADD_DASHBOARDS',
    dashboards
  };
};

export let setSearchText = (searchText) => {
  return {
    type: 'SET_SEARCH_TEXT',
    searchText
  };
};

export let addItems = (items) => {
  return {
    type: 'ADD_ITEMS',
    items
  }
}

export let selectItem = (id, dashboardId) => {
  return {
    type: 'SELECT_ITEM',
    id,
    dashboardId
  }
}

export let toggleItem = (id) => {
  return {
    type: 'TOGGLE_ITEM',
    id
  }
}

export let setItemCheckInterval = (id, checkInterval) => {
  return {
    type: 'SET_ITEM_CHECK_INTERVAL',
    id,
    checkInterval
  }
}

export let addCheckIntervals = (checkIntervals) => {
  return {
    type: 'ADD_CHECK_INTERVALS',
    checkIntervals
  }
}
