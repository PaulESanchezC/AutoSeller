$(document).ready(function () {
    var currentPage = $("#PageMenu").data("page");
    var pageCount = $("#PageMenu").data("total-pages");
    var searchVm = buildSearchVm();

    // Next Page
    $("#nextPage").click(function () {
        searchVm["CurrentPage"] = validateNextPage();
        $.ajax({
            url: "Home/Search",
            type: "GET",
            data: {
                Maker: searchVm["Maker"],
                MaxPrice: searchVm["MaxPrice"],
                MaxYear: searchVm["MaxYear"],
                Mileage: searchVm["Mileage"],
                Color: searchVm["Color"],
                Transmission: searchVm["Transmission"],
                DriveTrain: searchVm["DriveTrain"],
                PageSize: searchVm["PageSize"],
                CurrentPage: searchVm["CurrentPage"]
            },
            beforeSend: function () {
                $("#loadingVehicleSearchResultGif").show();
            },
            success: function (data, result) {
                if (result) {
                    $("#indexPartialView").html(data);
                }
            }
        }).done(function () {
            $("#loadingVehicleSearchResultGif").hide();
        });
    });

    // Previous Page
    $("#previousPage").click(function () {
        searchVm["CurrentPage"] = validatePreviousPage();
        $.ajax({
            url: "Home/Search",
            type: "GET",
            data: {
                Maker: searchVm["Maker"],
                MaxPrice: searchVm["MaxPrice"],
                MaxYear: searchVm["MaxYear"],
                Mileage: searchVm["Mileage"],
                Color: searchVm["Color"],
                Transmission: searchVm["Transmission"],
                DriveTrain: searchVm["DriveTrain"],
                PageSize: searchVm["PageSize"],
                CurrentPage: searchVm["CurrentPage"]
            },
            beforeSend: function () {
                $("#loadingVehicleSearchResultGif").show();
            },
            success: function (data, result) {
                if (result) {
                    $("#indexPartialView").html(data);
                }
            }
        }).done(function () {
            $("#loadingVehicleSearchResultGif").hide();
        });
    });

    // Resize Page Size
    $("#pageSize").change(function () {
        searchVm = buildSearchVm();
        $.ajax({
            url: "Home/Search",
            type: "GET",
            data: {
                MakerName: searchVm["Maker"],
                MaxPrice: searchVm["MaxPrice"],
                MaxYear: searchVm["MaxYear"],
                Mileage: searchVm["Mileage"],
                Color: searchVm["Color"],
                Transmission: searchVm["Transmission"],
                DriveTrain: searchVm["DriveTrain"],
                PageSize: searchVm["PageSize"],
                CurrentPage: searchVm["CurrentPage"]
            },
            beforeSend: function () {
                $("#loadingVehicleSearchResultGif").show();
            },
            success: function (data, result) {
                if (result) {
                    $("#indexPartialView").html(data);
                    console.log(data);
                    pageCount = $("#PageMenu").data("total-pages");
                }
            }
        }).done(function () {
            $("#loadingVehicleSearchResultGif").hide();
        });
    });

    // Search
    $("#search").click(function () {
        searchVm = buildSearchVm();
        $.ajax({
            url: "Home/Search",
            type: "GET",
            data: {
                MakerName: searchVm["Maker"],
                MaxPrice: searchVm["MaxPrice"],
                MaxYear: searchVm["MaxYear"],
                Mileage: searchVm["Mileage"],
                Color: searchVm["Color"],
                Transmission: searchVm["Transmission"],
                DriveTrain: searchVm["DriveTrain"],
                PageSize: searchVm["PageSize"],
                CurrentPage: searchVm["CurrentPage"]
            },
            beforeSend: function () {
                $("#loadinggif").fadeIn(500);
                $("#loadingiftext").text("Searching");
                $("#loadingiftext").css("color", "rgba(250,127,8,0.8)");
                $("#loadingVehicleSearchResultGif").show();
            },
            success: function (data, result) {
                if (result) {
                    $("#indexPartialView").html(data);
                    pageCount = $("#PageMenu").data("total-pages");
                }
            }
        }).done(function () {
            $("#loadinggif").fadeOut(500);
            $("#loadingiftext").text("Search");
            $("#loadingiftext").css("color", "#03A678");
            $("#loadingVehicleSearchResultGif").hide();
        });
    });

    function validateNextPage() {
        currentPage++
        if (currentPage > pageCount) {
            currentPage--;
            return currentPage;
        }
        return currentPage;
    }

    function validatePreviousPage() {
        currentPage--;
        if (currentPage <= 1) {
            currentPage = 1;
            return currentPage;
        }
        return currentPage;
    }

    function buildSearchVm() {
        var maker = $("#maker option:selected").val();
        var maxPrice = $("#maxPrice").val();
        var maxYear = $("#maxYear").val();
        var mileage = $("#mileage").val();
        var color = $("#color option:selected").val();
        var transmission = $("#transmission option:selected").val();
        var driveTrain = $("#driveTrain option:selected").val();
        var pageSize = $("#pageSize").val();
        var currentPage = $("#PageMenu").data("page");
        return {
            Maker: maker,
            MaxPrice: maxPrice,
            MaxYear: maxYear,
            Mileage: mileage,
            Color: color,
            Transmission: transmission,
            DriveTrain: driveTrain,
            PageSize: pageSize,
            CurrentPage: currentPage
        }
    }
});