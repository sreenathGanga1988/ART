<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SkuMasterEdit.aspx.cs" Inherits="ArtWebApp.Merchandiser.AtcEditOptions.SkuMasterEdit" %>
<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        



    <asp:MultiView ID="MultiView1" runat="server">


        <asp:View ID="SkuMasterView" runat="server">

            <table class="FullTable">
        <tr>
            <td class="RedHeadding">ATC RAW MATERIAL</td>
        </tr>
        <tr>
            <td >
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD"><span class="auto-style22">&nbsp;ATC # :&nbsp;</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmb_atc" ErrorMessage="Select Atc" ForeColor="#FF0066" InitialValue="Select Atc" ValidationGroup="AtcValidation" CssClass="auto-style22">*</asp:RequiredFieldValidator>

                        </td>
                        <td class="NormalTD">


                              <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="atcmasterdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" OnSelectedIndexChanged="cmb_atc_SelectedIndexChanged">
                            </ucc:DropDownListChosen>
                        </td>
                        <td class="NormalTD">

                            <asp:Button ID="buttonAtc" runat="server" Text="Show SKU Master" Height="26px" OnClick="buttonAtc_Click" ValidationGroup="AtcValidation" CssClass="auto-style22" ToolTip="Show the SKU Master of the Atc" />

                        </td>
                        <td class="NormalTD">



                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />

                        </td>
                        <td class="NormalTD" rowspan="3">



                          



                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">

                            &nbsp;</td>
                        <td class="NormalTD">

                            &nbsp;</td>
                        <td class="NormalTD">

                            &nbsp;</td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:UpdatePanel ID="Upd_label"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                             </ContentTemplate>


                    </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="smallgridtable">
            <td >

              
                    <asp:UpdatePanel ID="upd_skugrid"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                          
                            <asp:GridView ID="tbl_skumaster" runat="server" DataSourceID="Skumaster" AutoGenerateColumns="False" DataKeyNames="SkU_PK" OnRowDeleting="tbl_skumaster_RowDeleting" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowCommand="tbl_skumaster_RowCommand">
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
                                       <asp:CommandField ShowDeleteButton="True" />  
                                    <asp:TemplateField HeaderText="SkuPk" InsertVisible="False" SortExpression="Sku_Pk">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_skuPK" runat="server" Text='<%# Bind("Sku_Pk") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Atc_id" HeaderText="Atcid" SortExpression="Atc_id" />
                                    <asp:BoundField DataField="AtcRaw_PK" HeaderText="AtcRawPK" SortExpression="AtcRaw_PK" />
                                    <asp:BoundField DataField="TemplateCode" HeaderText="Template" SortExpression="TemplateCode" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:BoundField DataField="RMNum" HeaderText="RMNum" SortExpression="RMNum" />
                                    <asp:BoundField DataField="Composition" HeaderText="Composition" SortExpression="Composition" />
                                    <asp:BoundField DataField="Construction" HeaderText="Construction" SortExpression="Construction" />
                                    <asp:BoundField DataField="OrderMulti" HeaderText="OrderMulti" SortExpression="OrderMulti" />
                                    <asp:BoundField DataField="OrderMin" HeaderText="OrderMin" SortExpression="OrderMin" />
                                    <asp:BoundField DataField="Weight" HeaderText="Weight" SortExpression="Weight" />
                                    <asp:BoundField DataField="Width" HeaderText="Width" SortExpression="Width" />
                                    <asp:BoundField DataField="isCommon" HeaderText="isCM" SortExpression="isCommon" />
                                    <asp:BoundField DataField="IsCD" HeaderText="IsCD" SortExpression="IsCD" />
                                    <asp:BoundField DataField="IsSD" HeaderText="IsSD" SortExpression="IsSD" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                                    <asp:BoundField DataField="Template_pk" HeaderText="Temp_pk" SortExpression="Template_pk" />
                                    <asp:BoundField DataField="UomCode" HeaderText="UomCode" SortExpression="UomCode" />
                                    <asp:BoundField DataField="WastagePercentage" HeaderText="Wastage" ReadOnly="True" SortExpression="WastagePercentage" />
                                    <asp:ButtonField CommandName="DeleteSkuDetails" Text="Delete Sku Details" />
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
        </tr>
       
        <tr>
            <td>
                   <asp:UpdatePanel ID="Upd_button"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                <asp:Button ID="Btn_updatesku" runat="server" Text="Update SKU Master" ValidationGroup="skumstr" OnClick="Btn_updatesku_Click" />

 </ContentTemplate>


                    </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>

                <asp:SqlDataSource ID="Skumaster" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT SkuRawMaterialMaster.Sku_Pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.AtcRaw_PK, Template_Master.TemplateCode, Template_Master.Description, SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.OrderMulti, SkuRawMaterialMaster.OrderMin, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, SkuRawMaterialMaster.IsSD, SkuRawMaterialMaster.Rate, SkuRawMaterialMaster.Template_pk, UOMMaster.UomCode, UOMMaster.Uom_PK, SkuRawMaterialMaster.Uom_PK AS Expr1, ISNULL(SkuRawMaterialMaster.WastagePercentage, 0) AS WastagePercentage FROM SkuRawMaterialMaster INNER JOIN Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN UOMMaster ON Template_Master.Uom_PK = UOMMaster.Uom_PK WHERE (SkuRawMaterialMaster.Atc_id = @Atc_id)" DeleteCommand="DELETE FROM [SkuRawMaterialMaster] WHERE [Sku_Pk] = @original_Sku_Pk AND (([Atc_id] = @original_Atc_id) OR ([Atc_id] IS NULL AND @original_Atc_id IS NULL)) AND (([AtcRaw_PK] = @original_AtcRaw_PK) OR ([AtcRaw_PK] IS NULL AND @original_AtcRaw_PK IS NULL)) AND (([Template_pk] = @original_Template_pk) OR ([Template_pk] IS NULL AND @original_Template_pk IS NULL)) AND (([RMNum] = @original_RMNum) OR ([RMNum] IS NULL AND @original_RMNum IS NULL)) AND (([Composition] = @original_Composition) OR ([Composition] IS NULL AND @original_Composition IS NULL)) AND (([Construction] = @original_Construction) OR ([Construction] IS NULL AND @original_Construction IS NULL)) AND (([OrderMulti] = @original_OrderMulti) OR ([OrderMulti] IS NULL AND @original_OrderMulti IS NULL)) AND (([OrderMin] = @original_OrderMin) OR ([OrderMin] IS NULL AND @original_OrderMin IS NULL)) AND (([Weight] = @original_Weight) OR ([Weight] IS NULL AND @original_Weight IS NULL)) AND (([Width] = @original_Width) OR ([Width] IS NULL AND @original_Width IS NULL)) AND (([Uom_PK] = @original_Uom_PK) OR ([Uom_PK] IS NULL AND @original_Uom_PK IS NULL)) AND (([AltUom_pk] = @original_AltUom_pk) OR ([AltUom_pk] IS NULL AND @original_AltUom_pk IS NULL)) AND (([isCommon] = @original_isCommon) OR ([isCommon] IS NULL AND @original_isCommon IS NULL)) AND (([IsCD] = @original_IsCD) OR ([IsCD] IS NULL AND @original_IsCD IS NULL)) AND (([IsSD] = @original_IsSD) OR ([IsSD] IS NULL AND @original_IsSD IS NULL)) AND (([Currency_pk] = @original_Currency_pk) OR ([Currency_pk] IS NULL AND @original_Currency_pk IS NULL)) AND (([Rate] = @original_Rate) OR ([Rate] IS NULL AND @original_Rate IS NULL)) AND (([WastagePercentage] = @original_WastagePercentage) OR ([WastagePercentage] IS NULL AND @original_WastagePercentage IS NULL))" InsertCommand="INSERT INTO [SkuRawMaterialMaster] ([Atc_id], [AtcRaw_PK], [Template_pk], [RMNum], [Composition], [Construction], [OrderMulti], [OrderMin], [Weight], [Width], [Uom_PK], [AltUom_pk], [isCommon], [IsCD], [IsSD], [Currency_pk], [Rate], [WastagePercentage]) VALUES (@Atc_id, @AtcRaw_PK, @Template_pk, @RMNum, @Composition, @Construction, @OrderMulti, @OrderMin, @Weight, @Width, @Uom_PK, @AltUom_pk, @isCommon, @IsCD, @IsSD, @Currency_pk, @Rate, @WastagePercentage)" UpdateCommand="UPDATE [SkuRawMaterialMaster] SET [Atc_id] = @Atc_id, [AtcRaw_PK] = @AtcRaw_PK, [Template_pk] = @Template_pk, [RMNum] = @RMNum, [Composition] = @Composition, [Construction] = @Construction, [OrderMulti] = @OrderMulti, [OrderMin] = @OrderMin, [Weight] = @Weight, [Width] = @Width, [Uom_PK] = @Uom_PK, [AltUom_pk] = @AltUom_pk, [isCommon] = @isCommon, [IsCD] = @IsCD, [IsSD] = @IsSD, [Currency_pk] = @Currency_pk, [Rate] = @Rate, [WastagePercentage] = @WastagePercentage WHERE [Sku_Pk] = @original_Sku_Pk AND (([Atc_id] = @original_Atc_id) OR ([Atc_id] IS NULL AND @original_Atc_id IS NULL)) AND (([AtcRaw_PK] = @original_AtcRaw_PK) OR ([AtcRaw_PK] IS NULL AND @original_AtcRaw_PK IS NULL)) AND (([Template_pk] = @original_Template_pk) OR ([Template_pk] IS NULL AND @original_Template_pk IS NULL)) AND (([RMNum] = @original_RMNum) OR ([RMNum] IS NULL AND @original_RMNum IS NULL)) AND (([Composition] = @original_Composition) OR ([Composition] IS NULL AND @original_Composition IS NULL)) AND (([Construction] = @original_Construction) OR ([Construction] IS NULL AND @original_Construction IS NULL)) AND (([OrderMulti] = @original_OrderMulti) OR ([OrderMulti] IS NULL AND @original_OrderMulti IS NULL)) AND (([OrderMin] = @original_OrderMin) OR ([OrderMin] IS NULL AND @original_OrderMin IS NULL)) AND (([Weight] = @original_Weight) OR ([Weight] IS NULL AND @original_Weight IS NULL)) AND (([Width] = @original_Width) OR ([Width] IS NULL AND @original_Width IS NULL)) AND (([Uom_PK] = @original_Uom_PK) OR ([Uom_PK] IS NULL AND @original_Uom_PK IS NULL)) AND (([AltUom_pk] = @original_AltUom_pk) OR ([AltUom_pk] IS NULL AND @original_AltUom_pk IS NULL)) AND (([isCommon] = @original_isCommon) OR ([isCommon] IS NULL AND @original_isCommon IS NULL)) AND (([IsCD] = @original_IsCD) OR ([IsCD] IS NULL AND @original_IsCD IS NULL)) AND (([IsSD] = @original_IsSD) OR ([IsSD] IS NULL AND @original_IsSD IS NULL)) AND (([Currency_pk] = @original_Currency_pk) OR ([Currency_pk] IS NULL AND @original_Currency_pk IS NULL)) AND (([Rate] = @original_Rate) OR ([Rate] IS NULL AND @original_Rate IS NULL)) AND (([WastagePercentage] = @original_WastagePercentage) OR ([WastagePercentage] IS NULL AND @original_WastagePercentage IS NULL))">
                    <DeleteParameters>
                        <asp:Parameter Name="original_Sku_Pk" Type="Decimal" />
                        <asp:Parameter Name="original_Atc_id" Type="Decimal" />
                        <asp:Parameter Name="original_AtcRaw_PK" Type="Decimal" />
                        <asp:Parameter Name="original_Template_pk" Type="Decimal" />
                        <asp:Parameter Name="original_RMNum" Type="String" />
                        <asp:Parameter Name="original_Composition" Type="String" />
                        <asp:Parameter Name="original_Construction" Type="String" />
                        <asp:Parameter Name="original_OrderMulti" Type="Decimal" />
                        <asp:Parameter Name="original_OrderMin" Type="Decimal" />
                        <asp:Parameter Name="original_Weight" Type="String" />
                        <asp:Parameter Name="original_Width" Type="String" />
                        <asp:Parameter Name="original_Uom_PK" Type="Decimal" />
                        <asp:Parameter Name="original_AltUom_pk" Type="Decimal" />
                        <asp:Parameter Name="original_isCommon" Type="String" />
                        <asp:Parameter Name="original_IsCD" Type="String" />
                        <asp:Parameter Name="original_IsSD" Type="String" />
                        <asp:Parameter Name="original_Currency_pk" Type="Decimal" />
                        <asp:Parameter Name="original_Rate" Type="Decimal" />
                        <asp:Parameter Name="original_WastagePercentage" Type="Decimal" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Atc_id" Type="Decimal" />
                        <asp:Parameter Name="AtcRaw_PK" Type="Decimal" />
                        <asp:Parameter Name="Template_pk" Type="Decimal" />
                        <asp:Parameter Name="RMNum" Type="String" />
                        <asp:Parameter Name="Composition" Type="String" />
                        <asp:Parameter Name="Construction" Type="String" />
                        <asp:Parameter Name="OrderMulti" Type="Decimal" />
                        <asp:Parameter Name="OrderMin" Type="Decimal" />
                        <asp:Parameter Name="Weight" Type="String" />
                        <asp:Parameter Name="Width" Type="String" />
                        <asp:Parameter Name="Uom_PK" Type="Decimal" />
                        <asp:Parameter Name="AltUom_pk" Type="Decimal" />
                        <asp:Parameter Name="isCommon" Type="String" />
                        <asp:Parameter Name="IsCD" Type="String" />
                        <asp:Parameter Name="IsSD" Type="String" />
                        <asp:Parameter Name="Currency_pk" Type="Decimal" />
                        <asp:Parameter Name="Rate" Type="Decimal" />
                        <asp:Parameter Name="WastagePercentage" Type="Decimal" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenField1" DefaultValue="0" Name="Atc_id" PropertyName="Value" Type="Decimal" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Atc_id" Type="Decimal" />
                        <asp:Parameter Name="AtcRaw_PK" Type="Decimal" />
                        <asp:Parameter Name="Template_pk" Type="Decimal" />
                        <asp:Parameter Name="RMNum" Type="String" />
                        <asp:Parameter Name="Composition" Type="String" />
                        <asp:Parameter Name="Construction" Type="String" />
                        <asp:Parameter Name="OrderMulti" Type="Decimal" />
                        <asp:Parameter Name="OrderMin" Type="Decimal" />
                        <asp:Parameter Name="Weight" Type="String" />
                        <asp:Parameter Name="Width" Type="String" />
                        <asp:Parameter Name="Uom_PK" Type="Decimal" />
                        <asp:Parameter Name="AltUom_pk" Type="Decimal" />
                        <asp:Parameter Name="isCommon" Type="String" />
                        <asp:Parameter Name="IsCD" Type="String" />
                        <asp:Parameter Name="IsSD" Type="String" />
                        <asp:Parameter Name="Currency_pk" Type="Decimal" />
                        <asp:Parameter Name="Rate" Type="Decimal" />
                        <asp:Parameter Name="WastagePercentage" Type="Decimal" />
                        <asp:Parameter Name="original_Sku_Pk" Type="Decimal" />
                        <asp:Parameter Name="original_Atc_id" Type="Decimal" />
                        <asp:Parameter Name="original_AtcRaw_PK" Type="Decimal" />
                        <asp:Parameter Name="original_Template_pk" Type="Decimal" />
                        <asp:Parameter Name="original_RMNum" Type="String" />
                        <asp:Parameter Name="original_Composition" Type="String" />
                        <asp:Parameter Name="original_Construction" Type="String" />
                        <asp:Parameter Name="original_OrderMulti" Type="Decimal" />
                        <asp:Parameter Name="original_OrderMin" Type="Decimal" />
                        <asp:Parameter Name="original_Weight" Type="String" />
                        <asp:Parameter Name="original_Width" Type="String" />
                        <asp:Parameter Name="original_Uom_PK" Type="Decimal" />
                        <asp:Parameter Name="original_AltUom_pk" Type="Decimal" />
                        <asp:Parameter Name="original_isCommon" Type="String" />
                        <asp:Parameter Name="original_IsCD" Type="String" />
                        <asp:Parameter Name="original_IsSD" Type="String" />
                        <asp:Parameter Name="original_Currency_pk" Type="Decimal" />
                        <asp:Parameter Name="original_Rate" Type="Decimal" />
                        <asp:Parameter Name="original_WastagePercentage" Type="Decimal" />
                    </UpdateParameters>
                </asp:SqlDataSource>



                <asp:SqlDataSource ID="atcmasterdata" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>"
                    SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]"></asp:SqlDataSource>



            </td>
        </tr>
        <tr>
            <td>

           


            </td>
        </tr>
    </table>

        </asp:View>

        <asp:View ID="View1" runat="server">




            <table class="FullTable">
        <tr>
            <td class="RedHeadding">ATC RAW MATERIAL</td>
        </tr>
        <tr>
            <td >
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD"><span class="auto-style22">&nbsp;ATC # :&nbsp;</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmb_atc" ErrorMessage="Select Atc" ForeColor="#FF0066" InitialValue="Select Atc" ValidationGroup="AtcValidation" CssClass="auto-style22">*</asp:RequiredFieldValidator>

                        </td>
                        <td class="NormalTD">


                              <ucc:DropDownListChosen ID="drp_skudetatc" runat="server" DataSourceID="atcdetdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" OnSelectedIndexChanged="cmb_atc_SelectedIndexChanged">
                            </ucc:DropDownListChosen>
                        </td>
                        <td class="NormalTD">
                            <asp:Button ID="Button3" runat="server" Text="Show Sku Details" ToolTip="Show the BOM (Sku Details)Of the Atc" OnClick="Button1_Click" />
                            </td>
                        <td class="NormalTD">



                            <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />

                        </td>
                        <td class="NormalTD" rowspan="3">



                          



                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">

                            &nbsp;</td>
                        <td class="NormalTD">

                            
                            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Delete Sku Details" />

                            
                        </td>
                        <td class="NormalTD">

                            &nbsp;</td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                             </ContentTemplate>


                    </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="smallgridtable">
            <td >

              
                    <asp:UpdatePanel ID="UpdatePanel2"  UpdateMode="Conditional" runat="server">






                        <ContentTemplate>
                            <asp:GridView ID="tbl_skuDetails" runat="server" AutoGenerateColumns="False" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SkuDet_PK" DataSourceID="skudet">
                                <Columns>
                                     <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                     <asp:TemplateField HeaderText="SkuDet_PK" InsertVisible="False" SortExpression="SkuDet_PK">
                                        
                                         <ItemTemplate>
                                             <asp:Label ID="lbl_skudetpk" runat="server" Text='<%# Bind("SkuDet_PK") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                    <asp:BoundField DataField="RMNum" HeaderText="RMNum" SortExpression="RMNum" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                                    <asp:BoundField DataField="ColorCode" HeaderText="ColorCode" SortExpression="ColorCode" />
                                    <asp:BoundField DataField="SizeCode" HeaderText="SizeCode" SortExpression="SizeCode" />
                                    <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" SortExpression="ItemColor" />
                                    <asp:BoundField DataField="SupplierColor" HeaderText="SupplierColor" SortExpression="SupplierColor" />
                                    <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" SortExpression="ItemSize" />
                                    <asp:BoundField DataField="SupplierSize" HeaderText="SupplierSize" SortExpression="SupplierSize" />
                                    <asp:BoundField DataField="Pogiven" HeaderText="Pogiven" ReadOnly="True" SortExpression="Pogiven" />
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </ContentTemplate>






                    </asp:UpdatePanel>
                
            </td>
        </tr>
       
        <tr>
            <td>
                   <asp:UpdatePanel ID="UpdatePanel3"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                <asp:Button ID="Button4" runat="server" Text="Update SKU Master" ValidationGroup="skumstr" OnClick="Btn_updatesku_Click" />

 </ContentTemplate>


                    </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>

                <asp:SqlDataSource ID="atcdetdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [AtcId], [AtcNum] FROM [AtcMaster]">
                </asp:SqlDataSource>



                <asp:SqlDataSource ID="skudet" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>"
                    SelectCommand="SELECT Atc_id, SkuDet_PK, RMNum, Description, ColorCode, SizeCode, ItemColor, SupplierColor, ItemSize, SupplierSize, ISNULL(pogiven, 0) AS Pogiven FROM (SELECT SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode, SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, (SELECT COUNT(ProcurementMaster.PONum) AS Expr1 FROM ProcurementMaster INNER JOIN ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk WHERE (ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK) GROUP BY ProcurementDetails.SkuDet_PK) AS pogiven, SkuRawMaterialMaster.Atc_id FROM SkuRawmaterialDetail INNER JOIN SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK INNER JOIN Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK LEFT OUTER JOIN SizeMaster ON SkuRawmaterialDetail.SizeCode = SizeMaster.SizeCode WHERE (SkuRawMaterialMaster.Atc_id = @Param1)) AS tt">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drp_skudetatc" Name="Param1" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>



            </td>
        </tr>
        <tr>
            <td>

           


            </td>
        </tr>
    </table>







        </asp:View>
    </asp:MultiView>




</asp:Content>
