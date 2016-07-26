
//just for testing
const HIGHLIGHT_CLASSNAME = 'dashboardify-highlight';

function disableEvents(callback) {}

//Returns XPath to element
function getPathTo(element) {
    if (element.tagName == 'HTML')
        return '/HTML[1]';
    if (element===document.body)
        return '/HTML[1]/BODY[1]';

    var ix= 0;
    var siblings= element.parentNode.childNodes;
    for (var i= 0; i<siblings.length; i++) {
        var sibling= siblings[i];
        if (sibling===element)
            return getPathTo(element.parentNode)+'/'+element.tagName+'['+(ix+1)+']';
        if (sibling.nodeType===1 && sibling.tagName===element.tagName)
            ix++;
    }
}

function alertXPath() {
	alert(getPathTo(this));

	//Remove highlight and event listener from clicked element
	this.classList.remove(HIGHLIGHT_CLASSNAME);
	this.removeEventListener('click', alertXPath);
	document.body.onmouseover = undefined;
}

function highlightDOMElements() {
	var prev;

	document.body.onmouseover = handler;
	
	function handler(event) {
		if (event.target === document.body || (prev && prev === event.target)) {
			return;
		}
    
		if (prev) {
			prev.removeEventListener('click', alertXPath);
			prev.classList.toggle(HIGHLIGHT_CLASSNAME);
			prev = undefined;
		}

		if (event.target) {
	
		prev = event.target;
		prev.classList.toggle(HIGHLIGHT_CLASSNAME);
		document.addEventListener("click",handler,true);
		function handler(e){
			e.preventDefault();
		}
		prev.addEventListener('click', alertXPath);
		}
	}  
}
