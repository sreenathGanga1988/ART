<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="TrApproval.aspx.cs" Inherits="ArtWebApp.Shipping.LC.TrApproval" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"><link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
     
    </style>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="DataEntryTable">
        <tr>
            <td>
                <table class="tittlebar">
                    <tr>
                        <td class="RedHeadding" colspan="5">TR Approval</td>
                       
                        <td class="RedHeadding">&nbsp;</td>
                       
                    </tr>
                                       
                  
                  
                </table>
            </td>
        </tr>
        <tr>
            <td>  <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
        <tr class="gridtable">
            <td>       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" DataKeyNames="BankAdvice_Pk" DataSourceID="tradata">
                            <Columns>
                                  <asp:TemplateField>                                   
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BankAdvice_Pk" InsertVisible="False" SortExpression="BankAdvice_Pk">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BankAdvicePk" runat="server" Text='<%# Bind("BankAdvice_Pk") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ATCnum" HeaderText="ATCnum" SortExpression="ATCnum" />
                                <asp:BoundField DataField="PONUM" HeaderText="PONUM" SortExpression="PONUM" />
                                <asp:BoundField DataField="TrValue" HeaderText="TrValue" SortExpression="TrValue" />
                                <asp:BoundField DataField="Docnum" HeaderText="Docnum" SortExpression="Docnum" />
                                <asp:BoundField DataField="LCNum" HeaderText="LCNum" SortExpression="LCNum" />
                                <asp:BoundField DataField="BankName" HeaderText="BankName" SortExpression="BankName" />
                                <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                <asp:BoundField DataField="Issuedate" HeaderText="Issuedate" SortExpression="Issuedate" />
                                <asp:BoundField DataField="ExpiryDate" HeaderText="ExpiryDate" SortExpression="ExpiryDate" />
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
</td></tr><tr><td>

              <asp:Button ID="Button1" runat="server" Text="Approve TR" OnClick="Button1_Click" />

              </td></tr>
        </table>
     <asp:SqlDataSource ID="tradata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        LCBankAdviceDetail.BankAdvice_Pk, LCDetails.ATCnum, LCDetails.PONUM, LCBankAdviceDetail.TrValue, LCBankAdviceDetail.Docnum, LCMaster.LCNum, BankMaster.BankName, SupplierMaster.SupplierName, 
                         LCMaster.Issuedate, LCMaster.ExpiryDate, LCBankAdviceDetail.IsApproved
FROM            LCBankAdviceDetail INNER JOIN
                         LCDetails ON LCBankAdviceDetail.LCDet_PK = LCDetails.LCDet_PK INNER JOIN
                         LCMaster ON LCDetails.LC_PK = LCMaster.LC_PK INNER JOIN
                         BankMaster ON LCMaster.Bank_PK = BankMaster.Bank_PK INNER JOIN
                         SupplierMaster ON LCMaster.Supplier_pk = SupplierMaster.Supplier_PK
WHERE        (LCBankAdviceDetail.IsApproved = N'A')"></asp:SqlDataSource>
    </asp:Content>

