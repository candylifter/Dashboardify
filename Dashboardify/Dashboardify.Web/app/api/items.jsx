export default {
  getItems () {

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
        checkInterval: 600000,
        lastChecked: '2016-08-23 06:36:40',
        lastModified: '2016-08-23 06:36:40'
      },
      {
        id: 2,
        dashboardId: 2,
        name: 'Antras Dashas',
        img: 'https://www.placecage.com/gif/500/200',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: 600000,
        lastChecked: '2016-08-23 06:36:40',
        lastModified: '2016-08-23 06:36:40'
      },
      {
        id: 3,
        dashboardId: 3,
        name: 'Maestro spec. būrys',
        img: 'https://media.giphy.com/media/aheCqneBWGTyU/giphy.gif',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: 600000,
        lastChecked: '2016-08-23 06:36:40',
        lastModified: '2016-08-23 06:36:40'
      },
      {
        id: 4,
        dashboardId: 3,
        name: 'Maestro šovė pirmą',
        img: 'https://i.ytimg.com/vi/-JveHAi__f8/maxresdefault.jpg',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: 600000,
        lastChecked: '2016-08-23 06:36:40',
        lastModified: '2016-08-23 06:36:40'
      },
      {
        id: 5,
        dashboardId: 3,
        name: 'Maestro šauna dar vieną',
        img: 'http://sventesgidas.lt/galerija/ArtYras_Orlauskas_YvenYiY_renginiY_vedYjas_komikas0_1.jpg',
        url: '//www.autogidas.lt',
        isActive: true,
        isSelected: false,
        checkInterval: 600000,
        lastChecked: '2016-08-23 06:36:40',
        lastModified: '2016-08-23 06:36:40'
      },
    ];

    return items;

    // return axios.get('//localhost/api/Items/GetItems/')
    //   .then((res) => {
    //     console.log(res.data.items);
    //     return res.data.items;
    //
    //   })
    //   .catch((err) => console.log(err));

  },

  filterItems (items, dashboardId, searchText) {
    let filteredItems = items;

    filteredItems = filteredItems.filter((item) => {
      return item.dashboardId == dashboardId;
    });

    filteredItems = filteredItems.filter((item) => {
      let containsSearchText = item.name.toLowerCase().indexOf(searchText) !== -1;
      return searchText.length === 0 || containsSearchText;
    });

    return filteredItems;
  },
}
