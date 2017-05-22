<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ShipmentHandover.aspx.cs" Inherits="ArtWebApp.Production.ShipmentHandover" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
     
    </style>
    
    
<script src="../JQuery/GridJQuery.js"></script>
       

    <script src="../JQuery/Validator.js"></script>



    <script lang="javascript" type="text/javascript">
        var gridID = "<%= tbl_podetails.ClientID %>";


        function CheckBoxSelectionValidation(objText) {
            debugger;
            var gridView = document.getElementById(gridViewID);
            var sum = 0;
            for (var i = 1; i < gridView.rows.length-1; i++) {
                var count = 0;
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];

                var txtQty = gridView.rows[i].getElementsByClassName("txtQty")[0];
                var lblbal = gridView.rows[i].getElementsByClassName("lblbal")[0];

                if (chkConfirm.checked) {
                    if (txtQty.value == "" || lblbal.value == "") {
                        gridView.rows[i].style.backgroundColor = "red";
                        txtQty.focus();

                        return false;
                    }
                   if (txtQty.value == "0" || lblbal.value == "0") {
                        gridView.rows[i].style.backgroundColor = "red";
                        txtQty.focus();

                        return false;
                    }
                   if (parseFloat(txtQty.value) > parseFloat(lblbal.innerText)) {

                        newqtytextbox[0].value = 0;
                        alert("Extra Qty Cannot be Allowed");
                        newqtytextbox[0].focus();
                   } else
                   {
                       sum = sum + parseFloat(txtQty.value);

                   }
                }
            }

            var footer = gridView.getElementsByClassName("qtyfooter")[0];
            footer.innerHTML = sum.toString();
        }
   
     
        </script>

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td class="NormalTD" colspan="4"> Shipment Handover</td>
        </tr>
        <tr>
            <td class="NormalTD">Factory</td>
             <td class="NormalTD">
                 <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                 </ucc:DropDownListChosen>
            </td>
             <td class="NormalTD">
                 <asp:Button ID="Btn_showJC" runat="server" Text="S" OnClick="Btn_showJC_Click" />
            </td>
             <td class="NormalTD"></td>
        </tr>
        <tr>
             <td class="NormalTD">
                 Job Contract</td>
            <td class="NormalTD">
                <ig:WebDropDown ID="drp_jobcontract" runat="server" EnableMultipleSelection="True" EnableClosingDropDownOnSelect="False" TextField="Name" ValueField="pk" Width="200px">
                    <DropDownItemBinding TextField="Name" ValueField="pk" />
                </ig:WebDropDown>
            </td>
             <td class="NormalTD">
                 <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="S" />
            </td>
             <td class="NormalTD">
            </td>
        </tr>
                 <tr>
                     <td class="NormalTD">Shipment date</td>
                     <td class="NormalTD">
                        

                         <ig:WebDatePicker ID="shipdate" runat="server">
                         </ig:WebDatePicker>
                        

                     </td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                 </tr>
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>

     <div class="gridtable">

         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" ShowFooter="True">
                            <Columns>      
                <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="JobContractDetail_pk">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_JobContractDetail_pk" runat="server" Text='<%# Bind("JobContractDetail_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="JOBContractNUM" HeaderText="JOBContractNUM" />
                <asp:BoundField DataField="POPackNUm" HeaderText="POPackNUm" />
                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" />
                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" />
                <asp:BoundField DataField="POQTY" HeaderText="POQTY" />
                                <asp:BoundField DataField="ShippedQty" HeaderText="ShippedQty" />
                                
                                <asp:TemplateField HeaderText="BalQty">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BalQty" CssClass="lblbal" runat="server" Text='<%# Bind("BalQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                  
                                    <ItemTemplate>
                                       <asp:TextBox ID="txt_qty"  CssClass="txtQty" onkeypress="return isNumberKey(event,this)"  onkeyup ="validateQtyWithBalance(this)"  runat ="server" Text='<%# Bind("BalQty") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                   <FooterTemplate>

                                          <asp:Label ID="lbl_qtyfooter" CssClass="qtyfooter" runat="server" ></asp:Label>
                                   </FooterTemplate>

                                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#000066" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Black" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
              </ContentTemplate>
     </asp:UpdatePanel>

    </div>

    <div>



        <asp:Button ID="btn_submitShipment" runat="server" Text="Submit" OnClientClick="return CheckBoxSelectionValidation()" OnClick="btn_submitShipment_Click" />



    </div>
    <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
       
</asp:Content>
