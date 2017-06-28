<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ASQShuffle.aspx.cs" Inherits="ArtWebApp.Merchandiser.ASQ.ASQShuffle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
<script type="text/javascript">

    



</script>

    
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>

    
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="FullTable">


        <div class="DataEntryTable">

            <div class="RedHeaddingdIV">Asq Shuffle</div>

             <div class="DataEntryTable"  >
               
                        <table class="DataEntryTable">
                            <tr>
                                <td class="NormalTD" >Atc #</td>
                                <td class="NormalTD"  >
                                   
                                    <asp:UpdatePanel ID="upd_atc"  UpdateMode="Conditional" runat ="server">
                                            <ContentTemplate>
                              <ucc:DropDownListChosen ID="drp_atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                                 </ContentTemplate>
                                        </asp:UpdatePanel>
                                </td><td class="SearchButtonTD"  >
                                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_atc" runat="server" Text="S" OnClick="btn_atc_Click" /></ContentTemplate>
                                        </asp:UpdatePanel>
                                </td>
                                <td >&nbsp;</td>
                                <td >
                                     
                                </td>
                                <td  >
                                    &nbsp;</td>
                                
                            </tr>
                            <tr>
                                <td class="NormalTD" >OurStyle</td>
                                <td  class="NormalTD">

                                  
                                   <asp:UpdatePanel ID="upd_ourstyle" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                     <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                                 </ContentTemplate>
                                        </asp:UpdatePanel>

                                </td>
                               <td class="SearchButtonTD"  >
                                   <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                       <ContentTemplate>
                                           <asp:Button ID="btn_OURSTYLE" runat="server" OnClick="btn_OURSTYLE_Click" Text="S" />
                                       </ContentTemplate>
                                   </asp:UpdatePanel>
                                </td>
                                <td >
                                     
                                    <asp:Label ID="lbl_ourstyleid" runat="server" Text="lbl_ourstyleid"></asp:Label>
                                     
                                </td>
                                <td  >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                               <td class="NormalTD"  >&nbsp;</td>
                                <td >
                                    
                                </td>
                                <td class="SearchButtonTD" >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                            </tr>
                        </table>
                 
            </div>

        </div>

    </div>


     <div class="FullTable">
         <div class="DataEntryTable"  >
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>



                        <div class="SUBRedHeadding">Asq Detial</div>

                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="6"></td>
                            </tr>
                            <tr>
                                <td class="NormalTD" >
                                    
                                    FROM ASQ #:</td>
                                <td >
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <ucc:DropDownListChosen ID="cmb_po" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" OnSelectedIndexChanged="cmb_po_SelectedIndexChanged" Width="200px">
                                                </ucc:DropDownListChosen>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>
                                <td class="SearchButtonTD" >
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_OURSTYLE0" runat="server"  Text="S" OnClick="btn_OURSTYLE0_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="NormalTD">To ASQ #:</td>
                                 
                               <td class="NormalTD" >
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <ig:WebDropDown ID="drp_popack" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId" Width="200px">
                                                <DropDownItemBinding TextField="name" ValueField="pk" />
                                            </ig:WebDropDown>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_OURSTYLE1" runat="server"  Text="S" OnClick="btn_OURSTYLE1_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            
                            
                            
                            
                            
                            
                           
                            <tr>
                                <td class="gridtable" colspan="3">  
                                    <asp:UpdatePanel ID="updgrid1" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="tbl_fromPOdetails" runat="server" AutoGenerateColumns="False" DataKeyNames="PoPack_Detail_PK" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Smaller">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                         <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                         <asp:TemplateField HeaderText="PoDet_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  InsertVisible="False" SortExpression="PoPack_Detail_PK">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PoDet_PK" runat="server" Text='<%# Bind("PoPack_Detail_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="POPackId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="POPackId">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_poapckid" runat="server" Text='<%# Bind("POPackId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OurStyleID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  SortExpression="OurStyleID">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ASQ"  SortExpression="ASQ">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_asq" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color Name"  SortExpression="ColorName">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_colorname" runat="server" Text='<%# Bind("ColorName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color Code"  SortExpression="ColorCode">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_colorcode" runat="server" Text='<%# Bind("ColorCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size Name" SortExpression="SizeName">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sizename" runat="server" Text='<%# Bind("SizeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SIze Code" SortExpression="SIzeCode">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sizecoode" runat="server" Text='<%# Bind("SIzeCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Po Qty" SortExpression="PoQty">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_poQty" runat="server" Text='<%# Bind("PoQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="new Qty" ItemStyle-Width="50" HeaderStyle-Width="50" SortExpression="newQty">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_newQty" runat="server" Text='<%# Bind("PoQty") %>' OnTextChanged="lbl_newQty_TextChanged" AutoPostBack="true" Font-Size="Smaller" Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjusted Qty" SortExpression="AdjustedQty">
                                           
                                            <ItemTemplate>
                                                 <asp:UpdatePanel ID="upd_adjusterQty" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                 <asp:Label ID="lbl_adjusterQty" runat="server" Text='<%# Bind("AdjustedQty") %>'></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                       


                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Balanced Qty" SortExpression="Balanced Qty">
                                           
                                            <ItemTemplate>
                                                 <asp:UpdatePanel ID="upd_bal" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                 <asp:Label ID="lbl_bal" runat="server" Text="0"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel> </ItemTemplate>
                                                </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <SortedDescendingHeaderStyle BackColor="#820000" />
                                    </asp:GridView>
  </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                </td>
                                <td  class="gridtable" colspan="3"><asp:UpdatePanel ID="updgrid2" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="tbl_topodetails" runat="server" AutoGenerateColumns="False" DataKeyNames="PoPack_Detail_PK" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Smaller">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                         <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PoDet_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  InsertVisible="False" SortExpression="PoPack_Detail_PK">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PoDet_PK" runat="server" Text='<%# Bind("PoPack_Detail_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="POPackId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  SortExpression="POPackId">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_poapckid" runat="server" Text='<%# Bind("POPackId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OurStyleID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  SortExpression="OurStyleID">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:TemplateField>
                                      
                                         <asp:TemplateField HeaderText="ASQ"  SortExpression="ASQ">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_asq" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color Name"  SortExpression="ColorName">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_colorname" runat="server" Text='<%# Bind("ColorName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color Code"  SortExpression="ColorCode">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_colorcode" runat="server" Text='<%# Bind("ColorCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size Name" SortExpression="SizeName">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sizename" runat="server" Text='<%# Bind("SizeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SIze Code" SortExpression="SIzeCode">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sizecoode" runat="server" Text='<%# Bind("SIzeCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Po Qty" SortExpression="PoQty">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_poQty" runat="server" Text='<%# Bind("PoQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="new Qty" ItemStyle-Width="50" HeaderStyle-Width="50" SortExpression="newQty">
                                           
                                            <ItemTemplate>
                                                   <asp:UpdatePanel ID="upd1234" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                               <asp:TextBox ID="txt_newQty" runat="server" Text='<%# Bind("PoQty") %>' OnTextChanged="txt_newQty_TextChanged" AutoPostBack="true" Font-Size="Smaller" Width="40px"></asp:TextBox>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>

                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjusted Qty" SortExpression="AdjustedQty">
                                           
                                            <ItemTemplate>
                                                     <asp:UpdatePanel ID="upd_toadjusterQty" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lbl_toadjusterQty" runat="server" Text='<%# Bind("AdjustedQty") %>'></asp:Label>
                                                 </ContentTemplate>
                                        </asp:UpdatePanel>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="frm popack" SortExpression="frm popack">
                                           
                                            <ItemTemplate>
                                                     <asp:UpdatePanel ID="upd_frmpopack" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lbl_frmpopack" runat="server" Text="0"></asp:Label>
                                                 </ContentTemplate>
                                        </asp:UpdatePanel>
                                                </ItemTemplate></asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <SortedDescendingHeaderStyle BackColor="#820000" />
                                    </asp:GridView>
  </ContentTemplate>
                                    </asp:UpdatePanel></td>
                            </tr>
                            
                            
                            
                            
                            
                            
                           
                            <tr class="RedHeadding">
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            

                            
                            <tr>
                                <td colspan="6">
                                    <table class="auto-style1">
                                        <tr>
                                            <td>Group</td>
                                            <td>
                                                <asp:UpdatePanel ID="upd_groupname" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                                <asp:Label ID="lbl_group" runat="server" Text="Label"></asp:Label>
                                            </ContentTemplate>
                                                    </asp:UpdatePanel>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:Button ID="btn_confirmshuffle" runat="server" OnClick="btn_confirmshuffle_Click" Text="Confirm Asq Shuffle" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div id="Messaediv" runat="server">
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="Submit Asq Shuffle request" Enabled="False" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr>
                                <td  colspan="6" style="align-content:center">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td  colspan="6">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="NormalTD"></td>
                                <td class="NormalTD"></td>
                                <td class="NormalTD"></td>
                                <td class="NormalTD">
                                    &nbsp;</td>
                                <td class="NormalTD"></td>
                                <td class="NormalTD"></td>
                            </tr>


                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
           </div>


</asp:Content>
