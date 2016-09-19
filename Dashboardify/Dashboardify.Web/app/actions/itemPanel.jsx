export default {
  open () {
    return {
      type: 'OPEN_ITEM_PANEL'
    }
  },

  close () {
    return {
      type: 'CLOSE_ITEM_PANEL'
    }
  },

  set (item) {
    return {
      type: 'SET_ITEM_PANEL',
      item
    }
  }
}
