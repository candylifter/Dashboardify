function getXPath(element) {
    if (element.tagName == 'HTML')
        return '/HTML[1]';
    if (element===document.body)
        return '/HTML[1]/BODY[1]';

    var ix= 0;
    var siblings= element.parentNode.childNodes;
    for (var i= 0; i<siblings.length; i++) {
        var sibling= siblings[i];
        if (sibling===element)
            return (getXPath(element.parentNode)+'/'+element.tagName+'['+(ix+1)+']').toLowerCase();
        if (sibling.nodeType===1 && sibling.tagName===element.tagName)
            ix++;
    }
}

export default getXPath
