<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IPOSPOTracker.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.IPOSPOTracker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/jquery-3.1.1.min.js"></script>  
  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.css"/>
<%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>--%>
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.js"></script>
    <script src="../../Scripts/jquery.table2excel.js"></script>
    <script src="../../JQuery/ExporttoExcel.js"></script>

<script type="text/javascript" charset="utf-8">
   
  
       
    $(document).ready(function () {
        $('.example').DataTable();
    });

  </script>

</head>
<body>
    <form id="form1" runat="server">
     <div id="MasterDiv" runat="server">

    </div>
    </form>
</body>
</html>
