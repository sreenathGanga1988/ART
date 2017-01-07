<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="WrongDocument.aspx.cs" Inherits="ArtWebApp.Inventory.Inventory_Requests.WrongDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">    
       

       
        .hidden {
            display: none;
        }


        .auto-style8 {
            height: 27px;
        }


        </style>
    
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
       
        <tr>
            <td> <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="6">incorrect MRN/Transfer note </td>
                            </tr>

                            <tr>
                                <td >Location :</td>
                                <td  class="NormalTD">
                                   <%-- <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableDropDownAsChild="false" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px">
                                        <DropDownItemBinding TextField="name" ValueField="pk" />
                                    </ig:WebDropDown>--%>


                                    <ucc:DropDownListChosen ID="drp_loc" runat="server" DataTextField="LocationName" DataValueField="Location_PK" Height="17px" Width="200px" DataSourceID="LocationData" DisableSearchThreshold="10">
                                    </ucc:DropDownListChosen>


                                </td>
                                 <td  class="NormalTD">
                                     <asp:Button ID="Button3" runat="server" Text="S" OnClick="Button3_Click" />
                                </td>
                                <td >Responsible (Store clerk/Inventory)</td>
                               <td  class="NormalTD">
                                   


                                   <ucc:DropDownListChosen ID="drp_user" runat="server"  DataTextField="UserName" DataValueField="User_PK" Height="17px" Width="200px" DataSourceID="Userdata" DisableSearchThreshold="10">
                                    </ucc:DropDownListChosen></td>
                                 <td  class="NormalTD">
                                     <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="S" />
                                </td>
                            </tr>
                            <tr>
                                <td>Document Type </td>
                                <td class="NormalTD"><%-- <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableDropDownAsChild="false" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px">
                                        <DropDownItemBinding TextField="name" ValueField="pk" />
                                    </ig:WebDropDown>--%>
                                    <ucc:DropDownListChosen ID="drp_doctype" runat="server" Height="17px" Width="200px">
                                        <asp:ListItem>TransferNote</asp:ListItem>
                                        <asp:ListItem>MRN</asp:ListItem>
                                        <asp:ListItem>RO Receipt</asp:ListItem>
                                        <asp:ListItem>LOAN</asp:ListItem>
                                    </ucc:DropDownListChosen>
                                </td>
                                <td class="NormalTD">
                                    <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click1" />
                                </td>
                                <td>Document #</td>
                                <td class="NormalTD">
                                    <ucc:DropDownListChosen ID="drp_docnumber" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td class="NormalTD">
                                    <asp:Button ID="Button2" runat="server" Text="S" />
                                </td>
                            </tr>
                            <tr>
                                <td  class="NormalTD">
                                    explanation</td>
                                 <td  class="NormalTD"><asp:TextBox ID="txt_exp" runat="server" Height="47px" Width="171px"></asp:TextBox>
                                </td>
                                <td >&nbsp;</td>
                                <td>&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                               <td  class="NormalTD">Action Required</td>
                                 <td  class="NormalTD"><asp:TextBox ID="txt_actnreq" runat="server" Height="47px" Width="171px"></asp:TextBox></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                 <td  class="NormalTD"></td>
                                 <td  class="NormalTD">
                                    
                                </td>
                                <td class="auto-style8"></td>
                                <td class="auto-style8"></td>
                                <td class="auto-style8"></td>
                                <td class="auto-style8"></td>
                            </tr>
                            <tr>
                                <td class="NormalTD" >RemaRK FROM it(<em>prior to management approval</em>)</td>
                                <td class="NormalTD" >
                                    <asp:TextBox ID="txt_remark" runat="server" Enabled="False" Height="47px" Width="171px"></asp:TextBox>
                                </td>
                                <td >&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            
                            <caption>
                            </caption>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
        </tr>
        
         <tr class="SmallSearchButton">
            <td>

                
                        <asp:Button ID="Btn_submit" runat="server" Text="Submit" Height="25px" OnClick="Btn_submit_Click" style="font-size: small; font-family: Calibri; text-align: center" />
                 

            </td>
        </tr>
        <tr>
            <td>
   

               
              <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
    </table>
    <asp:SqlDataSource ID="LocationData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster]"></asp:SqlDataSource>
                                        <asp:SqlDataSource ID="Userdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [User_PK], [UserName], [UserLoc_PK] FROM [UserMaster] WHERE ([UserLoc_PK] = @UserLoc_PK)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="drp_loc" Name="UserLoc_PK" PropertyName="SelectedValue" Type="Decimal" />
                                            </SelectParameters>
</asp:SqlDataSource>
</asp:Content>
