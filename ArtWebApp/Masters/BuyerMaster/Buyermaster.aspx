<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Masters_Buyermaster" Codebehind="Buyermaster.aspx.cs" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
     
    </style> 
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div >
         <table class="FullTable">
             <tr>
                 <td class="RedHeadding">NEW BUYER DETAILS</td>
             </tr>
            <tr>
                <td>
                    <table class="DataEntryTable" >
                        <tr>
                            <td >NAME:</td>
                            <td >
                                <asp:TextBox ID="txt_buyername" runat="server" CssClass="auto-style26"></asp:TextBox>
                            </td>
                            <td >PREFIX : </td>
                            <td >
                                <asp:TextBox ID="txt_prefix" runat="server" CssClass="auto-style26"></asp:TextBox>
                            </td>
                            <td >ADDRESS:</td>
                            <td >
                                <textarea id="txta_address" cols="20" name="S1" runat ="server"  rows="2" class="auto-style15"></textarea></td>
                        </tr>
                        <tr>
                            <td >TELEPHONE:</td>
                            <td >
                                <asp:TextBox ID="txt_telephone" runat="server" CssClass="auto-style26"></asp:TextBox>
                            </td>
                            <td >FAX:</td>
                            <td >
                                <asp:TextBox ID="txt_fax" runat="server" CssClass="auto-style26"></asp:TextBox>
                            </td>
                            <td >E-MAIL :</td>
                            <td >
                                <asp:TextBox ID="txt_email" runat="server" CssClass="auto-style15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >CONTACT PERSON:</td>
                            <td >
                                <asp:TextBox ID="txt_contactperson" runat="server" CssClass="auto-style26"></asp:TextBox>
                            </td>
                            <td >CURRENCY : </td>
                            <td>
                                <asp:DropDownList ID="cmb_currency" runat="server" DataSourceID="SqlDataSource1" DataTextField="CurrencyCode" DataValueField="CurrencyID" CssClass="auto-style26">
                                </asp:DropDownList>
                            </td>
                            <td >Agent : </td>
                            <td>
                                <asp:TextBox ID="txt_agent" runat="server" CssClass="auto-style15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >PAYMENT MODE:</td>
                            <td >
                                <ig:WebDropDown ID="cmb_payment" runat="server" DataSourceID="SqlDataSource2" Width="151px" Height="16px">
                                    <DropDownItemBinding ValueField="PaymentModeID" TextField="PaymentModeCode" />
                                </ig:WebDropDown>
                            </td>
                            <td >DEPARTMENT:</td>
                            <td >
                                <asp:TextBox ID="txt_dept" runat="server" CssClass="auto-style26"></asp:TextBox>
                            </td>
                            <td " >Allowance</td>
                            <td>
                                <asp:TextBox ID="txt_allowance" runat="server" CssClass="auto-style15"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td >
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]"></asp:SqlDataSource>
                            </td>
                            <td >
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT * FROM [PaymentModeMaster]"></asp:SqlDataSource>
                            </td>
                            <td >
                                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" CssClass="auto-style15" />
                            </td>
                            <td >
                                &nbsp;</td>
                            <td  >&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
           <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Height="350px" Width="100%">
                        <Columns>
                            <ig:BoundDataField DataFieldName="BuyerID" Key="BuyerID">
                                <Header Text="BuyerID">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="BuyerName" Key="BuyerName">
                                <Header Text="BuyerName">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Prefix" Key="Prefix">
                                <Header Text="Prefix">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Adress" Key="Adress">
                                <Header Text="Adress">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Telephone" Key="Telephone">
                                <Header Text="Telephone">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Fax" Key="Fax">
                                <Header Text="Fax">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Email" Key="Email">
                                <Header Text="Email">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="ContactPerson" Key="ContactPerson">
                                <Header Text="ContactPerson">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="PaymentModeCode" Key="PaymentModeCode">
                                <Header Text="PaymentModeCode">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Department" Key="Department">
                                <Header Text="Department">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Agent" Key="Agent">
                                <Header Text="Agent">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Allowance" Key="Allowance">
                                <Header Text="Allowance">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AccountCode" Key="AccountCode">
                                <Header Text="AccountCode">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Debitaccid" Key="Debitaccid">
                                <Header Text="Debitaccid">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Br" Key="Br">
                                <Header Text="Br">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="CurrencyCode" Key="CurrencyCode">
                                <Header Text="CurrencyCode">
                                </Header>
                            </ig:BoundDataField>
                        </Columns>
                        <Behaviors>
                            <ig:EditingCore>
                                <Behaviors>
                                    <ig:CellEditing>
                                    </ig:CellEditing>
                                    <ig:RowDeleting />
                                </Behaviors>
                            </ig:EditingCore>
                            <ig:Selection CellClickAction="Row" RowSelectType="Single">
                            </ig:Selection>
                            <ig:RowSelectors>
                            </ig:RowSelectors>
                            <ig:Filtering>
                            </ig:Filtering>
                            <ig:Paging>
                            </ig:Paging>
                            <ig:Sorting>
                            </ig:Sorting>
                        </Behaviors>
                    </ig:WebDataGrid>
         <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM BuyerMaster WHERE (BuyerID = @original_BuyerID)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT BuyerMaster.BuyerID, BuyerMaster.BuyerName, BuyerMaster.Prefix, BuyerMaster.Adress, BuyerMaster.Telephone, BuyerMaster.Fax, BuyerMaster.Email, BuyerMaster.ContactPerson, PaymentModeMaster.PaymentModeCode, BuyerMaster.Department, BuyerMaster.Agent, BuyerMaster.Allowance, BuyerMaster.AccountCode, BuyerMaster.Debitaccid, BuyerMaster.Br, CurrencyMaster.CurrencyCode FROM PaymentModeMaster INNER JOIN BuyerMaster ON PaymentModeMaster.PaymentModeID = BuyerMaster.PaymentModeCode INNER JOIN CurrencyMaster ON BuyerMaster.CurrencyCode = CurrencyMaster.CurrencyID ORDER BY BuyerMaster.BuyerName" UpdateCommand="UPDATE [BuyerMaster] SET [BuyerName] = @BuyerName, [Prefix] = @Prefix, [Adress] = @Adress, [Telephone] = @Telephone, [Fax] = @Fax, [Email] = @Email, [ContactPerson] = @ContactPerson, [CountryCode] = @CountryCode, [CurrencyCode] = @CurrencyCode, [PaymentModeCode] = @PaymentModeCode, [Department] = @Department, [Agent] = @Agent, [Allowance] = @Allowance, [AccountCode] = @AccountCode, [Debitaccid] = @Debitaccid, [Br] = @Br WHERE [BuyerID] = @original_BuyerID AND (([BuyerName] = @original_BuyerName) OR ([BuyerName] IS NULL AND @original_BuyerName IS NULL)) AND (([Prefix] = @original_Prefix) OR ([Prefix] IS NULL AND @original_Prefix IS NULL)) AND (([Adress] = @original_Adress) OR ([Adress] IS NULL AND @original_Adress IS NULL)) AND (([Telephone] = @original_Telephone) OR ([Telephone] IS NULL AND @original_Telephone IS NULL)) AND (([Fax] = @original_Fax) OR ([Fax] IS NULL AND @original_Fax IS NULL)) AND (([Email] = @original_Email) OR ([Email] IS NULL AND @original_Email IS NULL)) AND (([ContactPerson] = @original_ContactPerson) OR ([ContactPerson] IS NULL AND @original_ContactPerson IS NULL)) AND (([CountryCode] = @original_CountryCode) OR ([CountryCode] IS NULL AND @original_CountryCode IS NULL)) AND (([CurrencyCode] = @original_CurrencyCode) OR ([CurrencyCode] IS NULL AND @original_CurrencyCode IS NULL)) AND (([PaymentModeCode] = @original_PaymentModeCode) OR ([PaymentModeCode] IS NULL AND @original_PaymentModeCode IS NULL)) AND (([Department] = @original_Department) OR ([Department] IS NULL AND @original_Department IS NULL)) AND (([Agent] = @original_Agent) OR ([Agent] IS NULL AND @original_Agent IS NULL)) AND (([Allowance] = @original_Allowance) OR ([Allowance] IS NULL AND @original_Allowance IS NULL)) AND (([AccountCode] = @original_AccountCode) OR ([AccountCode] IS NULL AND @original_AccountCode IS NULL)) AND (([Debitaccid] = @original_Debitaccid) OR ([Debitaccid] IS NULL AND @original_Debitaccid IS NULL)) AND (([Br] = @original_Br) OR ([Br] IS NULL AND @original_Br IS NULL))">
                        <DeleteParameters>
                            <asp:Parameter Name="original_BuyerID" Type="Decimal" />
                           
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="BuyerName" Type="String" />
                            <asp:Parameter Name="Prefix" Type="String" />
                            <asp:Parameter Name="Adress" Type="String" />
                            <asp:Parameter Name="Telephone" Type="String" />
                            <asp:Parameter Name="Fax" Type="String" />
                            <asp:Parameter Name="Email" Type="String" />
                            <asp:Parameter Name="ContactPerson" Type="String" />
                            <asp:Parameter Name="CountryCode" Type="Decimal" />
                            <asp:Parameter Name="CurrencyCode" Type="Decimal" />
                            <asp:Parameter Name="PaymentModeCode" Type="Decimal" />
                            <asp:Parameter Name="Department" Type="String" />
                            <asp:Parameter Name="Agent" Type="String" />
                            <asp:Parameter Name="Allowance" Type="Decimal" />
                            <asp:Parameter Name="AccountCode" Type="Decimal" />
                            <asp:Parameter Name="Debitaccid" Type="Decimal" />
                            <asp:Parameter Name="Br" Type="String" />
                            <asp:Parameter Name="original_BuyerID" Type="Decimal" />
                            <asp:Parameter Name="original_BuyerName" Type="String" />
                            <asp:Parameter Name="original_Prefix" Type="String" />
                            <asp:Parameter Name="original_Adress" Type="String" />
                            <asp:Parameter Name="original_Telephone" Type="String" />
                            <asp:Parameter Name="original_Fax" Type="String" />
                            <asp:Parameter Name="original_Email" Type="String" />
                            <asp:Parameter Name="original_ContactPerson" Type="String" />
                            <asp:Parameter Name="original_CountryCode" Type="Decimal" />
                            <asp:Parameter Name="original_CurrencyCode" Type="Decimal" />
                            <asp:Parameter Name="original_PaymentModeCode" Type="Decimal" />
                            <asp:Parameter Name="original_Department" Type="String" />
                            <asp:Parameter Name="original_Agent" Type="String" />
                            <asp:Parameter Name="original_Allowance" Type="Decimal" />
                            <asp:Parameter Name="original_AccountCode" Type="Decimal" />
                            <asp:Parameter Name="original_Debitaccid" Type="Decimal" />
                            <asp:Parameter Name="original_Br" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
        <table class="auto-style2">
            <tr>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td>
                 
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

