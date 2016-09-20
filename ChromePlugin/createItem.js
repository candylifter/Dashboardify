document.body.appendChild(div);

function getWebsite(str) {
    theleft = str.indexOf("?website=") + 9;
    theright = str.lastIndexOf("&xpath=");
    return (str.substring(theleft, theright));
}

function getXpath(str) {
  theleft = str.indexOf("&xpath=") + 7;
  theright = str.length;
  return (str.substring(theleft, theright));
}

var url = window.location.href;
var xpath = getXpath(url);
var website = getWebsite(url);
var form = document.getElementById('create-item-form')

var websiteInput = document.getElementById("website");
var xpathInput = document.getElementById("xpath");

websiteInput.value = website;
xpathInput.value = xpath;

form.onsubmit = function (event) {
  event.preventDefault()

  var name = document.getElementById('item-name').value

  $.ajax({
    type: "POST",
    url: "http://localhost/api/Items/createItem",
    data: {
      Item:{
        DashBoardId: 1,
        CheckInterval: 30000,
        XPath: xpath,
        CSS: "body",
        Website: website,
        Name: name
      }
    },
    success: function(data){
      console.log(data);
      window.close();
    },
    error: function(data){
      console.log(data)
    }
  })
}
