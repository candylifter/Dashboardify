var validate = require('./validate')
var handleScreenshot = require('./handleScreenshot')

var handleRequests = (req) => {
  console.log('\n==>', 'Route:', '"' + req.url + '"', 'POST: ', req.body)

  var response = validate(req)

  if (response.hasErrors) {
    return new Promise(
      (resolve) => resolve(response)
    )
  }

  var url = req.body.Website
  var xpath = req.body.XPath
  var css = req.body.CSS

  return new Promise(
    (resolve) => resolve(handleScreenshot(url, xpath, css).then(
      (filename) => {
        if (filename !== null) {
          response.success = true
          response.filename = filename
        } else {
          response.success = false
          response.hasErrors = true
          response.errors.push('Failed to take screenshot')
        }
        return response
      },
      (err) => {
        response.success = false
        response.hasErrors = true
        response.errors.push(err.message)

        return response
      }
    ))
  )
}

module.exports = handleRequests
