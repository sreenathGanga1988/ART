<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="LCMaster.aspx.cs" Inherits="ArtWebApp.Shipping.LC.LCMaster" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"><link href="../../css/style.css" rel="stylesheet" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="DataEntryTable">
        <tr>
            <td>
                <table class="tittlebar">
                    <tr>
                        <td class="RedHeadding" colspan="4">LC&nbsp; data Entry</td>
                       
                    </tr>
                    <tr>
                        <td class="NormalTD">Lc Num#:</td>
                        <td class="NormalTD">
                            <asp:TextBox ID="txt_lcnum" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD">Bank</td>
                        <td class="NormalTD"><ig:WebDropDown ID="drp_bank" runat="server" DataSourceID="BankDataSource" Width="200px" TextField="BankName" ValueField="Bank_PK">
                             <DropDownItemBinding TextField="BankName" ValueField="Bank_PK" />
                         </ig:WebDropDown></td>
                       
                    </tr>
                    <tr>
                        <td>Supplier : </td>
                        <td>
                            <ig:WebDropDown ID="drp_supplier" runat="server" DataSourceID="supplierdata" Height="23px" style="height: 23px" TextField="SupplierName" ValueField="Supplier_PK" Width="200px">
                                         <DropDownItemBinding TextField="SupplierName" ValueField="Supplier_PK" />
                                     </ig:WebDropDown></td>
                        <td>Value </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>Issue Date :</td>
                        <td>
                            <ig:WebDatePicker ID="dtp_issuedate" runat="server">
                            </ig:WebDatePicker>
                        </td>
                        <td>Expiry date :</td>
                        <td>
                            <ig:WebDatePicker ID="dtp_expirydate" runat="server">
                            </ig:WebDatePicker>
                        </td>
                        
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit LC" />
                        </td>
                        <td>
                              </td>
                        
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

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" DataKeyNames="LC_PK" DataSourceID="lcdata">
                            <Columns>
                                <asp:BoundField DataField="LC_PK" HeaderText="LC_PK" InsertVisible="False" ReadOnly="True" SortExpression="LC_PK" />
                                <asp:BoundField DataField="LCNum" HeaderText="LCNum" SortExpression="LCNum" />
                                <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                <asp:BoundField DataField="BankName" HeaderText="BankName" SortExpression="BankName" />
                                <asp:BoundField DataField="Issuedate" HeaderText="Issuedate" SortExpression="Issuedate" />
                                <asp:BoundField DataField="ExpiryDate" HeaderText="ExpiryDate" SortExpression="ExpiryDate" />
                                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                                <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" SortExpression="AddedDate" />
                                <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" SortExpression="AddedBy" />
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
</td>
        </tr>
    </table>
    <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
  <asp:SqlDataSource ID="BankDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Bank_PK], [BankName] FROM [BankMaster]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="lcdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT LCMaster.LC_PK, LCMaster.LCNum, SupplierMaster.SupplierName, BankMaster.BankName, LCMaster.Issuedate, LCMaster.ExpiryDate,LCMaster.Status, LCMaster.Value, LCMaster.AddedDate, LCMaster.AddedBy FROM LCMaster INNER JOIN SupplierMaster ON LCMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN BankMaster ON LCMaster.Bank_PK = BankMaster.Bank_PK ORDER BY LCMaster.LC_PK DESC"></asp:SqlDataSource>

</asp:Content>
