var AtcList = []

LoadAtc($('#AtcID'));
function LoadAtc(element) {

    if (AtcList.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/ArtMVC/api/GetAtc',
            success: function (data) {
                AtcList = data;
                //render catagory
                renderCategory(element);
            }
        })
    }
    else {
        //render catagory to the element
        renderCategory(element);
    }
}

function renderAtc(element) {
    var $ele = $('#AtcID');
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));

    $.each(Categories, function (i, val) {
        //alert('Hi');
        $ele.append($('<option/>').val(val.chargeId).text(val.chargeName))
        //  $ele.append($('<option/>').val(val.chargeId).text(val.chargeName));
    })
}