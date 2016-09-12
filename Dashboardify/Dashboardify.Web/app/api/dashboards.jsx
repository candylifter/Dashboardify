export default {
  getDashboards (/*userId*/) {
    return [
      {
        id: 1,
        userId: 1,
        isActive: true,
        name: 'Automobilių kainos',
        img: 'https://upload.wikimedia.org/wikipedia/commons/c/cf/Volkswagen_Beetle_.jpg',
        dateCreated: '2016-05-12 12:36:40',
        dateModified: '2016-05-15 13:36:40'
      },
      {
        id: 2,
        userId: 1,
        isActive: true,
        name: 'IT Naujienos',
        img: 'http://www.tibco.com/blog/wp-content/uploads/2013/07/spaghetti-integration.jpg',
        dateCreated: '2016-08-12 12:36:40',
        dateModified: '2016-08-19 21:36:40'
      },
      {
        id: 3,
        userId: 1,
        isActive: true,
        name: 'Maestro bajeriai',
        img: 'http://g4.dcdn.lt/images/pix/arturas-orlauskas-64067958.jpg',
        dateCreated: '2016-08-23 06:36:40',
        dateModified: '2016-08-25 18:36:40'
      }
    ]

  },

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
}
