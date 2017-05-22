<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="ArtWebApp.Administrator.UserMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            height: 27px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>




        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">

                <table class="FullTable">
             <tr><td class="RedHeadding">
                 NEW USER FORM</td></tr>
            <tr>
                <td>
                    <table class="DataEntryTable" >
                        <tr>
                            <td >USERNAME:</td>
                            <td >
                                <asp:TextBox ID="txt_username" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                                                 
                            </td>
                        </tr>
                        <tr>
                            <td >PASSWORD:</td>
                            <td >
                                <asp:TextBox ID="txt_password" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >User Profile</td>
                            <td >
                             

                                 <ucc:DropDownListChosen ID="drp_userprofilePK" runat="server" DataSourceID="userprofiledatasource" DataTextField="UserProfileName" DataValueField="UserProfile_Pk" DisableSearchThreshold="10" Height="16px" Width="200px">
                                     <asp:ListItem></asp:ListItem>
                                 </ucc:DropDownListChosen>
                                 <asp:SqlDataSource ID="userprofiledatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [UserProfile_Pk], [UserProfileName], [UserProfileCode], [Description], [IsActive] FROM [UserProfileMaster] WHERE ([IsActive] = @IsActive)">
                                     <SelectParameters>
                                         <asp:Parameter DefaultValue="Y" Name="IsActive" Type="String" />
                                     </SelectParameters>
                                 </asp:SqlDataSource>

                                 

                            </td>
                        </tr>
                        
                        <tr>
                            <td>USER LOCATION :</td>
                            <td>
                                <ucc:DropDownListChosen ID="drp_userlocation" runat="server" DataSourceID="SqlDataSource2" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Height="16px" Width="200px">
                                    <asp:ListItem></asp:ListItem>
                                </ucc:DropDownListChosen>
                            </td>
                        </tr>
                        
                        <tr>
                            <td >&nbsp;</td>
                            <td >
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="btn_submit_Click" Text="SUBMIT" />
                            </td>
                            <td >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                               <div id="Div1" runat="server">
                 


                           <asp:Label ID="Label3" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr  class="gridtable">
                <td>
                    
                    &nbsp;</td>
            </tr>
        </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table class="FullTable">
                  
                    <tr>
                        <td class="RedHeadding">password Change</td>
                    </tr>
                    <tr>
                        <td>
                            <table class="DataEntryTable">
                                <tr>
                                    <td>USERNAME:</td>
                                    <td>
                                        <ucc:DropDownListChosen ID="drp_Name" runat="server" DataSourceID="SqlDataSource1" DataTextField="UserName" DataValueField="User_PK" Width="200px">
                                        </ucc:DropDownListChosen>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>oldPASSWORD:</td>
                                    <td>
                                        <asp:TextBox ID="txt_oldpassword" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>new password :</td>
                                    <td>
                                        <asp:TextBox ID="txt_newpassword"  runat="server" Height="22px" Width="156px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>confirm password :</td>
                                    <td>
                                        <asp:TextBox ID="txt_confirmpassword" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click1" Text="SUBMIT" />
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="Messaediv" runat="server">
                                            <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster]"></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [User_PK], [UserName] FROM [UserMaster]"></asp:SqlDataSource></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="gridtable">
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <table class="DataEntryTable">
                    <tr>
                        <td class="RedHeadding" colspan="3">User Profile Assignment</td>
                       
                    </tr>
                    <tr>
                        <td class="NormalTD">UserName</td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="drp_user" runat="server" DataSourceID="Userdata" DataTextField="UserName" DataValueField="User_PK" DisableSearchThreshold="10" Height="17px" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td  class="NormalTD">User Profile</td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="drp_userprofile" runat="server" datasourceid="userprofiledatasource0" DataTextField="UserProfileName" DataValueField="UserProfile_Pk" DisableSearchThreshold="10" Height="16px" Width="200px">
                                <asp:ListItem></asp:ListItem>
                            </ucc:DropDownListChosen>
                            <asp:SqlDataSource ID="userprofiledatasource0" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [UserProfile_Pk], [UserProfileName], [UserProfileCode], [Description], [IsActive] FROM [UserProfileMaster] WHERE ([IsActive] = @IsActive)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Y" Name="IsActive" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="Userdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [User_PK], [UserName], [UserLoc_PK] FROM [UserMaster] ">
                            </asp:SqlDataSource>
                        </td>
                          <td class="SearchButtonTD">&nbsp;</td>
                       
                    </tr>
                 
                    <tr>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                            <asp:Button ID="Button2" runat="server" Text="Assign Profile" OnClick="Button2_Click" />
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                    </tr>
                 
                </table>
            </asp:View>
        </asp:MultiView>




    </div>
</asp:Content>