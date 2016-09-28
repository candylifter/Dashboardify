var validate = (req) => {
  var res = {
    hasErrors: false,
    errors: []
  }

  if (req.body.Website === undefined) {
    res.errors.push('Website not defined')
  } else if (typeof (req.body.Website) !== 'string') {
    res.errors.push('Website has to be string')
  }

  if (req.body.XPath === undefined) {
    res.errors.push('XPath not defined')
  } else if (typeof (req.body.XPath) !== 'string') {
    res.errors.push('XPath has to be string')
  }

  if (req.body.CSS === undefined) {
    // res.errors.push('CSS not defined')
    req.body.CSS = null
  } else if (typeof (req.body.CSS) !== 'string') {
    res.errors.push('CSS has to be string')
  }

  if (res.errors.length > 0) {
    res.hasErrors = true
  }

  return res
}

module.exports = validate
