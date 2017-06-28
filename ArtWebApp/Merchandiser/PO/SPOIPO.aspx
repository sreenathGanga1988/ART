<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SPOIPO.aspx.cs" Inherits="ArtWebApp.Reports.Production.Proddata.SPOIPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/jquery-3.1.1.min.js"></script>
  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.css"/>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.js"></script>

   

<script type="text/javascript" charset="utf-8">
   
  
       
    $(document).ready(function () {
        $('.example').DataTable();
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
 
