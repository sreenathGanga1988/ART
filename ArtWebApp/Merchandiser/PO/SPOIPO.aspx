<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SPOIPO.aspx.cs" Inherits="ArtWebApp.Reports.Production.Proddata.SPOIPO" %>

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
        $('#example').DataTable({
            "footerCallback": function (row, data, start, end, display) {
                var api = this.api(), data;



                var table = $('#example').DataTable();

                // #myInput is a <input type="text"> element
                $('#myInput').on('keyup', function () {
                    table.search(this.value).draw();
                });



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

                //// Total over this page
                //pageTotal = api
                //    .column(4, { page: 'current' })
                //    .data()
                //    .reduce(function (a, b) {
                //        return intVal(a) + intVal(b);
                //    }, 0);

              
            }
        });
    });
       


     </script>

    <style type="text/css">
        .auto-style1 {
            width: 75%;
        }
        .auto-style2 {
            width: 223px;
            font-size: medium;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
  
    <div>
      
          </div>
  <div>

         <table class="auto-style1">
             <tr>
                 <td class="auto-style2">
      
                     &nbsp;</td>
                 <td class="auto-style3">

                     &nbsp;</td>
                 <td>
       
                      &nbsp;</td>
             </tr>
             </table>
&nbsp;&nbsp;

    </div>
       
 

      <div>
        </div>
    <div id="MasterDiv" runat="server">

    </div>
    <style>

        .auto-style3 {

   
        border-style: none;
            border-color: inherit;
            border-width: medium;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            width: 210px;
        }




    </style>
    
</asp:Content>
 
