import { combineReducers, createStore, compose, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import { dashboardsReducer, itemsReducer, searchTextReducer, checkIntervalsReducer } from 'reducers';

const configureStore = (initialState = {}) => {
  const reducer = combineReducers({
    dashboards: dashboardsReducer,
    items: itemsReducer,
    searchText: searchTextReducer,
    checkIntervals: checkIntervalsReducer,
  });

  const store = createStore(reducer, initialState, compose(
    applyMiddleware(thunk),
    window.devToolsExtension ? window.devToolsExtension() : f => f
  ));

  return store;
}

export default configureStore;
