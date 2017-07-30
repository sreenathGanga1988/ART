<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RollSplitterForm.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.RollSplitterForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
    <script>
        function Onselection(objref) {
            Check_Click(objref)
            calculatesumandbalance();
        }

        function OnSelectAllClick(objref) {
            checkAll(objref)
            calculatesumandbalance();
        }
        function calculatesumandbalance()
        {


               var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
            var sum = 0
            var intialtotal = 0;
            var newbalance = 0;
            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_syard = gridView.rows[i].getElementsByClassName("txt_syard")[0];

                    sum = sum + parseFloat(txt_syard.value);
                }

            } 
            var totalyardfooter = document.getElementsByClassName("totalyardfooter")[0];
            totalyardfooter.value = sum;

            var lbl_preqad = document.getElementsByClassName("lbl_preqad")[0];

            if (lbl_preqad.innerHTML == "Y")
            {
                intialtotal = document.getElementsByClassName("lbl_ayardage")[0];
            }
            else
            {
                intialtotal = document.getElementsByClassName("lbl_syard")[0];
            }

            newbalance = parseFloat(intialtotal.innerHTML) - parseFloat(sum)
            
            if (newbalance < 0)
            {
                alert("Intial Roll yard cannot be Zero");
            }

            var lbl_balance = document.getElementsByClassName("lbl_balance")[0];
            lbl_balance.value = newbalance;
        }


    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="FullTable">
        <table class="DataEntryTable">
            <tr>
                <td class="RedHeadding" colspan="8">Splitt Roll</td>
            </tr>
            <tr>
                <td class="NormalTD">Roll PK</td>
                <td class="NormalTD">
                    <asp:Label ID="lbl_rollpk" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="NormalTD">Rollnum</td>
                <td class="NormalTD">
                    <asp:Label ID="lbl_rollnum" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="NormalTD">Ayardage</td>
                <td class="NormalTD">
                    <asp:Label ID="lbl_ayardage" CssClass="lbl_ayardage" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="NormalTD"> Pre QAD Inspected</td>
                <td class="NormalTD"><asp:Label ID="lbl_preqad" CssClass="lbl_preqad" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td class="NormalTD">Syard</td>
                <td class="NormalTD">
                    <asp:Label ID="lbl_syard" CssClass="lbl_syard" runat="server" Text="Label"></asp:Label>
                </td>
                <td class="NormalTD">New Balance</td>
                <td class="NormalTD">
                    <asp:TextBox ID="lbl_balance" CssClass="lbl_balance" runat="server" Text="0"></asp:TextBox>
                </td>
                <td class="NormalTD">&nbsp;</td>
                <td class="NormalTD">
                    &nbsp;</td>
                <td class="NormalTD"> &nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
            </tr>
            <tr>
                <td class="NormalTD">No of new&nbsp; Rolls</td>
                <td class="NormalTD">
                    <asp:TextBox ID="txt_noofroll" runat="server"></asp:TextBox>
                </td>
                <td class="NormalTD">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="S" />
                </td>
                <td class="NormalTD">
                   
                </td>
                <td class="NormalTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
            </tr>
        </table>
    </div>
    <div class="DataEntryTable">
        <table class="DataEntryTable">
            <tr>
                <td class="NormalTD">&nbsp;</td>
            </tr>
            <tr class="smallgridtable">
                <td class="NormalTD"><asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowFooter="True" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" >
            <Columns>
                     <asp:TemplateField ControlStyle-Width="10px" HeaderStyle-Width="10px" FooterStyle-Width="10px" >  
                                       <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="OnSelectAllClick(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Onselection(this)" />
                                                    </ItemTemplate>
                                </asp:TemplateField>
                <asp:TemplateField HeaderText="Roll#">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_rollnum" runat="server" onkeyup="enter(this)" Text='<%# Bind("Rollnum") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_remark" runat="server" CssClass="txt_remark" onkeyup="enter(this)" Text='<%# Bind("Remark") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Yardage">
                    <FooterTemplate>
                        <asp:TextBox ID="txt_totalyard" runat="server" CssClass="totalyardfooter"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txt_syard" runat="server" CssClass="txt_syard" onChange="calculatesumandbalance()" onkeyup="enter(this)" Text="0" Width="70px"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Width="70px" />
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
        </asp:GridView></td>
            </tr>
            <tr class="smallgridtable">
                <td class="NormalTD">
                    <asp:Button ID="btn_saveRolls" runat="server" OnClick="btn_saveRolls_Click" Text="Save Rolls" />
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>
