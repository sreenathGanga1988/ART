<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MRN.aspx.cs" Inherits="ArtWebApp.Inventory.MRN" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../JQuery/GridJQuery.js"></script>
      <script type="text/javascript">

       //calculate the sum of qty on keypress
       function ValidateExtraQty(objText) {
       
        //   alert(objText.value);
           var cell = objText.parentNode;
           var row = cell.parentNode;
          
           var sum = 0;
           var textboxextra = row.getElementsByClassName("txtextra");

           var txtbalextraqty = row.getElementsByClassName("txtBalanceDocExtra");
          
           if (parseFloat(textboxextra[0].value) > parseFloat(txtbalextraqty[0].innerText))
           {
              

              
               alert("Excess Qty greater than DOC Extra not Allowed");
               textboxextra[0].value = 0;
           }
           else {


           }
              
         

       }

       function ValidateQty(objText) {

           //   alert(objText.value);
           var cell = objText.parentNode;
           var row = cell.parentNode;

           var sum = 0;
           var txtPOBalance = row.getElementsByClassName("txtPOBalance");



           if (parseFloat(txtPOBalance[0].innerText)>0)
           {

               var txtbalDocqty = row.getElementsByClassName("txtbalqty");

               if (parseFloat(txtPOBalance[0].innerText) < parseFloat(txtbalDocqty[0].innerText))
               {
                   // if poblance less than doc balance
                   if(parseFloat(txtPOBalance[0].innerText) < parseFloat(objText.value))
                   {
                       alert("Qty cannot be more than PO Balance Qty ");
                       objText.value = 0;
                   }
               }
               else {

                   if (parseFloat(txtbalDocqty[0].innerText) < parseFloat(objText.value)) {
                       alert("Qty cannot be more than DOC Balance ");
                       objText.value = 0;
                   }
               }
           }
           else
           {
               alert("Full Qty MRN Already made");
           }



         



       }




    








          </script>
     <style type="text/css">
       
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
     <tr class="SUBRedHeadding" >
        <td colspan="8">



             RECEIPT</td>

     </tr>

      
     <tr>
        <td colspan="8" >

            <div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                   <ContentTemplate>
                       <table class="DataEntryTable">
                     <tr>
                       <td class="NormalTD" >Supplier : </td>
                       <td class="NormalTD" >
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                     <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
                                     
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         </td>
                       <td class="NormalTD" >
                            <asp:Button ID="Button1" runat="server" Text="S" Height="26px" OnClick="Button1_Click" ToolTip="Press here to see all the reciepts of selected supplier" /> </td>
                       <td class="NormalTD" ></td>
                       <td class="NormalTD" ></td>
                         <td class="NormalTD"  ></td>
                     </tr>
                     <tr>
                         <td class="NormalTD" >Container # :</td>
                         <td  class="NormalTD">
                             <asp:TextBox ID="txt_container" runat="server" Width="189px" ></asp:TextBox>
                         </td>
                         <td  class="NormalTD" >In House Date</td>
                       <td class="NormalTD" >
                             <ig:WebDatePicker ID="dtp_inhousedate" runat="server">
                             </ig:WebDatePicker>

                            
                         </td>
                         <td  class="NormalTD" > <ig:WebDatePicker ID="dtp_deliverydate" runat="server" Height="19px" Width="171px">
                             </ig:WebDatePicker></td>
                         <td class="NormalTD" >
                             
                         </td>
                     </tr>
                     <tr>
                       <td class="NormalTD" >BOE # :</td>
                       <td class="NormalTD" >
                             <asp:TextBox ID="txt_boe" runat="server" Width="185px" ></asp:TextBox>
                         </td>
                       <td class="NormalTD" >Remark : </td>
                       <td class="NormalTD" >
                             <textarea id="txta_remark" runat="server" cols="20" name="S1" rows="2" ></textarea></td>
                       <td class="NormalTD" >&nbsp;</td>
                         <td class="NormalTD">&nbsp;</td>
                     </tr>
                     
                       
                           <tr >
                               <td colspan="8"> <asp:Button ID="btn_savercpt" runat="server" OnClick="btn_savercpt_Click" Text="Save Receipt" /></td>
                           </tr>    
                           
                          
                           
                 </table>
                   </ContentTemplate>
               </asp:UpdatePanel>
            </div>
             


                
             

         </td>

     </tr>
        <tr>

             <td  colspan="8" >
                  <asp:UpdatePanel ID="updstatus" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>

                       <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>

                   </ContentTemplate></asp:UpdatePanel>
                   
                 </td>

        </tr>
    <tr>
        <td  colspan="8" >
           
          

              
      <table class="DataEntryTable">
                    <tr>
                        <td>

                            <div>

                                <table class="DataEntryTable">
                                <tr class="SUBRedHeadding">
                                    <td colspan="5">MRN</td>
                                </tr>
                                <tr>
                                  <td class="NormalTD" >Receipt #&nbsp; :</td>
                                    <td class="NormalTD" >
                                          <asp:UpdatePanel ID="Upd_rxptdrop" UpdateMode="Conditional"  runat="server">
  <ContentTemplate>
                                        <ucc:DropDownListChosen ID="drp_rcpt" runat="server" DataSourceID="recieptdata" DataTextField="RecieptNum" DataValueField="Reciept_Pk" DisableSearchThreshold="10" Width="200px">
                                        </ucc:DropDownListChosen>
         </ContentTemplate>
                </asp:UpdatePanel>
                                    </td>
                                    <td class="SearchButtonTD" >
                                        <asp:Button ID="btn_confirmRcpt" runat="server" OnClick="btn_confirmRcpt_Click" Text="S" />
                                    </td>
                                  <td class="NormalTD" ></td>
                                  <td class="NormalTD" ></td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">Po # </td>
                                  <td class="NormalTD" >
                                        <ucc:DropDownListChosen ID="drp_po" runat="server" DataSourceID="podata" DataTextField="PONum" DataValueField="PO_Pk" DisableSearchThreshold="10" Width="200px">
                                        </ucc:DropDownListChosen>
                                    </td>
                                  <td class="SearchButtonTD" >
                                        <asp:Button ID="btn_confirmPO" runat="server"  OnClick="btn_confirmPO_Click" Text="S" />
                                    </td>
                                  <td class="NormalTD" >Delivery Note # : </td>
                                  <td class="NormalTD" >
                                        <asp:TextBox ID="txt_deliverynote" runat="server" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">ADN # :</td>
                                  <td class="NormalTD" >
                                        <ucc:DropDownListChosen ID="drp_doc" runat="server" DisableSearchThreshold="10" Width="200px" DataTextField="name" DataValueField="pk">
                                        </ucc:DropDownListChosen></td>
                                  <td class="SearchButtonTD" >
                                        <asp:Button ID="btn_confirmADN" runat="server"  Text="S" OnClick="btn_confirmADN_Click" />
                                    </td>
                                  <td class="NormalTD" >adn TYpe</td>
                                  <td class="NormalTD" >
                                        <asp:Label ID="lbl_adntype" runat="server" Text="NA"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">exp #: </td>
                                  <td class="NormalTD" >
                                        <asp:Label ID="lbl_expnum" runat="server" Text="NA"></asp:Label>
                                    </td>
                                  <td class="SearchButtonTD" >
                                        &nbsp;</td>
                                  <td class="NormalTD" >&nbsp;</td>
                                  <td class="NormalTD" >
                                        &nbsp;</td>
                                </tr>
                            </table>
                            </div>
                            
                        </td>
                    </tr>
                    <tr class="smallgridtable">
                        <td>
                            <div>

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
                                    <asp:TemplateField HeaderText="PoDet_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_podet_pk" runat="server" Text='<%# Bind("PoDet_PK") %>'></asp:Label>
                                        </ItemTemplate>

<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="SkuDet_pk" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_skudet_pk" runat="server" Text='<%# Bind("SkuDet_pk") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" />
                                    <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" />
                                    <asp:BoundField DataField="SupplierColor" HeaderText="S Color" />
                                    <asp:BoundField DataField="Suppliersize" HeaderText="S Size" />
                                    <asp:TemplateField HeaderText="UOM" >
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UOM" runat="server" Text='<%# Bind("UOMCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="POQty">
                                       
                                         <ItemTemplate>
                                             <asp:Label ID="lbl_poaty" CssClass="txtPoqty"  runat="server" Text='<%# Bind("POQty") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CURate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_curate"  runat="server" Text='<%# Bind("CURate") %>'></asp:Label>
                                        </ItemTemplate>

<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ReceivedQty" HeaderText="RecievedQty" />
                                    

                                    <asp:TemplateField HeaderText="PO Balance">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_POBalance"  CssClass="txtPOBalance"  runat="server" Text='<%# Bind("POBalance") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="DocQty">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DocQty"  CssClass="txtDocQty"  runat="server" Text='<%# Bind("DocQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Doc Rcvd Qty">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DocRcvdQty"  CssClass="txtDocRcvdQty"  runat="server" Text='<%# Bind("DocRcvdQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Doc Bal Qty">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_balnceqty"  CssClass="txtbalqty"  runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Reciept Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_reciept"  onkeypress="return isNumberKey(event,this)"  onkeyup ="return ValidateQty(this)" Height="16px" Width="70px" runat="server">0</asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="Doc Extra Qty">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DocExtraQty"  CssClass="txtDocExtraQty"  runat="server" Text='<%# Bind("DocExtraQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Doc Extra Rcvd Qty">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_DocExtraRcvdQty"  CssClass="txtDocExtraRcvdQty"  runat="server" Text='<%# Bind("DocExtraRcvdQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Balance Doc Extra">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_BalanceDocExtra"  CssClass="txtBalanceDocExtra"  runat="server" Text='<%# Bind("BalanceDocExtra") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Extra">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_extra" CssClass="txtextra" onkeypress="return isNumberKey(event,this)" onkeyup ="return ValidateExtraQty(this)" runat="server" Height="16px" Width="70px" >0</asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    


                                    
                                    <asp:TemplateField HeaderText="Doc_Pk" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Doc_Pk" runat="server" Text='<%# Bind("Doc_Pk") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Uom_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_uom_pk" runat="server" Text='<%# Bind("Uom_PK") %>'></asp:Label>
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
                            </div>

                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="style1">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="auto-style24">&nbsp;</td>
                                    <td class="style7">
                                        <asp:Button ID="btn_saveMrn" runat="server" OnClick="btn_saveMrn_Click"   Text="Save MRN to Reciept" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td><asp:Button ID="btn_excess" runat="server" OnClick="btn_excess_Click" Text="Save Excess MRN to Reciept" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                      <td class="NormalTD" >
                            
                            
                            
                           </td>
                    </tr>
                </table>


   
               

                
               

               



            
            
        </td>
    </tr>

    <tr>
        <td class="auto-style16">
            <asp:SqlDataSource ID="recieptdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT Reciept_Pk, RecieptNum FROM RecieptMaster WHERE (IsCompleted = @IsCompleted) AND (RecptLocation_PK = @Param1)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="N" Name="IsCompleted" Type="String" />
                    <asp:SessionParameter DefaultValue="0" Name="Param1" SessionField="UserLoc_pk" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td class="style3">
                <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
                                </td>
        <td class="auto-style26">
            <asp:SqlDataSource ID="podata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT ProcurementMaster.PONum, ProcurementMaster.PO_Pk, ProcurementMaster.IsApproved, RecieptMaster.Reciept_Pk, ProcurementMaster.IsNormal FROM RecieptMaster INNER JOIN ProcurementMaster ON RecieptMaster.Supplier_PK = ProcurementMaster.Supplier_Pk WHERE (ProcurementMaster.IsApproved = N'Y') AND (RecieptMaster.Reciept_Pk = @Param1) AND (ProcurementMaster.IsNormal = N'Y')">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hdn_rcptnum" DefaultValue="0" Name="Param1" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
        <td class="auto-style16">
            <asp:HiddenField ID="hdn_rcptnum" runat="server" />
        </td>
        <td class="style6">
            &nbsp;</td>
        <td class="style18">
            &nbsp;</td>
        <td class="style8">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>
