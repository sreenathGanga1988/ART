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
            <td class="NormalTD">
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">Atc # :</td>
                        <td class="NormalTD">



                            <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="atcmasterdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>

                        </td>
                        <td class="NormalTD">
                            <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />
                        </td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
                    <tr>
                        <td class="NormalTD">ASQ #:</td>
                        <td class="NormalTD">


                            <ucc:DropDownListChosen ID="cmb_po" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td class="NormalTD">
                            <asp:Button ID="Button2" runat="server" Text="S" OnClick="Button2_Click" />
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" colspan="5"><table class="auto-style9">
                                <tr>
                                    <td class="NormalTD">Po Status</td>
                                    <td  class="NormalTD">
                                        <asp:UpdatePanel ID="upd_postatus" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lbl_postatus" runat="server" Text="lbl_postatus" Font-Bold="True" Font-Italic="True" ForeColor="#FF3300"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td  class="NormalTD">&nbsp;</td>
                                    <td  class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                </tr>
                            </table></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <table class="DataEntryTable" >
                                    <tr>
                                        <td class="RedHeadding" colspan="6">ASQ DETAILS</td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">PoPackID</td>
                                        <td class="NormalTD">
                                            <asp:Label ID="lbl_popackid" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">Buyer PO# :</td>
                                        <td class="NormalTD">
                                            <asp:TextBox ID="txt_buyerpo" runat="server" CssClass="NormalTD9"></asp:TextBox>
                                            <b>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_buyerpo" CssClass="NormalTD6" ErrorMessage="Enter Buyer PO" ForeColor="Red" ValidationGroup="newpo">*</asp:RequiredFieldValidator>
                                            </b></td>
                                        <td class="NormalTD">Channel </td>
                                        <td class="NormalTD">


                                            <ucc:DropDownListChosen ID="drp_channel" runat="server" DataSourceID="ChannelData" DataTextField="Channelname" DataValueField="ChannelID" DisableSearchThreshold="10" Width="200px">
                                            </ucc:DropDownListChosen>
                                        </td>
                                        <td class="NormalTD">Destination : </td>
                                        <td class="NormalTD">

                                            <ucc:DropDownListChosen ID="drp_dest" runat="server" DataSourceID="Destinationdata" DataTextField="BuyerDestination" DataValueField="BuyerDestination_PK" DisableSearchThreshold="10" Width="200px">
                                            </ucc:DropDownListChosen>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">Pack Instruction : </td>
                                        <td class="NormalTD">
                                            <asp:TextBox ID="txt_packdetail" runat="server" CssClass="NormalTD9" Height="145px" Width="150px" TextMode="MultiLine"></asp:TextBox>
                                            <b>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_packdetail" CssClass="NormalTD6" ErrorMessage="ENter Pack Instruction" ForeColor="Red" ValidationGroup="newpo">*</asp:RequiredFieldValidator>
                                            </b></td>
                                        <td class="NormalTD">&nbsp;first Delivery date : </td>
                                        <td class="NormalTD">
                                            <ig:WebDatePicker ID="dtp_deliverydate" runat="server" Enabled="False">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td class="NormalTD">Inhouse Date :</td>
                                        <td class="NormalTD">
                                            <ig:WebDatePicker ID="dtp_inhousedate" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">Handover Date</td>
                                        <td class="NormalTD">
                                            <ig:WebDatePicker ID="dtp_hd" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td class="NormalTD">Revised Delivery Date</td>
                                        <td class="NormalTD">
                                            <ig:WebDatePicker ID="dtp_rsd" runat="server">
                                            </ig:WebDatePicker>
                                        </td>
                                        <td class="NormalTD">Factory(Recomended)</td>
                                        <td class="NormalTD">
                                            <ucc:DropDownListChosen ID="drp_loc" runat="server" DataSourceID="FACTORYDATASOURCE" DataTextField="LocationName" DataValueField="Location_PK"  Width="170px"  Font-Size="Smaller">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </ucc:DropDownListChosen>
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
                                        <td class="NormalTD"><%--<ig:WebDropDown ID="drp_season" runat="server" DataSourceID="SeasonData" Height="16px" TextField="SeasonName" ValueField="season_pk" Width="174px">
                                             <DropDownItemBinding TextField="SeasonName" ValueField="season_pk" />
                                         </ig:WebDropDown>--%>

                                            <ucc:DropDownListChosen ID="drp_season" Width="170px" runat="server" DataSourceID="SeasonData" DataTextField="SeasonName" DataValueField="season_pk">
                                            </ucc:DropDownListChosen>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">
                                            <asp:Button ID="btn_save" runat="server" CssClass="NormalTD9" OnClick="btn_save_Click" Text="Update" ValidationGroup="newpo" Width="100%" />
                                        </td>
                                        <td class="NormalTD">
                                            <asp:Label ID="lbl_errordisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="96px" ValidationGroup="newpo" />
                                        </td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">
                                            <asp:SqlDataSource ID="atcmasterdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="ChannelData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [ChannelID], [ChannelName] FROM [ChannelMaster] ORDER BY [ChannelName]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SeasonData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Season_PK], [SeasonName] FROM [SeasonMaster] ORDER BY [Season_PK] DESC, [SeasonName]"></asp:SqlDataSource>
                                                   <asp:SqlDataSource ID="FACTORYDATASOURCE" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="Destinationdata" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [BuyerDestinationMaster] WHERE [BuyerDestination_PK] = @original_BuyerDestination_PK AND (([BuyerDestination] = @original_BuyerDestination) OR ([BuyerDestination] IS NULL AND @original_BuyerDestination IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))" InsertCommand="INSERT INTO [BuyerDestinationMaster] ([BuyerDestination], [BuyerID]) VALUES (@BuyerDestination, @BuyerID)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT BuyerDestinationMaster.BuyerDestination_PK, BuyerMaster.BuyerName, BuyerDestinationMaster.BuyerDestination FROM BuyerDestinationMaster INNER JOIN BuyerMaster ON BuyerDestinationMaster.BuyerID = BuyerMaster.BuyerID ORDER BY BuyerDestinationMaster.BuyerDestination, BuyerDestinationMaster.BuyerID" UpdateCommand="UPDATE [BuyerDestinationMaster] SET [BuyerDestination] = @BuyerDestination, [BuyerID] = @BuyerID WHERE [BuyerDestination_PK] = @original_BuyerDestination_PK AND (([BuyerDestination] = @original_BuyerDestination) OR ([BuyerDestination] IS NULL AND @original_BuyerDestination IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))"></asp:SqlDataSource>
                                        </td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                        <td class="NormalTD">&nbsp;</td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="NormalTD"></td>

        </tr>
    </table>
</asp:Content>
