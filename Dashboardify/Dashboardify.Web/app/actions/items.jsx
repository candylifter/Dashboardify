// export let addItems = (items) => {
//   return {
//     type: 'ADD_ITEMS',
//     items
//   }
// }
//
// export let selectItem = (id, dashboardId) => {
//   return {
//     type: 'SELECT_ITEM',
//     id,
//     dashboardId
//   }
// }
//
// export let toggleItem = (id) => {
//   return {
//     type: 'TOGGLE_ITEM',
//     id
//   }
// }
//
// export let setItemCheckInterval = (id, checkInterval) => {
//   return {
//     type: 'SET_ITEM_CHECK_INTERVAL',
//     id,
//     checkInterval
//   }
// }

export default {
  addItems (items) {
    return {
      type: 'ADD_ITEMS',
      items
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
  },
}
