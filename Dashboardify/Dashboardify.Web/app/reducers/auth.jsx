const initialState = {
  isPosting: false,
  isAuthenticated: false,
  error: undefined,
  registerError: undefined
}

const authReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'START_LOGIN':
      return {
        ...state,
        isPosting: true
      }
    case 'COMPLETE_LOGIN':
      return {
        ...state,
        isPosting: false,
        isAuthenticated: true,
        error: undefined
      }
    case 'FAIL_LOGIN':
      return {
        ...state,
        isPosting: false,
        isAuthenticated: false,
        error: action.err.message
      }
    case 'LOGOUT':
      return {
        ...state,
        isAuthenticated: false
      }
    case 'START_REGISTER':
      return {
        ...state,
        isPosting: true
      }
    case 'COMPLETE_REGISTER':
      return {
        ...state,
        isPosting: false,
        registerError: undefined
      }
    case 'FAIL_REGISTER':
      return {
        ...state,
        isPosting: false,
        registerError: action.err
      }
    default:
      return state
  }
}

export default authReducer
