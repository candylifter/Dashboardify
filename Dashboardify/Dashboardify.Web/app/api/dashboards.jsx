import axios from 'axios';

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
        img: 'https://placeholdit.imgix.net/~text?txtsize=33&txt=Dashboard&w=200&h=200',
      }
    });
  },

  fetchDashboards(userId) {
    return axios.get(`http://localhost/api/Dashboards/GetList?userId=${userId}`);
  }
}
