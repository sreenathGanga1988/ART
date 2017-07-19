<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="CutplanHistory.aspx.cs" Inherits="ArtWebApp.Reports.Production.CutplanHistory" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
        }
        .auto-style2 {
            text-decoration: underline;
        }
        .auto-style3 {
            height: 25px;
            width: 200px;
        }
        .smalldetailtable,th,td
        {
            font-family:Calibri;
            border: 1px solid black;
            width:100%;

        }
        .auto-style4 {
            height: 30px;
            width: 200px;
        }
    </style>


    
       <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
   <script type="text/javascript" src= "https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
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

                 // DataTable
                 var table = $('#example').DataTable();



             // Setup - add a text input to each footer cell
                 $('#example tfoot th').each(


                     function () {
                 var title = $(this).text();
                 $(this).html('<input type="text" placeholder="Search ' + title + '" />');
             });

            


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




   
</head>
<body>
    <form id="form1" runat="server">
      <div id="wholediv" runat="server"></div>
    </form>
</body>
</html>