import Cookies from 'js-cookie'
import axios from 'axios'

const screenshotDomain = 'http://23.251.133.254/screenshots/'
const rootDomain = `http://${window.location.hostname}/api`

export default {
  fetchItems (dashboardId) {
    let data = {
      DashboardId: dashboardId,
      Ticket: Cookies.get('ticket')
    }

    return axios.post(`${rootDomain}/Items/GetList`, data)
  },

  mapBackendData (data) {
    return data.Items.map((item) => {
      return {
        id: item.Id,
        dashboardId: item.DashBoardId,
        name: item.Name,
        img: item.Screenshots.length >= 1 ? screenshotDomain + item.Screenshots[0].ScrnshtURL : '',
        url: item.Website,
        isActive: item.IsActive,
        isSelected: false,
        checkInterval: item.CheckInterval,
        created: item.Created,
        lastChecked: item.LastChecked,
        lastModified: item.Modified,
        notifyByEmail: item.NotifyByEmail,
        screenshots: item.Screenshots.map((screenshot) => {
          screenshot.ScrnshtURL = screenshotDomain + screenshot.ScrnshtURL
          return screenshot
        })
      }
    })
  },

  getItemById (items, itemId) {
    return items.filter((item) => {
      return item.id === itemId
    })[0]
  },

  updateItem (item) {
    let data = {
      CheckInterval: item.checkInterval,
      ItemId: item.id,
      Name: item.name,
      IsActive: item.isActive,
      NotifyByEmail: item.notifyByEmail,
      Ticket: Cookies.get('ticket')
    }

    return axios.post(`${rootDomain}/Items/Update`, data)
  },

  toggleItem (id, state) {
    let data = {
      ItemId: id,
      IsActive: state,
      Ticket: Cookies.get('ticket')
    }

    return axios.post(`${rootDomain}/Items/Update`, data)
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
