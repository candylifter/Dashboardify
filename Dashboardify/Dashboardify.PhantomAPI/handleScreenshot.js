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

const handleScreenshot = (url, xpath, css) => {
  console.log('==> Starting Horseman')
  let startTime = Date.now()
  let horseman = new Horseman()

  return horseman
    .viewport(1920, 1080)
    .open(url)
    .then(status => {
      if (status === 'success') {
        console.log('    [+] Opened %s', url)
        return horseman.evaluate(getElementBoundingClientRect, xpath, css)
      } else {
        throw { name: 'BadStatusCode', message: 'Failed to open website' }
      }
    })
    .then(clipRect => {
      if (clipRect) {
        let filename = uuid()
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
        throw { name: 'BadClipRect', message: 'Failed to get clipRect' }
      }
    })
    .catch(e => {
      console.log('==> Exiting horseman')
      console.log('    Completed in %sms\n', (Date.now() - startTime).toString())
      horseman.close()
      throw e
    })
}

module.exports = handleScreenshot
