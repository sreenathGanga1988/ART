<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="UserProfileCreation.aspx.cs" Inherits="ArtWebApp.Administrator.UserProfileCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
    .auto-style1 {
        width: 100%;
    }
     .myClass
    {
        border: solid 1px red;
    }
    .myClass input
    {
        background-color:Gray;	
        margin-left: -20px; 
    }
    .myClass label
    {
        font-weight:bold;
        font-size:small;
    }
    .myClass td 
    { 
        border: 3px solid #8AC007;
    padding-left: 20px; 
    width:200px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td class="RedHeadding">User Profile Creation/ Edit</td>
    </tr>
    <tr>
        <td>
            <div>
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">User Profile</td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="drp_userprofile" runat="server" DataSourceID="userprofiledatasource" DataTextField="UserProfileName" DataValueField="UserProfile_Pk" DisableSearchThreshold="10" Height="16px" Width="200px">
                                <asp:ListItem></asp:ListItem>
                            </ucc:DropDownListChosen>
                            <asp:SqlDataSource ID="userprofiledatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [UserProfile_Pk], [UserProfileName], [UserProfileCode], [Description], [IsActive] FROM [UserProfileMaster] WHERE ([IsActive] = @IsActive)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Y" Name="IsActive" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td class="SearchButtonTD">
                            <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Select All" />
                        </td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="SearchButtonTD">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div>
            </div>
            <asp:CheckBoxList ID="chkbx_gditem" runat="server" DataSourceID="Submenubardatasource" CssClass="myClass" DataTextField="MenuText" DataValueField="Menu_PK" RepeatColumns="15" RepeatDirection="Vertical" RepeatLayout="Table">
            </asp:CheckBoxList>
            <asp:SqlDataSource ID="Submenubardatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT MenuText, Menu_PK FROM SubMenuMaster WHERE (isEnable = @isEnable) AND (IsNormal = N'Y') ORDER BY Menu_PK ">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Y" Name="isEnable" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:Button ID="btn_saveProfile" runat="server" OnClick="btn_saveProfile_Click" Text="Save Profile Information" />
        </td>
    </tr>
</table>
</asp:Content>
