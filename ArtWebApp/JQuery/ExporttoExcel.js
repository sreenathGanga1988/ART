$(document).ready(function () {
    $(".mydatagrid").dblclick(function () {

     

        var retVal = confirm("Do you want to Export  the Table you clicked to Excel ?");
        if (retVal == true) {
            $(this).table2excel({
                exclude: ".noExl",
                name: "Excel Document Name",
                filename: "WebArtExport" + new Date().toISOString().replace(/[\-\:\.]/g, ""),
                fileext: ".xls",
                exclude_img: true,
                exclude_links: true,
                exclude_inputs: true
            });

        }
        else {

        }




    });

   



    








});
