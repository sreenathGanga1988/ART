<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MerchandisingDashBoard.aspx.cs" Inherits="ArtWebApp.Merchandiser.MerchandisingDashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">




    
  



    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.15/css/jquery.dataTables.css">

<script type="text/javascript" charset="utf8" src="//cdn.datatables.net/1.10.15/js/jquery.dataTables.js"></script>
        <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.min.js"></script>
  <script >
     
    $(document).ready(function () {
     
      
        $('.table').DataTable();

        //// Setup - add a text input to each header cell
        //$('.mydatagrid thead tr:eq(1) th').each(function () {
        //    var title = $('#example thead tr:eq(0) th').eq($(this).index()).text();
        //    $(this).html('<input type="text" placeholder="Search ' + title + '" />');
        //});

        //var table = $('.mydatagrid ').DataTable({
        //    orderCellsTop: true
        //});

        //// Apply the search
        //table.columns().every(function (index) {
        //    $('.mydatagrid  thead tr:eq(1) th:eq(' + index + ') input').on('keyup change', function () {
        //        table.column($(this).parent().index() + ':visible')
        //            .search(this.value)
        //            .draw();
        //    });
        //});


    });
       


     </script>
  
       
 
    <div class="container">
    
          <h2>Cutting DashBoard</h2>
       <button type="button" class="btn btn-info" data-toggle="collapse" data-target="<%=MasterDiv.ClientID%>">Pending PO</button>       
 
    <div id="MasterDiv"  class="collapse in"  runat="server">
    
    </div>


  <button type="button" class="btn btn-info" data-toggle="collapse" data-target="<%=MasterDiv2.ClientID%>"> PENDING COSTING FOR APPROVAL</button>
    
     <div id="MasterDiv2" class="collapse in" runat="server">
       
    </div>



  <button type="button" class="btn btn-info" data-toggle="collapse" data-target="<%=MasterDiv3.ClientID%>" >PENDING RO FOR APPROVAL</button>
    <div id="MasterDiv3" class="collapse in" runat="server">
         
    </div>



          <button type="button" class="btn btn-info" data-toggle="collapse" data-target="<%=MasterDiv4.ClientID%>">PENDING SRO FOR APPROVAL</button>
    
     <div id="MasterDiv4" class="collapse in" runat="server">
   
    </div>


  <button type="button" class="btn btn-info" data-toggle="collapse" data-target="<%=MasterDiv5.ClientID%>">PENDING EBOM FOR APPROVAL</button>
    
    <div id="MasterDiv5" class="collapse in" runat="server">
   
    </div>

         </div>

     






    
 
 
 
    </asp:Content>



    
    
 
 






