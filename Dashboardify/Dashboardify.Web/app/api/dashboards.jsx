import Cookies from 'js-cookie'

import axios from 'axios'

const rootDomain = `http://${window.location.hostname}/api`

export default {
  mapBackendArray (dashboards) {
    return dashboards.map((dashboard) => this.mapBackendObject(dashboard))
  },

  mapBackendObject (dashboard) {
    return {
      id: dashboard.Id,
      userId: dashboard.UserId,
      isActive: dashboard.IsActive,
      name: dashboard.Name,
      dateCreated: dashboard.DateCreated,
      dateModified: dashboard.DateModified,
      img: 'https://placeholdit.imgix.net/~text?txtsize=33&txt=Dashboard&w=200&h=200'
    }
  },

  fetchDashboards () {
    let data = {
      Ticket: Cookies.get('ticket')
    }

    return axios.post(`${rootDomain}/Dashboards/GetList`, data)
  },

  createDashboard (name) {
    let data = {
      Ticket: Cookies.get('ticket'),
      DashName: name
    }

    return axios.post(`${rootDomain}/Dashboards/Create`, data)
  },

  deleteDashboard (id) {
    let data = {
      Ticket: Cookies.get('ticket'),
      DashboardId: id
    }

    return axios.post(`${rootDomain}/Dashboards/Delete`, data)
  }
}
