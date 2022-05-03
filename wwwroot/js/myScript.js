
var divs = document.querySelectorAll("div");
if (divs != null) {
    for (var i = 0; i < divs.length; i++) {
        if (divs[i].className == "container") {
            divs[i].className = "container-fluid";
        }
    }
}