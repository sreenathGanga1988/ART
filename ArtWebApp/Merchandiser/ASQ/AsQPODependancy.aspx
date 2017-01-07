<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AsQPODependancy.aspx.cs" Inherits="ArtWebApp.Merchandiser.ASQ.AsQPODependancy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
    
 
  
    <link href="../../css/style.css" rel="stylesheet" />
 
    <script src="../../JQuery/GridJQuery.js"></script>
    <script type="text/javascript">

       //calculate the sum of qty on keypress
       function sumofQty(objText) {
       
        
           var cell = objText.parentNode;
           
           var row = cell.parentNode;

           var sum = 0;
           var textboxs = row.getElementsByClassName("Qty");

           for (var i = 0; i < textboxs.length; i++)
           {
               sum += parseFloat(textboxs[i].value);
           }



           var textboxtotalqtys = row.getElementsByClassName("ColorTotal");

           textboxtotalqtys[0].value = sum.toString();
         

       }


       function ShowAlert() {
           debugger;

           alert("Multiple RM of same Type Cannot be selected");


       }
       function AnotherFunction() {
           alert("This is another function");
       }

     </script>
    <style type="text/css">
        .auto-style8 {
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="FullTable">
        <tr  class="RedHeadding">
            <td style="color: #FFFFFF; text-align: center; background-color: #990000"><strong>ASQ&nbsp; </strong></td>
        </tr>
        <tr>
            <td class="DataEntryTable">



                 <table class="DataEntryTable">
                        <tr>
                            <td class="NormalTD">

                                   Atc# 
                
                            </td>
                            <td class="NormalTD">
                               
                   
                    
               <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" OnSelectedIndexChanged="cmb_atc_SelectedIndexChanged">
                            </ucc:DropDownListChosen>
                               
                            </td>
                            <td class="NormalTD">
                                 
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="buttonAtc" runat="server" Text="S" Height="26px" OnClick="buttonAtc_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        <tr>
                            <td class="auto-style8" colspan="2">

                                <asp:UpdatePanel ID="upd_Messaediv" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div id="Messaediv" runat="server">
                                            <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="NormalTD">
                                 
                                
                     
                                <asp:SqlDataSource ID="gdITEMSOURCE" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [IsGD], [RMNum], [Sku_Pk] FROM [SkuRawMaterialMaster] WHERE (([Atc_id] = @Atc_id) AND ([IsGD] = @IsGD))">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmb_atc" DefaultValue="0" Name="Atc_id" PropertyName="SelectedValue" Type="Decimal" />
                                        <asp:Parameter DefaultValue="Y" Name="IsGD" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                 
                                
                     
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        <tr>
                            <td colspan="4">

                                <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional"  runat="server">
                                    <ContentTemplate>
                                       <%-- <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle" AutoPostBack="True" OnDataBound="cmb_ourstyle_DataBound" OnValueChanged="cmb_ourstyle_ValueChanged">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>--%>

                                        <asp:GridView ID="tbl_podata" 
                                            Width="100%" 
                                            runat="server"
                                             AutoGenerateColumns="False" 
                                            DataSourceID="SqlDataSource3"
                                             DataKeyNames="PoPackId"
                                             OnRowDataBound="tbl_podata_RowDataBound"
                                             style="font-size: small; font-family: Calibri; font-weight: 400;" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowCommand="tbl_podata_RowCommand" OnSelectedIndexChanged="tbl_podata_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                               
                                               <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                               
                                                <asp:TemplateField HeaderText="IDS" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                            <tr>
                                                                <td>POPAckid</td>
                                                                <td> <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Outstyleid</td>
                                                                <td> <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>atcid</td>
                                                                <td>  <asp:Label ID="lbl_atcid" runat="server" Text='<%# Bind("AtcId") %>'></asp:Label></td>
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

                                                  <asp:TemplateField HeaderText="ASQ Details" >
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                             <tr>
                                                                <td>ASQ</td>
                                                                <td> <asp:Label ID="Label1" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>PoPack#</td>
                                                                <td> <asp:Label ID="na" runat="server" Text='<%# Bind("PoPacknum") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>BuyerPO</td>
                                                                <td> <asp:Label ID="na1" runat="server" Text='<%# Bind("BuyerPO") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>BuyerStyle</td>
                                                                <td>  <asp:Label ID="na2" runat="server" Text='<%# Bind("BuyerStyle") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>OurStyle</td>
                                                                <td>  <asp:Label ID="na3" runat="server" Text='<%# Bind("OurStyle") %>'></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                        <ControlStyle Width="200px" />
                                                            <FooterStyle Width="200px" />
                                                            <HeaderStyle Width="200px" />
                                                            <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Details" SortExpression="Details">
                                                          
                                                            <ItemTemplate>



                                                               
                            <asp:UpdatePanel ID="upd_table"  runat="server">
                                            <ContentTemplate>
                            
                            <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled" Width="400px">
                                </asp:Table>
                            </asp:Panel>
                                                
                                                </ContentTemplate>
                                        </asp:UpdatePanel>



                                                               
                                                            </ItemTemplate>
                                                            
                                                            <ControlStyle Width="300px" />
                                                            <HeaderStyle Width="300px" />
                                                            <ItemStyle Width="300px" />
                                                            
                                                        </asp:TemplateField>
                                                        
                                                      
                                                        <asp:TemplateField HeaderText="GD Items">
                                                           
                                                            <ItemTemplate>
                                                            



                                                                <asp:CheckBoxList ID="chkbx_gditem" runat="server" DataSourceID="gdITEMSOURCE" DataTextField="RMNum" DataValueField="Sku_Pk" RepeatColumns="5" RepeatDirection="Horizontal" AutoPostBack="false" OnSelectedIndexChanged="chkbx_gditem_SelectedIndexChanged">
                                                                </asp:CheckBoxList>
                                                            
                                                            </ItemTemplate>
                                                </asp:TemplateField>
                                                        
                                                      
                                                       <asp:ButtonField CommandName="Update" Text="Update" ButtonType="Button" />
                                          <%--      <asp:TemplateField HeaderText="ad" SortExpression="ad">
                                                  
                                                    <ItemTemplate>
                                                        <br />
                                                        <asp:Button ID="Button1" runat="server" CommandName="Update"  CommandArgument='<%# Container.DataItemIndex %>'  Text="Update" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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

                </table>

                
               
               
            </td>
        </tr>
        <tr>
            <td class="DataEntryTable">
                
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                               
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]">
                </asp:SqlDataSource>
                    
               
                               
                            </td>
                        <td class="NormalTD">

                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.AtcId, PoPackMaster.IsCutable FROM PoPackMaster INNER JOIN POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID GROUP BY PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.AtcId, PoPackMaster.IsCutable HAVING (PoPackMaster.AtcId = @Param1) ORDER BY PoPackMaster.PoPackId">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
       
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="auto-style8"></td>
                        <td class="auto-style8"></td>
                    </tr>
                    <tr>
                        <td class="NormalTD" colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">
                            <table class="DataEntryTable">
                                <tr>
                                    <td class="NormalTD">
                                        <div></div>
                                        
                                    </td>
                                    <td><div></div></td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">
                     
                                <asp:HiddenField ID="ourstylehiden" runat="server" />
                     
                                    </td>
                                    <td>
                               
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT StyleColor.StyleColorid, StyleSize.StyleSizeID, StyleColor.AtcId, StyleColor.OurStyleID, StyleColor.OurStyle, StyleColor.GarmentColorCode, StyleColor.GarmentColor, StyleSize.SizeCode, StyleSize.SizeName, 000000 AS POQty FROM StyleColor INNER JOIN StyleSize ON StyleColor.AtcId = StyleSize.AtcId AND StyleColor.OurStyleID = StyleSize.OurStyleID WHERE (StyleColor.OurStyleID = @Param1)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ourstylehiden" DefaultValue="0" Name="Param1" PropertyName="Value" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
