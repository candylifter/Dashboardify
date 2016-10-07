export default {
  isEmailValid (email) {
    // http://stackoverflow.com/questions/46155/validate-email-address-in-javascript
    let re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/

    return re.test(email)
  },

  hasInvalidSymbols (input) {
    let re = /^[^!@#$%^&*()_=+\-<>?,.\\\/"'`~]+$/

    return !re.test(input)
  },

  validateName (name) {
    if (name.length < 1) {
      return 'Please enter your name'
    }

    if (/^\s/.test(name)) {
      return 'Name cannot start with a space'
    }

    if (name.length > 50) {
      return 'Name cannot be longer than 50 characters'
    }

    if (this.hasInvalidSymbols(name)) {
      return 'Name cannot have invalid symbols'
    }

    return ''
  },

  validateEmail (email) {
    if (email.length < 1) {
      return 'Please enter your email'
    }

    if (email.length > 50) {
      return 'Email cannot be longer than 50 characters'
    }

    if (!this.isEmailValid(email)) {
      return 'Please enter a valid email'
    }

    return ''
  },

  validatePassword (password) {
    if (password.length < 1) {
      return 'Please enter your password'
    }

    return ''
  },

  validateRepeatPassword (password, repeatPassword) {
    if (repeatPassword.length < 1) {
      return 'Please repeat your password'
    }

    if (repeatPassword !== password) {
      return 'Passwords have to match'
    }

    return ''
  },

  validateInvitationCode (invitationCode) {
    if (invitationCode.length < 1) {
      return 'Please enter invitation code'
    }

    return ''
  },

  validateLoginForm (email, password) {
    let validation = {
      hasErrors: false,
      emailError: '',
      passwordError: ''
    }

    validation.emailError = this.validateEmail(email)
    validation.passwordError = this.validatePassword(password)

    if (validation.emailError.length > 0 || validation.passwordError.length > 0) {
      validation.hasErrors = true
    }

    return validation
  },

  validateRegisterForm (name, email, password, repeatPassword, invitationCode) {
    let validation = {
      hasErrors: false,
      nameError: '',
      emailError: '',
      passwordError: '',
      repeatPasswordError: '',
      invitationCodeError: ''
    }

    validation.nameError = this.validateName(name)
    validation.emailError = this.validateEmail(email)
    validation.passwordError = this.validatePassword(password)
    validation.repeatPasswordError = this.validateRepeatPassword(password, repeatPassword)
    validation.invitationCodeError = this.validateInvitationCode(invitationCode)

    if (!!validation.nameError || !!validation.emailError || !!validation.passwordError || !!validation.repeatPasswordError || !!validation.invitationCodeError) {
      validation.hasErrors = true
    }

    return validation
  }
}
