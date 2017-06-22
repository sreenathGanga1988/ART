<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CutplanHTMLReportWithRoll.aspx.cs" Inherits="ArtWebApp.Reports.Production.CutplanHTMLReportWithRoll" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
        }
        .auto-style2 {
            text-decoration: underline;
        }
        .auto-style3 {
            height: 25px;
            width: 200px;
        }
        .smalldetailtable,th,td
        {
            font-family:Calibri;
            border: 1px solid black;
            width:100%;

        }
        .auto-style4 {
            height: 30px;
            width: 200px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="auto-style1" >
    <table   border="1" style="border-collapse:collapse; font-family: Calibri;width: 600px; font-size: medium; font-weight: bold; font-style: normal">
        <tr>
            <td class="RedHeadding"><span class="auto-style2">CUT PLAN REPORT </span> <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager></td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upd_main"  UpdateMode="Conditional"  ChildrenAsTriggers="false"       runat="server">
                                <ContentTemplate>

 <table class="tittlebar" border="1">
                    
                    <tr>
                        <td class="NormalTD">Cutorder #<asp:Label ID="lbl_cutplan" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="NormalTD">
                            
                               <strong>
                               <asp:Label ID="lbl_cutnum" runat="server" CssClass="auto-style2" Text="0"></asp:Label>
                               </strong>            
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
              
               
                 
              
               
                    <tr>
                        <td class="NormalTD" colspan="8" >

                             <asp:UpdatePanel ID="Upd_cutplandetails" runat="server" UpdateMode="Conditional">
                                 <ContentTemplate>
                                     <table class="smalldetailtable">
                                         <tr>
                                             <td class="NormalTD">Atc</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_atc" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Ourstyle</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_ourstyle" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Location</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_loc" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Fabrication</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_fabrication" runat="server" Text="0"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="auto-style4">Cutable Width</td>
                                             <td class="auto-style4">
                                                 <asp:Label ID="lbl_with" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="auto-style4">Shrinkage</td>
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
                                             <td class="auto-style3">Bom Consumption</td>
                                             <td class="auto-style3">
                                                 <asp:Label ID="lbl_bomconsumption" runat="server" CssClass="lbl_newConsumption" Text="0"></asp:Label>
                                             </td>
                                             <td class="auto-style3">Fabric</td>
                                             <td class="auto-style3">
                                                 <asp:Label ID="lbl_fabric" runat="server" Font-Size="X-Small" Text="0"></asp:Label>
                                             </td>
                                             <td class="auto-style3">Added By</td>
                                             <td class="auto-style3">
                                                 <asp:Label ID="lbl_addedBY" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="auto-style3">Ref Pattern</td>
                                             <td class="auto-style3">
                                                 <asp:Label ID="lbl_refpattern" runat="server" Text="0"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="NormalTD">Added Date</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_addeddate" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Approved By</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_approvedBy" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Approved Date</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_approveddate" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">CO Fab Qty</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_fabreq" runat="server" Text="0"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="NormalTD">Style</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_style" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">CutOrder Efficency</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_cutplaneffc" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">CutOrder Consumption</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_cutplancons" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">RollYard</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_rollyard" runat="server" Text="0"></asp:Label>
                                             </td>
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
                                   </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td class="RedHeadding" colspan="8">Marker Details</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" colspan="7">
                            <asp:UpdatePanel ID="upd_cutplanmarkergrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <br />
                                    <asp:GridView ID="tbl_cutplanmarkerdata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CutPlanMarkerDetails_PK" Enabled="False" OnDataBound="tbl_cutorderdata_DataBound" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;">
                                        <Columns>
                                            <asp:TemplateField HeaderText="PK" InsertVisible="False" SortExpression="CutPlanMarkerDetails_PK" ControlStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_CutOrderDet_PK" runat="server" Text='<%# Bind("CutPlanMarkerDetails_PK") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cutplan" SortExpression="MarkerNo">
                                                <ItemTemplate>
                                                    <table style=" width: inherit; border-style: solid; background-color: #FFFFFF">
                                                        <tr>
                                                            <td>Marker Num</td>
                                                            <td>
                                                                <asp:Label ID="lbl_markernum" CssClass="lbl_markernum" runat="server" Text='<%# Bind("MarkerNo") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>NoOfPlies</td>
                                                            <td>
                                                                <asp:Label ID="lbl_noofplies" CssClass="lbl_noofplies" runat="server" Text='<%# Bind("NoOfPlies") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Max Plies</td>
                                                            <td>
                                                                <asp:Label ID="lbl_cutperplies" CssClass="lbl_cutperplies" runat="server" Text='<%# Bind("CutPerPlies") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>No of Cut req</td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Cutreq") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marker"   SortExpression="Marker">
                                                <ItemTemplate>
                                                    <table style=" width:inherit; border-style: solid; background-color: #FFFFFF">
                                                        <tr>
                                                            <td>Marker Num</td>
                                                            <td >
                                                                <asp:Label ID="txt_newmarkernum" CssClass="txt_newmarkernum" runat="server"  Text='<%# Bind("PaternMarkerName") %>' Width="50px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Marker Length</td>
                                                            <td>
                                                                <asp:Label ID="txt_newMarkerlength" CssClass="txt_newMarkerlength"   runat="server"  Text='<%# Bind("MarkerLength") %>' Width="50px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Lay margin </td>
                                                            <td>
                                                                <asp:Label ID="txt_newtolerance" CssClass="txt_newtolerance"  runat="server" T Text='<%# Bind("Tolerancelength") %>' Width="50px"></asp:Label>
                                                            </td>
                                                           
                                                        </tr>
                                                    
                                                        <tr>
                                                            <td>Fab req</td>
                                                            <td>
                                                                <asp:Label ID="txt_fabreq" CssClass="txt_fabreq" runat="server"  Text='<%# Bind("TotalfabReq") %>' Enabled="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Efficiency</td>
                                                            <td>
                                                                <asp:Label ID="Label1" CssClass="txt_Efficiencydisp" runat="server"  Text='<%# Bind("Efficiency") %>' Enabled="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                             
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MarkerDetails" SortExpression="MarkerDetails">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                                                <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled" Width="400px">
                                                                </asp:Table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                                <ControlStyle Width="300px" />
                                                <FooterStyle Width="300px" />
                                                <HeaderStyle Width="300px" />
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
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                </table>
                                       </ContentTemplate>
                            </asp:UpdatePanel>
               
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updASQgrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="6">asq Details </td>
                                <tr>
                                    <%----%>
                                    <td colspan="6">
                                        <asp:GridView ID="tbl_ASQdata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PoPackId"  ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" OnDataBound="tbl_ASQdata_DataBound" Enabled="False">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                
                                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="IDS" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                            <tr>
                                                                <td>POPAckid</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Outstyleid</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>CutPlan_PK</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_CutPlan_PK" runat="server" Text='<%# Bind("CutPlan_PK") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="hidden" />
                                                    <ItemStyle CssClass="hidden" />
                                                    <ControlStyle Width="200px" />
                                                    <FooterStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PO Details">
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                            <tr>
                                                                <td>ASQ</td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td>BuyerPO</td>
                                                                <td>
                                                                    <asp:Label ID="na1" runat="server" Text='<%# Bind("BuyerPO") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                       
                                                        </table>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="200px" />
                                                    <FooterStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Color Size Details" SortExpression="Details">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="upd_table" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="panel2" runat="server" ViewStateMode="Enabled">
                                                                    <asp:Table ID="Table2" runat="server" ViewStateMode="Enabled" Width="400px">
                                                                    </asp:Table>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
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
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalTD"></td>
                                    <td class="NormalTD"></td>
                                    <td class="NormalTD">
                                        &nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD"></td>
                                    <td class="NormalTD"></td>
                                </tr>
                            </tr>
                            <tr>
                                <td class="auto-style8" colspan="6">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8" colspan="6">
                                    <asp:UpdatePanel ID="upd_markertype" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="tbl_markertype" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="CutPlanmarkerType" HeaderText="Marker Direction" />
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
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
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>    <asp:UpdatePanel ID="upd_roll" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="tbl_rolldata" runat="server" AutoGenerateColumns="False" DataKeyNames="Roll_PK" DataSourceID="Rolldata">
                    <Columns>
                        <asp:BoundField DataField="Roll_PK" HeaderText="Roll_PK" InsertVisible="False" ReadOnly="True" SortExpression="Roll_PK" />
                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                        <asp:BoundField DataField="AYard" HeaderText="AYard" SortExpression="AYard" />
                        <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                        <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                        <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                        <asp:BoundField DataField="AGsm" HeaderText="AGsm" SortExpression="AGsm" />
                        <asp:BoundField DataField="SWeight" HeaderText="SWeight" SortExpression="SWeight" />
                       <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                        <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                        <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                        <asp:BoundField DataField="MarkerType" HeaderText="MarkerType" SortExpression="MarkerType" />
                    </Columns>
                </asp:GridView>
                                              </ContentTemplate>
                                    </asp:UpdatePanel>
                
            </td>
        </tr>
        <tr>
            <td>
               
                        <asp:SqlDataSource ID="Rolldata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.AYard, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AGsm, FabricRollmaster.SWeight, 
                         CutPlanRollDetails.CutPlan_PK, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.MarkerType
FROM            CutPlanRollDetails INNER JOIN
                         FabricRollmaster ON CutPlanRollDetails.Roll_PK = FabricRollmaster.Roll_PK
WHERE        (CutPlanRollDetails.CutPlan_PK = @Param1)">
                            <SelectParameters>
                                <asp:SessionParameter Name="Param1" SessionField="cutpkrpt" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="cutplanmarkerdetails" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT CutPlanMarkerDetails_PK, CutPlan_PK, MarkerNo, NoOfPc, Qty, MarkerLength, LayLength, NoOfPlies, CutPerPlies, Cutreq, Tolerancelength, TotalfabReq, PaternMarkerName, Efficiency FROM CutPlanMarkerDetails WHERE (CutPlan_PK = @CutPlan_PK)">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="" Name="CutPlan_PK" SessionField="cutpkrpt" Type="Decimal" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
    <asp:SqlDataSource ID="cutplanmarkertypedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutPlanMarkerTypes_PK], [CutPlanmarkerType] FROM [CutPlanMarkerType] WHERE ([CutPlan_PK] = @CutPlan_PK)">
        <SelectParameters>
            <asp:SessionParameter Name="CutPlan_PK" SessionField="cutpkrpt" Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
