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
<%--    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>--%>
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

                            <div></div>

                             <div>
                                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingOrder" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" PageSize="25" ShowFooter="True">
                                <Columns>
                                      <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                       <asp:BoundField DataField="POId" HeaderText="POId" SortExpression="POId" />
                                      <asp:TemplateField HeaderText="POLineID" SortExpression="POLineID">
                                        
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_polineid" runat="server" Text='<%# Bind("POLineID") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                               
                                    <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:BoundField DataField="PO_Date" HeaderText="PO_Date" SortExpression="PO_Date" />
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                                    <asp:BoundField DataField="Odoo_UOM" HeaderText="Odoo_UOM" SortExpression="Odoo_UOM" />
                                    <asp:BoundField DataField="OrderedQty" HeaderText="OrderedQty" ReadOnly="True" SortExpression="OrderedQty" />
                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ReadOnly="True" SortExpression="Balance" />
                                    <asp:BoundField DataField="PendingFor" HeaderText="PendingFor" ReadOnly="True" SortExpression="PendingFor" />
                                    <asp:ButtonField CommandName="Cancel" HeaderText="OrderNow" ShowHeader="True" Text="OrderNow" />
                                </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                            </asp:GridView>
                             </div>
                            
                            
                            <asp:SqlDataSource ID="PendingOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        PONum, POId, Description, Qty, PO_Date, Odoo_UOM, OrderedQty, Qty - OrderedQty AS Balance, DATEDIFF(day, PO_Date, GETDATE()) AS PendingFor, POLineID
FROM            (SELECT        ODOOGPOMaster.PONum, ODOOGPOMaster.POId, ODOOGPOMaster.Description, ODOOGPOMaster.Qty, ODOOGPOMaster.PO_Date, ODOOGPOMaster.Odoo_UOM, 
                                                    ISNULL(SUM(StocPOForODOO.POQty), 0) AS OrderedQty, ODOOGPOMaster.POLineID
                          FROM            ODOOGPOMaster LEFT OUTER JOIN
                                                    StocPOForODOO ON ODOOGPOMaster.POId = StocPOForODOO.POId AND ODOOGPOMaster.POLineID = StocPOForODOO.POLineID
                          GROUP BY ODOOGPOMaster.PONum, ODOOGPOMaster.POId, ODOOGPOMaster.Description, ODOOGPOMaster.Qty, ODOOGPOMaster.PO_Date, ODOOGPOMaster.Odoo_UOM, ODOOGPOMaster.POLineID) 
                         AS tt
WHERE        (Qty - OrderedQty &gt; 0)"></asp:SqlDataSource>
                           
                        </td>
                        <td class="smallgridtable">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            <asp:Button ID="Button1" runat="server" Text="Order Now" OnClick="Button1_Click" />
                           
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

                            <div>

                            </div>

                            <div>


                                <asp:GridView ID="tbl_pendingtoreceive" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" DataKeyNames="SPO_Pk" DataSourceID="PendingtoReceive" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="pager" PageSize="20" RowStyle-CssClass="rows" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="SPO_Pk" HeaderText="SPO_Pk" InsertVisible="False" ReadOnly="True" SortExpression="SPO_Pk" />
                                        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                        <asp:BoundField DataField="SPONum" HeaderText="SPONum" SortExpression="SPONum" />
                                        <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" ReadOnly="True" SortExpression="itemDescription" />
                                        <asp:BoundField DataField="Unitprice" HeaderText="Unitprice" SortExpression="Unitprice" />
                                        <asp:BoundField DataField="POQty" HeaderText="POQty" ReadOnly="True" SortExpression="POQty" />
                                        <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" ReadOnly="True" SortExpression="ReceivedQty" />
                                        <asp:BoundField DataField="ExtraQty" HeaderText="ExtraQty" ReadOnly="True" SortExpression="ExtraQty" />
                                        <asp:BoundField DataField="BalanceToReceive" HeaderText="BalanceToReceive" ReadOnly="True" SortExpression="BalanceToReceive" />
                                    </Columns>
                                    <HeaderStyle CssClass="header" />
                                    <PagerStyle CssClass="pager" />
                                    <RowStyle CssClass="rows" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="PendingtoReceive" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        SPO_Pk, SupplierName, SPONum, itemDescription, Unitprice, POQty, ReceivedQty, ExtraQty, POQty - ReceivedQty AS BalanceToReceive
FROM            (SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, ISNULL(Template_Master.Description, N'') + ISNULL(StockPODetails.Composition, N'') + ISNULL(StockPODetails.Construct, N'') 
                                                    + ISNULL(StockPODetails.TemplateColor, N'') + ISNULL(StockPODetails.TemplateWidth, N'') + ISNULL(StockPODetails.TemplateWeight, N'') AS itemDescription, StockPODetails.Unitprice, 
                                                    SUM(StockPODetails.POQty) AS POQty, SUM(StockMRNDetails.ReceivedQty) AS ReceivedQty, SUM(StockMRNDetails.ExtraQty) AS ExtraQty, SupplierMaster.SupplierName
                          FROM            StockPOMaster INNER JOIN
                                                    StockPODetails ON StockPOMaster.SPO_Pk = StockPODetails.SPO_PK INNER JOIN
                                                    StockMRNDetails ON StockPODetails.SPODetails_PK = StockMRNDetails.SPODetails_PK INNER JOIN
                                                    SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                                                    Template_Master ON StockPODetails.Template_PK = Template_Master.Template_PK INNER JOIN
                                                    UserMaster ON StockPOMaster.AddedBy = UserMaster.UserName
                          GROUP BY StockPOMaster.SPO_Pk, StockPOMaster.SPONum, StockPODetails.Composition, StockPODetails.Construct, StockPODetails.TemplateColor, StockPODetails.TemplateWidth, 
                                                    StockPODetails.TemplateWeight, StockPODetails.Unitprice, SupplierMaster.SupplierName, Template_Master.Description, Template_Master.ItemGroup_PK, StockPOMaster.AddedBy, 
                                                    UserMaster.Department_PK
                          HAVING         (UserMaster.Department_PK =@dept_pk)) AS tt
WHERE        (POQty - ReceivedQty &gt; 0)">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="dept_pk" SessionField="Department_PK" />
                                    </SelectParameters>
                                </asp:SqlDataSource>


                            </div>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
