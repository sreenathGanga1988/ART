<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="laysheetRollPrintable.aspx.cs" Inherits="ArtWebApp.Reports.Production.LaySheet.laysheetRollPrintable" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../css/style.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
        }
        .auto-style3 {
            height: 25px;
            width: 200px;
        }
        .smalldetailtable,th,td
        {
            font-family:Calibri;
            border: 1px solid black;
            /*width:100%;*/

        }
        .auto-style4 {
            height: 30px;
            width: 200px;
        }
        .auto-style5 {
            height: 44px;
        }
        .auto-style6 {
            width: 68px;
        }
        .auto-style7 {
            height: 30px;
            width: 68px;
        }
        .auto-style8 {
            height: 25px;
            width: 68px;
        }
        .auto-style9 {
            height: 44px;
            width: 68px;
        }
        .auto-style10 {
            width: 698px;
        }
        .auto-style11 {
            height: 30px;
            width: 698px;
        }
        .auto-style12 {
            height: 25px;
            width: 698px;
        }
        .auto-style13 {
            height: 44px;
            width: 698px;
        }
        .auto-style14 {
            height: 27px;
            width: 8px;
        }
        .auto-style15 {
            height: 30px;
            width: 8px;
        }
        .auto-style16 {
            height: 25px;
            width: 8px;
        }
        .auto-style17 {
            height: 44px;
            width: 8px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
    <div class="auto-style1" >
    <table   border="1" style="border-collapse:collapse; font-family: Calibri;width: 600px; font-size: medium; font-weight: bold; font-style: normal">
      
        <tr>
            <td>
                <asp:UpdatePanel ID="upd_main"  UpdateMode="Conditional"  ChildrenAsTriggers="false"       runat="server">
                                <ContentTemplate>

 <table class="tittlebar" border="1">
                    
                    <tr>
                        <td class="NormalTD">La# <asp:Label ID="lbl_laysheetnum" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="NormalTD">
                            
                               <asp:Label ID="lbl_Rl" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                            
                        </td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                    <caption>
                        MANUAL LAYSHEET<tr>
                            <td class="NormalTD" colspan="8">
                                <asp:UpdatePanel ID="Upd_cutplandetails" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table class="smalldetailtable">
                                            <tr>
                                                <td class="auto-style10">Atc</td>
                                                <td class="NormalTD">
                                                    <asp:Label ID="lbl_atc" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style6">Ourstyle</td>
                                                <td class="NormalTD">
                                                    <asp:Label ID="lbl_ourstyle" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="NormalTD">Location</td>
                                                <td class="NormalTD">
                                                    <asp:Label ID="lbl_loc" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="NormalTD">Style</td>
                                                <td class="auto-style14">
                                                    <asp:Label ID="lbl_style" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style11">Buyer</td>
                                                <td class="auto-style4">
                                                    <asp:Label ID="lbl_buyer" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style7">Cut Order</td>
                                                <td class="auto-style4"><asp:Label ID="lbl_cutordernum" runat="server" Text="0"></asp:Label></td>
                                                <td class="auto-style4">CP#</td>
                                                <td class="auto-style4"><asp:Label ID="lbl_cutplannum" runat="server" Text="0"></asp:Label></td>
                                                <td class="auto-style4">Markername</td>
                                                <td class="auto-style15"> <asp:Label ID="lbl_patternname" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style11">Marker Length</td>
                                           <td class="auto-style4">
                                                     <asp:Label ID="lbl_markerlength" runat="server" Text="0"></asp:Label> </td>
                                                <td class="auto-style7">Tolerance</td>
                                                <td class="auto-style4">
                                                    2</td>
                                                <td class="auto-style4">No of Plies</td>
                                                <td class="auto-style4">
                                                     <asp:Label ID="lbl_noofplies" runat="server" Text="0"></asp:Label> </td></td>
                                                <td class="auto-style4">No of Plies</td>
                                                <td class="auto-style15">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style11">Cutable Width</td>
                                                <td class="auto-style4">
                                                    <asp:Label ID="lbl_with" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style7">Shrinkage</td>
                                                <td class="auto-style4">
                                                    <asp:Label ID="lbl_shrink" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style4">Marker Type</td>
                                                <td class="auto-style4">
                                                    <asp:Label ID="lbl_Markertype" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style4">Cutting Mode</td>
                                                <td class="auto-style15">
                                                    <asp:Label ID="lbl_patternmode" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style12">Fabrication</td>
                                                <td class="auto-style3">
                                                     <asp:Label ID="lbl_fabrication" runat="server" Text="0"></asp:Label>
                                                    
                                                    &nbsp;</td>
                                                <td class="auto-style8">Fabric</td>
                                                <td class="auto-style3">
                                                    <asp:Label ID="lbl_fabric" runat="server" Font-Size="X-Small" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style3">Added By</td>
                                                <td class="auto-style3">
                                                    <asp:Label ID="lbl_addedBY" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style3">Added Date</td>
                                                <td class="auto-style16">
                                                    <asp:Label ID="lbl_addeddate" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style13">&nbsp;</td>
                                                <td class="auto-style5">
                                                    <asp:Label ID="lbl_manualcutnum" runat="server" Text="0" Visible="False"></asp:Label>
                                                </td>
                                                <td class="auto-style9">Garment Color</td>
                                                <td class="auto-style5">
                                                    <asp:Label ID="lbl_colorname" runat="server" Text="0" Visible="False"></asp:Label>
                                                </td>
                                                <td class="auto-style5">&nbsp;</td>
                                                <td class="auto-style5">
                                                    &nbsp;</td>
                                                <td class="auto-style5">&nbsp;</td>
                                                <td class="auto-style17">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style10">&nbsp;</td>
                                                <td class="NormalTD"></td>
                                                <td class="auto-style6">&nbsp;</td>
                                                <td class="NormalTD">
                                                    &nbsp;</td>
                                                <td class="NormalTD">&nbsp;</td>
                                                <td class="NormalTD">
                                                    &nbsp;</td>
                                                <td class="NormalTD">&nbsp;</td>
                                                <td class="auto-style14">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="NormalTD" colspan="8">
                                <asp:UpdatePanel ID="upd_mastertable" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="masterpanel" runat="server" ViewStateMode="Enabled">
                                            <asp:Table ID="Mastertable" runat="server" ViewStateMode="Enabled" Width="400px">
                                            </asp:Table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="RedHeadding" colspan="8">Roll Details</td>
                        </tr>
                        <tr>
                            <td class="NormalTD" colspan="7">
                                <asp:UpdatePanel ID="upd_cutplanmarkergrid" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" DataSourceID="RollData" ForeColor="Black">
                                            <Columns>


                                                
                                                 <asp:BoundField DataField="Roll_PK" HeaderText="Roll_PK" SortExpression="Roll_PK" />
                                               <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                                <asp:BoundField DataField="invoice" HeaderText="invoice" SortExpression="invoice" ReadOnly="True" />

                                                
                                                
                                                
                                                
                                                           <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                                                  <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                                                           <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                                                  <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                                                      

                                                <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                                                  <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                                             
                                               
                                                <asp:BoundField DataField="AYard" HeaderText="AYard" SortExpression="AYard" />
                                                   <asp:BoundField DataField="Yardage" HeaderText="Yardage" SortExpression="Yardage" />
                                                   <asp:BoundField DataField="NoOfPlies" HeaderText="NoOfPlies" SortExpression="NoOfPlies" />
                                                <asp:BoundField DataField="FabUtilized" HeaderText="FabUtilized" SortExpression="FabUtilized" />
                                                <asp:BoundField DataField="BalToCut" HeaderText="BalToCut" SortExpression="BalToCut" />
                                                <asp:BoundField DataField="ExcessOrShort" HeaderText="Excess Or Short" SortExpression="ExcessOrShort" />
                                                <asp:BoundField DataField="IsRecuttable" HeaderText="cuttable" SortExpression="IsRecuttable" />           </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                            <RowStyle BackColor="White" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="RollData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        FabricRollmaster.RollNum, FabricRollmaster.AShade, '' AS NoOfPlies, FabricRollmaster.SYard, FabricRollmaster.AYard, '' AS FabUtilized, '' AS EndBit,  '' AS BalToCut, 
                         SupplierDocumentMaster.AtracotrackingNum + '/' + SupplierDocumentMaster.SupplierDocnum AS invoice, '' AS ExcessOrShort, '' AS IsRecuttable, FabricRollmaster.AWidth, FabricRollmaster.AShrink, 
                         FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, LaySheetRollDetails.LaysheetRollmaster_Pk, LaySheetRollDetails.Yardage, LaySheetRollDetails.Roll_PK
FROM            FabricRollmaster INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         LaySheetRollDetails ON FabricRollmaster.Roll_PK = LaySheetRollDetails.Roll_PK
WHERE        (LaySheetRollDetails.LaysheetRollmaster_Pk = @Param1) ORDER BY 
FabricRollmaster.ShadeGroup">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="Param1" QueryStringField="laysheetpk" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="NormalTD">&nbsp;</td>
                        </tr>
                    </caption>
                    <tr>
                        <td class="NormalTD" colspan="7">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="LaysheetRollmaster_Pk" DataSourceID="laysheetData">
                                <Columns>
                                    <asp:BoundField DataField="LayRollRef" HeaderText="LayRollRef" SortExpression="LayRollRef" />
                                    <asp:BoundField DataField="NoofPlies" HeaderText="NoofPlies" SortExpression="NoofPlies" />
                                    <asp:BoundField DataField="Layedplies" HeaderText="Layedplies" ReadOnly="True" SortExpression="Layedplies" />
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
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                </table>
                                       </ContentTemplate>
                            </asp:UpdatePanel>
               
            </td>
        </tr>
        <tr>
            <td>
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
    </table>
    </div>
       <asp:SqlDataSource ID="laysheetData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        LayRollRef, NoofPlies, ISNULL
                             ((SELECT        SUM(LaySheetDetails.NoOfPlies) AS Expr1
                                 FROM            LaySheetRollDetails INNER JOIN
                                                          LaySheetDetails ON LaySheetRollDetails.LaySheetRoll_Pk = LaySheetDetails.LaySheetRoll_Pk
                                 WHERE        (LaySheetDetails.IsDeleted = N'N') AND (LaySheetRollDetails.LaysheetRollmaster_Pk = LaySheetRollMaster.LaysheetRollmaster_Pk)), 0) AS Layedplies, LaysheetRollmaster_Pk
FROM            LaySheetRollMaster
WHERE        (LaysheetRollmaster_Pk = @Param1)">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="Param1" QueryStringField="laysheetpk" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
    </form>
</body>
</html>
