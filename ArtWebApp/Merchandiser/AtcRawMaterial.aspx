<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Merchandiser_AtcRawMaterial" CodeBehind="AtcRawMaterial.aspx.cs" %>

<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
       

        .hidden {
            display: none;
        }

        
    </style>
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" id="igClientScript">
<!--

    //function Validate() {
    //    var isValid = false;
    //    isValid = Page_ClientValidate('VG1');
    //    if (isValid) {
    //        isValid = Page_ClientValidate('VG2');
    //    }

    //    return isValid;
    //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">ATC RAW MATERIAL</td>
        </tr>
        <tr>
            <td class="auto-style22">
                <table class="DataEntryTable">
                    <tr>
                        <td class="auto-style17"><span class="auto-style22">&nbsp;ATC # :&nbsp;</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmb_atc" ErrorMessage="Select Atc" ForeColor="#FF0066" InitialValue="Select Atc" ValidationGroup="AtcValidation" CssClass="auto-style22">*</asp:RequiredFieldValidator>

                        </td>
                        <td class="auto-style19">

                             <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="atcmasterdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td class="auto-style9">

                            

                           
                                    <asp:Button ID="buttonAtc" runat="server" CssClass="auto-style22" Height="26px" OnClick="buttonAtc_Click" Text="S" ValidationGroup="AtcValidation" />
                             

                        </td>
                        <td class="auto-style14">



                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />

                        </td>
                        <td class="auto-style24" rowspan="4">



                            <br class="auto-style22" />



                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style16"><span class="auto-style22">ITEMS</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmb_item" ErrorMessage="Select Raw Material" ForeColor="#FF0066" InitialValue="Select Raw Material" ValidationGroup="valItem" CssClass="auto-style22">*</asp:RequiredFieldValidator>

                        </td>
                        <td class="auto-style20">

                            <asp:UpdatePanel ID="upd_item" runat="server">
                                <ContentTemplate>
                                    <ig:WebDropDown ID="cmb_item" runat="server" Width="200px"
                                DataSourceID="ItemMasterData" TextField="Description" ValueField="Template_pk"
                                OnSelectionChanged="cmb_item_SelectionChanged" EnableMultipleSelection="True" EnableClosingDropDownOnSelect="False" CurrentValue="Select Raw Material">
                                        <DropDownItemBinding TextField="Description" ValueField="Template_pk" />
                                    </ig:WebDropDown>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="auto-style10">

                            

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btn_item" runat="server" CssClass="auto-style22" Font-Size="Smaller" Height="26px" OnClick="btn_item_Click" Text="Add Raw Material" ValidationGroup="valItem" Width="174px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                        <td class="auto-style15">

                            &nbsp;</td>
                    </tr>

                    <tr>
                        <td class="auto-style16">Group item</td>
                        <td class="auto-style20">

                            <asp:UpdatePanel ID="upd_item0" runat="server">
                                <ContentTemplate>
                                    <ig:WebDropDown ID="cmb_item0" runat="server" CurrentValue="Select Raw Material" DataSourceID="ItemMasterData" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" OnSelectionChanged="cmb_item_SelectionChanged" TextField="Description" ValueField="Template_pk" Width="200px">
                                        <DropDownItemBinding TextField="Description" ValueField="Template_pk" />
                                    </ig:WebDropDown>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="auto-style10">

                            

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btn_item0" runat="server" CssClass="auto-style22" Font-Size="Smaller" Height="26px" OnClick="btn_item0_Click" Text="Add  Group Raw Material" ValidationGroup="ASDDSFSDF" Width="172px" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                        <td class="auto-style15">

                            &nbsp;</td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
           <td class="NormalTD">
                <table class="DataEntryTable" style="height: 353px">
                    <tr>
                        <td colspan="2" class="gridtable">
                             <asp:UpdatePanel ID="upd_rawmaterial" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" DataKeyFields="AtcRaw_PK" DataSourceID="SqlDataSource1" Height="100%" Width="100%">
                                        <Columns>
                                            <ig:BoundDataField DataFieldName="AtcRaw_PK" Key="AtcRaw_PK">
                                                <Header Text="ID">
                                                </Header>
                                            </ig:BoundDataField>
                                            <ig:BoundDataField DataFieldName="Template_PK" Hidden="True" Key="Template_PK">
                                                <Header Text="TempID">
                                                </Header>
                                            </ig:BoundDataField>
                                            <ig:BoundDataField DataFieldName="Atc_id" Key="Atc_id">
                                                <Header Text="AtcId">
                                                </Header>
                                            </ig:BoundDataField>
                                            <ig:BoundDataField DataFieldName="TempCode" Hidden="True" Key="TempCode">
                                                <Header Text="Code">
                                                </Header>
                                            </ig:BoundDataField>
                                            <ig:BoundDataField DataFieldName="TemplateName" Key="TemplateName">
                                                <Header Text="TemplateName">
                                                </Header>
                                            </ig:BoundDataField>
                                            <ig:BoundDataField DataFieldName="TemplateCount" Key="TemplateCount">
                                                <Header Text="Count">
                                                </Header>
                                            </ig:BoundDataField>
                                        </Columns>
                                        <Behaviors>
                                            <ig:EditingCore>
                                                <Behaviors>
                                                    <ig:CellEditing>
                                                    </ig:CellEditing>
                                                </Behaviors>
                                            </ig:EditingCore>
                                        </Behaviors>
                                    </ig:WebDataGrid>
                                </ContentTemplate>
                            </asp:UpdatePanel>                        
                      


                        </td>
                    </tr>

                    <tr>
                        <td class="SmallSearchButton">
                            <asp:Button ID="btn_generatesku" runat="server" OnClick="btn_generatesku_Click" Text="Generate SKU" />



                         



                        </td>
                        <td class="auto-style11">&nbsp;</td>
                    </tr>

                    </table>
            </td>
        </tr>
        <tr>
            <td class="gridtable">

                <div id="grid" style="overflow: auto">
                    <asp:UpdatePanel ID="upd_skugrid"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="tbl_skumaster" runat="server" AutoGenerateColumns="False" DataKeyNames="Sku_Pk" DataSourceID="Skumaster" OnRowEditing="GridView1_RowEditing" OnRowDataBound="tbl_skumaster_RowDataBound" Width="87%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri">
                                <Columns>
                                    <asp:BoundField DataField="Sku_Pk" HeaderText="Sku_Pk" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" InsertVisible="False" ReadOnly="True" SortExpression="Sku_Pk">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="TempID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="Template_pk">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_templatepk" runat="server" Text='<%# Bind("Template_pk") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TemplateCode" HeaderText="Code" SortExpression="TemplateCode" />
                                    <asp:BoundField DataField="Description" HeaderText="Item" SortExpression="Description" />
                                    <asp:BoundField DataField="RMNum" HeaderText="RMNum" SortExpression="RMNum" />
                                    <asp:TemplateField HeaderText="Composition" SortExpression="Composition">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_com" runat="server" Visible="false" Text='<%# Bind("Composition") %>'></asp:Label>
                                            <ucc:DropDownListChosen  ID="ddl_comp"  Width="170px" runat="server">
                                            </ucc:DropDownListChosen>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Construction" SortExpression="Construction">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_con" runat="server" Visible="false" Text='<%# Bind("Construction") %>'></asp:Label>
                                           <ucc:DropDownListChosen ID="ddl_con" Width="170px" runat="server">
                                             </ucc:DropDownListChosen>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Width" SortExpression="Width">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_width" Visible="false" runat="server" Text='<%# Bind("Width") %>'></asp:Label>
                                           <ucc:DropDownListChosen ID="ddl_width" Width="120px" runat="server">
                                            </ucc:DropDownListChosen>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight" SortExpression="Weight">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_weight" Visible="false" runat="server" Text='<%# Bind("Weight") %>'></asp:Label>
                                            <ucc:DropDownListChosen ID="ddl_weight" Width="120px" runat="server">
                                             </ucc:DropDownListChosen>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CD" SortExpression="IsCD">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_iscd" Visible="false" runat="server" Text='<%# Bind("IsCD") %>'></asp:Label>
                                            <asp:CheckBox ID="chk_isCD" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SD" SortExpression="IsSD">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_isSD" Visible="false" runat="server" Text='<%# Bind("IsSD") %>'></asp:Label>
                                            <asp:CheckBox ID="chk_isSD" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CM" SortExpression="isCommon">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_isCommon" Visible="false" runat="server" Text='<%# Bind("isCommon") %>'></asp:Label>
                                            <asp:CheckBox ID="chk_isCommon" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="GD" SortExpression="isGD">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_isGD" Visible="false" runat="server" Text='<%# Bind("isGD") %>'></asp:Label>
                                            <asp:CheckBox ID="chk_isisGD" runat="server" Enabled="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="TD" SortExpression="isTD">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_isTD" Visible="false" runat="server" Text='<%# Bind("isTD") %>'></asp:Label>
                                            <asp:CheckBox ID="chk_isTD" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="PD" SortExpression="isPD">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_isPD" Visible="false" runat="server" Text='<%# Bind("isPD") %>'></asp:Label>
                                            <asp:CheckBox ID="chk_isPD" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate" SortExpression="Rate">

                                        <ItemTemplate>

                                            <asp:TextBox ID="txt_rate" runat="server" Text='<%# Bind("Rate") %>' Height="16px" Width="74px"></asp:TextBox>USD
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_rate" ErrorMessage="*" ForeColor="#CC3300" ValidationGroup="skumstr"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_rate" ErrorMessage="**" ValidationExpression="^[\d.]+$" ValidationGroup="skumstr"></asp:RegularExpressionValidator>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="OrderMulti" ItemStyle-CssClass="hidden" HeaderText="OrderMulti" HeaderStyle-CssClass="hidden" SortExpression="OrderMulti">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="UOM" SortExpression="CurrencyCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UOM" runat="server" Text='<%# Bind("UomCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wastage" SortExpression="Wastage">
                                <ItemTemplate>
                                   
                                            <asp:TextBox ID="txt_Wastage" runat="server" Text='<%# Bind("WastagePercentage") %>'  AutoPostBack="True" Height="16px" Width="59px" ></asp:TextBox>
                                 
                                            %
                                 
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AltUOM" SortExpression="AltUOM_pk">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_altuompk" runat="server" Text='<%# Bind("AltUom_pk") %>' Visible="False"></asp:Label>
                                            <asp:DropDownList ID="ddl_AltUOM" runat="server" DataSourceID="BaseUOM" DataTextField="UomCode" DataValueField="Uom_PK">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   
                                   
                                  <asp:TemplateField HeaderText="OrderMin" SortExpression="OrderMin">

                                    <ItemTemplate>
                                   
                                            <asp:TextBox ID="txt_ordermin" runat="server" Text='<%# Bind("OrderMin") %>'  AutoPostBack="True" Height="16px" Width="59px" ></asp:TextBox>
                                                                                                              
                                </ItemTemplate>
</asp:TemplateField>
                                    <asp:BoundField DataField="AtcRaw_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" HeaderText="AtcRawPK" SortExpression="AtcRaw_PK">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Atc_id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" HeaderText="Atc_id" SortExpression="Atc_id">
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>

                                    <asp:TemplateField ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" HeaderText="UomPK">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_uompk" runat="server" Text='<%# Bind("uom_pk") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:TemplateField>
                              
                                    <asp:TemplateField HeaderText="Body Part">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_body" Text='<%# Bind("BodyPartName") %>' runat="server"></asp:Label>
                                            <br />
                                            <asp:DropDownList ID="ddl_body" runat="server" DataSourceID="bodyPart" DataTextField="BodyPartName" DataValueField="BodyPart_PK">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="bodyPart" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [BodyPartName], [BodyPart_PK] FROM [BodyPartMaster]"></asp:SqlDataSource>
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
                </div>
            </td>
        </tr>
       
        <tr>
            <td class="SmallSearchButton">
                <asp:UpdatePanel ID="upd_btn"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                <asp:Button ID="Btn_updatesku" runat="server" Text="Update SKU Master" OnClick="Btn_updatesku_Click" ValidationGroup="skumstr" />

                            <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
                      </ContentTemplate>


                    </asp:UpdatePanel>

                
            </td>
        </tr>
       
        <tr>
            <td >
               </td>
        </tr>
        <tr>
           <td class="NormalTD">

                <asp:SqlDataSource ID="Skumaster" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT SkuRawMaterialMaster.Sku_Pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.AtcRaw_PK, Template_Master.TemplateCode, Template_Master.Description, SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.OrderMulti, ISNULL(SkuRawMaterialMaster.OrderMin, 0) AS OrderMin, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, SkuRawMaterialMaster.IsSD, SkuRawMaterialMaster.IsGD, SkuRawMaterialMaster.IsTD, SkuRawMaterialMaster.IsPD, ISNULL(SkuRawMaterialMaster.Rate, 0) AS Rate, SkuRawMaterialMaster.Template_pk, UOMMaster.UomCode, UOMMaster.Uom_PK, SkuRawMaterialMaster.Uom_PK AS Expr1, ISNULL(SkuRawMaterialMaster.WastagePercentage, 0) AS WastagePercentage, SkuRawMaterialMaster.BodyPartName FROM SkuRawMaterialMaster INNER JOIN Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN UOMMaster ON Template_Master.Uom_PK = UOMMaster.Uom_PK WHERE (SkuRawMaterialMaster.Atc_id = @Atc_id)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenField1" DefaultValue="0" Name="Atc_id" PropertyName="Value" Type="Decimal" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="Compositiondata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [TemplateCom_Pk], [Composition] FROM [TemplateComposition] WHERE ([Template_Pk] = @Template_Pk)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TemplatePk" DefaultValue="0" Name="Template_Pk" PropertyName="Value" Type="Decimal" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="Width" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT TemplateWidth_Pk, Template_Pk, Width FROM TemplateWidth"></asp:SqlDataSource>



                <asp:SqlDataSource ID="atcmasterdata" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>"
                    SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]"></asp:SqlDataSource>



                <asp:SqlDataSource ID="ConstructionData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Construct], [TemplateCon_Pk], [Template_Pk] FROM [TemplateConstruction] WHERE ([Template_Pk] = @Template_Pk)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TemplatePk" DefaultValue="0" Name="Template_Pk" PropertyName="Value" Type="Decimal" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="ItemMasterData" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>"
                    SelectCommand="SELECT Template_PK, TemplateCode, Description, ItemGroup_PK FROM Template_Master WHERE (ItemGroup_PK = 1) OR (ItemGroup_PK = 2) ORDER BY TemplateCode"></asp:SqlDataSource>



                <asp:SqlDataSource ID="Weight" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [TemplateWeight_Pk], [Template_Pk], [Weight] FROM [TemplateWeight]"></asp:SqlDataSource>



                <asp:HiddenField ID="TemplatePk" runat="server" Value="0" ViewStateMode="Enabled" />



                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [AtcRawMaterialMaster] WHERE [AtcRaw_PK] = @original_AtcRaw_PK AND (([Template_PK] = @original_Template_PK) OR ([Template_PK] IS NULL AND @original_Template_PK IS NULL)) AND (([Atc_id] = @original_Atc_id) OR ([Atc_id] IS NULL AND @original_Atc_id IS NULL)) AND (([TempCode] = @original_TempCode) OR ([TempCode] IS NULL AND @original_TempCode IS NULL)) AND (([TemplateName] = @original_TemplateName) OR ([TemplateName] IS NULL AND @original_TemplateName IS NULL)) AND (([TemplateCount] = @original_TemplateCount) OR ([TemplateCount] IS NULL AND @original_TemplateCount IS NULL))" InsertCommand="INSERT INTO [AtcRawMaterialMaster] ([Template_PK], [Atc_id], [TempCode], [TemplateName], [TemplateCount]) VALUES (@Template_PK, @Atc_id, @TempCode, @TemplateName, @TemplateCount)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT AtcRaw_PK, Template_PK, Atc_id, TempCode, TemplateName, TemplateCount FROM [AtcRawMaterialMaster ] WHERE (Atc_id = @Atc_id) ORDER BY TempCode" UpdateCommand="UPDATE [AtcRawMaterialMaster] SET [Template_PK] = @Template_PK, [Atc_id] = @Atc_id, [TempCode] = @TempCode, [TemplateName] = @TemplateName, [TemplateCount] = @TemplateCount WHERE [AtcRaw_PK] = @original_AtcRaw_PK AND (([Template_PK] = @original_Template_PK) OR ([Template_PK] IS NULL AND @original_Template_PK IS NULL)) AND (([Atc_id] = @original_Atc_id) OR ([Atc_id] IS NULL AND @original_Atc_id IS NULL)) AND (([TempCode] = @original_TempCode) OR ([TempCode] IS NULL AND @original_TempCode IS NULL)) AND (([TemplateName] = @original_TemplateName) OR ([TemplateName] IS NULL AND @original_TemplateName IS NULL)) AND (([TemplateCount] = @original_TemplateCount) OR ([TemplateCount] IS NULL AND @original_TemplateCount IS NULL))">
                    <DeleteParameters>
                        <asp:Parameter Name="original_AtcRaw_PK" Type="Decimal" />
                        <asp:Parameter Name="original_Template_PK" Type="Decimal" />
                        <asp:Parameter Name="original_Atc_id" Type="Decimal" />
                        <asp:Parameter Name="original_TempCode" Type="String" />
                        <asp:Parameter Name="original_TemplateName" Type="String" />
                        <asp:Parameter Name="original_TemplateCount" Type="Decimal" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Template_PK" Type="Decimal" />
                        <asp:Parameter Name="Atc_id" Type="Decimal" />
                        <asp:Parameter Name="TempCode" Type="String" />
                        <asp:Parameter Name="TemplateName" Type="String" />
                        <asp:Parameter Name="TemplateCount" Type="Decimal" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenField1" DefaultValue="0" Name="Atc_id" PropertyName="Value" Type="Decimal" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Template_PK" Type="Decimal" />
                        <asp:Parameter Name="Atc_id" Type="Decimal" />
                        <asp:Parameter Name="TempCode" Type="String" />
                        <asp:Parameter Name="TemplateName" Type="String" />
                        <asp:Parameter Name="TemplateCount" Type="Decimal" />
                        <asp:Parameter Name="original_AtcRaw_PK" Type="Decimal" />
                        <asp:Parameter Name="original_Template_PK" Type="Decimal" />
                        <asp:Parameter Name="original_Atc_id" Type="Decimal" />
                        <asp:Parameter Name="original_TempCode" Type="String" />
                        <asp:Parameter Name="original_TemplateName" Type="String" />
                        <asp:Parameter Name="original_TemplateCount" Type="Decimal" />
                    </UpdateParameters>
                </asp:SqlDataSource>



                        <asp:SqlDataSource ID="BaseUOM" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT Uom_PK, UomCode FROM UOMMaster WHERE (UomType = N'BASE') ORDER BY UomCode, UomName"></asp:SqlDataSource>



            </td>
        </tr>
    </table>
</asp:Content>

