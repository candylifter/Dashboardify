import Cookies from 'js-cookie'
import axios from 'axios'

const screenshotDomain = 'http://23.251.133.254/screenshots/'
const rootDomain = `http://${window.location.hostname}/api`

export default {
  fetchItems (dashboardId) {
    let ticket = Cookies.get('ticket')

    return axios.get(`${rootDomain}/Items/GetList?ticket=${ticket}&dashboardId=${dashboardId}`)
  },

  mapBackendData (data) {
    return data.Items.map((item) => {
      let currentImg = item.Screenshots.length >= 1 ? screenshotDomain + item.Screenshots[0].ScrnshtURL : item.Failed >= 3 ? screenshotDomain + '404--white.png' : screenshotDomain + 'pending--white.png'

      return {
        id: item.Id,
        dashboardId: item.DashBoardId,
        name: item.Name,
        img: currentImg,
        url: item.Website,
        failed: item.Failed,
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
    let ticket = Cookies.get('ticket')

    let data = {
      CheckInterval: item.checkInterval,
      Id: item.id,
      Name: item.name,
      IsActive: item.isActive,
      NotifyByEmail: item.notifyByEmail
    }

    return axios.post(`${rootDomain}/Items/Update?ticket=${ticket}`, data)
  },

  deleteItem (id) {
    let ticket = Cookies.get('ticket')

    return axios.get(`${rootDomain}/Items/Delete?ticket=${ticket}&itemId=${id}`)
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
      let containsSearchText = item.name.toLowerCase().indexOf(searchText.toLowerCase()) !== -1
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
