$(document).ready(function () {

    

    // Setup - add a text input to each footer cell
    $('.MyDataTable thead tr.filterrow th').each(function () {
        var title = $('.MyDataTable thead th').eq($(this).index()).text().trim();
        $(this).html('<input type="text" onclick="stopPropagation(event);" placeholder="Search ' + title + '" />');
    });
    
    // DataTable
    var table = $('.MyDataTable').DataTable({
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        orderCellsTop: true,
        dom: 'Blfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis', { extend: 'excelHtml5', footer: true },
        ],
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            var api1 = this.api(), data;

           




            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;


            };

            //// Total over all pages
            //total = api
            //    .column(4)
            //    .data()
            //    .reduce(function (a, b) {
            //        return intVal(a) + intVal(b);
            //    }, 0);



            
            var columnData = api
                .column(4, { page: 'current' })
                .data();
           
         
            //CCM
            // Total over this page
            totalccm = api
                .column(4, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
                      
            avgccm = totalccm / columnData.count();           

            // Update footer
            $(api.column(4).footer()).html(avgccm.toFixed(2));




            //JCM
            // Total over this page
            totaljcm = api
                .column(3, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgjcm = totaljcm / columnData.count();

            // Update footer
            $(api.column(3).footer()).html(avgjcm.toFixed(2));




            //Cost per Minute
            // Total over this page
            totalcostperminute = api
                .column(5, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgcostperminute = totalcostperminute / columnData.count();

            // Update footer
            $(api.column(5).footer()).html(avgcostperminute.toFixed(2));

           

            //TTL
            // Total over this page
            totalttl = api
                .column(6, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgtotalttl = totalttl / columnData.count();

            // Update footer
            $(api.column(6).footer()).html(avgtotalttl.toFixed(2));



            //BEPErcentJCm
            // Total over this page
            totalBEPErcentJCm = api
                .column(7, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgBEPErcentJCm = totalBEPErcentJCm / columnData.count();

            // Update footer
            $(api.column(7).footer()).html(avgBEPErcentJCm.toFixed(2));




            //BEPErcentCCm
            // Total over this page
            totalBEPErcentCCm = api
                .column(8, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgBEPErcentCCm = totalBEPErcentCCm / columnData.count();

            // Update footer
            $(api.column(8).footer()).html(avgBEPErcentCCm.toFixed(2));



            //Others
            // Total over this page
            totalOthers = api
                .column(9, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgOthers = totalOthers / columnData.count();

            // Update footer
            $(api.column(9).footer()).html(avgOthers.toFixed(2));



            //FOB
            // Total over this page
            totalotherPercent = api
                .column(10, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgotherpercent = totalotherPercent / columnData.count();

            // Update footer
            $(api.column(10).footer()).html(avgotherpercent.toFixed(2));



            //marginvalue
            // Total over this page
            totalmarginvalue = api
                .column(11, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgmarginvalue = totalmarginvalue / columnData.count();

            // Update footer
            $(api.column(11).footer()).html(avgmarginvalue.toFixed(2));

            //marginpercent
            // Total over this page
            totalmarginpercent = api
                .column(12, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgmarginpercent = totalmarginpercent / columnData.count();

            // Update footer
            $(api.column(12).footer()).html(avgmarginpercent.toFixed(2));

            //FOB
            // Total over this page
            totalFOB = api
                .column(13, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            avgFOB = totalFOB / columnData.count();

            // Update footer
            $(api.column(13).footer()).html(avgFOB.toFixed(2));


           
        }

        
    });
   
    // Apply the filter
    $(".MyDataTable thead input").on('keyup change', function () {
        table
            .column($(this).parent().index() + ':visible')
            .search(this.value)
            .draw();
    });










    function stopPropagation(evt) {
        if (evt.stopPropagation !== undefined) {
            evt.stopPropagation();
        } else {
            evt.cancelBubble = true;
        }
    }





});

