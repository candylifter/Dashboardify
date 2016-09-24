

function getWebsite(str) {
    theleft = str.indexOf("?website=") + 9;
    theright = str.lastIndexOf("&xpath=");
    return (str.substring(theleft, theright));
}

function getXpath(str) {
  theleft = str.indexOf("&xpath=") + 7;
  theright = str.lastIndexOf("&css=");
  return (str.substring(theleft, theright));
}

function getCSS(str) {
  theleft = str.indexOf("&css=") + 5;
  theright = str.length;
  return (str.substring(theleft, theright));
}

var url = window.location.href;
var xpath = getXpath(url);
var css = getCSS(url);
var website = getWebsite(url);
var userId = 1;
var form = document.getElementById('create-item-form')
var dashboard = document.getElementById("dashboard-selector");
var dashboardId = dashboard.value;
var interval = document.getElementById('timer');
var intervalValue = interval.value;
var websiteInput = document.getElementById("website");
var xpathInput = document.getElementById("xpath");
<<<<<<< HEAD
var createDashboard = document.getElementById("new-dashboard");

createDashboard.onClick = function newBoard (){
  location("www.google.lt");
}
=======
var cssInput = document.getElementById("css");
>>>>>>> 1c59f47e6e2ba3186aff014bfc1104d6644aeebf

interval.onchange=function(){intervalValue = this.value};
dashboard.onchange=function(){dashboardId = this.value};

websiteInput.value = website;
xpathInput.value = xpath;
cssInput.value = css;


form.onsubmit = function (event) {
  event.preventDefault()

  var name = document.getElementById('item-name').value
  $.ajax({
    type: "POST",
    url: "http://localhost/api/Items/createItem",
    data: {
      Item:{
        DashBoardId: dashboardId,
        CheckInterval: intervalValue,
        XPath: xpath,
        CSS: css,
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
