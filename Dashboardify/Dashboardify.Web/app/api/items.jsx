export default {
  mapBackendData (data) {
    return data.Items.map((item) => {
      return {
        id: item.Id,
        dashboardId: item.DashBoardId,
        name: item.Name,
        img: '',
        url: item.Website,
        isActive: item.IsActive,
        isSelected: false,
        created: item.Created,
        lastChecked: item.LastChecked,
        lastModified: item.Modified,
        screenshots: item.Screenshots,
      }
    });
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
