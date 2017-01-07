<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Merchandiser_AtcCreation" Codebehind="AtcCreation.aspx.cs" %>


<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>


<%--<%@ Register assembly="NumericTextBox" namespace="NumericTextBoxControl" tagprefix="cc2" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
       
    </style>
    <link href="../css/style.css" rel="stylesheet" />
    
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="FullTable">
            <table class="DataEntryTable">

            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                             <table class="DataEntryTable">
               <tr>
        <td class="RedHeadding" colspan="6">ATC CREATION</td>
    </tr>
    <tr>
        <td >Buyer/Dept:
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmb_Buyer" EnableTheming="True" ErrorMessage="Buyer Required" ForeColor="#FF3300" InitialValue="Select Buyer" CssClass="auto-style13">*</asp:RequiredFieldValidator>
        </td>
        <td >
            
           

             <ucc:DropDownListChosen ID="cmb_Buyer" runat="server" DataSourceID="SqlDataSource1" DataTextField="BuyerName" DataValueField="BuyerID" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
        </td>
        <td ><span >Country :</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmb_country" EnableTheming="True" ErrorMessage="Buyer Required" ForeColor="#FF3300" InitialValue="Select Country" CssClass="auto-style13">*</asp:RequiredFieldValidator>
        </td>
        <td >
           
             <ucc:DropDownListChosen ID="cmb_country" runat="server" DataSourceID="SqlDataSource2" DataTextField="Description" DataValueField="CountryID" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
        </td>
        <td >House Date :<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dtp_housedate" EnableTheming="True" ErrorMessage="Select HouseDate" ForeColor="#FF3300" CssClass="auto-style13">*</asp:RequiredFieldValidator>
        </td>
        <td >
            
            <ig:WebDatePicker ID="dtp_housedate" runat="server">
               
            </ig:WebDatePicker>
        </td>
    </tr>
   
    <tr>
        <td >No of Style Group : 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_stylenum" EnableTheming="True" ErrorMessage="Style No  Required" ForeColor="#FF3300" CssClass="auto-style13">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_stylenum" ErrorMessage="Only Numbers Allowed" ForeColor="#FF3300" ValidationExpression="\d+" CssClass="auto-style13">*</asp:RegularExpressionValidator>
        </td>
        <td >
            <asp:TextBox ID="txt_stylenum" runat="server" CssClass="auto-style13"></asp:TextBox>
        </td>
        <td><span >Ship Start Date </span>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtp_shipStartdate" EnableTheming="True" ErrorMessage="Select Ship Date" ForeColor="#FF3300" CssClass="auto-style13">*</asp:RequiredFieldValidator>
        </td>
        <td >
            <ig:WebDatePicker ID="dtp_shipStartdate" runat="server">
            </ig:WebDatePicker>
        </td>
        <td >Finish Date : 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtp_finishdate" EnableTheming="True" ErrorMessage="Select Finish Date" ForeColor="#FF3300" CssClass="auto-style13">*</asp:RequiredFieldValidator>
        </td>
        <td >
            <ig:WebDatePicker ID="dtp_finishdate" runat="server">

            </ig:WebDatePicker>
        </td>
    </tr>
    <tr>
        <td >ShipIT Reference:</td>
        <td >
            <asp:TextBox ID="txt_shipitrefernce" runat="server" CssClass="auto-style13"></asp:TextBox>
        </td>
        <td >Merchandiser</td>
        <td >
           
            
             <ucc:DropDownListChosen ID="drp_merchandiser" runat="server" DataSourceID="MerchandiserData" DataTextField="MerchandiserName" DataValueField="MerChandiser_Pk" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
        </td>
        <td >Atc#</td>
        <td >
           
            <asp:Label ID="lbl_atc" runat="server" Visible="False"></asp:Label>
           
            </td>
    </tr>
                                 <tr>
                                     <td >Season</td>
                                     <td >
                                       <%--  <ig:WebDropDown ID="drp_season" runat="server" DataSourceID="SeasonData" Height="23px" TextField="SeasonName" ValueField="season_pk" Width="200px">
                                             <DropDownItemBinding TextField="SeasonName" ValueField="season_pk" />
                                         </ig:WebDropDown>--%>
                                          <ucc:DropDownListChosen ID="drp_season" runat="server" DataSourceID="SeasonData" DataTextField="SeasonName" DataValueField="season_pk" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
                                     </td>
                                     <td >PROJECTION qTY</td>
                                     <td >
                                         <cc2:NumericTextBox ID="NumericTextBox4" runat="server" />
                                     </td>
                                     <td ></td>
                                     <td ></td>
                                 </tr>
    <tr >
        <td >
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [BuyerName], [BuyerID] FROM [BuyerMaster] ORDER BY [BuyerName]"></asp:SqlDataSource>
            
        </td>
        <td >
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Description], [CountryID] FROM [CountryMaster]"></asp:SqlDataSource>
            </td>
        <td >
            <asp:Button ID="Button1" runat="server" Text="Add " OnClick="Button1_Click" style="margin-bottom: 0px;" />
        </td>
        <td rowspan="2" >
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="52px" Font-Italic="True" Font-Size="X-Small" Font-Names="Calibri" />
        </td>
        <td class="NormalTD3">
           
            &nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td >
            <asp:SqlDataSource ID="BuyerStyle1" runat="server" ConnectionString="<%$ ConnectionStrings:CourierDetailsConnectionString %>" SelectCommand="SELECT DISTINCT Style FROM [OrderBooking_TBL ] ORDER BY Style DESC"></asp:SqlDataSource>
        </td>
        <td >

       
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [AtcDetails] WHERE [OutStyleID] = @original_OutStyleID AND [AtcId] = @original_AtcId AND (([OurStyle] = @original_OurStyle) OR ([OurStyle] IS NULL AND @original_OurStyle IS NULL)) AND (([BuyerStyle] = @original_BuyerStyle) OR ([BuyerStyle] IS NULL AND @original_BuyerStyle IS NULL)) AND (([Quantity] = @original_Quantity) OR ([Quantity] IS NULL AND @original_Quantity IS NULL)) AND (([FOB] = @original_FOB) OR ([FOB] IS NULL AND @original_FOB IS NULL))" InsertCommand="INSERT INTO [AtcDetails] ([AtcId], [OurStyle], [BuyerStyle], [Quantity], [FOB]) VALUES (@AtcId, @OurStyle, @BuyerStyle, @Quantity, @FOB)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [AtcDetails] WHERE ([AtcId] = @AtcId)" UpdateCommand="UPDATE [AtcDetails] SET [AtcId] = @AtcId, [OurStyle] = @OurStyle, [BuyerStyle] = @BuyerStyle, [Quantity] = @Quantity, [FOB] = @FOB WHERE [OutStyleID] = @original_OutStyleID AND [AtcId] = @original_AtcId AND (([OurStyle] = @original_OurStyle) OR ([OurStyle] IS NULL AND @original_OurStyle IS NULL)) AND (([BuyerStyle] = @original_BuyerStyle) OR ([BuyerStyle] IS NULL AND @original_BuyerStyle IS NULL)) AND (([Quantity] = @original_Quantity) OR ([Quantity] IS NULL AND @original_Quantity IS NULL)) AND (([FOB] = @original_FOB) OR ([FOB] IS NULL AND @original_FOB IS NULL))">
                    <DeleteParameters>
                        <asp:Parameter Name="original_OutStyleID" Type="Decimal" />
                        <asp:Parameter Name="original_AtcId" Type="Decimal" />
                        <asp:Parameter Name="original_OurStyle" Type="String" />
                        <asp:Parameter Name="original_BuyerStyle" Type="String" />
                        <asp:Parameter Name="original_Quantity" Type="Decimal" />
                        <asp:Parameter Name="original_FOB" Type="Decimal" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="AtcId" Type="Decimal" />
                        <asp:Parameter Name="OurStyle" Type="String" />
                        <asp:Parameter Name="BuyerStyle" Type="String" />
                        <asp:Parameter Name="Quantity" Type="Decimal" />
                        <asp:Parameter Name="FOB" Type="Decimal" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lbl_atcid" DefaultValue="" Name="AtcId" PropertyName="Text" Type="Decimal" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="AtcId" Type="Decimal" />
                        <asp:Parameter Name="OurStyle" Type="String" />
                        <asp:Parameter Name="BuyerStyle" Type="String" />
                        <asp:Parameter Name="Quantity" Type="Decimal" />
                        <asp:Parameter Name="FOB" Type="Decimal" />
                        <asp:Parameter Name="original_OutStyleID" Type="Decimal" />
                        <asp:Parameter Name="original_AtcId" Type="Decimal" />
                        <asp:Parameter Name="original_OurStyle" Type="String" />
                        <asp:Parameter Name="original_BuyerStyle" Type="String" />
                        <asp:Parameter Name="original_Quantity" Type="Decimal" />
                        <asp:Parameter Name="original_FOB" Type="Decimal" />
                    </UpdateParameters>
                </asp:SqlDataSource>
           

                </td>
        <td >

       
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [CategoryID], [CategoryName] FROM [GarmentCategory]">
                </asp:SqlDataSource>
           

        </td>
        <td >
           
            <asp:Label ID="lbl_atcid" runat="server" Visible="False"></asp:Label>
           
        </td>
        <td>&nbsp;</td>
    </tr>
</table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr class="gridtable">
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowHeaderWhenEmpty="True" OnRowDataBound="GridView1_RowDataBound" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" style="font-size: x-small; font-family: Calibri" Width="100%" >
             <Columns>
                 <asp:BoundField DataField="OurStyleID" HeaderText="OurStyleID" />
                 <asp:BoundField DataField="atcid" HeaderText="atcid" />
                 <asp:BoundField DataField="Ourstyle" HeaderText="Ourstyle" />
                 <asp:TemplateField HeaderText="BuyerStyle" SortExpression="BuyerStyle">
                     <ItemTemplate>
                         <asp:DropDownList ID="Mydrop" runat="server"  DataSourceID="BuyerStyle1" DataTextField="Style" DataValueField="Style" Font-Names="Calibri" Font-Size="X-Small" Height="16px"></asp:DropDownList>
                     </ItemTemplate>                     
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="FOB" SortExpression="Fob">
                      <ItemTemplate>
                         <asp:TextBox ID="txtfob" Text='<%# Bind("Fob") %>' runat="server" Font-Names="Calibri" Font-Size="X-Small" Height="15px"></asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Category">
                     <ItemTemplate>
                         <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource4" DataTextField="CategoryName" DataValueField="CategoryID" Font-Names="Calibri" Font-Size="X-Small" Height="16px">
                         </asp:DropDownList>
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
            <tr class="SmallSearchButton">
                <td>
       
   

        <asp:Button ID="Btn_addetails" runat="server" OnClick="Button2_Click" Text="Save" />

    
  
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="MerchandiserData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [MerChandiser_Pk], [MerchandiserName] FROM [MerchandiserMaster]"></asp:SqlDataSource>
                    <br />
                    <asp:SqlDataSource ID="SeasonData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Season_PK], [SeasonName] FROM [SeasonMaster] ORDER BY [Season_PK] DESC, [SeasonName]"></asp:SqlDataSource>
                </td>
            </tr>


        </table>



    </div>
       
    </asp:Content>

