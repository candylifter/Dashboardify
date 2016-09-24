import { combineReducers, createStore, compose, applyMiddleware } from 'redux'
import { routerReducer } from 'react-router-redux'
import thunk from 'redux-thunk'
import { dashboardsReducer, itemsReducer, searchTextReducer, checkIntervalsReducer, itemPanelReducer, authReducer } from 'reducers'

const configureStore = (initialState) => {
  const reducer = combineReducers({
    dashboards: dashboardsReducer,
    items: itemsReducer,
    itemPanel: itemPanelReducer,
    searchText: searchTextReducer,
    checkIntervals: checkIntervalsReducer,
    auth: authReducer,
    routing: routerReducer
  })

  const store = createStore(reducer, initialState, compose(
    applyMiddleware(thunk),
    window.devToolsExtension ? window.devToolsExtension() : f => f
  ))

  return store
}

export default configureStore
