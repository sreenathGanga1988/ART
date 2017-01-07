<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InspectionRollEntry.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.InspectionRollEntry" %>
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
                        pre qad roll VALIDATION</td>
                </tr>
                <tr>
                    <td">
                       
                    </td>
                    <td >
                          
                        Atc</td>
                    <td class="NormalTD"  >
                         

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
                        PO # :
                    </td>
                    <td class="NormalTD" >
                             
                      <asp:UpdatePanel ID="upd_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="ddl_po" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                    <td >
                         <asp:UpdatePanel ID="upd_btn_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_po" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_po_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td >
                        </td>
                    <td >
                        </td>
                </tr>
                <tr>
                    <td class="NormalTD" >
                        MRN&nbsp; # :</td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_mrn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_mrn" runat="server" Height="25px" Width="170px"  DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD" >
                         <asp:UpdatePanel ID="upd_btn_mrn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_mrn" runat="server" Text="S" Width="33px"  CssClass="auto-style10"  OnClick="btn_mrn_Click"          /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  
                    </td>
                    <td class="NormalTD" >
                        </td>
                    <td class="NormalTD" >
                        </td>
                </tr>
                <tr>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                         &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD" >

                          <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                     <asp:CheckBox ID="chk_selectall" runat="server" Text="Select all" AutoPostBack="True" OnCheckedChanged="chk_selectall_CheckedChanged" />
                     </ContentTemplate>
                                            </asp:UpdatePanel>  
                       
                    </td>
                    <td class="auto-style9" >
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                     <asp:CheckBox ID="chk_woven"   runat="server" Text="Knit" AutoPostBack="True" OnCheckedChanged="chk_woven_CheckedChanged" />
                     </ContentTemplate>
                                            </asp:UpdatePanel>
                    </td>
                    <td class="NormalTD" >
                         &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="smallgridtable" colspan="5">
                        <asp:UpdatePanel ID="upd_grid"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Roll_PK">
                                    <Columns>                                     
                                        <asp:TemplateField>
                                       
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk_select" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Roll_PK" InsertVisible="False" SortExpression="Roll_PK">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
                                          <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                                        <asp:BoundField  DataField="SShrink" HeaderText="SShrink" SortExpression="SShrink" />
                                        <asp:TemplateField HeaderText="AShrink" SortExpression="AShrink">
                                            
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_ashrink"  Width="70px" runat ="server" Text='<%# Bind("AShrink") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SYard" HeaderText="SYard" SortExpression="SYard" />
                                        <asp:TemplateField HeaderText="AYard" SortExpression="AYard">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_ayard" Width="70px" runat="server" Text='<%# Bind("AYard") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SShade" HeaderText="SShade" SortExpression="SShade" />

                                        <asp:TemplateField HeaderText="AShade" SortExpression="AShade">
                                            
                                            <ItemTemplate>
                                                 <asp:TextBox ID="txt_ashade" Width="70px" runat="server" Text='<%# Bind("AShade") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:BoundField DataField="SWidth" HeaderText="SWidth" SortExpression="SWidth" />
                                        <asp:TemplateField HeaderText="AWidth" SortExpression="AWidth">
                                            
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_awidth" Width="70px" runat="server" Text='<%# Bind("AWidth") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:BoundField DataField="SGsm" HeaderText="SGsm" SortExpression="SGsm" />
                                         <asp:TemplateField HeaderText="AGSM" SortExpression="AYard">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_agsm" Width="70px" runat="server" Text='<%# Bind("AGSM") %>'></asp:TextBox>
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
                                  <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="ButtonTR">
                    <td >
                        <asp:Button ID="btn_submitData" runat="server" Text="Save Roll Data" OnClick="btn_submitData_Click" />
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
        
               
                               
    </div>

    
</asp:Content>
