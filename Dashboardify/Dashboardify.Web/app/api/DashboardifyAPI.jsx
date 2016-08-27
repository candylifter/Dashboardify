export default {
  getDashboards: (/*userId*/) => {
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

  getItems: (dashboardId) => {

    //Mock method and data

    let items = [
      {
        id: 1,
        dashboardId: 1,
        name: 'Nikoliukas',
        img: 'https://www.placecage.com/gif/500/200',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: '5 min',
        lastChecked: 999,
        lastModified: 1472129223
      },
      {
        id: 2,
        dashboardId: 2,
        name: 'Antras Dashas',
        img: 'https://www.placecage.com/gif/500/200',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: '5 min',
        lastChecked: 999,
        lastModified: 1472129223
      },
      {
        id: 3,
        dashboardId: 3,
        name: 'Maestro spec. būrys',
        img: 'https://media.giphy.com/media/aheCqneBWGTyU/giphy.gif',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: '5 min',
        lastChecked: 999,
        lastModified: 1472129223
      },
      {
        id: 4,
        dashboardId: 3,
        name: 'Maestro šovė pirmą',
        img: 'https://i.ytimg.com/vi/-JveHAi__f8/maxresdefault.jpg',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: '5 min',
        lastChecked: 999,
        lastModified: 1472129223
      },
      {
        id: 5,
        dashboardId: 3,
        name: 'Maestro šauna dar vieną',
        img: 'http://sventesgidas.lt/galerija/ArtYras_Orlauskas_YvenYiY_renginiY_vedYjas_komikas0_1.jpg',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: '5 min',
        lastChecked: 999,
        lastModified: 1472129223
      },
    ];

    let filteredItems = items.filter((item) => {
      return item.dashboardId == dashboardId ? true : false;
    });

    return filteredItems;
  }
}
