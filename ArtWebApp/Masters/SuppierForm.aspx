<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SuppierForm.aspx.cs" Inherits="ArtWebApp.Masters.SuppierForm" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    
       
     
    </style> <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div >
         <table class="FullTable">
             <tr><td class="RedHeadding">
                 NEW SUPPLIER DETAILS
                 </td></tr>
            <tr>
                <td>
                    <table class="DataEntryTable" >
                        <tr>
                            <td >NAME:</td>
                            <td >
                                <asp:TextBox ID="txt_name" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Enter Supplier Name" ForeColor="#FF3300" ControlToValidate="txt_name">*</asp:RequiredFieldValidator>
                            </td>
                            <td >PREFIX : </td>
                            <td >
                                <asp:TextBox ID="txt_prefix" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter Prefix" ForeColor="#FF3300" ValidationGroup="size" ControlToValidate="txt_prefix">*</asp:RequiredFieldValidator>
                            </td>
                            <td >ADDRESS:</td>
                            <td >
                                <textarea id="txta_address" cols="20" name="S1" runat ="server" class="auto-style15"></textarea><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Enter Address" ForeColor="#FF3300" ControlToValidate="txta_address">*</asp:RequiredFieldValidator>
                                             </td>
                        </tr>
                        <tr>
                            <td >TELEPHONE:</td>
                            <td >
                                <asp:TextBox ID="txt_telephone" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td >FAX:</td>
                            <td >
                                <asp:TextBox ID="txt_fax" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td  >E-MAIL :</td>
                            <td >
                                <asp:TextBox ID="txt_email" runat="server" CssClass="auto-style15" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >CONTACT PERSON:</td>
                            <td >
                                <asp:TextBox ID="txt_contactperson" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td >CURRENCY : </td>
                            <td >
                                 <ucc:DropDownListChosen ID="cmb_currency" Width="200px"  runat="server" DataSourceID="currencydata" DataTextField="CurrencyCode" DataValueField="CurrencyID" DisableSearchThreshold="10" Height="16px"   >
                                        </ucc:DropDownListChosen>
                                
                            </td>
                            <td  >COUNTRY : </td>
                            <td >
                                

                                <ucc:DropDownListChosen ID="cmb_country" Width="200px"  runat="server" DataSourceID="countrydata" DataTextField="ShortName" DataValueField="CountryID" DisableSearchThreshold="10" Height="16px"   >
                                        </ucc:DropDownListChosen>

                            </td>
                        </tr>
                        <tr>
                            <td >PAYMENT MODE:</td>
                            <td >
                             

                                 <ucc:DropDownListChosen ID="cmb_paymentmode" Width="200px"  runat="server" DataSourceID="PaymentMode" DataTextField="PaymentModeCode" DataValueField="PaymentModeID" DisableSearchThreshold="10" Height="16px"   >
                                        </ucc:DropDownListChosen>

                            </td>
                            <td>SUPPLIER TYPE :</td>
                            <td >
                                <ig:WebDropDown ID="cmb_suppliertype" runat="server" Width="160px" Height="18px" CurrentValue="Select">
                                    <Items>
                                        <ig:DropDownItem Selected="False" Text="Fabric" Value="F">
                                        </ig:DropDownItem>
                                        <ig:DropDownItem Selected="False" Text="Trims" Value="T">
                                        </ig:DropDownItem>
                                        <ig:DropDownItem Selected="False" Text="Spare Parts" Value="SP">
                                        </ig:DropDownItem>
                                        <ig:DropDownItem Selected="False" Text="Others" Value="O">
                                        </ig:DropDownItem>
                                        <ig:DropDownItem Selected="False" Text="Service" Value="S">
                                        </ig:DropDownItem>
                                    </Items>
                                </ig:WebDropDown>
                            </td>
                            <td  >PAYMENT TERM</td>
                            <td>
                                



                                 <ucc:DropDownListChosen ID="cmb_paymentterm" Width="200px"  runat="server" DataSourceID="Paymenttermdata" DataTextField="PaymentTermCode" DataValueField="PaymentTermID" DisableSearchThreshold="10" Height="16px"   >
                                        </ucc:DropDownListChosen>
                            </td>
                        </tr>
                        <tr>
                            <td >Debit Account :</td>
                            <td >
                                <asp:TextBox ID="txt_contactperson0" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td >CREDIT ACCOUNT</td>
                            <td >
                                <asp:TextBox ID="txt_contactperson1" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td  >&nbsp;</td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td >&nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td  >&nbsp;</td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td >
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                            </td>
                            <td  >&nbsp;</td>
                            <td >
                                <asp:Button ID="Button1" runat="server" Text="Submit"  CssClass="auto-style35" OnClick="Button1_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                               <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr  class="gridtable">
                <td>
                    <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="Supplierdata" Height="100%" Width="100%">
                        <Columns>
                            <ig:BoundDataField DataFieldName="Supplier_PK" Key="Supplier_PK">
                                <Header Text="Supplier_PK">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="ShortName" Key="ShortName">
                                <Header Text="ShortName">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SupplierName" Key="SupplierName">
                                <Header Text="SupplierName">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SupplierPrefix" Key="SupplierPrefix">
                                <Header Text="SupplierPrefix">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SupplierAddress" Key="SupplierAddress">
                                <Header Text="SupplierAddress">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SupplierType" Key="SupplierType">
                                <Header Text="SupplierType">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Telephone" Key="Telephone">
                                <Header Text="Telephone">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="ContactPerson" Key="ContactPerson">
                                <Header Text="ContactPerson">
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
                    <asp:SqlDataSource ID="Supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT SupplierMaster.Supplier_PK, SupplierMaster.SupplierName, SupplierMaster.SupplierPrefix, SupplierMaster.SupplierAddress, SupplierMaster.SupplierType, SupplierMaster.Telephone, SupplierMaster.Email, SupplierMaster.Fax, SupplierMaster.ContactPerson, CurrencyMaster.CurrencyCode, CountryMaster.ShortName, PaymentModeMaster.PaymentModeCode, PaymentTermMaster.PaymentTermCode, SupplierMaster.IsActive FROM SupplierMaster INNER JOIN PaymentModeMaster ON SupplierMaster.PaymentModeID = PaymentModeMaster.PaymentModeID INNER JOIN PaymentTermMaster ON SupplierMaster.PaymentTermID = PaymentTermMaster.PaymentTermID INNER JOIN CountryMaster ON SupplierMaster.CountryID = CountryMaster.CountryID INNER JOIN CurrencyMaster ON SupplierMaster.CurrencyID = CurrencyMaster.CurrencyID"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <div>

        <table class="auto-style2">
            <tr>
                <td>
                    <asp:SqlDataSource ID="Paymenttermdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [PaymentTermCode], [PaymentTermID] FROM [PaymentTermMaster] ORDER BY [PaymentTermCode]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="countrydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [ShortName], [CountryID] FROM [CountryMaster]"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="PaymentMode" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [PaymentModeID], [PaymentModeCode] FROM [PaymentModeMaster] ORDER BY [PaymentModeCode]"></asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                                <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]" ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>"></asp:SqlDataSource>
                            </td>
            </tr>
        </table>

    </div>
</asp:Content>

