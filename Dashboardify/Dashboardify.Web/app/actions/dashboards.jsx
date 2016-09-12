import axios from 'axios';

import { DashboardsAPI } from 'api';

export default {
  addDashboards (dashboards) {
    return {
      type: 'ADD_DASHBOARDS',
      dashboards
    }
  },

  loadDashboards (userId) {
    return (dispatch) => {
      return this.fetchDashboards(userId)
        .then(
          (res) => dispatch(
            this.addDashboards(
              DashboardsAPI.mapBackendData(res.data)
            )
          ),
          (err) => console.error(err) // TODO: display notification that fetch failed
        );
    }
  },

  fetchDashboards (userId) {
    return axios.get(`http://localhost/api/Dashboards/GetList?userId=${userId}`);
  },
}
