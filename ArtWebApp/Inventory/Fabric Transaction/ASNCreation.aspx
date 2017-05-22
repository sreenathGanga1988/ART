<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ASNCreation.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.ASNCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style39 {}
    .auto-style40 {
        height: 27px;
    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <link href="../../css/style.css" rel="stylesheet" />

</div>
        <table  class="DataEntryTable">
                     <tr>
                         <td class="RedHeadding" colspan="6" >Supplier ASN Generator</td>
                     </tr>
                     <tr>
                         <td >
                            
                             Supplier</td>
                         <td >
                             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                 <ContentTemplate>
                                     <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         </td>
                         <td >&nbsp;</td>
                         <td >
                             &nbsp;</td>
                         <td >&nbsp;</td>
                         <td >
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td class="auto-style40" >Supplier INV # :</td>
                         <td class="auto-style40" >
                             <asp:TextBox ID="txt_doc" runat="server" Width="189px" CssClass="auto-style39"></asp:TextBox>
                         </td>
                         <td class="auto-style40" >ETA ETd Date </td>
                         <td class="auto-style40" >
                             <ig:WebDatePicker ID="dtp_deliverydate" runat="server" Height="19px" Width="171px">
                             </ig:WebDatePicker>
                         </td>
                         <td class="auto-style40" ></td>
                         <td class="auto-style40" >
                             </td>
                     </tr>
                     <tr>
                         <td >container # :>
                         <td >
                             <asp:TextBox ID="txt_container" runat="server" Width="185px" CssClass="auto-style39"></asp:TextBox>
                         </td>
                         <td >Remark : </td>
                         <td >
                             <textarea id="txta_remark" runat="server" cols="20" name="S1" rows="2" class="auto-style39"></textarea></td>
                         <td ></td>
                         <td ></td>
                     </tr>
                     <tr>
                         <td >&nbsp;</td>
                         <td >
                             <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
                         </td>
                         <td >
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                     <asp:Button ID="btn_savercpt" runat="server" CssClass="auto-style39" OnClick="btn_savercpt_Click" Text="Save Document" Height="26px" />
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         </td>
                         <td >
                             &nbsp;</td>
                         <td >&nbsp;</td>
                         <td >&nbsp;</td>
                     </tr>
                     <tr>
                         <td colspan="6" >
                             
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                 <ContentTemplate>
                             <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>   </ContentTemplate>
                             </asp:UpdatePanel></td>
                     </tr>
                 </table>
</asp:Content>
