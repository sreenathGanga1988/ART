var AtcList = []
var ItemList=[]
LoadAtc(('#SPOID'));

function LoadAtc(element) {

    if (AtcList.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            dataType:"json",
            url: "/ArtMVCMerchandiser/StockPOFreightCharge/GetPOList",
            success: function (data) {
                AtcList = data;
                
                renderAtc(element);
            }
            
        })
    }
    else {
        //render catagory to the element
        renderCategory(element);
    }
}
//fetch alloweddata
function loadItem(AtcDD) {


    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/ArtMVCMerchandiser/StockPOFreightCharge/GetItemDescription",
        data: { 'id': $(AtcDD).val() },

        success: function (data) {
            //render products to appropriate dropdown
            alert($(AtcDD).val());
            ItemList = data;
            renderItem()

        },
        error: function (error) {
            console.log(error);
        }
    })
}


function renderAtc(element) {
    var $ele = $(element);

    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));

    $.each(AtcList, function (i, val) {
        //alert('Hi');
       
        $ele.append($('<option value=' +
            this.Value + '>' + this.Text + '</option>'));
       


        //  
    })

  
}
function renderItem() {
    var $ele = $('#ItemID');

    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));

    $.each(ItemList, function (i, val) {
        //alert('Hi');

        $ele.append($('<option value=' +
            this.Value + '>' + this.Text + '</option>'));



        //  
    })


}

















$(document).ready(function () {

    $('#SPOID').change(function () {
      
        loadItem(this);
    });


    //Add button click event
    $('#add').click(function () {
        //validation and add order items
        var isAllValid = true;
        if ($('#SPOID').val() == "0") {
            isAllValid = false;
            $('#SPOID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#SPOID').siblings('span.error').css('visibility', 'hidden');
        }


        if (!($('#ItemID').val().trim() != '' && (parseInt($('#ItemID').val()) || 0))) {
            isAllValid = false;
            $('#ItemID').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#ItemID').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#NewValue').val().trim() != '' && !isNaN($('#NewValue').val().trim()))) {
            isAllValid = false;
            $('#NewValue').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#NewValue').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.SPOID', $newRow).val($('#SPOID').val());
            $('.ItemID', $newRow).val($('#ItemID').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#SPOID,#ItemID,#NewValue,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            $('#SPOID').val('0');
            $('#ItemID,').val('0');
            $('#NewValue').val('');
            $('#orderItemError').empty();
        }

    })

    //remove button click event
    $('#orderdetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tbody tr').each(function (index, ele) {
           
           
            if (
                $('select.SPOID', this).val() == "0" ||
                $('select.ItemID', this).val() == "0"  ||
                $('.NewValue', this).val() == "" ||
                isNaN($('.NewValue', this).val())
            ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var orderItem = {
                    SpoPK: $('select.SPOID', this).val(),
                    SPODetails_PK: $('select.ItemID', this).val(),
                    FreightCharge: parseFloat($('.NewValue', this).val())
                }
                list.push(orderItem);
            }
        })

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in Atc item list.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('At least 1 Atc item required.');
            isAllValid = false;
        }

        if ($('#FromParty').val().trim() == '') {
            $('#FromParty').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#FromParty').siblings('span.error').css('visibility', 'hidden');
        }
        
        if ($('#ToParty').val().trim() == '') {
            $('#ToParty').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#ToParty').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Shipper').val().trim() == '') {
            $('#Shipper').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#Shipper').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Weight').val().trim() == '') {
            $('#Weight').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#Weight').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#DebitTo').val().trim() == '') {
            $('#DebitTo').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#DebitTo').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Reason').val().trim() == '') {
            $('#Reason').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#Reason').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#Merchandiser').val().trim() == '') {
            $('#Merchandiser').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#Merchandiser').siblings('span.error').css('visibility', 'hidden');
        }
        if ($('#ApproximateCharges').val().trim() == '') {
            $('#ApproximateCharges').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#ApproximateCharges').siblings('span.error').css('visibility', 'hidden');
        }

        




        if (isAllValid) {
            var data = {
               
                stockFreightChargeDetails: list,


                FromParty: $('#FromParty').val(),
                ToParty: $('#ToParty').val(),
                Shipper: $('#Shipper').val(),
                Weight: $('#Weight').val(),
                ContentofPackage: $('#ContentofPackage').val(),
                DebitTo: $('#DebitTo').val(),
                Reason: $('#Reason').val(),
                Merchandiser: $('#Merchandiser').val(),
                ForwarderDetails: $('#ForwarderDetails').val(),
                ApproximateCharges: $('#ApproximateCharges').val(),
                Remark: $('#Remark').val().trim(),
       
      




            }

            $(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/ArtMVCMerchandiser/StockPOFreightCharge/Create',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert('Successfully saved ' + data.Reqnum);
                        //here we will clear the form
                        
                        list = [];
                        $('#orderNo,#orderDate,#description').val('');
                        $('#orderdetailsItems').empty();
                    }
                    else {
                        alert('Error');
                    }
                    $('#submit').val('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').val('Save');
                }
            });
        }

    });

});


