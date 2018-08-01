<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="StockMRN.aspx.cs" Inherits="ArtWebApp.Inventory.StockMRN" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style type="text/css">

          .auto-style1 {
              height: 30px;
          }

    </style>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/GridJQuery.js"></script>


   <script type="text/javascript">
      
       function validateQTY ()
       {


           var tbl_podetails = document.getElementsByClassName("tbl_podetails")[0];
           for (var i = 1; i < tbl_podetails.rows.length; i++)
           {
              
               var newQty = tbl_podetails.rows[i].getElementsByClassName("txt_reci")[0].value;
               var balqty = tbl_podetails.rows[i].getElementsByClassName("lblbal")[0].innerHTML;

               if (parseFloat(newQty) > parseFloat(balqty))
               {
                   alert("Extra Not allowed More Than POQTY");
                   newQty = 0;
                   tbl_podetails.rows[i].getElementsByClassName("txt_reci")[0].value = 0;

               }
               else {
              
               }


           }

       }

    



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
     <tr class="RedHeadding">
        <td colspan="8" >General&nbsp;RECEIPT</td>

     </tr>
     <tr>
        <td  colspan="8" >



           
                 <table class="DataEntryTable">
                     <tr>
                         <td >Supplier : </td>
                         <td >
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                    

                                     <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10">
                                     </ucc:DropDownListChosen>
                                 
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         </td>
                         <td >
                             <asp:Button ID="btn_searchReciept" runat="server" OnClick="btn_searchReciept_Click" Text="S" ToolTip="Press here to see all the reciepts of selected supplier" />
                         </td>
                         <td >&nbsp;</td>
                         <td >&nbsp;</td>
                         <td >&nbsp;</td>
                     </tr>
                     <tr>
                         <td >Container # :</td>
                         <td >
                             <asp:TextBox ID="txt_container" runat="server" Width="189px" CssClass="auto-style39"></asp:TextBox>
                         </td>
                         <td >Delivery Date: </td>
                         <td >
                             <ig:WebDatePicker ID="dtp_deliverydate" runat="server" Height="19px" Width="171px">
                             </ig:WebDatePicker>
                         </td>
                         <td >In House Date</td>
                         <td >
                             <ig:WebDatePicker ID="dtp_inhousedate" runat="server">
                             </ig:WebDatePicker>
                         </td>
                     </tr>
                     <tr>
                         <td >BOE # :</td>
                         <td >
                             <asp:TextBox ID="txt_boe" runat="server" Width="185px" CssClass="auto-style39"></asp:TextBox>
                         </td>
                         <td >Remark : </td>
                         <td >
                             <textarea id="txta_remark" runat="server" cols="20" name="S1" rows="2" class="auto-style39"></textarea></td>
                         <td >&nbsp;</td>
                         <td >&nbsp;</td>
                     </tr>
                     <tr>
                         <td >&nbsp;</td>
                         <td >&nbsp;</td>
                         <td >
                             <asp:Button ID="btn_savercpt" runat="server" OnClick="btn_savercpt_Click" Text="Save Receipt" CssClass="auto-style39" />
                         </td>
                         <td >
                             &nbsp;</td>
                         <td >&nbsp;</td>
                         <td >&nbsp;</td>
                     </tr>
                 </table>
             
             

               

         </td>

     </tr>
        <tr class="SUBRedHeadding">
            <td colspan="8">
                general MRN
            </td>
        </tr>
    <tr>
        <td  colspan="8" >
           
           
               

               

               
               

                <table class="DataEntryTable">
                    <tr>
                        <td>
                            
                                     <table class="DataEntryTable">
                                <tr>
                                    <td >Receipt #&nbsp; :</td>
                                    <td >
                                           <asp:UpdatePanel ID="Upd_rxptdrop" UpdateMode="Conditional"  runat="server">
  <ContentTemplate>
                                       

         <ucc:DropDownListChosen ID="drp_rcpt" runat="server" DataSourceID="recieptdata" Width="200px"  DataTextField="StockRecieptNum" DataValueField="SReciept_Pk" DisableSearchThreshold="10">
                                     </ucc:DropDownListChosen>
       </ContentTemplate>
                </asp:UpdatePanel>
                                    </td>
                                    <td >
                                        <asp:Button ID="btn_confirmRcpt" runat="server" CssClass="auto-style39" OnClick="btn_confirmRcpt_Click" Text="S" />
                                    </td>
                                    <td ></td>
                                    <td ></td>
                                </tr>
                                <tr>
                                    <td >SPO # </td>
                                    <td >
                                        

                                          <ucc:DropDownListChosen ID="drp_Po" runat="server" Width="200px" DataSourceID="podata" DataTextField="SPONum" DataValueField="SPO_Pk" DisableSearchThreshold="10">
                                     </ucc:DropDownListChosen>

                                    </td>
                                    <td >
                                        <asp:Button ID="btn_confirmPO" runat="server" CssClass="auto-style39" OnClick="btn_confirmPO_Click" Text="S" />
                                    </td>
                                    <td >Delivery Note # : </td>
                                    <td >
                                        <asp:TextBox ID="txt_deliverynote" runat="server" CssClass="auto-style39"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1" >SADN</td>
                                    <td class="auto-style1" >
                                    <ucc:DropDownListChosen ID="drp_sadn" runat="server" DisableSearchThreshold="10" Width="200px" DataTextField="name" DataValueField="pk">
                                        </ucc:DropDownListChosen>    

                                    </td>
                                    <td class="auto-style1" >
                                        <asp:Button ID="btn_sdn" runat="server" OnClick="btn_sdn_Click" Text="S" Width="27px" />
                                    </td>
                                    <td class="auto-style1" > </td>
                                    <td class="auto-style1" >
                                    </td>
                                </tr>
                                <tr>
                                    <td >EXP</td>
                                    <td >
                                        

                                          &nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                    <td >&nbsp;</td>
                                    <td >
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td >PO Type</td>
                                    <td >
                                        

                                          <asp:Label ID="lbl_potype" runat="server" Text="Local"></asp:Label>
                                    </td>
                                    <td >
                                        &nbsp;</td>
                                    <td >MRN Done At</td>
                                    <td >
                                        <asp:Label ID="lbl_country" runat="server" Text="UAE"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                                
                            
                        </td>
                    </tr>
                    <tr class="gridtable">
                        <td>
                            <asp:GridView ID="tbl_Podetails" CssClass="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri" Width="90%">
                                <Columns>
                                  

                                     <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    


                                    <asp:TemplateField HeaderText="SPDet_PK">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SPODetails_PK" runat="server" Text='<%# Bind("SPODetails_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="Temp_PK" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Template_PK" runat="server" Text='<%# Bind("Template_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                       
<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
                                       
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Composition">
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_composition" runat="server" Text='<%# Bind("Composition") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Construct">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_construct" runat="server" Text='<%# Bind("Construct") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_color" runat="server" Text='<%# Bind("TemplateColor") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_size" runat="server" Text='<%# Bind("TemplateSize") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Width">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_width" runat="server" Text='<%# Bind("TemplateWidth") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_weight" runat="server" Text='<%# Bind("TemplateWeight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UOM" runat="server" Text='<%# Bind("UOMCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="POQty" HeaderText="POQty" />
                                    <asp:BoundField DataField="ReceivedQty" HeaderText="RecievedQty" />
                                    

                                     <asp:TemplateField HeaderText="BalanceQty">
                                        
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" CssClass="lblbal" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>

                                     
                                    <asp:TemplateField HeaderText="RecieptQty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_reciept" CssClass="txt_reci"  onchange="validateQTY()" runat="server" onkeypress="return isNumberKey(event,this)" Text='<%# Bind("BalanceQty") %>'  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Extra">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_extra"  onkeypress="return isNumberKey(event,this)"  Text="0"  runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Unitprice">
                                         
                                         <ItemTemplate>
                                             <asp:Label ID="lbl_unitprice" runat="server" Text='<%# Bind("Unitprice") %>'></asp:Label>
                                         </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="style1">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="auto-style24">&nbsp;</td>
                                    <td class="style7">
                                        <asp:Button ID="btn_saveMrn" runat="server" OnClick="btn_saveMrn_Click" Text="Save MRN to Receipt" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style7"></td>
                    </tr>
                </table>
               

              
            
        </td>
    </tr>
    <tr>
        <td class="auto-style16">
            &nbsp;</td>
        <td class="style3">
                <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
                                </td>
        <td class="auto-style26">
            <asp:SqlDataSource ID="recieptdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [StockRecieptNum], [SReciept_Pk] FROM [StockRecieptMaster] ORDER BY [StockRecieptNum] DESC">
            </asp:SqlDataSource>
        </td>
        <td class="auto-style16">
            <asp:HiddenField ID="hdn_rcptnum" runat="server" />
        </td>
        <td class="style6">
            <asp:SqlDataSource ID="podata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT StockPOMaster.SPO_Pk, StockPOMaster.SPONum, StockPOMaster.IsApproved FROM StockPOMaster INNER JOIN StockRecieptMaster ON StockPOMaster.Supplier_Pk = StockRecieptMaster.Supplier_PK WHERE (StockRecieptMaster.SReciept_Pk = @Param1) AND (StockPOMaster.IsApproved = N'Y')">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hdn_rcptnum" DefaultValue="0" Name="Param1" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td class="style18">
            &nbsp;</td>
        <td class="style8">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>
