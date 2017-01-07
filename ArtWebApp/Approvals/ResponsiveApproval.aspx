<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponsiveApproval.aspx.cs" Inherits="ArtWebApp.Approvals.ResponsiveApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <style type="text/css">
        .Background

        {

            background-color: Black;

            filter: alpha(opacity=90);

            opacity: 0.8;

        }

        .Popup

        {

            background-color: #FFFFFF;

            border-width: 3px;

            border-style: solid;

            border-color: black;

            padding-top: 10px;

            padding-left: 10px;

            width: 400px;

            height: 250px;

        }

        .lbl

        {

            font-size:16px;

            font-style:italic;

            font-weight:bold;

        }

    </style>
    <title>

      


    </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <td class="RedHeadding">AUTO PO Approval</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PO_pk" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Names="Calibri" Font-Size="Medium">
                    <Columns>
                         <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="PO_PK" HeaderText="PO_PK" InsertVisible="False" ReadOnly="True" SortExpression="PO_PK" />
                        <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                        <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" SortExpression="SupplierName" />
                        <asp:BoundField DataField="AddedDate" HeaderText="PODate" SortExpression="AddedDate" DataFormatString="dd/MM/yyyy" />
                        <asp:BoundField DataField="POValue" HeaderText="POValue" SortExpression="POValue" />
                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                        <asp:BoundField DataField="AddedBy" HeaderText="CreatedBy" SortExpression="AddedBy" />
                        <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Approve" Text="Approve" Visible="False" />
                        <asp:ButtonField CommandName="Reject" HeaderText="Delete" Text="Delete" />
                        <asp:ButtonField CommandName="Show" HeaderText="Show" Text="Show" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                            <asp:Button ID="btn_approveAll" runat="server" OnClick="btn_approveAll_Click" Text="Approve All selected" />
            </td>
        </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
        <tr>
            <td class="auto-style7">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, SUM(ProcurementDetails.POQty * ProcurementDetails.POUnitRate) 
                         AS POValue, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, ProcurementMaster.IsDeleted, POApproval.IApproved
FROM            ProcurementMaster INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                         POApproval ON ProcurementMaster.PO_Pk = POApproval.PO_PK
GROUP BY ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, 
                         ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, POApproval.IApproved
HAVING        (ProcurementMaster.IsApproved = N'N') AND (ProcurementMaster.IsDeleted &lt;&gt; N'Y') AND (POApproval.IApproved = N'N')"></asp:SqlDataSource>
            </td>
        </tr>
         <tr>
           <td>

<asp:ScriptManager ID="ScriptManager1" runat="server">

</asp:ScriptManager>
                   <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btn_approveAll"

    CancelControlID="Button2" BackgroundCssClass="Background">

</cc1:ModalPopupExtender>

<asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" style = "display:none">

    <table>

    <tr>

    <td>

    <asp:Label runat="server" CssClass="lbl" Text="Enter PassCode"></asp:Label>

    </td>

    <td>

    <asp:TextBox  ID="txt_passcode" runat="server" Font-Size="14px" ></asp:TextBox>

    </td>

    </tr>

    <tr>

    <td>

    <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="Enter Approval Code"></asp:Label>

    </td>

    <td>

    <asp:TextBox ID="txt_Appcode" runat="server" Font-Size="14px" ></asp:TextBox>

    </td>

    </tr>

   

    </table>

    <br />

    <asp:Button ID="Button2" runat="server" OnClick="btnSubmit_Click" Text="Submit" />

</asp:Panel>
           </td>
        </tr>
      
    

    </table>
    </div>
    </form>
</body>
</html>
