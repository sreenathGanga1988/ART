<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AsqPOWiseReport.aspx.cs" Inherits="ArtWebApp.Reports.MerchandiserReport.AsqPOWiseReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


       <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
<%--   <script type="text/javascript" src= https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>--%>
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
     
         $(document).ready(function () {

             debugger
             var datatableinstance = $('#example').DataTable({

                 dom: 'Blrtip',
                 buttons: [
                     {
                         extend: 'copyHtml5',
                         exportOptions: {
                             columns: ':contains("Office")'
                         }
                     },
                     'excelHtml5',
                     'csvHtml5',
                     'pdfHtml5'
                 ],
              
                 "footerCallback": function (row, data, start, end, display) {
                     var api = this.api(), data;

                     var api1 = this.api(), data;

                     var api2 = this.api(), data;

                     var api3 = this.api(), data;

                     var api4 = this.api(), data;

                 
                     

                     // Remove the formatting to get integer data for summation
                     var intVal = function (i) {
                         return typeof i === 'string' ?
                             i.replace(/[\$,]/g, '') * 1 :
                             typeof i === 'number' ?
                             i : 0;                    


                         };

                         // Total over all pages
                         total = api
                             .column(4)
                             .data()
                             .reduce(function (a, b) {
                                 return intVal(a) + intVal(b);
                             }, 0);


                         total1 = api1
                            .column(3)
                            .data()
                            .reduce(function (a, b) {
                                return intVal(a) + intVal(b);
                            }, 0);

                         total2 = api2
                           .column(5)
                           .data()
                           .reduce(function (a, b) {
                               return intVal(a) + intVal(b);
                           }, 0);

                         total3 = api3
                           .column(6)
                           .data()
                           .reduce(function (a, b) {
                               return intVal(a) + intVal(b);
                           }, 0);

                         total4 = api4
                          .column(7)
                          .data()
                          .reduce(function (a, b) {
                              return intVal(a) + intVal(b);
                          }, 0);

                         // Total over this page
                         pageTotal = api
                             .column(4, { page: 'current' })
                             .data()
                             .reduce(function (a, b) {
                                 return intVal(a) + intVal(b);
                             }, 0);

                         pageTotal1 = api1
                             .column(3, { page: 'current' })
                             .data()
                             .reduce(function (a, b) {
                                 return intVal(a) + intVal(b);
                             }, 0);

                         pageTotal2 = api2
                             .column(5, { page: 'current' })
                             .data()
                             .reduce(function (a, b) {
                                 return intVal(a) + intVal(b);
                             }, 0);


                         pageTotal3 = api3
                             .column(6, { page: 'current' })
                             .data()
                             .reduce(function (a, b) {
                                 return intVal(a) + intVal(b);
                             }, 0);

                         pageTotal4 = api4
                            .column(7, { page: 'current' })
                            .data()
                            .reduce(function (a, b) {
                                return intVal(a) + intVal(b);
                            }, 0);

                         // Update footer
                         $(api.column(4).footer()).html(
                              pageTotal + ' ( $' + total + ' total)'
                         );


                         $(api1.column(3).footer()).html(
                             pageTotal1 + ' ( $' + total1 + ' total)'
                        );

                         $(api2.column(5).footer()).html(
                           pageTotal2 + ' ( $' + total2 + ' total)'
                       );


                         $(api3.column(6).footer()).html(
                         pageTotal3 + ' ( $' + total3 + ' total)'
                     );


                         $(api4.column(7).footer()).html(
                       pageTotal4 + ' ( $' + total4 + ' total)'
                 );
                 }

              

         });



         $('#example thead tr td').each(function () {
             var title = $(this).text();
             $(this).html('<input type="text" placeholder="Search ' + title + '" />');
         });


         $("#example thead input").on('keyup change', function () {
             datatableinstance
                 .column($(this).parent().index() + ':visible')
                 .search(this.value)
                 .draw();
         });

      

         });
       


     </script>


     <link href="../../css/style.css" rel="stylesheet" />
<style type="text/css">












body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}

   

                .headerclass
        {
 background: #fcfcfc;
border-top-color: #c8c8c8;
border-left-color: #c8c8c8;
border-bottom-color: #179bd7;
border-width: 0 0 0 1px;
border-style: solid;
height: 40px;
padding: 0 .5em;
text-overflow: ellipsis;
white-space: nowrap;
text-align: left;
color:black;
font-weight: bold;
font-size: 14px;



line-height: 29px;
margin: -7px;
padding: 0 .7em;
text-align: left;
white-space: nowrap;

        }
   rowcell {
    border-width: 1px 0 0 1px;
    padding: .7em;
    line-height: 14px;
    white-space: nowrap;
    width: auto;
    vertical-align: middle;
}
.rowcell {
    border-width: 1px 0 0 1px;
    padding: .7em;
    line-height: 14px;
    white-space: nowrap;
    width: auto;
    vertical-align: middle;
    color:black;
}
     
      






   

    </style>
    
      

   
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--  <div class=""> ATC CHART</div>--%>

     <div class="FullTable"> 
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeaddingdIV" colspan="6" >ATC ASQ SizeWise Report</td>
                            </tr>
                              <tr>
                                  <td class="NormalTD">ATC # : </td>
                                  <td class="NormalTD">
                                     
                                              <asp:DropDownList ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" Height="17px" Width="200px">
                                              </asp:DropDownList>

                                  </td>
                                  <td class="SearchButtonTD">
                                      
                                              <asp:Button ID="ShowBom" runat="server" Height="23px" OnClick="ShowBom_Click" Text="S" Width="34px" />
                                         
                                  </td>
                                  <td class="NormalTD">pROJ qTY:</td>
                                  <td class="NormalTD">
                                  
                                              <asp:Label ID="lbl_qty" runat="server" Font-Size="Smaller" Text="0"></asp:Label>
                                       
                                  </td>
                                 <td class="NormalTD"></td>
                            </tr>
                              
                            
                            
                            
                            <tr>
                                <td class="NormalTD">ASQ</td>
                                <td class="NormalTD">
                                 
                                            <ig:WebDropDown ID="drp_popack" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId" Width="200px">
                                                <DropDownItemBinding TextField="name" ValueField="pk" />
                                            </ig:WebDropDown>
                                  
                                </td>
                                <td class="SearchButtonTD">
                                  
                                            <asp:Button ID="ShowBom0" runat="server" Height="23px" OnClick="ShowBom0_Click" Text="S" Width="34px" />
                                      
                                </td>
                                <td class="NormalTD">&nbsp;</td>
                                <td class="NormalTD">
                                    &nbsp;</td>
                                <td class="NormalTD">&nbsp;</td>
                            </tr>
                              
                            
                            
                            
                        </table>

                      </div>

    

   
    <div id="MasterDiv"  runat="server">

        


        

        


        
    </div>

    
     
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
<%--       <asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                   <div class="modal">
        <div class="center">
          <img  src="../../Image/loader.gif" style="position: relative; top: 45%;" > </img>
        </div>
    </div>
                                     
                                       
                                        
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
</asp:Content>
