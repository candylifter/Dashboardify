var redux = require('redux');
import { dashboardsReducer, itemsReducer, searchTextReducer, checkIntervalsReducer } from 'reducers';

export var configure = (initialState = {}) => {
  var reducer = redux.combineReducers({
    dashboards: dashboardsReducer,
    items: itemsReducer,
    searchText: searchTextReducer,
    checkIntervals: checkIntervalsReducer,
  });

  var store = redux.createStore(reducer, initialState, redux.compose(
    window.devToolsExtension ? window.devToolsExtension() : f => f
  ));

  return store;
}
