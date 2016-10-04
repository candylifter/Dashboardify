import { ItemsAPI } from 'api'

export default {
  addItems (items) {
    return {
      type: 'ADD_ITEMS',
      items
    }
  },

  fetchItems (dashboardId) {
    return (dispatch) => {
      dispatch(this.startItemsFetch())

      return ItemsAPI.fetchItems(dashboardId)
        .then(
          (res) => {
            dispatch(this.addItems(ItemsAPI.mapBackendData(res.data)))
            dispatch(this.completeItemsFetch())
          },
          (err) => dispatch(this.failItemsFetch(err))
        )
    }
  },

  startItemsFetch () {
    return {
      type: 'START_ITEMS_FETCH'
    }
  },

  completeItemsFetch () {
    return {
      type: 'COMPLETE_ITEMS_FETCH'
    }
  },

  failItemsFetch (err) {
    return {
      type: 'FAIL_ITEMS_FETCH',
      err
    }
  },

  selectItem (id, dashboardId) {
    return {
      type: 'SELECT_ITEM',
      id,
      dashboardId
    }
  },

  updateItem (item) {
    return (dispatch) => {
      dispatch(this.startUpdateItem(item))

      return ItemsAPI.updateItem(item)
        .then(
          (res) => dispatch(this.completeUpdateItem(item)),
          (err) => dispatch(this.failUpdateItem(err))
        )
    }
  },

  startUpdateItem () {
    return {
      type: 'START_UPDATE_ITEM'
    }
  },

  completeUpdateItem (item) {
    return {
      type: 'COMPLETE_UPDATE_ITEM',
      item
    }
  },

  failUpdateItem (err) {
    return {
      type: 'FAIL_UPDATE_ITEM',
      err
    }
  },

  toggleItem (id, state) {
    return (dispatch) => {
      dispatch(this.startToggleItem())

      return ItemsAPI.toggleItem(id, state)
        .then(
          (res) => dispatch(this.completeToggleItem(id)),
          (err) => dispatch(this.failToggleItem(err))
        )
    }
  },

  startToggleItem () {
    return {
      type: 'START_TOGGLE_ITEM'
    }
  },

  completeToggleItem (id) {
    return {
      type: 'COMPLETE_TOGGLE_ITEM',
      id
    }
  },

  failToggleItem (err) {
    return {
      type: 'FAIL_TOGGLE_ITEM',
      err
    }
  },

  setItemCheckInterval (id, checkInterval) {
    return {
      type: 'SET_ITEM_CHECK_INTERVAL',
      id,
      checkInterval
    }
  }
}
