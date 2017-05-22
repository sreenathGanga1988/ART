<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ServicePO.aspx.cs" Inherits="ArtWebApp.Merchandiser.ServicePO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style type="text/css">
         
    </style>
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table class="FullTable">
        <tr class="RedHeadding">
            <td>
                SERVICE P.O.
            </td>
        </tr>
         <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">Debit From :</td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                 <%--   <ig:WebDropDown ID="ddl_debitfrom" runat="server" AutoPostBack="True" EnableAutoFiltering="Client" OnSelectionChanged="ddl_debitfrom_SelectionChanged" Width="200px" CurrentValue="ATC">
                                        <Items>
                                            <ig:DropDownItem Selected="False" Text="ATC" Value="Atc">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Buyer" Value="Buyer">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Department" Value="Department">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Factory" Value="Factory">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Individual" Value="Individual">
                                            </ig:DropDownItem>
                                        </Items>
                                    </ig:WebDropDown>--%>


                                     <ucc:DropDownListChosen ID="ddl_debitfrom" AutoPostBack="True" Width="170px" runat="server" OnSelectedIndexChanged="ddl_debitfrom_SelectedIndexChanged">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>ATC</asp:ListItem>
                                                <asp:ListItem>Buyer</asp:ListItem>
                                                <asp:ListItem>Department</asp:ListItem>
                                                <asp:ListItem>Factory</asp:ListItem>
                                                <asp:ListItem>Individual</asp:ListItem>
                                                <asp:ListItem>Atraco</asp:ListItem>
                                            </ucc:DropDownListChosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">Name:</td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                  


                                    <ucc:DropDownListChosen ID="drp__name" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">Service Type:</td>
                        <td class="NormalTD">
                          

                             <ucc:DropDownListChosen ID="drp_serviceType" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                        </td>
                        <td class="NormalTD">Amount</td>
                        <td class="NormalTD">
                            <asp:TextBox ID="txt_amount" runat="server" Width="217px"></asp:TextBox>
                        </td>
                        <td class="NormalTD">Currency</td>
                        <td class="NormalTD">
                            <%--<ig:WebDropDown ID="drp_currency" runat="server" TextField="name" ValueField="pk" Width="200px">
                                <DropDownItemBinding TextField="name" ValueField="pk" />
                            </ig:WebDropDown>--%>


                             <ucc:DropDownListChosen ID="drp_currency" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">Description:</td>
                        <td class="NormalTD" colspan="3">
                            <asp:TextBox ID="txt_description" runat="server" Height="60px" Width="226px"></asp:TextBox>
                    <br />
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD">
                            <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Save" Width="93px" />
                        </td>
                        <td class="NormalTD">
                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" Width="79px" />
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD" colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_errordisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
            </td>
        </tr>
    </table>

   
    
    
</asp:Content>
