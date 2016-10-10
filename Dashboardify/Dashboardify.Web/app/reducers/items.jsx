const initialState = {
  isFetching: false,
  isPosting: false,
  postError: undefined,
  data: [],
  error: undefined
}

const itemsReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'ADD_ITEMS':
      return {
        ...state,
        data: action.items
      }
    case 'START_ITEMS_FETCH':
      return {
        ...state,
        isFetching: true
      }
    case 'COMPLETE_ITEMS_FETCH':
      return {
        ...state,
        isFetching: false,
        error: undefined
      }
    case 'FAIL_ITEMS_FETCH':
      return {
        ...state,
        isFetching: false,
        error: action.err.response
      }
    case 'SELECT_ITEM':
      return {
        ...state,
        data: state.data.map((item) => {
          if (item.dashboardId === action.dashboardId) {
            item.isSelected = item.id === action.id
          }

          return item
        })
      }
    case 'START_TOGGLE_ITEM':
      return {
        ...state,
        isPosting: true,
        postError: undefined
      }
    case 'COMPLETE_TOGGLE_ITEM':
      return {
        ...state,
        isPosting: false,
        postError: undefined,
        data: state.data.map((item) => {
          if (item.id === action.id) {
            item.isActive = !item.isActive
          }

          return item
        })
      }
    case 'FAIL_TOGGLE_ITEM':
      return {
        ...state,
        isPosting: false,
        postError: action.err.response
      }
    case 'START_DELETE_ITEM':
      return {
        ...state,
        isPosting: true,
        postError: undefined
      }
    case 'COMPLETE_DELETE_ITEM':
      return {
        ...state,
        isPosting: false,
        postError: undefined,
        data: state.data.filter((item) => {
          return item.id !== action.id
        })
      }
    case 'SET_ITEM_CHECK_INTERVAL':
      return {
        ...state,
        data: state.data.map((item) => {
          if (item.id === action.id) {
            item.checkInterval = action.checkInterval
          }

          return item
        })
      }

    case 'START_UPDATE_ITEM':
      return {
        ...state,
        isPosting: true,
        postError: undefined
      }
    case 'COMPLETE_UPDATE_ITEM':
      return {
        ...state,
        isPosting: false,
        postError: undefined,
        data: state.data.map((item) => {
          if (item.id === action.item.id) {
            item = action.item
          }
          return item
        })
      }
    case 'FAIL_UPDATE_ITEM':
      return {
        ...state,
        isPosting: false,
        postError: action.err.response
      }
    default:
      return state
  }
}

export default itemsReducer
