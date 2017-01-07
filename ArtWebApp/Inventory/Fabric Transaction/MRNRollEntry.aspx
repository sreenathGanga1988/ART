<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MRNRollEntry.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.MRNRollEntry" %>
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
                        &nbsp;&nbsp;&nbsp;&nbsp; MRN ROlls Details</td>
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
                        PO # :
                    </td>
                    <td class="auto-style9" >
                             
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
                    <td class="auto-style7" >
                        MRN&nbsp; # :</td>
                    <td class="auto-style9" >
                        <asp:UpdatePanel ID="upd_mrn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_mrn" runat="server" Height="25px" Width="170px"  DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="auto-style7" >
                         <asp:UpdatePanel ID="upd_btn_mrn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_mrn" runat="server" Text="S" Width="33px"  CssClass="auto-style10"  OnClick="btn_mrn_Click"          /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  
                    </td>
                    <td class="auto-style7" >
                        </td>
                    <td class="auto-style7" >
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
                    <td >
                        No of Rolls</td>
                    <td class="auto-style9" >
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style7" >
                        <asp:UpdatePanel ID="upd_btn_fabriccolor" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_color" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_color_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="chk_woven" runat="server" Text="KNIT" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                <tr>
                    <td class="gridtable" colspan="5">
                        <asp:UpdatePanel ID="upd_grid"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Roll#">
                                          
                                            <ItemTemplate>
                                                 <asp:TextBox ID="txt_rollnum" runat="server" Text='<%# Bind("Rollnum") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Remark">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_remark" runat="server" Text='<%# Bind("Remark") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                              

                                          <asp:TemplateField HeaderText="S Yardage">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_syard" runat="server" Width="70px" Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="UOM">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_uom" runat="server" Text='<%# Bind("UOM") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="S Shade">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Sshade" runat="server" Width="70px" Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="S Shrinkage">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sshrinkage" runat="server" Width="70px"  Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="S Width">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sWidth" runat="server" Width="70px"  Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S GSM">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sgsm" runat="server" Width="70px"  Text='0'></asp:TextBox>
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
                    </td>
                </tr>
                <tr class="ButtonTR">
                    <td >
                        <asp:Button ID="Button1" runat="server" Text="Save Roll Data" OnClick="Button1_Click" />
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
                    
               
                               
    </div>

    
</asp:Content>
