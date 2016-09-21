var express = require('express')
var bodyParser = require('body-parser')

var handleRequests = require('./handleRequests')

var app = express()
const PORT = process.env.PORT || 3000

app.use(bodyParser.json())
app.use(bodyParser.urlencoded({ extended: true }))

app.post('/', function (req, res) {
  handleRequests(req).then(
    (response) => res.send(response)
  )
})

app.listen(PORT)

console.log('Express server is up on port ' + PORT)
