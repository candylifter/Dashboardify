const initialState = {
  open: false,
  item: {}
}

const itemPanelReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'OPEN_ITEM_PANEL':
      return {
        ...state,
        open: true
      }
    case 'CLOSE_ITEM_PANEL':
      return {
        ...state,
        open: false
      }
    case 'SET_ITEM_PANEL':
      return {
        ...state,
        item: action.item
      }
    default:
      return state
  }
}

export default itemPanelReducer
