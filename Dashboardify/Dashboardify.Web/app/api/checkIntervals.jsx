export default {
  getCheckIntervals: () => {
    let checkIntervals = [
      {
        id: 1,
        checkInterval: 300000 // 5 min
      },
      {
        id: 2,
        checkInterval: 600000 // 10 min
      },
      {
        id: 3,
        checkInterval: 900000 // 15 min
      },
      {
        id: 4,
        checkInterval: 1800000 // 30 min
      },
      {
        id: 5,
        checkInterval: 3600000 // 1 hour
      },
      {
        id: 6,
        checkInterval: 10800000 // 3 hours
      },
      {
        id: 7,
        checkInterval: 32400000 // 9 hours
      },
      {
        id: 8,
        checkInterval: 86400000 // 24 hours
      }
    ]

    return checkIntervals
  }
}
