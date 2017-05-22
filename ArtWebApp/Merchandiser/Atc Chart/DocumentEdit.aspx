<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DocumentEdit.aspx.cs" Inherits="ArtWebApp.Merchandiser.Atc_Chart.MerchandiserEta" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
   
 
        }
   
        .auto-style9 {
            height: 27px;
            width: 35px;
        }
   
        .auto-style10 {
            margin-bottom: 0px;
        }
   
    </style>

<%--lbl_poqty
lbl_docqty
lbl_docextra
lbl_mrnqty
lbl_mrnextra
lbl_totaldocqty
lbl_totaldocextra
txt_qty
txt_extraqty--%>
      <script type="text/javascript">

       //calculate the sum of qty on keypress
       function validateQty(objText) {
           debugger;
        
           var cell = objText.parentNode;
           var row = cell.parentNode; 

           var sum = 0; 

           var newqtytextbox = row.getElementsByClassName("txt_qty");
           var poqty = row.getElementsByClassName("lbl_poqty");
           var lbl_docqty = row.getElementsByClassName("lbl_docqty");
           var lbl_totaldocqty = row.getElementsByClassName("lbl_totaldocqty");
           var lbl_mrnqty = row.getElementsByClassName("lbl_mrnqty");
           
           if (parseFloat(newqtytextbox[0].value) >= parseFloat(lbl_docqty[0].innerText))
           {
               //if adding value

               //if((totaldocqty-docqty)+newqty)>poqty)
               if (parseFloat(((lbl_totaldocqty[0].innerText) - parseFloat(lbl_docqty[0].innerText)) + parseFloat(newqtytextbox[0].value)) > parseFloat(poqty[0].innerText) )
               {
                   newqtytextbox[0].value = 0;
                   alert("Extra Qty Cannot be greater than POqty");
               }
               
             
           }else
           {
              // if reducing
               if(parseFloat(newqtytextbox[0].value)< parseFloat(lbl_mrnqty[0].innerText) )
               {
                   newqtytextbox[0].value = 0;
                   alert("Cannot Reduce Qty Mrn Already Done");
               }
           }


       }

    
       function validateExcessQty(objText) {
           debugger;
           
           
           var cell = objText.parentNode;
           var row = cell.parentNode;

           var sum = 0; 
           var txt_extraqty = row.getElementsByClassName("txt_extraqty");
           var poqty = row.getElementsByClassName("lbl_poqty");
           var lbl_docextra = row.getElementsByClassName("lbl_docextra");
           var lbl_totaldocextra = row.getElementsByClassName("lbl_totaldocextra");
           var lbl_mrnextra = row.getElementsByClassName("lbl_mrnextra");
           var allowedexcess = 0;
           if (txt_potype[0].innerText.trim() == "F")
           {
               allowedexcess = (3 / 100) * parseFloat(lbl_poqty[0].innerText);
           }
         

           if (parseFloat(((lbl_totaldocextra[0].innerText) - parseFloat(lbl_docextra[0].innerText)) + parseFloat(txt_extraqty[0].value)) > parseFloat(allowedexcess))
           {
               txt_newExcessqty[0].value = 0;
               alert("Extra Qty Cannot be Allowed over 3 %");
           }

          

       }


</script>







    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
     <tr>
        <td  class="RedHeadding" colspan="8" >



             atraco exports/ kenya import chart Edit</td>

     </tr>
       
     <tr>
        <td  colspan="8">

                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <ContentTemplate >

                         <table  class="DataEntryTable">
               
                             <tr>
                                 <td>Supplier : </td>
                                 <td>
                                     


                                       <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10" Width="200px">
</ucc:DropDownListChosen>
                                 </td>
                                 <td>
                                     <asp:Button ID="btn_confrmsup" runat="server" OnClick="btn_confrmsup_Click" Text="S" />
                                 </td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                                 <td>&nbsp;</td>
                             </tr>
                                   <tr>
                         <td class="NormalTD" > DOC # :</td>
                         <td class="NormalTD" >
                             
                                  
                                               <ucc:DropDownListChosen ID="drp_rcptmstr" runat="server" DataSourceID="recieptdata" DataTextField="DocNum" DataValueField="Doc_Pk" DisableSearchThreshold="10" Width="200px">
</ucc:DropDownListChosen>

                                           </td>
                         <td class="NormalTD" >
                             <asp:Button ID="btn_confirmRcpt" runat="server" CssClass="auto-style39" OnClick="btn_confirmRcpt_Click" Text="S" /></td>
                         <td class="NormalTD" ></td>
                         <td class="NormalTD" ></td>
                         <td class="NormalTD" ></td>
                     </tr>
                     <tr>
                         <td class="NormalTD" >Supplier Document # :</td>
                         <td class="NormalTD" >
                             <asp:TextBox ID="txt_container" runat="server" Width="189px" CssClass="auto-style39"></asp:TextBox>
                         </td>
                         <td class="NormalTD" >ETA Date </td>
                         <td class="NormalTD" >
                             <ig:WebDatePicker ID="dtp_deliverydate" runat="server" Height="19px" Width="171px">
                             </ig:WebDatePicker>
                         </td>
                         <td class="NormalTD" >&nbsp;</td>
                         <td class="NormalTD" >
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td class="NormalTD" >container # :</td>
                         <td class="NormalTD" >
                             <asp:TextBox ID="txt_boe" runat="server" Width="185px" CssClass="auto-style39"></asp:TextBox>
                         </td>
                         <td class="NormalTD" >Remark : </td>
                         <td class="NormalTD" >
                             <textarea id="txta_remark" runat="server" cols="20" name="S1" rows="2" class="auto-style39"></textarea></td>
                         <td class="NormalTD" ></td>
                         <td class="NormalTD" ></td>
                     </tr>
                             <tr>
                                 <td class="NormalTD">adn type</td>
                                 <td class="NormalTD">
                                     <ucc:DropDownListChosen ID="ddl_adnType" runat="server" DisableSearchThreshold="10" Width="200px">
                                         <asp:ListItem>Select</asp:ListItem>
                                         <asp:ListItem Value="LocalUAE">Local UAE ADN</asp:ListItem>
                                         <asp:ListItem Value="LocalKenya">Local Kenyan ADN</asp:ListItem>
                                         <asp:ListItem Value="IntlSupplier">International ADN</asp:ListItem>
                                     </ucc:DropDownListChosen>
                                 </td>
                                 <td class="NormalTD">&nbsp;</td>
                                 <td class="NormalTD">&nbsp;</td>
                                 <td class="NormalTD">&nbsp;</td>
                                 <td class="NormalTD">&nbsp;</td>
                             </tr>
                     <tr>
                         <td class="NormalTD" >&nbsp;</td>
                         <td class="NormalTD" >&nbsp;</td>
                         <td class="NormalTD" >
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                     <asp:Button ID="btn_savercpt" runat="server" CssClass="auto-style39" OnClick="btn_savercpt_Click" Text="Save Document" />
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         </td>
                         <td class="NormalTD" >
                             <asp:Label ID="lbl_errordisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                         </td>
                         <td class="NormalTD" >&nbsp;</td>
                         <td class="NormalTD" >&nbsp;</td>
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
                                    <td class="RedHeadding" colspan="6">PO Eta</td>
                                </tr>
                                <tr>
                                    <td  >ADN&nbsp; #&nbsp; :</td>
                                    <td >
                                        
                                        <ucc:DropDownListChosen ID="drp_rcpt" runat="server" DataSourceID="recieptdata" DataTextField="DocNum" DataValueField="Doc_Pk" DisableSearchThreshold="10" Width="200px">
                                        </ucc:DropDownListChosen>
                                        
                                    </td>
                                      <td class="auto-style9">
                                        <asp:Button ID="Button3" runat="server" CssClass="auto-style39"  Text="S" OnClick="Button3_Click" style="width: 23px" />
                                    </td>
                                    <td  >ETA Date :</td>
                                    <td  >
                                        <asp:UpdatePanel ID="upd_eta" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <ig:WebDatePicker ID="dtp_eta" runat="server" Height="19px" Width="171px">
                                                </ig:WebDatePicker>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td >
                                        <asp:UpdatePanel ID="upd_etabutton" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Apply ETA to Selected" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalTD" >&nbsp;</td>
                                    <td class="NormalTD" >
                                        &nbsp;</td>
                                    <td class="auto-style9" >
                                        &nbsp;</td>
                                    <td class="NormalTD" >Delivery Note /Invoice #: </td>
                                    <td class="NormalTD" >
                                        <asp:TextBox ID="txt_deliverynote" runat="server" CssClass="auto-style39"></asp:TextBox>
                                    </td>
                                    <td class="NormalTD" >
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Apply to Selected" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalTD" >
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chk_selectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chk_selectAll_CheckedChanged" Text="Select All" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="NormalTD" >&nbsp;</td>
                                    <td class="auto-style9" >&nbsp;</td>
                                    <td class="NormalTD" >&nbsp;</td>
                                    <td class="NormalTD" >
                                        <asp:Label ID="lbl_mssgdisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </td>
                                    <td class="NormalTD" >&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="smallgridtable">
                                <asp:UpdatePanel ID="upd_grid"  UpdateMode="Conditional"  runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="tbl_Podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri" Width="90%" >
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chk_select" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="DocDet_Pk">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_DocDet_Pk" runat="server" Text='<%# Bind("DocDet_Pk") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Ponum" HeaderText="Ponum" />
                                            <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="ItemColor" HeaderText="I Color" />
                                            <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" />
                                            <asp:BoundField DataField="SupplierColor" HeaderText="S Color" />
                                            <asp:BoundField DataField="Suppliersize" HeaderText="S size" />
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_UOM" runat="server" Text='<%# Bind("UOMCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="POQty">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_poqty" CssClass="lbl_poqty" runat="server" Text='<%# Bind("POQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" dOC Qty">
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_docqty" CssClass="lbl_docqty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="dOC Extra">
                                              
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_docextra" CssClass="lbl_docextra" runat="server" Text='<%# Bind("ExtraQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRN Qty">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_mrnqty" CssClass="lbl_mrnqty" runat="server" Text='<%# Bind("ReceivedQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRN Extra">
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_mrnextra" CssClass="lbl_mrnextra" runat="server" Text='<%# Bind("ReceivedExtra") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total DocQty">
                                             
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_totaldocqty" CssClass="lbl_totaldocqty" runat="server" Text='<%# Bind("TotalDocQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total ExtraQty">
                                              
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_totaldocextra"  CssClass="lbl_totaldocextra" runat="server" Text='<%# Bind("TotalExtraQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            
                                            <asp:TemplateField HeaderText="Qty" ItemStyle-Font-Size="Smaller">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_qty" CssClass="txt_qty" Width="70px" Text='<%# Bind("Qty") %>' runat="server"  onChange="validateQty(this)" Font-Size="Smaller"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="Smaller" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="ExtraQty" ItemStyle-Font-Size="Smaller">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_extraqty" CssClass="txt_extraqty" Width="70px" Text='<%# Bind("ExtraQty") %>' onChange="validateExcessQty(this)" runat="server" Font-Size="Smaller"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="Smaller" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="DO/Inv#">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_do"  Text='<%# Bind("Donumber") %>' Font-Size="Smaller"  Width="70px" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ETA">
                                                <ItemTemplate>
                                                    <ig:WebDatePicker ID="wdp_etadate"  Font-Size="Smaller" runat="server">
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
                             </div>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table >
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="NormalTD" >&nbsp;</td>
                                    <td class="NormalTD" >
                                        <asp:UpdatePanel ID="Upd_save" UpdateMode="Conditional"  runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btn_saveMrn" runat="server" OnClick="btn_saveMrn_Click" Text="Update Details" CssClass="auto-style10" />
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
                        <td class="NormalTD" ></td>
                    </tr>
                </table>
               

                <br />



            </asp:Panel>
            
        </td>
    </tr>
    <tr>
        <td class="NormalTD" >
            <asp:SqlDataSource ID="recieptdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [DocNum], [Doc_Pk] FROM [DocMaster]">
            </asp:SqlDataSource>
              <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
       
               <asp:SqlDataSource ID="podata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        ProcurementMaster.PONum, ProcurementMaster.PO_Pk, ProcurementMaster.IsApproved, DocMaster.Doc_Pk
FROM            ProcurementMaster INNER JOIN
                         DocMaster ON ProcurementMaster.Supplier_Pk = DocMaster.Supplier_PK
WHERE        (ProcurementMaster.IsApproved = N'Y') AND (DocMaster.Doc_Pk = @Param1)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hdn_rcptnum" DefaultValue="0" Name="Param1" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
            
             <asp:HiddenField ID="hdn_rcptnum" runat="server" />
            
             </td>
        <td >
              
                                </td>
        <td >
         
        </td>
        <td >
           
        </td>
        <td>
            &nbsp;</td>
        <td >
            &nbsp;</td>
        <td >
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
    </asp:Content>