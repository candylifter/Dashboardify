const checkIntervalsReducer = (state = [], action) => {
  switch (action.type) {
    case 'ADD_CHECK_INTERVALS':
      return action.checkIntervals
    default:
      return state
  }
}

export default checkIntervalsReducer
