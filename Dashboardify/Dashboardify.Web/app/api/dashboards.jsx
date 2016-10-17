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
    let ticket = Cookies.get('ticket')

    return axios.get(`${rootDomain}/Dashboards/GetList?ticket=${ticket}`)
  },

  createDashboard (name) {
    let ticket = Cookies.get('ticket')

    return axios.get(`${rootDomain}/Dashboards/Create?ticket=${ticket}&dashName=${name}`)
  },

  deleteDashboard (id) {
    let ticket = Cookies.get('ticket')

    return axios.get(`${rootDomain}/Dashboards/Delete/${id}?ticket=${ticket}`)
  }
}
