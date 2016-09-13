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
console.log(xpath, website);

var websiteInput = document.getElementById("website");
var xpathInput = document.getElementById("xpath");

websiteInput.value = website;
xpathInput.value = xpath;
