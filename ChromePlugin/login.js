// var form = document.getElementById('login-form')

var loginButton = document.getElementById('login-button')
loginButton.onclick= function (){
  var newTab = {url : "http://localhost:3000"}
  chrome.tabs.create(newTab);
}

var registerButton = document.getElementById('register-button')
registerButton.onclick= function (){
  var newTab = {url : "http://localhost:3000/register"}
  chrome.tabs.create(newTab);
}


chrome.cookies.get({
        url: "http://localhost:3000",
        name: "ticket",
    },
    function(cookie) {
        if (cookie === null) {
            return null;
        } else {
          window.location = "popup.html";
        }

    })



// form.onsubmit = function(event) {
//     event.preventDefault()
//
//     var email = document.getElementById('email').value
//     var password = document.getElementById('password').value
//
//     console.log(email)
//     console.log(password)
//
//     $.ajax({
//         type: "POST",
//         url: "http://localhost/api/Login/index",
//         data: {
//             User: {
//                 Email: email,
//                 Password: password
//             }
//         },
//         success: function(data) {
//             console.log(data);
//             window.location = "popup.html";
//             console.log = "sessionId=" + SessionId;
//         },
//         error: function(data) {
//             console.log(data)
//             document.getElementById("login-error").innerHTML = "Nepavyko prisijungti."
//         }
//     })
// }
