<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Invoicing.aspx.cs" Inherits="ArtWebApp.Shipping.Invoicing" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/Validator.js"></script>
    <script src="../JQuery/GridJQuery.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script>

$(document).ready(function () {

   

    $(".NewFOB").blur(function () {
       
        debugger;
        var enteredVal = $(this).val();
        
        var LastFOB = $(this).closest('tr').find("td:eq(8)").text();
        var LastFOB = LastFOB.trim();
   
        if (LastFOB > enteredVal)
        {
            
            $(this).css({ 'background-color': '#FF0000' });
            alert("Cannot reduce FOB");
        }
        else
        {
            
        }
       
    });

});



function validatefob(objText) {
    debugger;

    var cell = objText.parentNode;
    var row = cell.parentNode;

    var sum = 0;

    var newfob = row.getElementsByClassName("NewFOB");
    var LastFOB = row.getElementsByClassName("LastFOB");


    if (parseFloat(newfob[0].value) < parseFloat(LastFOB[0].innerText)) {

        newfob[0].value = 0;
        alert("FOB cannot be Reduced");
    } else {

    }


}




</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td class="NormalTD" colspan="4">Invoicing</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Factory</td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     </td>
                     <td class="auto-style7">
                         <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="S" />
                     </td>
                     <td class="auto-style7"></td>
                 </tr>
        <tr>
            <td class="NormalTD">Shipment #:</td>
             <td class="NormalTD" >
                <ig:WebDropDown ID="drp_shpcode" runat="server" Width="200px" EnableMultipleSelection="True" TextField="name" ValueField="pk">
                      <DropDownItemBinding TextField="name" ValueField="pk" />
                  </ig:WebDropDown></td>
             <td class="auto-style7">
                 <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />
            </td>
             <td class="auto-style7"></td>
        </tr>
      
                 <tr>
                     <td class="NormalTD">
                      

                         bank #:</td>
                     <td class="NormalTD"><ucc:DropDownListChosen ID="drp_bank" runat="server" DataSourceID="BankDataSource" DataTextField="BankName" DataValueField="Bank_PK" DisableSearchThreshold="10" Width="200px">
                </ucc:DropDownListChosen>
                         
                     </td>
                     <td class="auto-style7">&nbsp;</td>
                     <td class="auto-style7">&nbsp;</td>
                 </tr>
      
                 <tr>
                     <td class="NormalTD">Ref #:</td>
                     <td class="NormalTD">
                         <asp:TextBox ID="txt_ref" runat="server" Width="200px"></asp:TextBox>
                     </td>
                     <td class="auto-style7">&nbsp;</td>
                     <td class="auto-style7">&nbsp;</td>
                 </tr>
      
                 <tr>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="auto-style7">&nbsp;</td>
                     <td class="auto-style7">&nbsp;</td>
                 </tr>
      
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>
    <div class="gridtable">

         <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" DataKeyNames="ShipmentHandOver_PK">
                            <Columns>   
                                 <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                
                                 <asp:TemplateField HeaderText="PoPackID" SortExpression="PoPackID">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackID") %>'></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle CssClass="hidden" />
                                     <ItemStyle CssClass="hidden" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="OurStyleID" SortExpression="OurStyleID">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle CssClass="hidden" />
                                     <ItemStyle CssClass="hidden" />
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="ShipOver_PK" InsertVisible="False" SortExpression="ShipmentHandOver_PK">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_ShipmentHandOverPK" runat="server" Text='<%# Bind("ShipmentHandOver_PK") %>'></asp:Label>
                                     </ItemTemplate>
                                     <HeaderStyle CssClass="hidden" />
                                     <ItemStyle CssClass="hidden" />
                                 </asp:TemplateField>
                                <asp:BoundField DataField="ShipmentHandOverCode" HeaderText="ShipmentCode" SortExpression="ShipmentHandOverCode" />
                                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                <asp:BoundField DataField="POPackNUm" HeaderText="POPackNUm" ReadOnly="True" SortExpression="POPackNUm" />
                                 <asp:TemplateField HeaderText="FOB" SortExpression="FOB">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_fob" runat="server"  CssClass="LastFOB"     Text ='<%# Bind("FOB") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:BoundField DataField="ShippedQty" HeaderText="ShippedQty" ReadOnly="True" SortExpression="ShippedQty" />
                                <asp:BoundField DataField="InvoicedQty" HeaderText="InvoicedQty" ReadOnly="True" SortExpression="InvoicedQty" />
                                 <asp:TemplateField HeaderText="BalToInvoice" SortExpression="BalToInvoice">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lblbal" CssClass="lblbal"  runat="server" Text='<%# Bind("BalToInvoice") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="New FOB">
                                  
                                     <ItemTemplate>
                                          <asp:TextBox ID="txt_newfob" Height="16px" Width="50px"   CssClass="NewFOB" onkeypress="return isNumberKey(event,this)" onchange="validatefob(this)" runat="server" Text='<%# Bind("FOB") %>'></asp:TextBox>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Qty">
                                  
                                     <ItemTemplate>
                                          <asp:TextBox ID="txt_qty"  CssClass="txtQty" Height="16px" Width="50px"  onkeypress="return isNumberKey(event,this)"  onkeyup ="validateQtyWithBalance(this)" runat="server" Text='<%# Bind("BalToInvoice") %>'></asp:TextBox>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Carton#">
                                     <ItemTemplate>
                                         <asp:TextBox ID="txt_Ctn" Height="16px" Width="50px"  onkeypress="return isNumberKey(event,this)" runat="server">0</asp:TextBox>
                                     </ItemTemplate>
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


    <div class="DataEntryTable">


        <asp:Button ID="btn_Submit" runat="server" Text="Submit" OnClientClick="return CheckBoxSelectionValidation()" OnClick="btn_JCSubmit_Click" style="height: 26px" />


    </div>

       <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
    <asp:SqlDataSource ID="BankDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Bank_PK], [BankName] FROM [BankMaster]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>


                </asp:Content>
