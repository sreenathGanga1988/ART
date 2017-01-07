<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="POPackEdit.aspx.cs" Inherits="ArtWebApp.Merchandiser.POPackEdit" %>

<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>



<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
      

      
       

    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">ASQ EDIT</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="auto-style3">Atc # :</td>
                        <td class="NormalTD">



                            <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="atcmasterdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>

                        </td>
                        <td class="auto-style2">
                            <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />
                        </td>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                    </tr>
                    <tr>
                        <td class="auto-style3">PoPack #:</td>
                        <td class="NormalTD">


                            <ucc:DropDownListChosen ID="cmb_po" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="S" OnClick="Button2_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <table class="DataEntryTable">
                                    <tr>
                                        <td class="RedHeadding" colspan="6">ASQ DETAILS</td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">PoPackID</td>
                                        <td>
                                            <asp:Label ID="lbl_popackid" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">Buyer PO# :</td>
                                        <td>
                                            <asp:TextBox ID="txt_buyerpo" runat="server" CssClass="auto-style39"></asp:TextBox>
                                            <b>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_buyerpo" CssClass="auto-style36" ErrorMessage="Enter Buyer PO" ForeColor="Red" ValidationGroup="newpo">*</asp:RequiredFieldValidator>
                                            </b></td>
                                        <td>Channel </td>
                                        <td>


                                            <ucc:DropDownListChosen ID="drp_channel" runat="server" DataSourceID="ChannelData" DataTextField="Channelname" DataValueField="ChannelID" DisableSearchThreshold="10" Width="200px">
                                            </ucc:DropDownListChosen>
                                        </td>
                                        <td>Destination : </td>
                                        <td>

                                            <ucc:DropDownListChosen ID="drp_dest" runat="server" DataSourceID="Destinationdata" DataTextField="BuyerDestination" DataValueField="BuyerDestination_PK" DisableSearchThreshold="10" Width="200px">
                                            </ucc:DropDownListChosen>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">Pack Instruction : </td>
                                        <td>
                                            <asp:TextBox ID="txt_packdetail" runat="server" CssClass="auto-style39" Height="16px" Width="120px"></asp:TextBox>
                                            <b>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_packdetail" CssClass="auto-style36" ErrorMessage="ENter Pack Instruction" ForeColor="Red" ValidationGroup="newpo">*</asp:RequiredFieldValidator>
                                            </b></td>
                                        <td>Delivery date : </td>
                                        <td>
                                            <ig:WebDatePicker ID="dtp_deliverydate" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td>Inhouse Date :</td>
                                        <td>
                                            <ig:WebDatePicker ID="dtp_inhousedate" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">PO Group</td>
                                        <td class="NormalTD">

                                            <ucc:DropDownListChosen ID="drp_pogroup" Width="170px" runat="server">
                                                <asp:ListItem>GP1</asp:ListItem>
                                                <asp:ListItem>GP2</asp:ListItem>
                                                <asp:ListItem>GP3</asp:ListItem>
                                                <asp:ListItem>GP4</asp:ListItem>
                                            </ucc:DropDownListChosen>




                                        </td>
                                        <td class="NormalTD">Tag group</td>
                                        <td class="NormalTD">

                                            <ucc:DropDownListChosen ID="drp_taggroup" Width="170px" runat="server">
                                                <asp:ListItem>TD1</asp:ListItem>
                                                <asp:ListItem>TD2</asp:ListItem>
                                                <asp:ListItem>TD3</asp:ListItem>

                                            </ucc:DropDownListChosen>
                                            <%--<ig:WebDropDown ID="drp_taggroup" runat="server" Width="148px" Height="17px">
                                                    <Items>
                                                        <ig:DropDownItem Selected="True" Text="TD1" Value="">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="TD2" Value="">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="TD3" Value="">
                                                        </ig:DropDownItem>
                                                    </Items>
                                                </ig:WebDropDown>--%>
                                        </td>
                                        <td class="NormalTD">Season</td>
                                        <td><%--<ig:WebDropDown ID="drp_season" runat="server" DataSourceID="SeasonData" Height="16px" TextField="SeasonName" ValueField="season_pk" Width="174px">
                                             <DropDownItemBinding TextField="SeasonName" ValueField="season_pk" />
                                         </ig:WebDropDown>--%>

                                            <ucc:DropDownListChosen ID="drp_season" Width="170px" runat="server" DataSourceID="SeasonData" DataTextField="SeasonName" DataValueField="season_pk">
                                            </ucc:DropDownListChosen>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btn_save" runat="server" CssClass="auto-style29" OnClick="btn_save_Click" Text="Update" ValidationGroup="newpo" Width="100%" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_errordisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="96px" ValidationGroup="newpo" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:SqlDataSource ID="atcmasterdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="ChannelData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [ChannelID], [ChannelName] FROM [ChannelMaster] ORDER BY [ChannelName]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SeasonData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Season_PK], [SeasonName] FROM [SeasonMaster] ORDER BY [Season_PK] DESC, [SeasonName]"></asp:SqlDataSource>

                                            <asp:SqlDataSource ID="Destinationdata" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [BuyerDestinationMaster] WHERE [BuyerDestination_PK] = @original_BuyerDestination_PK AND (([BuyerDestination] = @original_BuyerDestination) OR ([BuyerDestination] IS NULL AND @original_BuyerDestination IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))" InsertCommand="INSERT INTO [BuyerDestinationMaster] ([BuyerDestination], [BuyerID]) VALUES (@BuyerDestination, @BuyerID)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT BuyerDestinationMaster.BuyerDestination_PK, BuyerMaster.BuyerName, BuyerDestinationMaster.BuyerDestination FROM BuyerDestinationMaster INNER JOIN BuyerMaster ON BuyerDestinationMaster.BuyerID = BuyerMaster.BuyerID ORDER BY BuyerDestinationMaster.BuyerDestination, BuyerDestinationMaster.BuyerID" UpdateCommand="UPDATE [BuyerDestinationMaster] SET [BuyerDestination] = @BuyerDestination, [BuyerID] = @BuyerID WHERE [BuyerDestination_PK] = @original_BuyerDestination_PK AND (([BuyerDestination] = @original_BuyerDestination) OR ([BuyerDestination] IS NULL AND @original_BuyerDestination IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))"></asp:SqlDataSource>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td></td>

        </tr>
    </table>
</asp:Content>
