$(function () {
    console.log("document ready");
    $(document).on("click", ".edit-petrolStation-button", function () {
        console.log("you clicked button number: " + $(this).val());

        // store the product id number
        var petrolStationID = $(this).val();

        $.ajax({
            type: 'json',
            data: {
                "id": petrolStationID
            },
            url: '/PetrolStation/ShowOnePetrolStationJSON',
            success: function (data) {
                console.log(data)

                // fill in the input fields into the modal
                $("#modal-input-id").val(data.id);
                $("#modal-input-name").val(data.name);
                $("#modal-input-address").val(data.address);
                $("#modal-input-price").val(data.price);
            }
        })

    });

    $("#save-button").click(function () {
        //get the value from the input fields and create a json object to submit to the controller.
        var PetrolStation = {
            "Id": $("#modal-input-id")
                .val(),
            "Name": $("#modal-input-name")
                .val(),
            "Address": $("#modal-input-address")
                .val(),
            "Price": $("#modal-input-price")
                .val()
        };
        console.log("saved...");
        console.log(PetrolStation);

        //save the updated product in the db using the controller.
        $.ajax({
            type: "json",
            data: PetrolStation,
            url: '/PetrolStation/ProcessEditReturnPartial',
            success: function (data) {
                console.log(data);
                $("#card-number-" + PetrolStation.Id).html(data).hide().fadeIn(2200);
            }

        })

    })
});