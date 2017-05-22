<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MrnRollEntryNew.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.MrnRollEntryNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/style.css" rel="stylesheet" />
 
    <script type="text/javascript" >

      


        function Onselection(objref)
        {
            Check_Click(objref)
            GetSumofSelectedLabelinFooterTextbox('DataEntry', 'lbl_supplieryard', 'txt_syardfooter');
        }

    </script>
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
                          
                        Atc

                    </td>
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
                       <asp:Button ID="btn_atc" runat="server" Text="S" Width="33px"  CssClass="NormalTD" OnClick="btn_atc_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>   </td >
                       
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
                    </td >
                    <td>
                         <asp:UpdatePanel ID="upd_btn_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_po" runat="server" Text="S" Width="33px"  CssClass="NormalTD" OnClick="btn_po_Click" /></td>
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
                       <asp:Button ID="btn_mrn" runat="server" Text="S" Width="33px"  CssClass="NormalTD"  OnClick="btn_mrn_Click"          /></td>
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
                        Fabric Details :
                    </td>
                    <td class="NormalTD" >
                       <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD" >
                         <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="Button2" runat="server" Text="S" Width="33px"  CssClass="NormalTD"  OnClick="Button2_Click"          /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  
                    </td>
                    <td class="NormalTD" >
                        </td>
                    <td class="NormalTD" >
                         </td>
                </tr>
                <tr>
                    <td >
                        ASN #</td>
                    <td class="NormalTD" >
                         <asp:UpdatePanel ID="UPD_ASN" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_asn" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_btn_fabriccolor" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_color" runat="server" Text="S" Width="33px"  CssClass="NormalTD" OnClick="btn_color_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        
                        MRNQTY</td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_qty" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_mrnQty" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lbl_UOM" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel></td>
                    <td class="NormalTD" >
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
                                <asp:GridView ID="tbl_InverntoryDetails" CssClass="DataEntry" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Roll_PK" ShowFooter="True">
                                    <Columns>


                                             <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="calculateSyardSum(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Roll_PK" InsertVisible="False" SortExpression="Roll_PK">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Lotnum" HeaderText="Lot#" SortExpression="Lotnum" />
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="SShrink" HeaderText="SShrink" SortExpression="SShrink" />
                                             <asp:TemplateField HeaderText="SYard" SortExpression="SYard">
                                                 
                                                
                                                 
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_supplieryard" CssClass="lbl_supplieryard" runat="server" Text='<%# Bind("SYard") %>'></asp:Label>
                                                 </ItemTemplate>
                                                  <FooterTemplate>
                                                     <asp:TextBox ID="txt_syardfooter" CssClass="txt_syardfooter" runat="server"></asp:TextBox>
                                                 </FooterTemplate>
                                             </asp:TemplateField>
                                        <asp:BoundField DataField="SShade" HeaderText="SShade" SortExpression="SShade" />
                                        <asp:BoundField DataField="SWidth" HeaderText="SWidth" SortExpression="SWidth" />
                                        <asp:BoundField DataField="SGsm" HeaderText="SGsm" SortExpression="SGsm" />
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
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
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
                    
               
                               
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Roll_PK], [RollNum], [SShrink], [SYard], [SShade], [SWidth], [SGsm] FROM [FabricRollmaster]"></asp:SqlDataSource>
                    
               
                               
    </div>

    
</asp:Content>
