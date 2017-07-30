<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MerchandisingDashBoard.aspx.cs" Inherits="ArtWebApp.Merchandiser.MerchandisingDashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">




    
  



    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.15/css/jquery.dataTables.css">
  
<script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.15/js/jquery.dataTables.js"></script>

  <script >
     
    $(document).ready(function () {
        alert('HI');
      


        // Setup - add a text input to each header cell
        $('.mydatagrid thead tr:eq(1) th').each(function () {
            var title = $('#example thead tr:eq(0) th').eq($(this).index()).text();
            $(this).html('<input type="text" placeholder="Search ' + title + '" />');
        });

        var table = $('.mydatagrid ').DataTable({
            orderCellsTop: true
        });

        // Apply the search
        table.columns().every(function (index) {
            $('.mydatagrid  thead tr:eq(1) th:eq(' + index + ') input').on('keyup change', function () {
                table.column($(this).parent().index() + ':visible')
                    .search(this.value)
                    .draw();
            });
        });


    });
       


     </script>
    <div>
      
          </div>
       
 

      <div>
        </div>




    
    <div class="RedHeaddingdIV"> PENDING&nbsp; PO</div>
    <div id="MasterDiv"  runat="server">
    
    </div>
    <div class="RedHeaddingdIV"> PENDING COSTING FOR APPROVAL</div>
     <div id="MasterDiv2" runat="server">
       
    </div>
     <div class="RedHeaddingdIV"> PENDING RO FOR APPROVAL</div>
     <div id="MasterDiv3" runat="server">
         
    </div>

       <div class="RedHeaddingdIV"> PENDING SRO FOR APPROVAL</div>
     <div id="MasterDiv4" runat="server">
   
    </div>
     <div class="RedHeaddingdIV">PENDING EBOM FOR APPROVAL</div>
    <div id="MasterDiv5" runat="server">
   
    </div>

    
 
 
 
    </asp:Content>



    
    
 
 






