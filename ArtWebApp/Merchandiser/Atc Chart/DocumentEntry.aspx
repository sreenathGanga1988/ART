<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DocumentEntry.aspx.cs" Inherits="ArtWebApp.Merchandiser.Atc_Chart.DocumentEntry" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.NavigationControls" tagprefix="ig1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
    <script type="text/javascript">

       //calculate the sum of qty on keypress
       function validateQty(objText) {
           debugger;
        
           var cell = objText.parentNode;
           var row = cell.parentNode; 

           var sum = 0; 

           var newqtytextbox = row.getElementsByClassName("txtQty");
           var balQtylabel = row.getElementsByClassName("txtbal");
        
         
           if (parseFloat(newqtytextbox[0].value) > parseFloat(balQtylabel[0].innerText))
           {
               
               newqtytextbox[0].value = 0;
               alert("Extra Qty Cannot be Allowed");
           }else
           {

           }


       }

    
       function validateExcessQty(objText) {
           debugger;
           
           
           var cell = objText.parentNode;
           var row = cell.parentNode;

           var sum = 0; 
           var txt_potype = row.getElementsByClassName("txt_potype");
           var txtexcess = row.getElementsByClassName("txtexcess");
           var txt_newExcessqty = row.getElementsByClassName("txt_newExcessqty");
           var lbl_poqty = row.getElementsByClassName("poqty");
           var allowedexcess = 0;
           if (txt_potype[0].innerText.trim() == "F")
           {
               allowedexcess = (3 / 100) * parseFloat(lbl_poqty[0].innerText);
           }
         

           var totalexcess = parseFloat(txtexcess[0].innerText) + parseFloat(txt_newExcessqty[0].value);

           if (parseFloat(totalexcess) > parseFloat(allowedexcess)) {

               txt_newExcessqty[0].value = 0;
               alert("Extra Qty Cannot be Allowed over 3 %");
           } else {

           }


       }


</script>

    <style type="text/css">
        .auto-style1 {
            height: 27px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
     <tr>
        <td  class="RedHeadding" colspan="8" >



             ADN</td>

     </tr>
       
     <tr>
        <td  colspan="8">

                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <ContentTemplate >

                         <table  class="DataEntryTable">
                     <tr>
                         <td  class=NormalTD>Supplier : </td>
                         <td  class=NormalTD>
                             
                                     <ucc:dropdownlistchosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10">
                                     </ucc:DropDownListChosen>
                                 
                         </td><td  class=NormalTD>
                         <td  class=NormalTD>
                             &nbsp;</td>
                         <td  class=NormalTD>&nbsp;</td>
                         <td  class=NormalTD>&nbsp;</td>
                         <td  class=NormalTD>&nbsp;</td>
                     </tr>
                     <tr>
                         <td  class=NormalTD>Supplier Doc # :</td>
                         <td  class=NormalTD>
                             <asp:TextBox ID="txt_container" runat="server" Width="189px" CssClass="auto-style39"></asp:TextBox>
                         </td>
                         <td  class=NormalTD>ETA Date </td>
                         <td  class=NormalTD>
                             <ig:webdatepicker ID="dtp_deliverydate" runat="server" Height="19px" Width="171px">
                             </ig:WebDatePicker>
                         </td>
                         <td  class=NormalTD>&nbsp;</td>
                         <td  class=NormalTD>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td  class=NormalTD>container # :</td>
                         <td  class=NormalTD>
                             <asp:TextBox ID="txt_boe" runat="server" Width="185px" CssClass="auto-style39"></asp:TextBox>
                         </td>
                         <td  class=NormalTD>Remark : </td>
                         <td  class=NormalTD>
                             <textarea id="txta_remark" runat="server" cols="20" name="S1" rows="2" class="auto-style39"></textarea></td>
                         <td  class=NormalTD></td>
                         <td  class=NormalTD></td>
                     </tr>
                             <tr>
                                 <td>Doc Value</td>
                                 <td>
                                     <asp:TextBox ID="txt_docvalue" runat="server"></asp:TextBox>
                                 </td>
                                 <td>Currency</td>
                                 <td>
                                     <ucc:DropDownListChosen ID="drp_currency" runat="server"  DataSourceID="currencydata" DataTextField="CurrencyCode" DataValueField="CurrencyID" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
                                 </td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                             </tr>
                             <tr>
                                 <td>ADN TYPE</td>
                                 <td>
                                     <ucc:DropDownListChosen ID="ddl_adnType" runat="server" DisableSearchThreshold="10" Width="200px">
                                         <asp:ListItem>Select</asp:ListItem>
                                         <asp:ListItem Value="LocalUAE">Local UAE ADN</asp:ListItem>
                                         <asp:ListItem Value="LocalKenya">Local Kenyan ADN</asp:ListItem>
                                         <asp:ListItem Value="IntlSupplier">International ADN</asp:ListItem>
                                     </ucc:DropDownListChosen>
                                 </td>
                                 <td>From Country</td>
                                 <td>
                                     <asp:TextBox ID="txt_fromcountry" runat="server"></asp:TextBox>
                                 </td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                             </tr>
                             <tr>
                                 <td>To</td>
                                 <td>
                                     <ucc:DropDownListChosen ID="drp_ToWarehouse" runat="server" Width="200px">
                                     </ucc:DropDownListChosen>
                                 </td>
                                 <td>Shipment Mode</td>
                                 <td>
                                     <ucc:DropDownListChosen ID="drp_deliverymode" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                                 </td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                             </tr>
                             <tr>
                                 <td>estimated Cost(USD)</td>
                                 <td>
                                     <asp:TextBox ID="txt_estimatedcost" runat="server"></asp:TextBox>
                                 </td>
                                 <td>Payment term</td>
                                 <td>
                                     <ucc:DropDownListChosen ID="ddl_paymentterm" runat="server" DisableSearchThreshold="10" Width="200px">
                                         <asp:ListItem>Select</asp:ListItem>
                                         <asp:ListItem Value="FOB">FOB</asp:ListItem>
                                         <asp:ListItem Value="CNF">CNF</asp:ListItem>
                                         <asp:ListItem Value="Ex-Factory">Ex-Factory</asp:ListItem>
                                     </ucc:DropDownListChosen>
                                 </td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                             </tr>
                     <tr>
                         <td  class=NormalTD>&nbsp;</td>
                         <td  class=NormalTD>&nbsp;</td>
                         <td  class=NormalTD>
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                     <asp:Button ID="btn_savercpt" runat="server" CssClass="auto-style39" OnClick="btn_savercpt_Click" Text="Save Document" />
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         </td>
                         <td  class=NormalTD>
                             
                         </td>
                         <td  class=NormalTD>&nbsp;</td>
                         <td  class=NormalTD>&nbsp;</td>
                     </tr>
                             <tr>
                                 <td class="auto-style1" colspan="5">
                                     <asp:UpdatePanel ID="updstatus" runat="server" UpdateMode="Conditional">
                                         <ContentTemplate>
                                             <div id="Messaediv" runat="server">
                                                 <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                             </div>
                                         </ContentTemplate>
                                     </asp:UpdatePanel>
                                 </td>
                                 <td class="NormalTD">&nbsp;</td>
                             </tr>
                 </table>
                     </ContentTemplate>
                </asp:UpdatePanel>


                
             

         </td>

     </tr>
       
    <tr>
        <td class="style17" colspan="8" bgcolor="White">
           
            <asp:Panel ID="Panel1" runat="server" Height="100%" Width="100%" CssClass="auto-style53">
               

        
               

                <table class="FullTable">
                    <tr>
                        <td>
                            <table class="DataEntryTable">
                                <tr>
                                    <td class="RedHeadding" colspan="6">PO ADN DETAILS</td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">ADN&nbsp; #&nbsp; :</td>
                                    <td class="NormalTD">
                                        <asp:UpdatePanel ID="udp_drprcpt" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                 <ucc:dropdownlistchosen ID="drp_rcpt" runat="server" DataSourceID="recieptdata" DataTextField="DocNum" DataValueField="Doc_Pk" DisableSearchThreshold="10" Width="200px">
                                        </ucc:DropDownListChosen>
                                        

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="NormalTD">
                                        <asp:Button ID="btn_confirmRcpt" runat="server" CssClass="auto-style39" OnClick="btn_confirmRcpt_Click" Text="S" />
                                    </td>
                                    <td class="NormalTD">ETA Date :</td>
                                    <td class="NormalTD">
                                        <asp:UpdatePanel ID="upd_eta" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <ig:webdatepicker ID="dtp_eta" runat="server" Height="19px" OnValueChanged="dtp_eta_ValueChanged" Width="171px">
                                                </ig:WebDatePicker>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="NormalTD">
                                        <asp:UpdatePanel ID="upd_etabutton" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Apply ETA to Selected" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">Atc # :</td>
                                    <td class="NormalTD"><ucc:dropdownlistchosen ID="ddl_atc"  runat="server" DataSourceID="AtcData" DataTextField="AtcNum" DataValueField="AtcId" Width="200px"    >
                                        </ucc:DropDownListChosen>
                                        <asp:SqlDataSource ID="AtcData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        AtcMaster.AtcId, AtcMaster.AtcNum
FROM            AtcMaster INNER JOIN
                         ProcurementMaster ON AtcMaster.AtcId = ProcurementMaster.AtcId INNER JOIN
                         DocMaster ON ProcurementMaster.Supplier_Pk = DocMaster.Supplier_PK
WHERE        (DocMaster.Doc_Pk = @Param1)
GROUP BY AtcMaster.AtcId, AtcMaster.AtcNum">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="hdn_rcptnum" DefaultValue="0" Name="Param1" PropertyName="Value" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                    <td class="NormalTD"><asp:Button ID="btn_confirmatc" runat="server" CssClass="auto-style39" OnClick="btn_confirmatc_Click" Text="S" /></td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">Po # </td>
                                    <td class="NormalTD">
                                        <asp:UpdatePanel ID="udp_drppo" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <ig:webdropdown ID="drp_po" runat="server" DataSourceID="podata" EnableMultipleSelection="True" Height="23px" style="height: 23px" TextField="Ponum" ValueField="PO_Pk" Width="200px" EnableClosingDropDownOnSelect="False">
                                                    <DropDownItemBinding TextField="Ponum" ValueField="PO_Pk" />
                                                </ig:WebDropDown>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="NormalTD">
                                        <asp:Button ID="btn_confirmPO" runat="server" CssClass="auto-style39" OnClick="btn_confirmPO_Click" Text="S" />
                                    </td>
                                    <td class="NormalTD">Delivery Note /Invoice #: </td>
                                    <td class="NormalTD">
                                        <asp:TextBox ID="txt_deliverynote" runat="server" CssClass="auto-style39"></asp:TextBox>
                                    </td>
                                    <td class="NormalTD">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Apply to Selected" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD">
                                        <asp:Label ID="lbl_mssgdisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </td>
                                    <td class="NormalTD">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="upd_grid"  UpdateMode="Conditional"  runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="tbl_Podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri" Width="90%">
                                        <Columns>

                                             <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    

                                         
                                            <asp:BoundField DataField="Ponum" HeaderText="Ponum" />
                                             <asp:TemplateField HeaderText="Type">
                                               
                                                 <ItemTemplate>
                                                    <asp:Label ID="lbl_potype" CssClass="txt_potype" runat="server" Text='<%# Bind("POType") %>'></asp:Label>
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PoDet_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_podet_pk" runat="server" Text='<%# Bind("PoDet_PK") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="ItemColor" HeaderText="IColor" />
                                            <asp:BoundField DataField="ItemSize" HeaderText="ISize" />
                                            <asp:BoundField DataField="SupplierColor" HeaderText="S Color" />
                                            <asp:BoundField DataField="Suppliersize" HeaderText="S Size" />
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_UOM" runat="server" Height="16px" Width="80px" Text='<%# Bind("UOMCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="POQty">
                                              
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_poqty" CssClass="poqty" runat="server" Text='<%# Bind("POQty") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                            <asp:BoundField DataField="AddedQty" HeaderText="Added Qty" />
                                            <asp:TemplateField HeaderText="Excess Qty">
                                                <ItemTemplate>
                                                   <asp:Label ID="lbl_excessqty"  CssClass="txtexcess" runat="server" Height="16px" Width="80px" Text='<%# Bind("ExtraQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="BAl Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_bal"  CssClass="txtbal" runat="server" Height="16px" Width="80px" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_qty" CssClass="txtQty" Text='<%# Bind("BalanceQty") %>' onkeypress="return isNumberKey(event,this)" onkeyup ="validateQty(this)" Height="16px" Width="70px" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Excess Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_newExcessqty" CssClass="txt_newExcessqty" Text="0" onkeypress="return isNumberKey(event,this)" onkeyup ="validateExcessQty(this)" Height="16px" Width="70px" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="DO/Inv#">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_do" Height="16px" Width="120px" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ETA">
                                                <ItemTemplate>
                                                    <ig:webdatepicker ID="wdp_etadate" Height="16px" Width="120px" runat="server">
                                                    </ig:WebDatePicker>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="style1">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="style7">
                                        <asp:UpdatePanel ID="Upd_save" UpdateMode="Conditional"  runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btn_saveMrn" runat="server" OnClick="btn_saveMrn_Click" Text="Save Details" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="updstatus0" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="Messaediv1" runat="server">
                                        <asp:Label ID="lbl_msg0" runat="server" Text="*"></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
               

                <br />



            </asp:Panel>
            
        </td>
    </tr>
    <tr>
        <td class="auto-style16">
            <asp:SqlDataSource ID="recieptdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [DocNum], [Doc_Pk] FROM [DocMaster]">
            </asp:SqlDataSource>
        </td>
        <td class="style3">
                <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
                       <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]" ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>"></asp:SqlDataSource>         </td>
        <td class="NormalTD">
            <asp:SqlDataSource ID="podata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT ProcurementMaster.PONum, ProcurementMaster.PO_Pk, ProcurementMaster.IsApproved, DocMaster.Doc_Pk, ProcurementMaster.AtcId FROM ProcurementMaster INNER JOIN DocMaster ON ProcurementMaster.Supplier_Pk = DocMaster.Supplier_PK WHERE (ProcurementMaster.IsApproved = N'Y') AND (DocMaster.Doc_Pk = @Param1) AND (ProcurementMaster.AtcId = @Param2)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hdn_rcptnum" DefaultValue="0" Name="Param1" PropertyName="Value" />
                    <asp:ControlParameter ControlID="hdn_atc" DefaultValue="0" Name="Param2" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td class="NormalTD">
            <asp:HiddenField ID="hdn_rcptnum" runat="server" />
        </td>
        <td class="NormalTD">
            <asp:HiddenField ID="hdn_atc" runat="server" />
        </td>
        <td class="NormalTD">
            &nbsp;</td>
        <td class="NormalTD">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
    </asp:Content>
