import { combineReducers, createStore, compose, applyMiddleware } from 'redux';
import { routerReducer } from 'react-router-redux';
import thunk from 'redux-thunk';
import { dashboardsReducer, itemsReducer, searchTextReducer, checkIntervalsReducer } from 'reducers';

const initial = {
  dashboards: {
    isFetching: false,
    data: [],
    error: undefined,
  },
  items: {
    isFetching: false,
    data: [],
    error: undefined,
  },
}


const configureStore = (initialState = initial) => {
  const reducer = combineReducers({
    dashboards: dashboardsReducer,
    items: itemsReducer,
    searchText: searchTextReducer,
    checkIntervals: checkIntervalsReducer,
    routing: routerReducer,
  });

  const store = createStore(reducer, initialState, compose(
    applyMiddleware(thunk),
    window.devToolsExtension ? window.devToolsExtension() : f => f
  ));

  return store;
}

export default configureStore;
