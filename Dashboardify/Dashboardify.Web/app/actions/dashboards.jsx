import { DashboardsAPI } from 'api'

export default {
  addDashboards (dashboards) {
    return {
      type: 'ADD_DASHBOARDS',
      dashboards
    }
  },

  addDashboard (dashboard) {
    return {
      type: 'ADD_DASHBOARD',
      dashboard
    }
  },

  fetchDashboards () {
    return (dispatch) => {
      dispatch(this.startDashboardsFetch())

      return DashboardsAPI.fetchDashboards()
        .then(
          (res) => {
            dispatch(this.addDashboards(DashboardsAPI.mapBackendArray(res.data.Dashboards)))
            dispatch(this.completeDashboardsFetch())
          },
          (err) => dispatch(this.failDashboardsFetch(err))
        )
    }
  },

  startDashboardsFetch () {
    return {
      type: 'START_DASHBOARDS_FETCH'
    }
  },

  completeDashboardsFetch () {
    return {
      type: 'COMPLETE_DASHBOARDS_FETCH'
    }
  },

  failDashboardsFetch (err) {
    return {
      type: 'FAIL_DASHBOARDS_FETCH',
      err
    }
  },

  createDashboard (name) {
    return (dispatch) => {
      dispatch(this.startCreateDashboard())

      return DashboardsAPI.createDashboard(name)
        .then(
          (res) => {
            let dashboard = DashboardsAPI.mapBackendObject(res.data.Dashboard)
            dispatch(this.addDashboard(dashboard))
            dispatch(this.completeCreateDashboard())
          },
          (err) => dispatch(this.failCreateDashboard(err))
        )
    }
  },

  startCreateDashboard () {
    return {
      type: 'START_CREATE_DASHBOARD'
    }
  },

  completeCreateDashboard () {
    return {
      type: 'COMPLETE_CREATE_DASHBOARD'
    }
  },

  failCreateDashboard (err) {
    return {
      type: 'FAIL_CREATE_DASHBOARD',
      err
    }
  }
}
