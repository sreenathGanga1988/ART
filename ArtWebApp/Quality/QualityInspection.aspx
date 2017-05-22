<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="QualityInspection.aspx.cs" Inherits="ArtWebApp.Quality.QualityInspection" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
   
    <script type="text/javascript" src="../JQuery/GridJQuery.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
        <table class="DataEntryTable">
            <tr>
                <td class="RedHeadding" colspan="4">


                    pre qad iNSPECTION Approval</td>
            </tr>
            <tr>
                <td class="NormalTD">


                    aTC # :


                </td>
                <td class="NormalTD">


                   <asp:UpdatePanel ID="upd_atc"  runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" style="text-align: left" >
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>


                </td>
                <td class="NormalTD">


                    <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />


                </td>
                <td class="NormalTD">


                </td>
            </tr>




 <tr>
                    <td class="NormalTD"  >
                        supplier invoice /ASN #:</td>
                    <td class="NormalTD" >
                             
                              <asp:UpdatePanel ID="UPD_ASN" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_asn" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                       </td>
                    <td class="NormalTD"  >
                     <asp:UpdatePanel ID="upd_btn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_Asn" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_Asn_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    
                    <td class="NormalTD"  >
                        </td>
                </tr>
                
                <tr>
                    <td class="NormalTD"  >
                        Fabric Details :
                    </td>
                    <td class="NormalTD" >
                             
                       <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="200px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD"  >
                         <asp:UpdatePanel ID="upd_btn_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_fabric" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_fabric_Click1" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td class="NormalTD"  >
                        </td>
                    <td class="NormalTD"  >
                        </td>
                </tr>
                <tr>
                    <td colspan="5" >
                       <table style="border: thin double #C0C0C0; line-height: normal; vertical-align: middle;  text-align: center; white-space: normal; word-spacing: normal; letter-spacing: normal; background-color: #99CCFF; position: relative; width: 100%;" >
                            


                            <tr>
                                <td colspan="12" class="auto-style11">
                                    
                                    
                                    <strong>Quick Fill </strong></td>
                            </tr>



                            <tr>
                                <td colspan="12">
                                    


                                    <div>

                                           <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                     <asp:CheckBox ID="chk_select" runat="server" Text="Select all" AutoPostBack="True" OnCheckedChanged="chk_selectall_CheckedChanged" />
                     </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>



                            <tr>
                                <td class="Textboxtd">
                                    
                                    
                                    <asp:TextBox ID="txt_defect" placeholder="Enter Defect" runat="server" Width="99px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_remark" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_remark_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_defectper100" placeholder="Enter Defect/100YD" runat="server" Width="93px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_yard" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_yard_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_point" placeholder="Enter Point" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_shade" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" OnClick="btn_shade_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_pointper100"  placeholder="Enter Points/100YDS" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_shrinkage" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" OnClick="btn_shrinkage_Click" /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:DropDownList ID="drp_acceptable" runat="server">
                                                     <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                     <asp:ListItem>No</asp:ListItem></asp:DropDownList></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btn_width" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" Height="20px" OnClick="btn_width_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                   <asp:DropDownList ID="drp_markerType" runat="server">
                                                    <asp:ListItem Selected="True">REG</asp:ListItem>
                                                    <asp:ListItem>RSV</asp:ListItem>
                                                    <asp:ListItem>CSV</asp:ListItem>
                                                    <asp:ListItem>GRYSL</asp:ListItem>
                                                </asp:DropDownList>
                                  <td class="ButtonTD">
                                      <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                      <asp:Button ID="btn_gsm" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_gsm_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                            </tr>



                            <tr>
                                <td class="auto-style7">
                                    
                                    
                                    <asp:TextBox ID="txt_roll" placeholder="Enter rollnum" runat="server" Width="100%"></asp:TextBox></td>
                                <td class="auto-style8">
                                    <asp:Button ID="Button3" runat="server" Font-Size="X-Small" OnClick="Button3_Click" Text="Search" />
                                    </td>
                                <td class="auto-style7">
                                </td>
                                <td class="auto-style8">
                                </td>
                                <td class="auto-style7">
                                    </td>
                                <td class="auto-style8">
                                    </td>
                                <td class="auto-style7">
                                    </td>
                                <td class="auto-style8">
                                    </td>
                                <td class="auto-style7">
                                    </td>
                                <td class="auto-style8">
                                    </td>
                                <td class="auto-style7">
                                   <td class="auto-style8">
                                      </td>
                            </tr>
                        </table> 
                    </td>
                </tr>






            <tr>
                <td class="smallgridtable" colspan="4">
 <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional"  runat="server">
     <ContentTemplate>
         <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                            <asp:TemplateField>
                                       
                                     
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Roll_PK" InsertVisible="False" SortExpression="Roll_PK">
                                              
                                               <ItemTemplate>
                                                   <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                               </ItemTemplate>
                                               <HeaderStyle Width="20px" />
                                           </asp:TemplateField>
                                        <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                                           <asp:BoundField DataField="LOTnum" HeaderText="Lot#" SortExpression="LOTnum" />
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="SShrink" HeaderText="SShrink" SortExpression="SShrink" >
                                           <ControlStyle Width="40px" />
                                           <HeaderStyle Width="40px" />
                                           <ItemStyle Width="40px" />
                                           </asp:BoundField>
                                        <asp:BoundField DataField="SYard" HeaderText="SYard" SortExpression="SYard" />
                                        <asp:BoundField DataField="SShade" HeaderText="SShade" SortExpression="SShade" />
                                        <asp:BoundField DataField="SWidth" HeaderText="SWidth" SortExpression="SWidth" />
                                        <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                                        <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                                        <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                                        <asp:BoundField DataField="AYard" HeaderText="AYard" SortExpression="AYard" />
                                     <asp:TemplateField HeaderText="Total Defect" SortExpression="AYard">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_defect" Width="40px" onkeyup="enter(this)"  runat="server" ></asp:TextBox>
                                            </ItemTemplate>
                                          <HeaderStyle Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Defect /100yds" SortExpression="AYard">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_defectper100"  onkeyup="enter(this)"  Width="40px" runat="server" ></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Point" SortExpression="AYard">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_point" Width="40px" onkeyup="enter(this)"  runat="server" ></asp:TextBox>
                                            </ItemTemplate>
                                             <HeaderStyle Width="40px" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Total Point /100yds"  SortExpression="AYard">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_pointper100" Width="40px" onkeyup="enter(this)"  runat="server" ></asp:TextBox>
                                            </ItemTemplate>
                                               <HeaderStyle Width="40px" />
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Acceptable">
                                            
                                            <ItemTemplate>
                                                 <asp:DropDownList ID="drp_acceptable"  runat="server">
                                                     <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                     <asp:ListItem>No</asp:ListItem>
                                                 </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MarkerType">
                                           
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_markerType"  runat="server">
                                                    <asp:ListItem Selected="True">REG</asp:ListItem>
                                                    <asp:ListItem>RSV</asp:ListItem>
                                                    <asp:ListItem>CSV</asp:ListItem>
                                                    <asp:ListItem>GRYSL</asp:ListItem>
                                                </asp:DropDownList>
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
     </ContentTemplate>
     
     </asp:UpdatePanel>

                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, MrnMaster.MrnNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, FabricRollmaster.MarkerType, ProcurementMaster.PONum, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId FROM MrnDetails INNER JOIN MrnMaster ON MrnDetails.Mrn_PK = MrnMaster.Mrn_PK INNER JOIN FabricRollmaster ON MrnDetails.MrnDet_PK = FabricRollmaster.MRnDet_PK INNER JOIN ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk INNER JOIN SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId WHERE (AtcMaster.AtcId = @Param1)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drp_atc" DefaultValue="0" Name="Param1" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="NormalTD">


                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Update Inspection Inputs" />
                </td>
                <td class="NormalTD">


                    &nbsp;</td>
                <td class="NormalTD">


                    &nbsp;</td>
                <td class="NormalTD">


                    &nbsp;</td>
            </tr>
        </table>

        <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
   
</asp:Content>

