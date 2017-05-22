<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DisplayRoll.aspx.cs" Inherits="ArtWebApp.Production.CutOrder.DisplayRoll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


 
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
       <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.4/css/buttons.dataTables.min.css" />
       <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>




    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"> </script> 
 <script type="text/javascript" src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"> </script>
 <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/dataTables.buttons.min.js"> </script>
 <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.flash.min.js"> </script>
 <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"> </script>
 <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.24/build/pdfmake.min.js"> </script>
 <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.24/build/vfs_fonts.js"> </script>
 <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.html5.min.js"> </script>
 <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.print.min.js"> </script>


    
  

<script type="text/javascript" charset="utf-8">
     
    $(document).ready(function () {
        $('.Sree').DataTable();


        //$('.Sree').DataTable({
        //    "footerCallback": function (row, data, start, end, display) {
        //        var api = this.api(), data;



        //        var table = 

        //        // #myInput is a <input type="text"> element
        //        $('#myInput').on('keyup', function () {
        //            table.search(this.value).draw();
        //        });



        //        // Remove the formatting to get integer data for summation
        //        var intVal = function (i) {
        //            return typeof i === 'string' ?
        //                i.replace(/[\$,]/g, '') * 1 :
        //                typeof i === 'number' ?
        //                i : 0;
        //        };

        //        //// Total over all pages
        //        //total = api
        //        //    .column(4)
        //        //    .data()
        //        //    .reduce(function (a, b) {
        //        //        return intVal(a) + intVal(b);
        //        //    }, 0);

        //        //// Total over this page
        //        //pageTotal = api
        //        //    .column(4, { page: 'current' })
        //        //    .data()
        //        //    .reduce(function (a, b) {
        //        //        return intVal(a) + intVal(b);
        //        //    }, 0);

              
        //    }
        //});
    });
       
   

</script>

    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
  
    <div>
      
          </div>
       
 

      <div>
        </div>




    
    <div class="RedHeaddingdIV"> ALREADY DELIVERED TO SELECTED FACTORY</div>
    <div id="MasterDiv"  runat="server">
    
    </div>
    <div class="RedHeaddingdIV"> AVAILABLE ROLLS FOR DELIVERY</div>
     <div id="MasterDiv2" runat="server">
       
    </div>
     <div class="RedHeaddingdIV"> CUTPLAN MADE FOR THE SELECTED FANRIC/COLOR</div>
     <div id="MasterDiv3" runat="server">
         
    </div>

       <div class="RedHeaddingdIV"> PENDING SRO FOR APPROVAL</div>
     <div id="MasterDiv4" runat="server">
   
    </div>
     <div class="RedHeaddingdIV">PENDING EBOM FOR APPROVAL</div>
    <div id="MasterDiv5" runat="server">
   
    </div>

    
 
 
 
    </asp:Content>



