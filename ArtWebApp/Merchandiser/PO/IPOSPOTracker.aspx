<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IPOSPOTracker.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.IPOSPOTracker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>
         <%: System.Web.Optimization.Scripts.Render("~/bundles/jqueryui") %>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/bootstrap") %>
    <%: System.Web.Optimization.Styles.Render("~/bundle/css") %>
  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.css"/>

    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.js"></script>
    <script src="../../Scripts/jquery.table2excel.js"></script>
    <script src="../../JQuery/ExporttoExcel.js"></script>

<script type="text/javascript" charset="utf-8">
   
   
       
    $(document).ready(function () {
      ;


      $('#Showfilter').on('click',null, function () {
          
            $('.example').DataTable({
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
        });
           
        });

        
    });
   
  </script>

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="MainDiv  ">
              <div class="RedHeaddingdIV">SPO IPO Tracker </div>
                <div>
                <table class="">
                    <tr>
                        <td>Year</td>
                        <td><asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple">
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                            </asp:ListBox></td>
                        <td>
                            <asp:Button ID="ShoIPOTracker" runat="server" Height="23px" Text="S" Width="34px" OnClick="ShoIPOTracker_Click" />
                        </td>
                        <td><input type="button" id="Showfilter" value="Convert to Filter"></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div id="MasterDiv" class="MasterDiv" runat="server">

    </div>
        </div>
      

            
        
             <div></div>
            
        

     
    </form>
</body>
</html>
