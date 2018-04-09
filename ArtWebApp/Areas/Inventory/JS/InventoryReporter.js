    $(document).ready(function () {




        $("#GetGtockreportOnhanld").click(function (e, params) {

            var location_pk = 0;
            $('#loadingmessage').show();
            $.ajax({
                url: "@Url.Action("GetGstockData", "RollTracker")",
                traditional: true,
                type: "GET",
                data: { 'location_pk': location_pk },
                success: function (fooBarHTML) {
                    $('#loadingmessage').hide();

                    $("#output").html(fooBarHTML);
                },
                error: function (xhr, status, errorThrown) {
                    $('#loadingmessage').hide();
                }
            });

        });
    $("#ShowMonth").click(function (e, params) {
            var Month = $('#Month').find('option:selected').val();
                      $('#loadingmessage').show();
                $.ajax({
        url: "@Url.Action("GetRequestDetailView", "ProductionReport")",
                    traditional: true,
                    type: "GET",
                    data: {'Month': Month },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    document.title = "BE report  for " + Month;
                    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });

                    });


        $("#CostingAll").click(function (e, params) {
            var Id = 0;
            $('#loadingmessage').show();
                $.ajax({
        url: "@Url.Action("GetCostingData", "ProductionReport")",
                    traditional: true,
                    type: "GET",
                    data: {'Id': Id },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });

                    });
           $("#Costing").click(function (e, params) {
        $('#loadingmessage').show();

    var Id = $('#AtcID').find('option:selected').val();
                $.ajax({
        url: "@Url.Action("GetCostingData", "ProductionReport")",
                    traditional: true,
                    type: "GET",
                    data: {'Id': Id },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });

        });
           $("#RollAllocation").click(function (e, params) {
        $('#loadingmessage').show();

    var Id = $('#AtcID').find('option:selected').val();
                $.ajax({
        url: "@Url.Action("GetRollYardLocation", "RollTracker")",
                    traditional: true,
                    type: "GET",
                    data: {'AtcID': Id },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });

        });









        $("#PoofSupplier").click(function (e, params) {
        $('#loadingmessage').show();
    $.ajax({
        url: "@Url.Action("GetPurchasesfromSupplier", "ProductionReport")",
                    traditional: true,
                    type: "GET",
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
                    });


        $("#ShipmentReport").click(function (e, params) {
        $('#loadingmessage').show();
    var Id = $('#AtcID').find('option:selected').val();
                $.ajax({
        url: "@Url.Action("GetShipmentofAtc", "ProductionReport")",
                    traditional: true,
                    type: "GET",
                    data: {'Id': Id },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
                    });

            $("#ShipmentReportAll").click(function (e, params) {
        $('#loadingmessage').show();
    var Id =0;
                $.ajax({
        url: "@Url.Action("GetShipmentofAtc", "ProductionReport")",
                    traditional: true,
                    type: "GET",
                    data: {'Id': Id },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
                    });



            $("#RejectionrequestAll").click(function (e, params) {
        $('#loadingmessage').show();
    var Id =0;
                $.ajax({
        url: "@Url.Action("GetRejectionRequest", "ProductionReport")",
                    traditional: true,
                    type: "GET",
                    data: {'Id': Id },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
                    });



 $("#InventoryOrderWF").click(function (e, params) {
        $('#loadingmessage').show();
    var fromdate = $("#locFromDate").val();
            var todate = $("#locTodate").val();
            var dotype = "WF";
            var Id = $('#Location_pk').find('option:selected').val();
                $.ajax({
        url: "@Url.Action("GetInventoryBetweenDate", "RollTracker")",
                    traditional: true,
                    type: "GET",
                    data: {'fromdate': fromdate, 'todate': todate, 'Id': Id},
                    success: function (fooBarHTML) {
        alert("Hi");
    $('#loadingmessage').hide();
                    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
                    });

         $("#RoReport").click(function (e, params) {
            var Id = 0;
            $('#loadingmessage').show();
                $.ajax({
        url: "@Url.Action("GetRoReport", "RollTracker")",
                    traditional: true,
                    type: "GET",
                  //  data: {'Id': Id },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });

                    });


         $("#StockRoReport").click(function (e, params) {
            var Id = 0;
            $('#loadingmessage').show();
                $.ajax({
        url: "@Url.Action("GetStockRoReport", "RollTracker")",
                    traditional: true,
                    type: "GET",
                  //  data: {'Id': Id },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });

                    });







            $("#DeliveryOrderWF").click(function (e, params) {
        $('#loadingmessage').show();
    var fromdate = $("#FromDate").val();
            var todate = $("#Todate").val();
            var dotype = "WF";

                $.ajax({
        url: "@Url.Action("GetDoBetweenDate", "RollTracker")",
                    traditional: true,
                    type: "GET",
                    data: {'fromdate': fromdate, 'todate': todate, 'dotype': dotype},
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
                    });


            $("#SKUTrack").click(function (e, params) {
        $('#loadingmessage').show();
    var id = $("#srch-term").val();
            var supplierdock_pk = 0;
            var Skudetpk = id;
            var RollPk = 0;
            var cutplanPk = 0;
            var docnum = 'NA';
                $.ajax({
        url: "@Url.Action("GetRollTracker", "RollTracker")",
                    traditional: true,
                    type: "GET",
                    data: {'supplierdock_pk': supplierdock_pk, 'Skudetpk': Skudetpk, 'RollPk': RollPk, 'cutplanPk': cutplanPk, 'Docnum': docnum },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
                    });


            $("#ASNTrack").click(function (e, params) {
        $('#loadingmessage').show();
    var id = $("#srch-term").val();
            var supplierdock_pk = id;
            var Skudetpk = 0;
            var RollPk = 0;
            var cutplanPk = 0;
            var docnum = 'NA';

                $.ajax({
        url: "@Url.Action("GetRollTracker", "RollTracker")",
                    traditional: true,
                    type: "GET",
                    data: {'supplierdock_pk': supplierdock_pk, 'Skudetpk': Skudetpk, 'RollPk': RollPk, 'cutplanPk': cutplanPk, 'Docnum': docnum },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
                    });


            $("#RollPKTrack").click(function (e, params) {
        $('#loadingmessage').show();
    var id = $("#srch-term").val();
            var supplierdock_pk = 0;
            var Skudetpk = 0;
            var RollPk = id;
            var docnum = 'NA';
            var cutplanPk = 0;
                $.ajax({
        url: "@Url.Action("GetRollTracker", "RollTracker")",
                    traditional: true,
                    type: "GET",
                    data: {'supplierdock_pk': supplierdock_pk, 'Skudetpk': Skudetpk, 'RollPk': RollPk, 'cutplanPk': cutplanPk, 'Docnum': docnum },
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
            });


            $("#CutplanPKTrack").click(function (e, params) {
        $('#loadingmessage').show();
    var id = $("#srch-term").val();
            var supplierdock_pk = 0;
            var Skudetpk = 0;
            var RollPk = 0;
            var docnum = 'NA';
            var cutplanPk = id;
                $.ajax({
        url: "@Url.Action("GetRollTracker", "RollTracker")",
                    traditional: true,
                    type: "GET",
                    data: {'supplierdock_pk': supplierdock_pk, 'Skudetpk': Skudetpk, 'RollPk': RollPk, 'cutplanPk': cutplanPk, 'Docnum': docnum},
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
            });
            $("#Doctrack").click(function (e, params) {
        $('#loadingmessage').show();
    var id = $("#srch-term").val();
            var supplierdock_pk = 0;
            var Skudetpk = 0;
            var RollPk = 0;
            var docnum = id;
            var cutplanPk = 0;
                $.ajax({
        url: "@Url.Action("GetRollTracker", "RollTracker")",
                    traditional: true,
                    type: "GET",
                    data: {'supplierdock_pk': supplierdock_pk, 'Skudetpk': Skudetpk, 'RollPk': RollPk, 'cutplanPk': cutplanPk, 'Docnum': docnum},
                    success: function (fooBarHTML) {
        $('#loadingmessage').hide();
    $("#output").html(fooBarHTML);
                    },
                    error: function (xhr, status, errorThrown) {
        $('#loadingmessage').hide();
    }
                    });
            });


    });





