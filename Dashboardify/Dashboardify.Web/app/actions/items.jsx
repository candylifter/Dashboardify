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

  toggleItem (id) {
    return {
      type: 'TOGGLE_ITEM',
      id
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
