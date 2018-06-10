﻿$(document).ready(function () {
    var datatableinstance = $('#MyDataTable').DataTable({

        dom: 'Blrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: ':contains("Office")'
                }
            },
            'excelHtml5',
            'csvHtml5',
            'pdfHtml5'
        ],

        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            var api1 = this.api(), data;

            var api2 = this.api(), data;

            var api3 = this.api(), data;

            var api4 = this.api(), data;




            // Remove the formatting to get integer data for summation
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;


            };

            // Total over all pages
            total = api
                .column(4)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);


            total1 = api1
                .column(3)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            total2 = api2
                .column(5)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            total3 = api3
                .column(6)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            total4 = api4
                .column(7)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Total over this page
            pageTotal = api
                .column(4, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            pageTotal1 = api1
                .column(3, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            pageTotal2 = api2
                .column(5, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);


            pageTotal3 = api3
                .column(6, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            pageTotal4 = api4
                .column(7, { page: 'current' })
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer
            $(api.column(4).footer()).html(
                pageTotal + ' ( $' + total + ' total)'
            );


            $(api1.column(3).footer()).html(
                pageTotal1 + ' ( $' + total1 + ' total)'
            );

            $(api2.column(5).footer()).html(
                pageTotal2 + ' ( $' + total2 + ' total)'
            );


            $(api3.column(6).footer()).html(
                pageTotal3 + ' ( $' + total3 + ' total)'
            );


            $(api4.column(7).footer()).html(
                pageTotal4 + ' ( $' + total4 + ' total)'
            );
        }



    });



    $('#example thead tr td').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });


    $("#example thead input").on('keyup change', function () {
        datatableinstance
            .column($(this).parent().index() + ':visible')
            .search(this.value)
            .draw();
    });



});
       

