import Cookies from 'js-cookie'

import axios from 'axios'

const rootDomain = `http://${window.location.hostname}/api`

export default {
  mapBackendData (data) {
    return data.Dashboards.map((dashboard) => {
      return {
        id: dashboard.Id,
        userId: dashboard.UserId,
        isActive: dashboard.IsActive,
        name: dashboard.Name,
        dateCreated: dashboard.DateCreated,
        dateModified: dashboard.DateModified,
        img: 'https://placeholdit.imgix.net/~text?txtsize=33&txt=Dashboard&w=200&h=200'
      }
    })
  },

  fetchDashboards () {
    let data = {
      Ticket: Cookies.get('ticket')
    }

    return axios.post(`${rootDomain}/Dashboards/GetList`, data)
  }
}
