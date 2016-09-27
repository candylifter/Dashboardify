const initialState = {
  isLoggingIn: false,
  isAuthenticated: false,
  error: undefined,
  isRegistering: false,
  registerError: undefined
}

const authReducer = (state = initialState, action) => {
  switch (action.type) {
    case 'START_LOGIN':
      return {
        ...state,
        isLoggingIn: true
      }
    case 'COMPLETE_LOGIN':
      return {
        ...state,
        isLoggingIn: false,
        isAuthenticated: true,
        error: undefined
      }
    case 'FAIL_LOGIN':
      return {
        ...state,
        isLoggingIn: false,
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
        isRegistering: true
      }
    case 'COMPLETE_REGISTER':
      return {
        ...state,
        isRegistering: false,
        registerError: undefined
      }
    case 'FAIL_REGISTER':
      return {
        ...state,
        isRegistering: false,
        registerError: action.err
      }
    default:
      return state
  }
}

export default authReducer
