<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SupportTicket.aspx.cs" Inherits="ArtWebApp.Administrator.SupportTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <link href="../css/style.css" rel="stylesheet" />
    
     <style type="text/css">
   
    .TextArea1 {
        height: 253px;
        width: 646px;
    }
       
         .auto-style1 {
             height: 27px;
         }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="DataEntryTable">
                      
                        <tr>
                            <td class="RedHeadding" colspan="5"><strong>Generate Support Ticket</strong></td>
                        </tr>
                        <tr>
                            <td class="NormalTD">&nbsp;Support Tittle :</td>
                            <td class="NormalTD">
                                <asp:TextBox ID="txt_tittle" runat="server" Width="651px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="NormalTD">Description :</td>
                            <td colspan="3" class="NormalTD">
                                <textarea id="txta_desc" class="TextArea1" runat="server" name="S1"></textarea> </td>
                          
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="NormalTD">Priority</td>
                            <td class="NormalTD">
                                <asp:DropDownList ID="drp_priority" runat="server" Height="19px" Width="245px">
                                    <asp:ListItem>Low</asp:ListItem>
                                    <asp:ListItem>Medium</asp:ListItem>
                                    <asp:ListItem>High</asp:ListItem>
                                    <asp:ListItem>Emergency</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="NormalTD">&nbsp;</td>
                            <td class="NormalTD">
                                <asp:Button ID="btn_submit" runat="server" Text="Submit" Width="172px" OnClick="btn_submit_Click" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="NormalTD"></td>
                            <td class="NormalTD">
                                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#FF3300" Text="**"></asp:Label>
                            </td>
                            <td class="auto-style15"></td>
                            <td class="auto-style15"></td>
                            <td class="auto-style15"></td>
                        </tr>
                        <tr>
                            <td class="NormalTD">
                                <asp:Button ID="Button1" runat="server" Font-Size="Smaller" Text="Show Completed Support ticket" OnClick="Button1_Click" />
                            </td>
                            <td class="NormalTD">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="gridtable">
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="tbl_loanApproval" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="support_pk" DataSourceID="supportticketcase" OnRowCommand="tbl_loanApproval_RowCommand" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_selectloan" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Support_pk" HeaderText="Support_pk" />
                            <asp:BoundField DataField="Supportnum" HeaderText="Supportnum" />
                            <asp:BoundField DataField="SupportTittle" HeaderText="SupportTittle" />
                            <asp:BoundField DataField="SupportDescription" HeaderText="SupportDescription" />
                            <asp:BoundField DataField="Priority" HeaderText="Priority" />
                            <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" />
                            <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                            <asp:BoundField DataField="IsCompleted" HeaderText="IsCompleted" />
                            <asp:BoundField DataField="CompletedDate" HeaderText="CompletedDate" />
                             <asp:BoundField DataField="CompletedBy" HeaderText="Marked Complete By" />
                            <asp:BoundField DataField="Remark" HeaderText="Remark From IT" />
                            <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Completed" Text="Approve" />
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
            <asp:SqlDataSource ID="supportticketcase" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT Support_pk, Supportnum, SupportTittle, SupportDescription, Priority, AddedBy, AddedDate, Status, IsCompleted, CompletedDate, Remark, CompletedBy FROM SupportTicket WHERE (IsCompleted &lt;&gt; N'Y') ORDER BY Support_pk DESC"></asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>
