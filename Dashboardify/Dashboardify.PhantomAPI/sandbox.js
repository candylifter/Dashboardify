var Horseman = require('node-horseman')

function normal () {
  var startTime = Date.now()
  var horseman = new Horseman()

  horseman
    .open('http://localhost/')
    .then(() => {
      horseman
        .close()
        .then(() => {
          console.log((Date.now() - startTime) + 'ms')
        })
    })
}

function tabs () {
  var startTime = Date.now()
  var horseman = new Horseman()

  var startTime = Date.now()

  horseman
    .openTab('http://localhost/')
    .then(() => {
      horseman
        .closeTab(0)
        .then(() => {
          console.log((Date.now() - startTime) + 'ms')
        })
    })

}

var count = 5

console.log('Testing normal horseman %s times:', count)
for (var i = 0; i < count; i++) {
  normal()
}

// console.log('Testing tabs horseman %s times:', count)
// for (var i = 0; i < count; i++) {
//   tabs()
// }
