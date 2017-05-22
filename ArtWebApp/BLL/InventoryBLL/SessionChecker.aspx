<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SessionChecker.aspx.cs" Inherits="ArtWebApp.BLL.InventoryBLL.SessionChecker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style8 {
            width: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div>
       
   <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" style="height: 26px" />
        
        
       
  
        
        <table class="auto-style8">
            <tr>
               <td class="NormalTD">Online Users</td>
               <td class="NormalTD">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
            </tr>
            <tr>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
            </tr>
            <tr>
               <td class="NormalTD">Username<asp:Button ID="Username" runat="server" OnClick="Username_Click" Text="Button" />
                </td>
               <td class="NormalTD">
                   <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Send Logout Message" />
                </td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
            </tr>
            <tr>
               <td class="NormalTD">
                   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                       <AlternatingRowStyle BackColor="#CCCCCC" />
                       <Columns>
                           <asp:BoundField DataField="Username" HeaderText="Username" />
                           <asp:BoundField DataField="SessionID" HeaderText="SessionID" />
                       </Columns>
                       <FooterStyle BackColor="#CCCCCC" />
                       <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                       <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                       <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                       <SortedAscendingCellStyle BackColor="#F1F1F1" />
                       <SortedAscendingHeaderStyle BackColor="#808080" />
                       <SortedDescendingCellStyle BackColor="#CAC9C9" />
                       <SortedDescendingHeaderStyle BackColor="#383838" />
                   </asp:GridView>
                </td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
            </tr>
            <tr>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
               <td class="NormalTD">&nbsp;</td>
            </tr>
        </table>
       
  
        
</div>
   
</asp:Content>