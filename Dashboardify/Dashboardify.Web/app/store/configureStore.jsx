var redux = require('redux');
var { dashboardsReducer, itemsReducer, searchTextReducer } = require('reducers');

export var configure = (initialState = {}) => {
  var reducer = redux.combineReducers({
    dashboards: dashboardsReducer,
    items: itemsReducer,
    searchText: searchTextReducer
  });

  var store = redux.createStore(reducer, initialState, redux.compose(
    window.devToolsExtension ? window.devToolsExtension() : f => f
  ));

  return store;
}
