export default {
  translate (error) {
    switch (error) {
      case 'BAD_REQUEST':
      case 'TICKET_NOT_DEFINED':
      case 'DASHBOARD_NOT_DEFINED':
        return 'Dashboard was not defined'
      case 'USER_NOT_DEFINED':
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
