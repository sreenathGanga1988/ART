<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="laysheetprintable.aspx.cs" Inherits="ArtWebApp.Reports.Production.LaySheet.laysheetprintable" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
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
                        <td class="NormalTD">LaySheet # <asp:Label ID="lbl_laysheetnum" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="NormalTD">
                            
                               &nbsp;</td>
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
                        FABRIC LAYSHEET&nbsp;<tr>
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
                                                <td class="NormalTD">
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
                                                <td class="auto-style4"> <asp:Label ID="lbl_patternname" runat="server" Text="0"></asp:Label></td>
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
                                                    &nbsp;</td>
                                                <td class="auto-style4">Cut Req</td>
                                                <td class="auto-style4">
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
                                                <td class="auto-style4">
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
                                                <td class="auto-style3">
                                                    <asp:Label ID="lbl_addeddate" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style13">Cutnum(Manual)</td>
                                                <td class="auto-style5">
                                                    <asp:Label ID="lbl_manualcutnum" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style9">&nbsp;Garment Color</td>
                                                <td class="auto-style5">
                                                    <asp:Label ID="lbl_colorname" runat="server" Text="0"></asp:Label>
                                                </td>
                                                <td class="auto-style5">&nbsp;</td>
                                                <td class="auto-style5">
                                                    &nbsp;</td>
                                                <td class="auto-style5">&nbsp;</td>
                                                <td class="auto-style5">
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
                                                <td class="NormalTD">&nbsp;</td>
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
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" DataKeyNames="LaySheetDet_PK" DataSourceID="RollData" ForeColor="Black" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True">
                                            <Columns>
                                                 <asp:BoundField DataField="Roll_PK" HeaderText="Roll_PK" SortExpression="Roll_PK" />
                                                <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                                <asp:BoundField DataField="invoice" HeaderText="invoice" SortExpression="invoice" ReadOnly="True" />

                                                
                                                
                                                
                                                
                                                           <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                                                  <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                                                           <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                                                  <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                                                      

                                                <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                                                  <asp:BoundField DataField="RollYard" HeaderText="RollYard" SortExpression="RollYard" />
                                                
                                                  <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                                                <asp:TemplateField HeaderText="NoOfPlies" SortExpression="NoOfPlies">
                                                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("NoOfPlies") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>

                                                          <asp:Label ID="lbl_footNoOfPlies" runat="server" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AYard" SortExpression="AYard">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("AYard") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>

                                                          <asp:Label ID="lbl_footAYard" runat="server" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FabUtilized" SortExpression="FabUtilized">
                                                 
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FabUtilized") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>

                                                          <asp:Label ID="lbl_footFabUtilized" runat="server" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Balance Fabric" SortExpression="BalToCut">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("BalToCut") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                        
                                                          <asp:Label ID="lbl_footBalToCut" runat="server" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ExcessOrShort" SortExpression="ExcessOrShort">
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("ExcessOrShort") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>

                                                          <asp:Label ID="lbl_footExcessOrShort" runat="server" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IsRecuttable" HeaderText="cuttable" SortExpression="IsRecuttable" />
                                               
                                            </Columns>
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
                                        <asp:SqlDataSource ID="RollData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        FabricRollmaster.RollNum, FabricRollmaster.AShade, LaySheetDetails.NoOfPlies, FabricRollmaster.SYard, FabricRollmaster.AYard as RollYard, LaySheetDetails.FabUtilized, LaySheetDetails.EndBit, LaySheetDetails.BalToCut, 
                         SupplierDocumentMaster.AtracotrackingNum + '/' + SupplierDocumentMaster.SupplierDocnum AS invoice, LaySheetDetails.LaySheetDet_PK, LaySheetDetails.ExcessOrShort, LaySheetDetails.IsRecuttable, 
                         FabricRollmaster.AWidth, FabricRollmaster.AShrink, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.Roll_PK, LaySheetRollDetails.Yardage as Ayard
FROM            LaySheetDetails INNER JOIN
                         FabricRollmaster ON LaySheetDetails.Roll_PK = FabricRollmaster.Roll_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         LaySheetRollDetails ON LaySheetDetails.LaySheetRoll_Pk = LaySheetRollDetails.LaySheetRoll_Pk WHERE (LaySheetDetails.LaySheet_PK = @Param1) ORDER BY FabricRollmaster.ShadeGroup">
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
                        <td class="NormalTD" colspan="7">Layed By:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Cut By:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Checked by:-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Approved By:-</td>
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
    </form>
</body>
</html>
