<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="WarehouseMaster.aspx.cs" Inherits="ArtWebApp.Masters.Warehouse" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
      
    </style>
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div  >
         <table class="FullTable">
            <tr>
                <td>
                    <table class="DataEntryTable" >
                        <tr class="RedHeadding">
                            <td colspan="6" >NEW WAREHOUSE/FACTORY DETAILS</td>
                        </tr>
                        <tr>
                            <td >NAME :</td>
                            <td >
                                <asp:TextBox ID="txt_name" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_name" ErrorMessage="Enter Name" ForeColor="#FF3300" EnableTheming="True">*</asp:RequiredFieldValidator>
                            </td>
                            <td >&nbsp;PREFIX : </td>
                            <td >
                                <asp:TextBox ID="txt_prefix" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_prefix" ErrorMessage="Enter Prefix" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                            </td>
                            <td ">ADDRESS :</td>
                            <td>
                                <textarea id="txta_address" name="S1" runat ="server" class="auto-style15"></textarea></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td >TELEPHONE :</td>
                            <td >
                                <asp:TextBox ID="txt_telephone" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td >&nbsp;FAX :</td>
                            <td >
                                <asp:TextBox ID="txt_fax" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td  >E-MAIL :</td>
                            <td >
                                <asp:TextBox ID="txt_email" runat="server" CssClass="auto-style15" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >CONTACT PERSON :</td>
                            <td >
                                <asp:TextBox ID="txt_contactperson" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td >CURRENCY : </td>
                            <td >
                                <ig:WebDropDown ID="cmb_currency" runat="server" Width="160px" Height="18px" DataSourceID="currencydata" TextField="CurrencyCode" ValueField="CurrencyID" CurrentValue="Select Currency">
                                    <DropDownItemBinding TextField="CurrencyCode" ValueField="CurrencyID" />
                                </ig:WebDropDown>
                            </td>
                            <td  >COUNTRY : </td>
                            <td >
                                <ig:WebDropDown ID="cmb_country" runat="server" Width="160px" Height="18px" DataSourceID="countrydata" TextField="ShortName" ValueField="CountryID" CurrentValue="Select Country">
                                    <DropDownItemBinding TextField="ShortName" ValueField="CountryID" />
                                </ig:WebDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td >PAYMENT MODE :</td>
                            <td >
                                <ig:WebDropDown ID="cmb_paymentmode" runat="server" Width="160px" Height="18px" DataSourceID="paymentmodedata" TextField="PaymentModeCode" ValueField="PaymentModeID">
                                    <DropDownItemBinding TextField="PaymentModeCode" ValueField="PaymentModeID" />
                                </ig:WebDropDown>
                            </td>
                            <td >&nbsp;LOCATION TYPE :</td>
                            <td >
                                <ig:WebDropDown ID="cmb_locationtype" runat="server" Width="160px" Height="18px" AutoPostBack="True" OnSelectionChanged="cmb_locationtype_SelectionChanged" CurrentValue="Select Type">
                                    <Items>
                                        <ig:DropDownItem Selected="False" Text="WareHouse" Value="W">
                                        </ig:DropDownItem>
                                        <ig:DropDownItem Selected="False" Text="Factory" Value="F">
                                        </ig:DropDownItem>
                                    </Items>
                                </ig:WebDropDown>
                            </td>
                            <td class="auto-style43" >SUB TYPE :</td>
                            <td>
                                <ig:WebDropDown ID="cmb_subtype" runat="server" Width="160px" Height="18px" DataSourceID="subType" TextField="LocationType" ValueField="LocationType_Pk" CurrentValue="Select Sub Type">
                                    <DropDownItemBinding TextField="LocationType" ValueField="LocationType_Pk" />
                                </ig:WebDropDown>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style27">ACTIVE :</td>
                            <td class="auto-style19">
                                <asp:RadioButton ID="RadioButton1" runat="server" CssClass="auto-style50" OnCheckedChanged="RadioButton1_CheckedChanged" Text="YES" ValidationGroup="1" />
                                <asp:RadioButton ID="RadioButton2" runat="server" CssClass="auto-style50" Text="NO" ValidationGroup="1" />
                            </td>
                            <td class="auto-style49">&nbsp;</td>
                            <td class="auto-style6">
                                &nbsp;</td>
                            <td class="auto-style44" >&nbsp;</td>
                            <td class="auto-style15">
                                &nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
            </table>
    </div>
    <div>

        <table class="DataEntryTable">
            <tr>
                <td>
                                <asp:Button ID="Submit" runat="server" Text="Submit"  CssClass="auto-style15" Height="27px" Width="99px" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="locationdata" Height="350px" Width="100%">
                        <Columns>
                            <ig:BoundDataField DataFieldName="Location_PK" Key="Location_PK">
                                <Header Text="id">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LocationName" Key="LocationName">
                                <Header Text="LocationName">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LocationPrefix" Key="LocationPrefix">
                                <Header Text="LocationPrefix">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LocationAddress" Key="LocationAddress">
                                <Header Text="LocationAddress">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LocationType" Key="LocationType">
                                <Header Text="LocationType">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LocType" Key="LocType">
                                <Header Text="LocType">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Telephone" Key="Telephone">
                                <Header Text="Telephone">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Email" Key="Email">
                                <Header Text="Email">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Fax" Key="Fax">
                                <Header Text="Fax">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="ContactPerson" Key="ContactPerson">
                                <Header Text="ContactPerson">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="CurrencyCode" Key="CurrencyCode">
                                <Header Text="CurrencyCode">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="ShortName" Key="ShortName">
                                <Header Text="ShortName">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="PaymentModeCode" Key="PaymentModeCode">
                                <Header Text="PaymentModeCode">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="IsActive" Key="IsActive">
                                <Header Text="IsActive">
                                </Header>
                            </ig:BoundDataField>
                        </Columns>
                        <Behaviors>
                            <ig:Filtering>
                            </ig:Filtering>
                        </Behaviors>
                    </ig:WebDataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CurrencyCode], [CurrencyID] FROM [CurrencyMaster]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="subType" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [LocationType], [LocationType_Pk] FROM [LocationType] WHERE ([TypeFor] = @TypeFor)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hd_type" DefaultValue="w" Name="TypeFor" PropertyName="Value" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:HiddenField ID="hd_type" runat="server" />
                    <asp:SqlDataSource ID="countrydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [ShortName], [CountryID] FROM [CountryMaster]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="paymentmodedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [PaymentModeID], [PaymentModeCode] FROM [PaymentModeMaster]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="locationdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT LocationMaster.Location_PK, LocationMaster.LocationName, LocationMaster.LocationPrefix, LocationMaster.LocationAddress, LocationType.LocationType, LocationMaster.LocType, LocationMaster.Telephone, LocationMaster.Email, LocationMaster.Fax, LocationMaster.ContactPerson, CurrencyMaster.CurrencyCode, CountryMaster.ShortName, PaymentModeMaster.PaymentModeCode, LocationMaster.IsActive FROM LocationMaster INNER JOIN CurrencyMaster ON LocationMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN CountryMaster ON LocationMaster.CountryID = CountryMaster.CountryID INNER JOIN LocationType ON LocationMaster.LocationType_PK = LocationType.LocationType_Pk INNER JOIN PaymentModeMaster ON LocationMaster.PaymentModeID = PaymentModeMaster.PaymentModeID"></asp:SqlDataSource>
                    <br />
                </td>
            </tr>
        </table>

    </div>
</asp:Content>