function enter(obj) {


    var tr = obj.parentNode.parentNode;
    if (event.keyCode == 40) //Down 
    {
        if (tr.rowIndex < tr.parentNode.rows.length - 1)
            tr.parentNode.rows[tr.rowIndex + 1].cells[obj.parentNode.cellIndex].children[0].focus();
        return;
    }
    if (event.keyCode == 37) //Left 
    {
        if (obj.parentNode.cellIndex > 0)
            tr.parentNode.rows[tr.rowIndex].cells[obj.parentNode.cellIndex - 1].children[0].focus();
        return;

    }
    if (event.keyCode == 39) //Right 
    {
        if (obj.parentNode.cellIndex < tr.cells.length - 1)
            tr.parentNode.rows[tr.rowIndex].cells[obj.parentNode.cellIndex + 1].children[0].focus();
        return;

    }
    if (event.keyCode == 38) //Up 
    {
        if (tr.rowIndex > 1)
            tr.parentNode.rows[tr.rowIndex - 1].cells[obj.parentNode.cellIndex].children[0].focus();
        return;

    }
}



function isNumberKey(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
    {
       
        return false;


    }
    else
    {
        return true;
    }

   
}


function Check_Click(objRef) {

    //Get the Row based on checkbox

    var row = objRef.parentNode.parentNode;

    if (objRef.checked) {

        //If checked change color to Aqua

        row.style.backgroundColor = "aqua";

    }

    else {

        //If not checked change back to original color

        if (row.rowIndex % 2 == 0) {

            //Alternating Row Color

            row.style.backgroundColor = "#C2D69B";

        }

        else {

            row.style.backgroundColor = "white";

        }

    }



    //Get the reference of GridView

    var GridView = row.parentNode;



    //Get all input elements in Gridview

    var inputList = GridView.getElementsByTagName("input");



    for (var i = 0; i < inputList.length; i++) {

        //The First element is the Header Checkbox

        var headerCheckBox = inputList[0];



        //Based on all or none checkboxes

        //are checked check/uncheck Header Checkbox

        var checked = true;

        if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

            if (!inputList[i].checked) {

                checked = false;

                break;

            }

        }

    }

    headerCheckBox.checked = checked;



}



function checkAll(objRef) {

    var GridView = objRef.parentNode.parentNode.parentNode;

    var inputList = GridView.getElementsByTagName("input");

    for (var i = 0; i < inputList.length; i++) {

        //Get the Cell To find out ColumnIndex

        var row = inputList[i].parentNode.parentNode;

        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

            if (objRef.checked) {

                //If the header checkbox is checked

                //check all checkboxes

                //and highlight all rows

                row.style.backgroundColor = "aqua";

                inputList[i].checked = true;

            }

            else {

                //If the header checkbox is checked

                //uncheck all checkboxes

                //and change rowcolor back to original

                if (row.rowIndex % 2 == 0) {

                    //Alternating Row Color

                    row.style.backgroundColor = "#C2D69B";

                }

                else {

                    row.style.backgroundColor = "white";

                }

                inputList[i].checked = false;

            }

        }

    }

}



