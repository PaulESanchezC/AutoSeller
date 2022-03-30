function deleteImage(element) {
    var imageId = $(element).data("target");
    $.ajax({
        url: "/User/DeleteImage",
        type: "DELETE",
        data: { imageId },
        success: function (data) {
            if (data) {
                $("#" + imageId).fadeOut(300, function () {
                    $("#" + imageId).remove();
                });
            }
        },
        error: function () {
            swal.fire(
                'Tutorial Accounts',
                'Tutorial Accounts are protected from operations, the intentions is to keep the Portfolio in a healthy state. For more information, click on Tutorial on the side navigation menu. Thank you!',
                'error'
            );
        }
    });
};

function imageMove(element, direction) {
    var imageId = $(element).data("target");
    var listedVehicleId = $(element).data("vehicle");
    var imageIndex = $(element).data("index");

    if (imageIndex + direction >= 0) {
        $.ajax({
            url: "/User/ImageIndexMove",
            type: "PUT",
            data: { imageId: imageId, listedVehicleId: listedVehicleId, direction: direction },
            success: function (data) {
                $(".vehicle-images-container").html(data);
            },
            error: function () {
                swal.fire(
                    'Tutorial Accounts',
                    'Tutorial Accounts are protected from operations, the intentions is to keep the Portfolio in a healthy state. For more information, click on Tutorial on the side navigation menu. Thank you!',
                    'error'
                );
            }
        });
    }
};