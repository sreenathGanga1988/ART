function validateQtyWithBalance(objText) {
    debugger;

    var cell = objText.parentNode;
    var row = cell.parentNode;

    var sum = 0;
    var newqtytextbox = row.getElementsByClassName("txtQty");
    var balQtylabel = row.getElementsByClassName("lblbal");


    if (parseFloat(newqtytextbox[0].value) > parseFloat(balQtylabel[0].innerText)) {

        newqtytextbox[0].value = 0;
        alert("Extra Qty Cannot be Allowed");
        newqtytextbox[0].focus();
    } else {

    }


}


function CheckBoxSelectionValidation(gridID) {
    debugger;
    var gridView = document.getElementById(gridID);

    for (var i = 1; i < gridView.rows.length; i++) {
        var count = 0;
        var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];

        var txtQty = gridView.rows[i].getElementsByClassName("txtQty")[0];
        var lblbal = gridView.rows[i].getElementsByClassName("lblbal")[0];
       
        if (chkConfirm.checked) {
            if (txtQty.value == "" || txtQty.value == "0") {
                gridView.rows[i].style.backgroundColor = "red";
                txtQty.focus();

                return false;
            }
            else if (lblbal.value == "0" || lblbal.value == "0")
            {
                gridView.rows[i].style.backgroundColor = "red";
                txtQty.focus();

                return false;
            }
            else if (parseFloat(txtQty.value) > parseFloat(lblbal.innerText)) {

                newqtytextbox[0].value = 0;
                alert("Extra Qty Cannot be Allowed");
                txtQty.focus();
            }
        }
    }


}