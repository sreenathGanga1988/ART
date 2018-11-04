<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ADNClosing.aspx.cs" Inherits="ArtWebApp.Inventory.ADNClosing" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr class="SUBRedHeadding" >
        <td colspan="4">
             ADN CLOSING</td>
     </tr>
        <tr>
            <td class="NormalTD">Closing Date</td>
            <td> <ig:WebDatePicker ID="dtp_dodate" runat="server" Height="26px" Width="191px">
                        </ig:WebDatePicker></td>
            <td> <asp:Button ID="btn_confirmAtc" runat="server" Text="S" Width="33px" OnClick="btn_confirmAtc_Click" CssClass="auto-style10" style="height: 26px" /></td>
            <td>&nbsp;</td>
        </tr>
        
        <tr>
            <td class="gridtable" colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" BackColor="White" AutoGenerateColumns="False" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                    <Columns>
                                            <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick ="Check_Click(this)" AutoPostBack="True"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc_Pk">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoc_Pk" runat="server" Text='<%# Bind("Doc_Pk") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" />
                                        <asp:BoundField DataField="PONum" HeaderText="PONum" />
                                        <asp:BoundField DataField="MrnNum" HeaderText="MrnNum" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="DOC_Qty" HeaderText="DOC_Qty" />
                                        <asp:BoundField DataField="ExtraQty" HeaderText="ExtraQty" />
                                        <asp:BoundField DataField="MRN_Qty" HeaderText="MRN_Qty" />
                                        <asp:BoundField DataField="MRNExtraQty" HeaderText="MRNExtraQty" />
                                         <asp:BoundField DataField="FabRecv" HeaderText="FabRecvQty" />
                                        <asp:BoundField DataField="RollCount" HeaderText="RollCount" />
                                       
                                       
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
            <td class="auto-style2"> &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD" colspan="2"> <asp:Button ID="btn_closeadn" runat="server" Text="Close ADN" Width="149px" OnClick="btn_adnclose_Click" CssClass="auto-style10" /></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
         <tr class="ButtonTR">
                    <td colspan="4" >
                       <div id="Messaediv" runat="server">
                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                     
               </div></td>
                </tr>
    </table>

    
   
</asp:Content>
