<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SupportTicket.aspx.cs" Inherits="ArtWebApp.Administrator.SupportTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style9 {
        width: 163px;
    }
    #TextArea1 {
        height: 253px;
        width: 646px;
    }
        .auto-style10 {
            text-align: center;
            height: 23px;
            background-color: #99CCFF;
            width: 689px;
        }
        #txta_desc {
            width: 645px;
            height: 238px;
        }
        .auto-style11 {
            width: 163px;
            height: 23px;
            background-color: #FFFFFF;
        }
        .auto-style12 {
            width: 689px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style9">&nbsp;</td>
                            <td class="auto-style12">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style11"></td>
                            <td class="auto-style10"><strong>Generate Support Ticket</strong></td>
                            <td class="auto-style7"></td>
                            <td class="auto-style7"></td>
                            <td class="auto-style7"></td>
                        </tr>
                        <tr>
                            <td class="auto-style9">&nbsp;Support Tittle :</td>
                            <td class="auto-style12">
                                <asp:TextBox ID="txt_tittle" runat="server" Width="651px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style9">Description :</td>
                            <td class="auto-style12">
                                <textarea id="txta_desc" runat="server" name="S1"></textarea> </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style9">Priority</td>
                            <td class="auto-style12">
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
                            <td class="auto-style9">&nbsp;</td>
                            <td class="auto-style12">
                                <asp:Button ID="btn_submit" runat="server" Text="Submit" Width="172px" OnClick="btn_submit_Click" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style9">&nbsp;</td>
                            <td class="auto-style12">
                                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#FF3300" Text="**"></asp:Label>
                            </td>
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
        <td>
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
            <asp:SqlDataSource ID="supportticketcase" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Support_pk], [Supportnum], [SupportTittle], [SupportDescription], [Priority], [AddedBy], [AddedDate], [Status], [IsCompleted], [CompletedDate] FROM [SupportTicket] ORDER BY [Support_pk] DESC"></asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>
