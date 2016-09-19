import { DashboardsAPI } from 'api'

export default {
  addDashboards (dashboards) {
    return {
      type: 'ADD_DASHBOARDS',
      dashboards
    }
  },

  fetchDashboards (userId) {
    return (dispatch) => {
      dispatch(this.startDashboardsFetch())

      return DashboardsAPI.fetchDashboards(userId)
        .then(
          (res) => {
            dispatch(this.addDashboards(DashboardsAPI.mapBackendData(res.data)))
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
  }
}
