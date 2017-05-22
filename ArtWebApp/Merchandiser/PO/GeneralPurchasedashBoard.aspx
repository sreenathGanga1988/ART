<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GeneralPurchasedashBoard.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.GeneralPurchasedashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
           .mydatagrid

	{

	    width: 80%;
	    border: solid 2px black;

	    min-width: 80%;

	}

	.header

	{
	    background-color: #646464;

	    font-family: Arial;

	    color: White;

	    border: none 0px transparent;

	    height: 25px;

	    text-align: center;

	    font-size: 16px;

	}

	 

	.rows

	{

	    background-color: #fff;
	    font-family: Arial;
	    font-size: 14px;

	    color: #000;

	    min-height: 25px;

	    text-align: left;

	    border: none 0px transparent;

	}

	.rows:hover

	{

	    background-color: #ff8000;

	    font-family: Arial;

	    color: #fff;

	    text-align: left;

	}

	.selectedrow

	{

	    background-color: #ff8000;

	    font-family: Arial;

	    color: #fff;

	    font-weight: bold;

	    text-align: left;

	}

	.mydatagrid a /** FOR THE PAGING ICONS  **/

	{

	    background-color: Transparent;

	    padding: 5px 5px 5px 5px;

	    color: #fff;

	    text-decoration: none;

	    font-weight: bold;

	}

	 

	.mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/

	{

	    background-color: #000;

	    color: #fff;

	}

	.mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/

	{

	    background-color: #c9c9c9;

	    color: #000;

	    padding: 5px 5px 5px 5px;

	}

	.pager

	{

	    background-color: #646464;

	    font-family: Arial;

	    color: White;

	    height: 30px;

	    text-align: left;

	}

	 

	.mydatagrid td

	{

	    padding: 5px;

	}

	.mydatagrid th

	{

	    padding: 5px;

	}
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">general Purchase Dash Board</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="RedHeadding">
                            Pending Ordering</td>
                        <td class="RedHeadding">&nbsp;Pending Approval</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingOrder" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:BoundField DataField="PO_Date" HeaderText="PO_Date" SortExpression="PO_Date" />
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                                    <asp:BoundField DataField="Odoo_UOM" HeaderText="Odoo_UOM" SortExpression="Odoo_UOM" />
                                    <asp:BoundField DataField="OrderedQty" HeaderText="OrderedQty" ReadOnly="True" SortExpression="OrderedQty" />
                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ReadOnly="True" SortExpression="Balance" />
                                    <asp:BoundField DataField="PendingFor" HeaderText="PendingFor" ReadOnly="True" SortExpression="PendingFor" />
                                </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                            </asp:GridView>
                            
                            <asp:SqlDataSource ID="PendingOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        PONum, POId, Description, Qty, PO_Date, Odoo_UOM, OrderedQty,(Qty- OrderedQty) as Balance,DATEDIFF(day,PO_Date,GETDATE()) as PendingFor
FROM            (SELECT        ODOOGPOMaster.PONum, ODOOGPOMaster.POId, ODOOGPOMaster.Description, ODOOGPOMaster.Qty, ODOOGPOMaster.PO_Date, ODOOGPOMaster.Odoo_UOM, 
                                                    ISNULL(SUM(StocPOForODOO.POQty), 0) AS OrderedQty
                          FROM            ODOOGPOMaster LEFT OUTER JOIN
                                                    StocPOForODOO ON ODOOGPOMaster.POId = StocPOForODOO.POId AND ODOOGPOMaster.POLineID = StocPOForODOO.POLineID
                          GROUP BY ODOOGPOMaster.PONum, ODOOGPOMaster.POId, ODOOGPOMaster.Description, ODOOGPOMaster.Qty, ODOOGPOMaster.PO_Date, ODOOGPOMaster.Odoo_UOM) AS tt where (Qty- OrderedQty)&gt;0"></asp:SqlDataSource>
                           
                        </td>
                        <td class="smallgridtable">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="RedHeadding">Pending Receipt</td>
                        <td class="RedHeadding">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
