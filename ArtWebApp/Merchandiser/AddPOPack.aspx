<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AddPOPack.aspx.cs" Inherits="ArtWebApp.Merchandiser.AddPOPack" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div>




        <table class="FullTable">
            <tr>
                <td class="RedHeadding">Add New ASQ</td>
            </tr>
            <tr>
                <td>
                 
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table class="DataEntryTable">
                                        <tr>
                                            <td class="NormalTD">Atc # :</td>
                                            <td class="NormalTD">
                                                <asp:TextBox ID="txt_atcnum" runat="server" CssClass="auto-style39"></asp:TextBox>
                                                <b>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_atcnum" CssClass="auto-style36" ErrorMessage="Enter Atc" ForeColor="Red" ValidationGroup="newpo">*</asp:RequiredFieldValidator>
                                                </b></td>
                                            <td class="NormalTD"></td>
                                            <td class="NormalTD"></td>
                                            <td class="NormalTD">
                                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Repeat PO" AutoPostBack="True" />
                                            </td>
                                            <td class="NormalTD">
                                                
                                                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                                <ucc:DropDownListChosen ID="DropDownListChosen1" runat="server" DataSourceID="popack" DataTextField="BuyerPO" DataValueField="BuyerPO" DisableSearchThreshold="10" Width="200px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="DropDownListChosen1_SelectedIndexChanged">
                                     </ucc:DropDownListChosen>
                             </ContentTemplate>
                    </asp:UpdatePanel>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Buyer PO# :</td>
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
                                            <td>Pack Instruction : </td>
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
                                            <td>PO Group</td>
                                            <td>
                                              

                                                 <ucc:DropDownListChosen ID="drp_pogroup" Width="120px" runat="server">
                            <asp:ListItem Value="GP1">GP1</asp:ListItem>
                            <asp:ListItem Value="GP2">GP2</asp:ListItem>
                             <asp:ListItem Value="GP3">GP3</asp:ListItem>
                            <asp:ListItem Value="GP4">GP4</asp:ListItem>
                            
                            
                        </ucc:DropDownListChosen>
                                            </td>
                                            <td>Tag group</td>
                                            <td>
                                               

                                                   <ucc:DropDownListChosen ID="drp_taggroup" Width="120px" runat="server">
                            <asp:ListItem Value="TD1">TD1</asp:ListItem>
                            <asp:ListItem Value="TD2">TD2</asp:ListItem>
                             <asp:ListItem Value="TD3">TD3</asp:ListItem>
                          
                            
                            
                        </ucc:DropDownListChosen>

                                            </td>
                                            <td>Season</td>
                                            <td> 
                                                
                                                 <ucc:DropDownListChosen ID="drp_season" runat="server" DataSourceID="SeasonData" DataTextField="SeasonName" DataValueField="season_pk" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>


                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btn_save" runat="server" CssClass="auto-style29" OnClick="btn_save_Click" Text="Save" ValidationGroup="newpo" Width="100%" />
                                            </td>
                                            <td>
                                                <div class="">
                                                      <asp:Label ID="lbl_errordisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                                </div>
                                              
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary1" CssClass="error-message" runat="server" Height="96px" ValidationGroup="newpo" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                       
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                            <asp:SqlDataSource ID="ChannelData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [ChannelID], [ChannelName] FROM [ChannelMaster] ORDER BY [ChannelName]">
                            </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SeasonData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Season_PK], [SeasonName] FROM [SeasonMaster] ORDER BY [Season_PK] DESC, [SeasonName]"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="Destinationdata" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [BuyerDestinationMaster] WHERE [BuyerDestination_PK] = @original_BuyerDestination_PK AND (([BuyerDestination] = @original_BuyerDestination) OR ([BuyerDestination] IS NULL AND @original_BuyerDestination IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))" InsertCommand="INSERT INTO [BuyerDestinationMaster] ([BuyerDestination], [BuyerID]) VALUES (@BuyerDestination, @BuyerID)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT BuyerDestinationMaster.BuyerDestination_PK, BuyerMaster.BuyerName, BuyerDestinationMaster.BuyerDestination FROM BuyerDestinationMaster INNER JOIN BuyerMaster ON BuyerDestinationMaster.BuyerID = BuyerMaster.BuyerID ORDER BY BuyerDestinationMaster.BuyerDestination, BuyerDestinationMaster.BuyerID" UpdateCommand="UPDATE [BuyerDestinationMaster] SET [BuyerDestination] = @BuyerDestination, [BuyerID] = @BuyerID WHERE [BuyerDestination_PK] = @original_BuyerDestination_PK AND (([BuyerDestination] = @original_BuyerDestination) OR ([BuyerDestination] IS NULL AND @original_BuyerDestination IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))">
                            
                                 
                                
                             </asp:SqlDataSource>
                            <asp:HiddenField ID="hdn_atc" runat="server" />
                            <asp:SqlDataSource ID="popack" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT BuyerPO FROM PoPackMaster WHERE (AtcId = @Param1)">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="0" Name="Param1" QueryStringField="atcid" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                </td>
            </tr>
        </table>




    </div>
                    


                 

</asp:Content>
