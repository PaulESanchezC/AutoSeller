$(".dashboard-div").hide();
$("#listedVehicles").show();

$(".dashboard-navlinks").click(function () {
    $(".dashboard-navlinks").removeClass("selected");
    var target = $(this).attr("target");
    $(this).addClass("selected");

    $(".dashboard-div").hide();
    $("#" + target).fadeIn(300);

    if ($(window).width() <= 816) {
        $(".dashboard-nav").slideToggle(500);
    }
});

$(".dashboard-topmenu-dropdown-icon").click(function () {
    $(".dashboard-nav").slideToggle(500);
});

$(document).ready(function () {
    loadReceivedIntentsTable();
    loadSentIntentsTable();
});

function loadReceivedIntentsTable() {
    $("#receivedTableData").DataTable({
        responsive: {
            details: true,
        },
        "pageLength": 6,
        "ajax": "/User/GetReceivedIntents",
        "columns": [
            {
                "data": "IsRead", "orderable": false, "width": "40px", "render": function (data, type, row) {
                    if (!data) {
                        if ($("#receivedIntentsNotification").is(":visible")) {
                            //do nothing
                        } else
                            $("#receivedIntentsNotification").slideDown(400);
                        return `<button class="datatable-notification none" id="isRead${row.IntentId}" onclick="toggleIsRead('${row.IntentId}')"><i class="fa-solid fa-exclamation unread"></i></button>`;
                    } else {
                        return `<button class="datatable-notification none" id="isRead${row.IntentId}" onclick="toggleIsRead('${row.IntentId}')"><i class="fa-solid fa-exclamation unread" style="display:none;"></i></button>`;
                    }
                }
            },
            { "data": "ListedVehicle.Year", "width": "40px" },
            { "data": "ListedVehicle.Vehicle.Maker.MakerName", "width": "160px" },
            { "data": "ListedVehicle.Vehicle.VehicleName" },
            { "data": "ListedVehicle.Price", "width": "100px", "render": $.fn.dataTable.render.number(',', '.') },
            { "data": "IntentSender.LastName" },
            { "data": "IntentSender.FirstName" },
            { "data": "IntentSender.PhoneNumber" },
            { "data": "IntentSender.Email", "width": "auto" },
            {
                "data": "IntentId", "orderable": false, "render":
                    function (data, type, row) {
                        return `<button class="datatable-sell-button ${row.ListedVehicle.ListedVehicleId}" id="${data}" onclick="sellVehicle('${data}')" data-target="${row.ListedVehicle.ListedVehicleId}" ><i class="fa-solid fa-hand-holding-dollar fa-lg"></i></button>`;
                    }, "width": "60px"
            },
            {
                "data": "IntentId", "orderable": false, "render":
                    function (data) {
                        return `<button class="datatable-delete-button" onclick="discardIntent('${data}')"><i class="fa-solid fa-eraser fa-lg"></i></button>`;
                    }, "width": "60px"
            }
        ]
    });
}

function loadSentIntentsTable() {
    $("#sentTableData").DataTable({
        responsive: {
            details: true,
        },
        "pageLength": 6,
        "ajax": "/User/GetSentIntents",
        "columns": [
            { "data": "ListedVehicle.Year" },
            { "data": "ListedVehicle.Vehicle.Maker.MakerName" },
            { "data": "ListedVehicle.Vehicle.VehicleName" },
            { "data": "ListedVehicle.Price", "render": $.fn.dataTable.render.number(',', '.') },
            { "data": "ListedVehicle.Mileage", "render": $.fn.dataTable.render.number(',', '.') },
            { "data": "ListedVehicle.Transmission" },
            { "data": "ListedVehicle.DriveTrain" },
            { "data": "ListedVehicle.Color" },
            {
                "data": "IntentId", "orderable": false, "render":
                    function (data) {
                        return `<button class="datatable-button" onclick="discardIntent('${data}')" data-tooltip="Discard and delete"><i class="fa-solid fa-eraser fa-lg"></i></button>`;
                    }
            }

        ]
    });
}

function toggleIsRead(intentId) {
    console.log("isRead");
    $.ajax({
        url: "/User/ToggleIntentIsRead",
        type: "PUT",
        data: { intentId: intentId },
        success: function (data) {
            if (data.data) {
                $("#isRead" + intentId).children().fadeOut(400,
                    function () {
                        refreshNotifications();
                    });
            } else {
                $("#isRead" + intentId).children().fadeIn(400,
                    function () {
                        refreshNotifications();
                    });
            }
        }
    });
}

function refreshNotifications() {
    console.log("Refresh");
    if ($(".unread").is(":visible")) {
        if ($("#receivedIntentsNotification").is(":visible")) {
            return;
        }
        $("#receivedIntentsNotification").slideDown(400);
    } else {
        $("#receivedIntentsNotification").slideUp(400);
    }
}

function sellVehicle(intentId) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'swal-confirm-button',
            cancelButton: 'swal-cancel-button'
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "Selling the vehicle is not reversible, you cannot undo this.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, do it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/User/SetVehicleAsSold",
                type: "POST",
                data: { intentId: intentId },
                success: function (data) {
                    if (data != null) {
                        var targets = $("#" + intentId).data("target");
                        $("." + targets).parent().parent().fadeOut(2000,
                            function () {
                                $(this).remove();
                                $("#" + targets).remove();
                                refreshNotifications();
                            });
                        $("#soldListedVehiclesCollection").append(data);
                        swalWithBootstrapButtons.fire(
                            'Done!',
                            'Vehicle Is Sold',
                            'success'
                        );
                    } else {
                        swalWithBootstrapButtons.fire(
                            'Not Possible',
                            'An Error occurred',
                            'error'
                        );
                    }
                },
                error: function () {
                    swalWithBootstrapButtons.fire(
                        'Tutorial Accounts',
                        'Tutorial Accounts are protected from operations, the intentions is to keep the Portfolio in a healthy state. For more information, click on Tutorial on the side navigation menu. Thank you!',
                        'error'
                    );
                }
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Smart decisions, always merit peer respect. Thank you for choosing wisely!',
                'error'
            );
        }
    });
}

function discardIntent(intentId) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'swal-confirm-button',
            cancelButton: 'swal-cancel-button'
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "This will eliminate the buyers intent from your list, you won't be able to revert this",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, do it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/User/SetIntentAsDiscarded",
                type: "PUT",
                data: { intentId: intentId },
                success: function () {
                    $("#" + intentId).parent().parent().fadeOut(2000);
                    swalWithBootstrapButtons.fire(
                        'Done!',
                        'function worked',
                        'success'
                    );
                },
                error: function () {
                    swalWithBootstrapButtons.fire(
                        'Tutorial Accounts',
                        'Tutorial Accounts are protected from operations, the intentions is to keep the Portfolio in a healthy state. For more information, click on Tutorial on the side navigation menu. Thank you!',
                        'error'
                    );
                }
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Smart decisions, always merit peer respect. Thank you for choosing wisely!',
                'error'
            );
        }
    });
}

function deleteVehicle(element) {
    var listedVehicleId = $(element).data("target");
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'swal-confirm-button',
            cancelButton: 'swal-cancel-button'
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/User/DeleteListedVehicle",
                type: "delete",
                data: { listedVehicleId },
                success: function (data) {
                    if (data) {
                        $("#" + listedVehicleId).remove();
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
                            'Your Vehicle has been deleted.',
                            'success'
                        );
                    };
                },
                error: function () {
                    swalWithBootstrapButtons.fire(
                        'Tutorial Accounts',
                        'Tutorial Accounts are protected from operations, the intentions is to keep the Portfolio in a healthy state. For more information, click on Tutorial on the side navigation menu. Thank you!',
                        'error'
                    );
                }
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your Listed Vehicle file is safe, phew!',
                'error'
            );
        }
    });
}