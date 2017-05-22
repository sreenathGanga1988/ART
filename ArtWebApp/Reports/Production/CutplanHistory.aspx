<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutplanHistory.aspx.cs" Inherits="ArtWebApp.Reports.Production.CutplanHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">




       <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
   <script type="text/javascript" src= https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
 <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>










       <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.4/js/dataTables.buttons.min.js"></script>
   <script type="text/javascript" src="http://cdn.datatables.net/buttons/1.2.4/js/buttons.flash.min.js"></script>
   <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
   <script type="text/javascript" src="http://cdn.rawgit.com/bpampuch/pdfmake/0.1.24/build/pdfmake.min.js"></script>
   <script type="text/javascript" src="http://cdn.rawgit.com/bpampuch/pdfmake/0.1.24/build/vfs_fonts.js"></script>
   <script type="text/javascript" src="http://cdn.datatables.net/buttons/1.2.4/js/buttons.html5.min.js"></script>
   <script type="text/javascript" src="http://cdn.datatables.net/buttons/1.2.4/js/buttons.print.min.js"></script>
     <script type="text/javascript" charset="utf-8">
    
     
        
         $(document).ready(





             function () {
             // Setup - add a text input to each footer cell
                 $('#example tfoot th').each(


                     function () {
                 var title = $(this).text();
                 $(this).html('<input type="text" placeholder="Search ' + title + '" />');
             });

             // DataTable
                 var table = $('#example').DataTable();




             // Apply the search
             table.columns().every(function () {
                 var that = this;

                 $('input', this.footer()).on('keyup change', function () {
                     if (that.search() !== this.value) {
                         that
                             .search(this.value)
                             .draw();
                     }
                 });
             });



             });




        
        







        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wholediv" runat="server"></div>
</asp:Content>
