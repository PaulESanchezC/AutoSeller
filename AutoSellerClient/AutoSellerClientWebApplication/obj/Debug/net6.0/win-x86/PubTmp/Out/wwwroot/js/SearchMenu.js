$(document).ready(function () {
    //Initial Search Menu Toggle
    $("#searchmenu").slideToggle(650,
        function () {
            toggleBorderAndBUtton($("#searchmenu").css("display"));
        });

    // Search Menu Toggle
    $("#searchmenutoggler").click(function () {
        $("#searchmenu").slideToggle(600, function () {
            toggleBorderAndBUtton($("#searchmenu").css("display"));
        });
    });

    function toggleBorderAndBUtton(display) {
        if (display === "none") {
            $("#searchbuttonup").css("display", "none");
            $("#searchbuttondown").css("display", "inline");
        } else {
            $("#searchbuttonup").css("display", "inline");
            $("#searchbuttondown").css("display", "none");
        }
    }
})