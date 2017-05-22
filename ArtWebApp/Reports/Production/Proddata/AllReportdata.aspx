<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AllReportdata.aspx.cs" Inherits="ArtWebApp.Reports.Production.Proddata.AllReportdata" %>
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
             var datatableinstance=    $('#example').DataTable({

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


    <link href="../../../css/style.css" rel="stylesheet" />
   

   
    <style type="text/css">
        .auto-style1 {
            height: 27px;
        }
    </style>
   

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RedHeaddingdIV">Production Reports (All in One)</div>
        <div>
            <table class="DataEntryTable">
                <tr>
                    <td class="NormalTD">From Date</td>
                    <td class="auto-style1">
                        <ig:WebDatePicker ID="dtp_shipStartdate" runat="server">
                        </ig:WebDatePicker>
                    </td>
                    <td class="NormalTD">To Date</td>
                    <td class="NormalTD">
                        <ig:WebDatePicker ID="dtp_shipenddate" runat="server">
                        </ig:WebDatePicker>
                    </td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">Country</td>
                    <td class="auto-style1">
                        <ig:WebDropDown ID="drp_country" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableClosingDropDownOnSelect="False" EnableDropDownAsChild="False"  Height="21px" PageSize="12" TextField="Description" ValueField="CountryID" Width="156px" DataSourceID="CountryData">
                            <DropDownItemBinding TextField="Description" ValueField="Countryid" />
                        </ig:WebDropDown>
                        
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_showCountrydata" runat="server" Text="S" ToolTip="Show Selected Countries Production report for Selected period" OnClick="btn_showCountrydata_Click" />
                    </td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                </tr>
                <tr>
                    <td>Buyer</td>
                    <td class="auto-style2">
                        <ig:WebDropDown ID="drp_buyer" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableClosingDropDownOnSelect="False"  Height="21px" PageSize="12" TextField="Buyername" ValueField="Buyerid" Width="156px" DataSourceID="BuyerMasterData">
                            <DropDownItemBinding TextField="Buyername" ValueField="Buyerid" />
                        </ig:WebDropDown>
                        
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_showBuyerdata" runat="server" Text="S" ToolTip="Show Selected Buyer Production data For Selected period" OnClick="btn_showBuyerdata_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btn_showatc" runat="server" OnClick="btn_showatc_Click" Text="Show Atc of Buyer" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Atc</td>
                    <td class="auto-style2">
                        <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableClosingDropDownOnSelect="False" EnableDropDownAsChild="false" EnableMultipleSelection="True" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px">
                            <DropDownItemBinding TextField="name" ValueField="pk" />
                        </ig:WebDropDown>
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_showAtcdata" runat="server" Text="S" ToolTip="Selected Production data of ATC for Selected Period " OnClick="btn_showAtcdata_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <asp:SqlDataSource ID="CountryData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CountryID], [Description], [Isthere] FROM [CountryMaster] WHERE ([Isthere] = @Isthere)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Y" Name="Isthere" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="BuyerMasterData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [BuyerName], [BuyerID] FROM [BuyerMaster] WHERE ([IsActive] = @IsActive)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Y" Name="IsActive" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
    </div>
        <div></div>
        <div id="MasterDiv" runat="server"></div>
</asp:Content>
