import axios from 'axios'

const rootDomain = `http://${window.location.hostname}/api`

export default {
  fetchItems (dashboardId) {
    return axios.get(`${rootDomain}/Items/GetList?dashboardId=${dashboardId}`)
  },

  mapBackendData (data) {
    return data.Items.map((item) => {
      return {
        id: item.Id,
        dashboardId: item.DashBoardId,
        name: item.Name,
        img: item.Screenshots.length >= 1 ? 'http://' + '23.251.133.254' + '/screenshots/' + item.Screenshots[0].ScrnshtURL : '',
        url: item.Website,
        isActive: item.IsActive,
        isSelected: false,
        checkInterval: item.CheckInterval,
        created: item.Created,
        lastChecked: item.LastChecked,
        lastModified: item.Modified,
        screenshots: item.Screenshots
      }
    })
  },

  getItemById (items, itemId) {
    return items.filter((item) => {
      return item.id === itemId
    })[0]
  },

  filterItems (items, dashboardId, searchText) {
    let filteredItems = items

    filteredItems = filteredItems.filter((item) => {
      return item.dashboardId === dashboardId
    })

    filteredItems = filteredItems.filter((item) => {
      let containsSearchText = item.name.toLowerCase().indexOf(searchText) !== -1
      return searchText.length === 0 || containsSearchText
    })

    return filteredItems
  },

  getSelectedItemDashboardId (items, dashboardId) {
    return items.filter((item) => {
      return item.dashboardId === dashboardId && item.isSelected
    })[0]
  },

  extractDomain (url) {
    // find & remove protocol (http, ftp, etc.) and get domain
    let domain = url.indexOf('://') > -1 ? url.split('/')[2] : url.split('/')[0]

    // find & remove port number
    domain = domain.split(':')[0]

    return domain
  }
}
