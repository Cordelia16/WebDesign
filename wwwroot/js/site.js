// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function displayInfo(id) {
    if (document.getElementById(id) != null)
        document.getElementById(id).style.visibility = "visible";
}

function displayImage(url) {
    if (document.getElementById("imageDisplay") != null)
        document.getElementById("imageDisplay").innerHTML = "<img src='" + url + "'  class='img-responsive'/>"
}

function removeInfo(id) {
    if (document.getElementById(id) != null)
        document.getElementById(id).style.visibility = "hidden";
}