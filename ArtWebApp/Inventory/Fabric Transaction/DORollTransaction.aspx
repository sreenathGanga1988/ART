<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DORollTransaction.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.DORollTransaction" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    

    
        <table class="FullTable">

            <tr>
                <td>
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="5">
                        &nbsp;&nbsp;&nbsp;&nbsp;DO ROlls</td>
                </tr>
                <tr>
                    <td">
                        
                    </td>
                    <td >
                          
                        Atc</td>
                    <td class="auto-style9"  >
                         

                          <asp:UpdatePanel ID="upd_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>

                    </td>
                    <td >
                          <asp:UpdatePanel ID="upd_btn_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_atc" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_atc_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>   <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        DO # :
                    </td>
                    <td class="auto-style9" >
                             
                      <asp:UpdatePanel ID="upd_do" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="ddl_do" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                    <td >
                         <asp:UpdatePanel ID="upd_btn_do" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_do" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_do_Click"  /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td >
                        </td>
                    <td >
                        </td>
                </tr>
                <tr>
                    <td >
                        Fabric Details :
                    </td>
                    <td class="auto-style9" >
                       <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="auto-style7" >
                        
                    </td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>
                <tr>
                    <td class="gridtable" colspan="5">
                        <asp:UpdatePanel ID="upd_grid"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="rolldatasource">
                                    <Columns>
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="Docnumber" HeaderText="Docnumber" ReadOnly="True" SortExpression="Docnumber" />
                                        <asp:BoundField DataField="AYard" HeaderText="AYard" SortExpression="AYard" />
                                        <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                                        <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                                        <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="ButtonTR">
                    <td >
                        <asp:Button ID="Button1" runat="server" Text="Save Roll Data" />
                    </td>
                    <td class="auto-style9" >
                        &nbsp;</td>
                    <td class="auto-style7" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
            </table>

                </td>
            </tr>
        </table>
      
            
        
   
    <div class="footer">
        
                <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
                <asp:SqlDataSource ID="rolldatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum+' / '+SupplierDocumentMaster.AtracotrackingNum as Docnumber, FabricRollmaster.AYard, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup
FROM            FabricRollmaster INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk"></asp:SqlDataSource>
                <br />
                    
               
                               
    </div>

    
</asp:Content>
