<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProcurementReport.aspx.cs" Inherits="ArtWebApp.Reports.ProcurementReport" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%--<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
  
     
  
    </style>
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td>

            <div>

                <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="8">PO/MRN Reports</td>
                </tr>
                <tr>
                    <td class="NormalTD">Atc #</td>
                    <td class="NormalTD">

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                              
                                <ucc:DropDownListChosen ID="drp_Atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="NormalTD">
                         <asp:UpdatePanel ID="UPD_ATC" runat="server">
                            <ContentTemplate>
                        <asp:Button ID="BTN_ATC" runat="server" OnClick="Button1_Click" Text="S" />
                                   </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_cuttingticketbom" runat="server" OnClick="btn_cuttingticketbom_Click" Text="BOM" />
                    </td>
                    <td class="NormalTD">Po#</td>
                    <td class="NormalTD">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                             

                                <ucc:DropDownListChosen ID="drp_PO" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="NormalTD">


            <asp:Button ID="Btn_show" runat="server" OnClick="Btn_show_Click" Text="Show PO " />
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Po Status" />
                    </td>
                </tr>
                <tr>
                    <td class="NormalTD">Stock po </td>
                    <td class="NormalTD">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                               <%-- <ig:WebDropDown ID="drp_spo" runat="server" DataSourceID="Spodatasource" TextField="SPONum" ValueField="SPO_Pk" Width="200px">
                                    <DropDownItemBinding TextField="SPONum" ValueField="SPO_Pk" />
                                </ig:WebDropDown>--%>

                                  <ucc:DropDownListChosen ID="drp_spo" runat="server" DataSourceID="Spodatasource" DataTextField="SPONum" DataValueField="SPO_Pk" Width="200px">
                                    </ucc:DropDownListChosen>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                         <asp:Button ID="Button1" runat="server" Text="Show Stock PO" OnClick="Button1_Click1" />
                    </td>
                    <td>
                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Stock PO Status" />
                    </td>
                    <td>&nbsp;</td>
                    <td class="NormalTD">

                        &nbsp;</td>
                    <td class="NormalTD">


                        &nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">sERVICE po</td>
                    <td class="NormalTD">

                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                               <%-- <ig:WebDropDown ID="drp_spo" runat="server" DataSourceID="Spodatasource" TextField="SPONum" ValueField="SPO_Pk" Width="200px">
                                    <DropDownItemBinding TextField="SPONum" ValueField="SPO_Pk" />
                                </ig:WebDropDown>--%>

                                  <ucc:DropDownListChosen ID="drp_servicepo" runat="server" DataSourceID="ServicePodataSource" DataTextField="ServicePOnumber" DataValueField="ServicePO_PK" DisableSearchThreshold="10" Width="200px">
                                </ucc:DropDownListChosen>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                         <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Show " ToolTip="Show the Selected Service PO" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="NormalTD">

                        &nbsp;</td>
                    <td class="NormalTD">


                        &nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">PO# :</td>
                    <td class="NormalTD">

                        <asp:TextBox ID="txt_ponum" runat="server"></asp:TextBox>
                    </td>
                    <td>
                         <asp:Button ID="btn_search" runat="server" OnClick="btn_search_Click" Text="Search PO" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="NormalTD">

                        &nbsp;</td>
                    <td class="NormalTD">


                        &nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
            </table>

            </div>

            
        </td>
    </tr>
    <tr>
        <td class="ReportViewSection" >

            
                <div style="height:100vh">
                    <asp:UpdatePanel ID="upl_rpt" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                        <LocalReport ReportPath="Reports\RDLC\APO.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel>
                </div>

            

        </td>
    </tr>
    <tr>
        <td>
            <asp:SqlDataSource ID="Spodatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SPO_Pk], [SPONum] FROM [StockPOMaster]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="ServicePodataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [ServicePO_PK], [ServicePOnumber] FROM [ServicePOMaster] ORDER BY [ServicePOnumber] DESC"></asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>
