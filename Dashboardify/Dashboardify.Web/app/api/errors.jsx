export default {
  translate (error) {
    switch (error) {
      case 'BAD_REQUEST':
        return 'Bad request, please try again later'
      case 'TICKET_NOT_DEFINED':
        return 'Auth error, relog and try again'
      case 'DASHBOARD_NOT_DEFINED':
        return 'Dashboard has to have a name'
      case 'WRONG_EMAIL_FORMAT':
        return 'Email has wrong format, please try a valid one'
      case 'USERNAME_MUST_BE_ATLEAST_5_CHARACTERS_LONG':
        return 'Name has to be atleast 5 characters long'
      case 'EMAIL_ALREADY_TAKEN':
        return 'Email is already taken'
      case 'USER_NOT_DEFINED':
        return 'User has to have a name'
      case 'NAME_ALREADY_EXISTS':
        return 'Can\'t create dashboard with the same name'
      case 'INVALID_USERNAME_OR_PASSWORD':
        return 'Incorrect email or password'
      case 'INVITATION_CODE_DONT_MATCH':
        return 'Incorrect invitation code'
      default:
        return 'Unexpected error occured, try again later'
    }
  }
}
