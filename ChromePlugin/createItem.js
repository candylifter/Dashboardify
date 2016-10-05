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

function onlyUrl(str){
  theleft = str.indexOf("://") + 3;
  theright = str.indexOf("/", 10);;
  return (str.substring(theleft, theright));
}

var buttonDissable = document.getElementById("form-submit")
var url = window.location.href;
var xpath = getXpath(url);
var css = getCSS(url);
var website = getWebsite(url);
var userId = 1;
var form = document.getElementById('create-item-form')
var dashboard = document.getElementById("dashboard-selector");
var interval = document.getElementById('timer');
var intervalValue = parseInt(interval.value);
var websiteInput = document.getElementById("website");
var xpathInput = document.getElementById("xpath");
var cssInput = document.getElementById("css");
var ticketas;
var errorMessage = document.getElementById("error-message");

var createDashboard = document.getElementById("new-dashboard");
websiteInput.value = website;
xpathInput.value = xpath;
cssInput.value = css;



function getCookie() {
    chrome.cookies.get({
            url: "http://23.251.133.254",
            name: "ticket"
        },
        function(cookie) {
            if (cookie === null) {
                window.location = "login.html";
            } else {
                console.log(cookie);
                ticketas = cookie.value;
                getDashes(ticketas);
            }
        }
    )
}

function getDashes(ticket) {
    $.ajax({
        type: "POST",
        url: "http://23.251.133.254/api/Dashboards/GetList",
        data: {
            "Ticket": ticket
        },
        success: handleSuccess,
        error: function(data) {
            document.getElementById("errors").innerHTML = "We couldn't get your dashboards list."
            console.log("Error ecured. ", data)
        }
    })
}

// sessionValidation(ticketas);

// function sessionValidation(ticket) {
//     $.ajax({
//         type: "POST",
//         url: "http://localhost/api/Login/SessionValidation",
//         data: {
//             "Ticket": ticket
//         },
//         success: handleSession,
//         error: function(data) {
//             console.log("Įvyko klaida sessionValidation. ", data)
//         }
//     })
// }
//
// function handleSession(data) {
//     if(data.IsValid === false){
//       window.location = "login.html";
//     }
// }

getCookie();

interval.onchange = function() {
    intervalValue = parseInt(this.value)
};
dashboard.onchange = function() {
    dashboardId = parseInt(this.value)
};

var x = document.getElementById("dashboard-selector");
console.log(ticketas);

function handleSuccess(data) {
    data.Dashboards.map((dashboard) => {
        var option = document.createElement("option");
        option.text = dashboard.Name;
        option.value = dashboard.Id;
        x.add(option);
    })
}

form.onsubmit = function(event) {
    event.preventDefault()

    var dashboardId = parseInt(document.getElementById("dashboard-selector").value);
    var intervalValue = parseInt(document.getElementById('timer').value);
    var name = document.getElementById('item-name').value
    buttonDissable.disabled = true;

    var data = {
        Item: {
            DashBoardId: dashboardId,
            CheckInterval: intervalValue,
            XPath: xpath,
            CSS: css,
            Website: website,
            Name: name
        },
        Ticket: ticketas
    }
    console.log(data);
    $.ajax({
        type: "POST",
        url: "http://23.251.133.254/api/Items/createItem",
        data: data,
        success: function(data) {
            console.log(data);
            window.close();
        },
        error: function(data) {
            document.getElementById("error-occured").className = "has-error"
            errorMessage.innerHTML = "Please enter name of item"
            buttonDissable.disabled = false;
            console.log(data)
        }
    })
}

var forWebsite = document.getElementById("for-website");
forWebsite.innerHTML = " " + onlyUrl(website);
forWebsite.title=website;
forWebsite.href=website;
