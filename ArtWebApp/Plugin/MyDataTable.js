$(document).ready(function () {


    // Setup - add a text input to each footer cell
    $('.MyDataTable thead tr.filterrow th').each(function () {
        var title = $('.MyDataTable thead th').eq($(this).index()).text().trim();
        $(this).html('<input type="text" onclick="stopPropagation(event);" placeholder="Search ' + title + '" />');
    });
    
    // DataTable
    var table = $('.MyDataTable').DataTable({
        orderCellsTop: true,
        dom: 'Blfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print', 'colvis'
        ],
        
        
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

