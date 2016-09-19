var form = document.getElementById('login-form')

form.onsubmit = function (event) {
  event.preventDefault()

  var email = document.getElementById('email').value
  var password = document.getElementById('password').value

  console.log(email)
  console.log(password)

  $.ajax({
    type: "POST",
    url: "http://localhost/api/UserSession/login",
    data: {
      Name: email,
      Password: password
    },
    success: function(data){
      console.log(data)
    },
    error: function(data){
      console.log(data)
    }
  })
}
