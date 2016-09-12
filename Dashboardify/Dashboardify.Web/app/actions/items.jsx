import axios from 'axios';

import { ItemsAPI } from 'api';

export default {
  addItems (items) {
    return {
      type: 'ADD_ITEMS',
      items
    }
  },

  loadItems (dashboardId) {
    return (dispatch) => {
      return this.fetchItems(dashboardId)
        .then(
          (res) => dispatch(
            this.addItems(
              ItemsAPI.mapBackendData(res.data)
            )
          ),
          (err) => console.error(err)
        );
    }
  },

  fetchItems (dashboardId) {
    return axios.get(`http://localhost/api/Items/GetList?dashboardId=${dashboardId}`);
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
