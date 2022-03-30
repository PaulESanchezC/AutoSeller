function makerSelectOption() {
    var makerIdSelected = $("#makerSelect option:selected").val();
    //console.log(makerIdSelected);
    $.ajax({
        url: "/User/GetVehicleModelsListByMakerId",
        type: "GET",
        data: { makerId: makerIdSelected },
        dataType: "json",
        success: function (response) {
            var modelsData = jQuery.parseJSON(response);

            $('#modelsSelect').find('option').remove().end();

            $('#modelsSelect').append(`<option selected="" disabled="" value="">--Please Select--</option>`);
            for (var i in modelsData) {
                $('#modelsSelect').append(`<option value="${modelsData[i].VehicleId}" target="${makerIdSelected}">${modelsData[i].VehicleName}</option>`);
            }
        },
    });
}
