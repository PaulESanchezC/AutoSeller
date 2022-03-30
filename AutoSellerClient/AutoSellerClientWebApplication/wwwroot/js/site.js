function onEnter(element, event) {
    if (event.key === "Enter") {
        $(element).click();
    }
}

function sweetAlert(type, title, message) {
    Swal.fire({
        icon: type,
        title: title,
        text: message,
        confirmButtonText: "Continue!",
        buttonsStyling: false,
        customClass: {
            confirmButton: 'swal-confirm-button'
        },
        width: "480px"
    });
}

function loginSwal(type,message) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    });

    Toast.fire({
        icon: type,
        title: message
    });
}

function contactSeller(element) {
    const listedVehicleId = $(element).data("target-vehicle");
    const intentReceiverId = $(element).data("target-user");
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'swal-confirm-button',
            cancelButton: 'swal-cancel-button'
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "Your account information will be sent to lister user, you won't be able to revert this",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, do it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/User/SendIntent",
                type: "POST",
                data: { listedVehicleId: listedVehicleId, intentReceiverId: intentReceiverId },
                success: function (data) {
                    if (data.IsSuccessful) {
                        swalWithBootstrapButtons.fire(
                            'Done!',
                            'Your interest in this vehicle has been sent, thank you for using our services.',
                            'success'
                        );
                    } else {
                        swalWithBootstrapButtons.fire(
                            data.Title,
                            data.Message,
                            'error'
                        );
                    }
                }
            });
        } else if (
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
