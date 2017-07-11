<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="StyleCosting.aspx.cs" Inherits="ArtWebApp.Merchandiser.StyleCosting" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
 <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .dropdownStyle {
            height: 23px;
            width: 200px;
            background-color: #FFFF99;
        }
        .auto-style9 {
            font-family: Calibri;
            font-size: small;
            height: 25px;
        }
        .auto-style13 {
            height: 179px;
        }
        .auto-style14 {
            width: 100%;
        }
        .auto-style15 {
        }
        .auto-style16 {
            text-align: center;
            font-weight: bold;
            font-size: medium;
        }
        .auto-style17 {
            font-size: medium;
            font-weight: bold;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
        }
        .auto-style18 {
            width: 100%;
            text-align: center;
        }
        .auto-style22 {
            width: 204px;
        }
        .auto-style23 {
            width: 164px;
        }
        .RedHeadding {
    color: #FFFFFF;
    text-align: center;
    background-color: #990000;
   font-weight: bold;
    font-family: Calibri;
    font-size: large;
    height: 22px;
    width: 191px;
    text-align:center;
}
        .auto-style25 {}
        .auto-style26 {
            width: 208px;
        }
        .auto-style27 {
            width: 142px;
        }
        .auto-style29 {
            width: 471px;
        }
        .auto-style30 {
            height: 23px;
            width: 188px;
        }
        .auto-style32 {
            height: 23px;
            width: 471px;
            text-align: center;
        }
        .auto-style34 {
            width: 183px;
            height: 24px;
        }
        .auto-style35 {
            height: 24px;
        }
        .auto-style36 {
            height: 25px;
            text-align: center;
        }
        .auto-style37 {
            width: 188px;
        }
        .auto-style38 {
            width: 183px;
        }
        .auto-style40 {
            width: 183px;
            height: 26px;
        }
        .auto-style41 {
            height: 26px;
        }
        </style>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/GridJQuery.js"></script>
    <script type="text/javascript">

       //calculate the sum of qty on keypress
       function sumofQty(objText) {
       
          
           var cell = objText.parentNode;
           var row = cell.parentNode;
          
           var sum = 0;
           var txtconsumption = row.getElementsByClassName("txtconsumption");
           var txtrate = row.getElementsByClassName("txtrate");
           var lblpcpr = row.getElementsByClassName("lblpcpr");
           var lblpcprdzn = row.getElementsByClassName("lblpcprdzn");
           
         
           
           var perpc= parseFloat(txtconsumption[0].value) * parseFloat(txtrate[0].value);
           var perdzn = perpc * 12;

   
           

           lblpcpr[0].innerHTML = perpc.toString();
           lblpcprdzn[0].innerHTML = perdzn.toString();
         

       }

    




       function RefreshAll(objref) {
           debugger
           var retVal = confirm("Do you want to continue  Refresh Costing calculation ?");
           if (retVal == true) {



              

          

               PageMethods.RefreshAll( onSucess, onError);
               function onSucess(result) {
                  
               }
               function onError(result) {
                   alert('Something wrong.');
               }
           }
           else {

           }


           return false;

       }








       function calculateforAll()
       {
               var gridView = document.getElementById("<%= tbl_costing.ClientID %>");

        

        
           for (var i = 1; i < gridView.rows.length; i++) {
               

                   var sum = 0;
                   var txtconsumption = gridView.rows[i].getElementsByClassName("txtconsumption");
                   var txtrate = gridView.rows[i].getElementsByClassName("txtrate");
                   var lblpcpr = gridView.rows[i].getElementsByClassName("lblpcpr");
                   var lblpcprdzn = gridView.rows[i].getElementsByClassName("lblpcprdzn");



                   var perpc = parseFloat(txtconsumption[0].value) * parseFloat(txtrate[0].value);
                   var perdzn = perpc * 12;




                   lblpcpr[0].innerHTML = perpc.toString();
                   lblpcprdzn[0].innerHTML = perdzn.toString();
              
           }

       }



</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td  class="RedHeadding">STYLE COSTING</td>
    </tr>
    <tr>
        <td>
            <table class="DataEntryTable">
              
                <tr>
                    <td   class="NormalTD">Atc # :</td>
                    <td>
                       <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional"  runat="server">
                            <ContentTemplate>
                         <ucc:DropDownListChosen ID="ddl_atc" runat="server" DataSourceID="AtcSource" DataTextField="Atcnum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="SearchButtonTD" >
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="S" />
                    </td>
                    <td class="NormalTD" >OurStyle # :</td>
                    <td >
                       
                        <ucc:DropDownListChosen ID="ddl_ourstyle" runat="server" DataSourceID="ourstylesource" DataTextField="OurStyle" DataValueField="OurstyleId" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                    </td>
                    <td class="SearchButtonTD" >
                        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional"  runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_showRawmaterial" runat="server" OnClick="btn_showRawmaterial_Click" Text="s" CssClass="auto-style9" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td >&nbsp;</td>
                    
                    
                    
                </tr>
                <tr>
                    <td colspan="7"><strong>Enter&nbsp; Raw Material Costing</strong></td>
                </tr>
                <tr>
                    <td colspan="7">
                        <table >
                            <tr>
                                <td >
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td><asp:Label ID="lbl_cost" runat="server"></asp:Label>
                                </td>
                                <td>
                                      <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional"  runat="server">
                            <ContentTemplate>
                                    <asp:Button ID="btn_refresh" OnClick="btn_refresh_Click" runat="server" Text="Refresh" />
                                  </ContentTemplate>
                        </asp:UpdatePanel>
                               </td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="font-size: small; font-family: Calibri">

          
            <asp:UpdatePanel ID="upd_gridpanel" UpdateMode="Conditional"  ChildrenAsTriggers="false" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="tbl_costing" runat="server" AutoGenerateColumns="False" OnRowDataBound="tbl_costing_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1023px" ShowHeaderWhenEmpty="True">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sku_Pk" SortExpression="Sku_Pk">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_sku" runat="server" Text='<%# Bind("Sku_Pk") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                          

                                                        




                            <asp:TemplateField HeaderText="IsReq" SortExpression="IsRequired">
                                <HeaderTemplate>
                                      <asp:CheckBox ID="chk_isrequired" runat ="server" onclick="checkAll(this)"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_isrequired" runat="server" onclick="Check_Click(this)"/>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RMNum" HeaderText="RMNum" ReadOnly="True" SortExpression="RMNum" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                            <asp:BoundField DataField="Construction" HeaderText="Construction" ReadOnly="True" SortExpression="Construction" />
                            <asp:BoundField DataField="Weight" HeaderText="Weight" ReadOnly="True" SortExpression="Weight" />
                            <asp:BoundField DataField="Width" HeaderText="Width" ReadOnly="True" SortExpression="Width" />
                            <asp:TemplateField HeaderText="Wastage" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Wastage" runat="server" Text='<%# Bind("WastagePercentage") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RATE" SortExpression="RATE">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_rate" CssClass ="txtrate" Enabled="false" runat="server" Text='<%# Bind("RATE") %>' AutoPostBack="false"  onkeyup="sumofQty(this)"   OnTextChanged="txt_rate_TextChanged"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_rate" ErrorMessage="Required" ForeColor="#CC3300">*</asp:RequiredFieldValidator>
                                    
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txt_rate" ErrorMessage="Enter valid Unit Price" ForeColor="Red" ValidationExpression="^[\d.]+$">*</asp:RegularExpressionValidator>
                                     </ItemTemplate>
                              
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Consumption" SortExpression="Consumption">
                                <ItemTemplate>
                                   
                                            <asp:TextBox ID="txt_consumption" CssClass ="txtconsumption" runat="server" Text='<%# Bind("Consumption") %>' AutoPostBack="false"  onkeyup="sumofQty(this)"    OnTextChanged="txt_consumption_TextChanged"></asp:TextBox>
                                     
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_consumption" ErrorMessage="Required" ForeColor="#CC3300">*</asp:RequiredFieldValidator>
                                    
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txt_consumption" ErrorMessage="Enter valid Consumption" ForeColor="Red" ValidationExpression="^[\d.]+$">*</asp:RegularExpressionValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                             <asp:TemplateField HeaderText="Pr/Pc">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_pcpr" CssClass ="lblpcpr" runat="server" Text='<%# Bind("priceperpc") %>'  ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pr/Dz">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_pcDzn" CssClass ="lblpcprdzn" runat="server" Text='<%# Bind("PriceperDozen") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOMCODE">
                                
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsRequired">                                
                                <ItemTemplate>
                                   <asp:Label ID="lbl_isrequired"  runat="server" Text='<%# Bind("IsRequired") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsGD">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lbl_isgsd" runat="server" Text='<%# Bind("IsGD") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AtcRaw_PK" HeaderText="AtcRaw_PK" />

                             <asp:TemplateField HeaderText="NewPerPC">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NewPerPC" runat="server" Text='<%# Bind("NewPerPC") %>'></asp:Label>
                                </ItemTemplate>
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
    </tr>
    <tr>
        <td>
            <div class="ButtonTR">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"  runat="server">
                <ContentTemplate>
                <asp:Button ID="btn_InsertCosting" runat="server" Text="Confirm Raw Material Costing" OnClick="btn_InsertCosting_Click" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_message" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            <table class="DataEntryTable">
                <tr>
                    <td class="auto-style32"><strong>Enter&nbsp;Fixed Costing Components</strong></td>
                    <td class="auto-style30"></td>
                    <td class="auto-style7"></td>
                </tr>
                <tr class="DataEntryTable">
                    <td class="auto-style29">
                    <asp:UpdatePanel ID="upd_mandatorypandel" UpdateMode="Conditional"  runat="server">
                <ContentTemplate>
                    <asp:GridView ID="tbl_manadatorycomponent" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:TemplateField HeaderText="CC_Pk">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_costcomp_pk" runat="server" Text='<%# Bind("CostComp_Pk") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ComponentName">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ComponentName" runat="server" Text='<%# Bind("ComponentName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                
                            <asp:TemplateField HeaderText="CompValue">
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_compvalue" runat="server" Text='<%# Bind("CompValue") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CalculationMode">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_calcmode" runat="server" Text='<%# Bind("CalculationMode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
                    </td>
                    <td class="auto-style37">&nbsp;</td>
                    <td rowspan="4">
                        <asp:UpdatePanel ID="upd_basic"   UpdateMode="Conditional"  runat="server">
                            <ContentTemplate>
                                <table class="auto-style14" style="font-family: Calibri; font-size: large; font-weight: bold; font-style: italic">
                                    <tr>
                                        <td class="auto-style36" colspan="2">&nbsp;Costing Details :&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style34">Projection Qty</td>
                                        <td class="auto-style35">
                                            <asp:Label ID="lbl_projqty" runat="server" CssClass="auto-style9" Height="0px" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style34">Style asq Qty</td>
                                        <td class="auto-style35">
                                            <asp:Label ID="lbl_styleqty" runat="server" CssClass="auto-style9" Height="0px" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style40">FOB :</td>
                                        <td class="auto-style41">
                                            <asp:Label ID="lbl_stylefob" runat="server" CssClass="auto-style9" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style38">Total Cost :</td>
                                        <td>
                                            <asp:Label ID="lbl_styletotalcost" runat="server" CssClass="auto-style9" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style38">Margin :</td>
                                        <td>
                                            <asp:Label ID="lbl_stylemargin" runat="server" CssClass="auto-style9" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="ButtonTR">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_addprimaryComponent" runat="server" OnClick="btn_addprimaryComponent_Click" Text="Confirm Primary Costing Components" />
                                <asp:Label ID="lbl_primaryComponentmsg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="auto-style37">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style32"><span class="auto-style17"> Enter Optional Components&nbsp;</span></td>
                    <td class="auto-style30"></td>
                </tr>
                <tr>
                    <td class="auto-style29">
                        <table class="auto-style14">
                            <tr>
                                <td class="auto-style25">Optional Components : </td>
                                <td class="auto-style26">
                                    <asp:UpdatePanel ID="udp_optionalcombo"  UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <ig:WebDropDown ID="drp_optionalcomb" runat="server" Width="200px" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="ComponentName" ValueField="CostComp_PK">
                                                <DropDownItemBinding TextField="ComponentName" ValueField="CostComp_PK" />
                                            </ig:WebDropDown>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="auto-style27">
                                    <asp:UpdatePanel ID="UpdatePanel2"  UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                        <asp:Button ID="btn_addOptionalData" runat="server" OnClick="btn_addOptionalData_Click" Text="Add to Costing" />
                                 </ContentTemplate>
                        </asp:UpdatePanel>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style25" colspan="3">
                        <asp:UpdatePanel ID="udp_optionalGrid"  UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_OptionalComponent" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="CostComp_PK">
                                    <Columns>
                                         <asp:TemplateField HeaderText="CC_Pk">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_optcostcomp_pk" runat="server" Text='<%# Bind("CostComp_Pk") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                        <asp:BoundField DataField="ComponentName" HeaderText="ComponentName" />
                                        <asp:TemplateField HeaderText="CompValue">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_optcompvalue" runat="server" Text='<%# Bind("CompValue") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CalculationMode">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_optcalcmode" runat="server" DataSourceID="CalculationMode" DataTextField="CalculationMode" DataValueField="CalCulationMode_PK" Width="100%">
                                                </asp:DropDownList>
                            <br />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="ButtonTR" colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_secondarycomponent" runat="server" Text="Confirm Optional Components" OnClick="btn_secondarycomponent_Click" />
                                            <asp:Label ID="lbl_secondaryComponentmsg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td class="auto-style37">&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="auto-style16">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style13">
           
        </td>
    </tr>
    <tr>
        <td class="ButtonTR">
            <asp:Button ID="Button1" runat="server" Font-Bold="True" Text="Show Costing Report" Width="100%" OnClick="Button1_Click" />
        </td>
    </tr>
    <tr>
        <td class="auto-style18">
            &nbsp;<span class="auto-style17"> </span></td>
    </tr>
    <tr>
        <td>
            <table class="auto-style14">
                <tr>
                    <td class="auto-style23">&nbsp;</td>
                    <td class="auto-style22">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style15" colspan="3">
                        <asp:SqlDataSource ID="CalculationMode" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CalculationMode], [CalCulationMode_PK] FROM [CalculationModeMaster] ORDER BY [CalculationMode], [CalCulationMode_PK]"></asp:SqlDataSource>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
                        <asp:SqlDataSource ID="AtcSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT AtcId, AtcNum, IsClosed FROM AtcMaster WHERE (IsClosed = N'N')"></asp:SqlDataSource>
                        <asp:HiddenField ID="hdf_atcid" runat="server" />
                        <asp:SqlDataSource ID="ourstylesource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [OurStyleID], [OurStyle] FROM [AtcDetails] WHERE ([AtcId] = @AtcId)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdf_atcid" DefaultValue="0" Name="AtcId" PropertyName="Value" Type="Decimal" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
    </tr>
</table>
</asp:Content>
