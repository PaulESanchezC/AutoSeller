const menuIconEl = $('.menu-icon');
const sidenavEl = $('.sidenav');
const sidenavCloseEl = $('.sidenav__close-icon');

// Add and remove provided class names
function toggleClassName(el, className) {
    if (el.hasClass(className)) {
        el.removeClass(className);
    } else {
        el.addClass(className);
    }
}

// Open the side nav on click
menuIconEl.on('click', function () {
    toggleClassName(sidenavEl, 'active');
});

// Close the side nav on click
sidenavCloseEl.on('click', function () {
    toggleClassName(sidenavEl, 'active');
});

function ActiveSideNavElement(pathname) {
    $("#quickLoginLink").slideUp("400");
    if (pathname === "/") {
        $("#Index").css("background-color", "#11a8ab");
    }
    if (pathname === "/Login/Login") {
        $("#Login").css("background-color", "#11a8ab");
        $("#quickLoginLink").slideDown("400");
    }
    if (pathname === "/Register/Register") {
        $("#Register").css("background-color", "#11a8ab");
    }
    if (pathname === "/Home/About") {
        $("#About").css("background-color", "#11a8ab");
    }
    if (pathname === "/User/Dashboard") {
        $("#Dashboard").css("background-color", "#11a8ab");
    }
    if (pathname === "/User/Profile") {
        $("#Profile").css("background-color", "#11a8ab");
    }
    if (pathname === "/User/RegisterAVehicle") {
        $("#RegisterVehicle").css("background-color", "#11a8ab");
    }
}

$(document).ready(function() {
    var element = $(location).attr("pathname");
    //console.log("sidenav path: " + element);
    ActiveSideNavElement(element);
});

function QuickLogin() {
    $("#quickLoginLink").slideDown("400");
}
