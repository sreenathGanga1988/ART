<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutPlanUnApprovalFactory.aspx.cs" Inherits="ArtWebApp.Production.Cutting.CutPlanUnApprovalFactory" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
  
    <link href="../../css/style.css" rel="stylesheet" />
    
    <script src="../../JQuery/GridJQuery.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

   <script type="text/javascript">



</script>



 
    </asp:Content>



<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RedHeaddingdIV">
        CUT PLAN APPROVAL
    </div>
    <div class="DataEntryTable">
        <asp:UpdatePanel ID="upd_main"  UpdateMode="Conditional"  ChildrenAsTriggers="false"       runat="server">
                                <ContentTemplate>

 <table class="tittlebar">
                    
                    <tr>
                        <td class="NormalTD">CuTPLAN #</td>
                        <td class="NormalTD">
                            
                               <asp:UpdatePanel ID="upd_cutorder" UpdateMode="Conditional"  runat="server">
                                            <ContentTemplate>
                            <ucc:DropDownListChosen ID="drp_cutplan" runat="server"  DataTextField="CutPlanNUM" DataValueField="CutPlan_PK" Width="200px" DataSourceID="cutplandatasource" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                
                                     </ContentTemplate>
                                        </asp:UpdatePanel>            
                                                </td>
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_cutorder" runat="server"  Text="S" OnClick="btn_cutorder_Click" /></ContentTemplate>
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD">
                            <asp:SqlDataSource ID="cutplandatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT CutPlan_PK, CutPlanNUM, IsRollAdded FROM CutPlanMaster WHERE (IsApproved = @IsApproved) AND (IsRatioAdded = @IsRatioAdded) AND (IsRollAdded = N'Y') AND (IsDeleted = N'N')">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Y" Name="IsApproved" Type="String" />
                                    <asp:Parameter DefaultValue="Y" Name="IsRatioAdded" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                 
              
               
                    <tr>
                        <td class="NormalTD" colspan="7" >

                             <asp:UpdatePanel ID="Upd_cutplandetails" runat="server" UpdateMode="Conditional">
                                 <ContentTemplate>
                                     <table width="100%">
                                         <tr>
                                             <td class="NormalTD">Atc</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_atc" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">ourstyle</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_ourstyle" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Location</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_loc" runat="server" Text="0"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="NormalTD">Cutable Width</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_with" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Shrinkage</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_shrink" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">marker Type</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_Markertype" runat="server" Text="0"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="NormalTD">bom Consumption</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_bomconsumption" runat="server" CssClass="lbl_newConsumption" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">fabric</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_fabric" runat="server" Font-Size="X-Small" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Ref paattern if any </td>
                                             <td class="NormalTD">  
                                                 <asp:UpdatePanel ID="upd_refpattern" UpdateMode="Conditional" runat="server">
                                       <ContentTemplate>
                                        <asp:TextBox ID="txt_refpattern" runat="server"></asp:TextBox>
                                             </ContentTemplate>
                                                     </asp:UpdatePanel></td>
                                         </tr>
                                         <tr>
                                             <td class="NormalTD">Fabrication</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_fabrication" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Is Roll Added</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_RollAdded" runat="server" Font-Size="X-Small" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">&nbsp;</td>
                                             <td class="NormalTD">&nbsp;</td>
                                         </tr>
                                     </table>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD" >
                             <asp:UpdatePanel ID="upd_markertype" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                            <asp:GridView ID="tbl_markertype" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="CutPlanmarkerTypeName" HeaderText="Marker Direction" />
                                </Columns>
                            </asp:GridView>
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
                                            <asp:TemplateField HeaderText="PK" InsertVisible="False" SortExpression="CutPlanMarkerDetails_PK">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_CutOrderDet_PK" runat="server" Text='<%# Bind("CutPlanMarkerDetails_PK") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MarkerNo" SortExpression="MarkerNo">
                                                <ItemTemplate>
                                                    <table class="tittlebar" style=" width: inherit; border-style: solid; background-color: #FFFFFF">
                                                        <tr>
                                                            <td>Marker Num</td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("MarkerNo") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td>NoOfPlies</td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("NoOfPlies") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td>Max Plies</td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("CutPerPlies") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td> No of Cut req</td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Cutreq") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td> No of Cut req</td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Cutreq") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ControlStyle Width="200px" />
                                                <FooterStyle Width="200px" />
                                                <HeaderStyle Width="200px" />
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
    </div>
    <div>
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
                                                            <tr>
                                                                <td>BuyerStyle</td>
                                                                <td>
                                                                    <asp:Label ID="na2" runat="server" Text='<%# Bind("BuyerStyle") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>OurStyle</td>
                                                                <td>
                                                                    <asp:Label ID="na3" runat="server" Text='<%# Bind("OurStyle") %>'></asp:Label>
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
                                    <td class="NormalTD">
                                        </tr>
    </table>    
                                       </ContentTemplate>
                                    </asp:UpdatePanel>
    </div>
    <div>
        <table class="DataEntryTable">
    
       
        <tr>
            <td>
                
                                    </td>
                                    <td class="NormalTD">
                                        Unapprove reason</td>
                                    <td class="NormalTD">
                                        <asp:TextBox ID="txt_unapprove_reason" runat="server" style="margin-left: 0px" Width="227px"></asp:TextBox>
                                    </td>
                                    <td class="NormalTD"></td>
                                    <td class="NormalTD"></td>
                                </tr>
                         
                        </table>

    </div>
    <div>

          <table class="DataEntryTable">

               
                            <tr>
                                <td class="auto-style8" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                             <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="UnApprove Cut Plan " />
                                        </ContentTemplate>
                                       
                                    </asp:UpdatePanel>
                                </td>
                                <td class="auto-style8" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                       
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8" colspan="6">
                                    <asp:UpdatePanel ID="upd_Messaediv1" runat="server">
                                        <ContentTemplate>
                                            <div id="Messaediv1" runat="server">
                                                
                                                
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

          </table>

    </div>
    
    
         
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
    
    <asp:SqlDataSource ID="cutplanmarkerdetails" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CutPlanMarkerDetails_PK, CutPlan_PK, MarkerNo, NoOfPc, Qty, MarkerLength, LayLength, NoOfPlies, CutPerPlies, Cutreq
FROM            CutPlanMarkerDetails
WHERE        (CutPlan_PK = @CutPlan_PK) ">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drp_cutplan" DefaultValue="" Name="CutPlan_PK" PropertyName="SelectedValue" Type="Decimal" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
    <asp:SqlDataSource ID="rejectiondatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutPlanRejectionID], [CutplanRejection], [IsActive] FROM [CutPlanRejectionMaster] WHERE ([IsActive] = @IsActive)">
        <SelectParameters>
            <asp:Parameter DefaultValue="true" Name="IsActive" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="cutplanmarkertypedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutPlanMarkerTypes_PK], [CutPlanmarkerTypeName] FROM [CutPlanMarkerType] WHERE ([CutPlan_PK] = @CutPlan_PK)">
        <SelectParameters>
            <asp:ControlParameter ControlID="drp_cutplan" Name="CutPlan_PK" PropertyName="SelectedValue" Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>



