<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="fabricedit.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.fabricedit" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
      
     
      
      
     
        .Textboxtd {
            width: 90px;
        }
        .ButtonTD {
              width: 55px;
        }
      
     
      
      
     
        .auto-style10 {
            width: 147px;
        }
        .auto-style11 {
            text-align: center;
        }
      
     
      
      
     
    </style>
    
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    

    
        <table class="FullTable">

            <tr>
                <td>
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="5">
                        &nbsp;&nbsp;&nbsp;&nbsp; Fabric ROlls Details</td>
                </tr>
               
                       
                
                
                <tr>
                    <td >
                        Atc# :</td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_atc" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ucc:DropDownListChosen ID="drp_atc" runat="server" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Height="25px" Width="170px">
                                </ucc:DropDownListChosen>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </td>
                    <td class="NormalTD" >
                        
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>

                       
                
                
                <tr>
                    <td >
                        asn #</td>
                    <td class="NormalTD" >
                       <ucc:DropDownListChosen ID="drp_asn" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10" DataSourceID="Asn" DataTextField="AtracotrackingNum" DataValueField="SupplierDoc_pk">
                        </ucc:DropDownListChosen>
                        
                    </td>
                    <td class="NormalTD" >
                        
                        <asp:UpdatePanel ID="upd_btn_fabriccolor" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_color" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_color_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>

    <tr>
                    <td class="NormalTD" >
                        Fabric Details :
                    </td>
                    <td class="NormalTD" colspan="2" >
                       <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="400" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <asp:UpdatePanel ID="upd_btn_fabriccolor0" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btn_fabriccolor" runat="server" CssClass="auto-style10" OnClick="btn_color_Click" Text="show" Width="33px" />
                            </td>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <td class="NormalTD" >
                        <asp:Button ID="Button2" runat="server" Font-Size="Smaller" OnClick="Button2_Click" Text="S" />
                    </td>
                    <td class="NormalTD" >
                         </td>
                </tr>
                <tr>
                    <td >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                          </td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
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

                                           <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                     <asp:CheckBox ID="chk_selectall" runat="server" Text="Select all" AutoPostBack="True" OnCheckedChanged="chk_selectall_CheckedChanged" />
                     </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>



                            <tr>
                                <td class="Textboxtd">
                                    
                                    
                                    <asp:TextBox ID="txt_remark" placeholder="Enter Remark" runat="server" Width="99px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_remark" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_remark_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_yardage" placeholder="Enter Yard" runat="server" Width="93px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_yard" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_yard_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_shade" placeholder="Enter Shade" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_shade" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" OnClick="btn_shade_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_Shrinkage"  placeholder="Enter Shrinkage" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_shrinkage" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" OnClick="btn_shrinkage_Click" /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_width"  placeholder="Enter Width" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btn_width" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" Height="20px" OnClick="btn_width_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_gsm" placeholder="Enter GSM" runat="server" Width="90px"></asp:TextBox></td>
                                  <td class="ButtonTD">
                                      <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                      <asp:Button ID="btn_gsm" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_gsm_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="gridtable" colspan="5">
                        <asp:UpdatePanel ID="upd_grid"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel8"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                         <asp:TemplateField>
                                       
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk_select" runat="server" />
                                        </ItemTemplate>
                                             <HeaderStyle Width="20px" Wrap="True" />
                                             <ItemStyle Width="20px" />
                                    </asp:TemplateField>


                                         <asp:TemplateField HeaderText="RollPK">
                                            
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                             </ItemTemplate>
                                         </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Roll#">
                                          
                                            <ItemTemplate>
                                                 <asp:TextBox ID="txt_rollnum" runat="server" Text='<%# Bind("Rollnum") %>'></asp:TextBox>
                                            </ItemTemplate>
                                             <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Remark">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_remark" runat="server" Text='<%# Bind("Remark") %>'></asp:TextBox>
                                            </ItemTemplate>
                                             <HeaderStyle Width="80px" />
                                        </asp:TemplateField>


                              

                                          <asp:TemplateField HeaderText="Yardage" >
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_syard" runat="server" Width="70px" Text='<%# Bind("SYard") %>'></asp:TextBox>
                                            </ItemTemplate>
                                               <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="UOM">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_uom" runat="server" Text='<%# Bind("UOM") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                              <HeaderStyle Width="40px" />
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Shade">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Sshade" runat="server" Width="70px" Text='<%# Bind("SShade") %>'></asp:TextBox>
                                            </ItemTemplate>
                                              <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Shrinkage">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sshrinkage" runat="server" Width="70px"  Text='<%# Bind("SShrink") %>'></asp:TextBox>
                                            </ItemTemplate>
                                               <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Width">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sWidth" runat="server" Width="70px"  Text='<%# Bind("SWidth") %>'></asp:TextBox>
                                            </ItemTemplate>
                                               <HeaderStyle Width="70px" />
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="GSM">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sgsm" runat="server" Width="70px"  Text='<%# Bind("SGsm") %>'></asp:TextBox>
                                            </ItemTemplate>
                                                <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Weight">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sweight" CssClass="txt_weight1"  Text='<%# Bind("SWeight") %>' runat="server" Width="70px"  ></asp:TextBox>
                                            </ItemTemplate>
                                                <HeaderStyle Width="70px" />
                                        
                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="LOTnum">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_LOTnum" CssClass="txt_LOTnum"  Text='<%# Bind("LOTnum") %>' runat="server" Width="70px"  ></asp:TextBox>
                                            </ItemTemplate>
                                                <HeaderStyle Width="70px" />
                                        
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
                                    <%--<SortedDescendingHeaderStyle BackColor="#7E0000" />--%>

                                    
 


                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="ButtonTR">
                    <td >
                        <asp:Button ID="Button1" runat="server" Text="Update Roll Data" OnClick="Button1_Click" />
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr >
                    <td colspan="5" >
                        <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                </tr>
            </table>

                </td>
            </tr>
        </table>
      
            
        
   
    <div class="footer">
        <asp:SqlDataSource ID="Asn" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierDoc_pk], [AtracotrackingNum] FROM [SupplierDocumentMaster] ">
                        </asp:SqlDataSource>
                <asp:SqlDataSource ID="atcdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId"></asp:SqlDataSource>
                    
               
                               
    </div>

    
</asp:Content>
