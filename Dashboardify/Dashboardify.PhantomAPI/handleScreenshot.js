var Horseman = require('node-horseman')
var uuid = require('node-uuid')

function getElementBoundingClientRect (xpath, css) {
  var element = document.evaluate(
      xpath,
      document,
      null,
      // eslint-disable-next-line no-undef
      XPathResult.FIRST_ORDERED_NODE_TYPE,
      null).singleNodeValue || document.querySelector(css)

  return element === null ? null : element.getBoundingClientRect()
}

var handleScreenshot = (url, xpath, css) => {
  console.log('==> Starting horseman')
  var startTime = Date.now()
  var horseman = new Horseman()

  return horseman
    .viewport(1920, 1080)
    .open(url)
    .then(status => {
      if (status === 'success') {
        console.log('    [+] Opened %s', url)
        return horseman.evaluate(getElementBoundingClientRect, xpath, css)
      } else {
        console.log('    [-] Failed to open url %s', url)
        return null
      }
    })
    .then(clipRect => {
      if (clipRect !== '' && clipRect !== null) { // evaluate returns empty string instea of null for some reason
        var filename = uuid()
        filename = filename.split('-').join('') + '.png'

        return horseman
          .crop(clipRect, '../Dashboardify.Screenshots/' + filename)
          .then(() => {
            console.log('    [+] Saved screenshot: %s', filename)
            console.log('==> Exiting horseman')
            horseman.close()
            console.log('    Completed in %sms\n', (Date.now() - startTime).toString())
            return filename
          })
      } else {
        console.log('    [-] Failed to get element')
        console.log('    Completed in %sms\n', (Date.now() - startTime).toString())
        console.log('==> Exiting horseman')
        horseman.close()
        return null
      }
    })
}

module.exports = handleScreenshot
