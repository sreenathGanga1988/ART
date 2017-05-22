<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="FabricEntrynew.aspx.cs" Inherits="ArtWebApp.Sampling.FabricEntrynew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig"  %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>


<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/style.css" rel="stylesheet" />
            
    <style type="text/css">
        .auto-style1 {
            height: 34px;
        }
    </style>
            
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




    <div >
         <table class="FullTable">
             <tr><td class="RedHeadding">
               
                 courier details entry<br />
                 </td></tr>
            <tr>
                <td class="auto-style7">
                    <table class="DataEntryTable" >
                        <tr>
                            <td class="auto-style11" >MERCH NAME</td>
                            <td class="auto-style8" >
                                <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
                                    
                         

<asp:TextBox ID="txt_merchname" runat="server"  Height="22px" Width="158px"></asp:TextBox>



  
 






                                
                                             
                            </td>
                            <td class="auto-style9" >
                                &nbsp;</td>
                            <td class="auto-style8" >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style11" >DISCRIPTION</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_discription" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                COLOR/CONST</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_color" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" >QTY </td>
                            <td class="auto-style8" >
                             

                                 <asp:TextBox ID="txt_qty" runat="server" Width="155px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                             

                                 WIDTH</td>
                            <td class="auto-style8" >
                             

                                 <asp:TextBox ID="txt_width" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" >WEIGHT</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_weg" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                UNITS</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_unit" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" >SUPPLIER</td>
                            <td class="auto-style8" >
                                <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="SamSupplier" DataTextField="SamSupplierName" DataValueField="SampSupplierID" DisableSearchThreshold="10">
                                </ucc:DropDownListChosen>
                                <asp:SqlDataSource ID="SamSupplier" runat="server" ConnectionString="Data Source=192.168.1.4;Initial Catalog=Art;Persist Security Info=True;User ID=sa;Password=Sreenath@123" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [SamSupplierName], [SampSupplierID] FROM [SamSupplierMaster]"></asp:SqlDataSource>
                            </td>
                            <td class="auto-style9" >
                                SUPREF</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_supref" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">
                                DATE</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_date" runat="server" ReadOnly="true" Height="16px" Width="153px"></asp:TextBox>
                                <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_date"
    Format="dd/MMM/yyyy" >
</cc1:CalendarExtender>      
                            </td>
                            <td class="auto-style9" >
                                AWB NUM</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_awbnum" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">
                                fabric swatch</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_fabricswatch" runat="server" Width="158px" Height="46px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                &nbsp;</td>
                            <td class="auto-style8" >
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style11">
                                &nbsp;</td>
                            <td class="auto-style8" >
                                &nbsp;</td>
                            <td class="auto-style9" >
                                &nbsp;</td>
                            <td class="auto-style8" >
                                &nbsp;</td>
                        </tr>
                    
                        <tr>
                            <td colspan="2" class="auto-style1">
                                <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="SUBMIT" />
                            &nbsp;
                            </td>
                            <td class="auto-style1">
                                </td>
                            <td class="auto-style1">
                                <asp:Label ID="lbl_msg" runat="server" style="color: #009900"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">

                                     <div id="Messaediv" runat="server">
                                 <div id="Div1" runat="server">
                                                 <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                                             </div>
                                         </div>
                            </td>
                            <td class="auto-style9">
                                &nbsp;</td>
                            <td class="auto-style8">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="SamplingFab_PK" DataSourceID="SqlDataSource1" GridLines="Vertical">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:BoundField DataField="SamplingFab_PK" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="SamplingFab_PK" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
                                        <asp:BoundField DataField="Merch_Name" HeaderText="Merch_Name" SortExpression="Merch_Name" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                        <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                                        <asp:BoundField DataField="Width" HeaderText="Width" SortExpression="Width" />
                                        <asp:BoundField DataField="Supplier" HeaderText="Supplier" SortExpression="Supplier" />
                                        <asp:BoundField DataField="SuperRef" HeaderText="SuperRef" SortExpression="SuperRef" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                                        <asp:BoundField DataField="AwbNum" HeaderText="AwbNum" SortExpression="AwbNum" />
                                        <asp:BoundField DataField="Weight" HeaderText="Weight" SortExpression="Weight" />
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr  class="gridtable">
                <td class="auto-style7">
                    
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>

        <table class="DataEntryTable">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SamplingFab_PK], [Code], [Merch_Name], [Description], [Color], [Qty], [Width], [Supplier], [SuperRef], [Date], [AwbNum], [Weight], [Unit] FROM [SampleFabricEntryMaster]"></asp:SqlDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            </table>

    </div>
</asp:Content>

