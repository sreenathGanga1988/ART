<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CourierEntry.aspx.cs" Inherits="ArtWebApp.Shipping.CourierEntry" %>
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
                            <td class="auto-style11" >COURIER DATE</td>
                            <td class="auto-style8" >
                                <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>--%>
                                    
                         

<asp:TextBox ID="txt_courierdate" runat="server" ReadOnly="true"></asp:TextBox>



  
<cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_courierdate"
    Format="dd/MMM/yyyy" >
</cc1:CalendarExtender>       






                                
                                             
                            </td>
                            <td class="auto-style9" >
                                TYPE OF SAMPLE</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_typeofsample" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" >BUYER</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_buyer" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                ATC/STYLE</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_atcstyle" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" >QTY </td>
                            <td class="auto-style8" >
                             

                                 <asp:TextBox ID="txt_qty" runat="server" Width="155px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                             

                                 SENDER</td>
                            <td class="auto-style8" >
                             

                                 <asp:TextBox ID="txt_sender" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" >RECIVER </td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_reciver" runat="server" CssClass="auto-style26" Height="18px" Width="160px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                DESTINATION</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_destination" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" >ACCOUNT NUM </td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_accountnum" runat="server" Width="159px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                IS OUR ACCOUNT</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_isouraccount" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">
                                other account</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_otheraccount" runat="server" Height="16px" Width="153px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                ISNORMALCOURIER</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_isnormalcourier" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">
                                WEIGHT</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_weight" runat="server" Width="158px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                APPRCOST</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_apprcost" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">
                                REMARK</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_remark" runat="server" Height="16px" Width="153px"></asp:TextBox>
                            </td>
                            <td class="auto-style9" >
                                ADDEDDATE</td>
                            <td class="auto-style8" >
                                <asp:TextBox ID="txt_addeddate" runat="server" ReadOnly="true"></asp:TextBox>



  
<cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_addeddate"
    Format="dd/MMM/yyyy" >
</cc1:CalendarExtender>       

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                            <td class="auto-style9">
                                awb num</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="txt_awbnum" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="auto-style14">
                                </td>
                            <td class="auto-style15">
                                &nbsp;</td>
                            <td class="auto-style16">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="SUBMIT" />
                            </td>
                            <td class="auto-style9">
                                &nbsp;</td>
                            <td class="auto-style8">
                                <asp:Label ID="lbl_msg" runat="server" style="color: #009900"></asp:Label>
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

                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CourierID" DataSourceID="SqlDataSource1">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="CourierID" HeaderText="CourierID" InsertVisible="False" ReadOnly="True" SortExpression="CourierID" />
                                    <asp:BoundField DataField="TypeofSample" HeaderText="TypeofSample" SortExpression="TypeofSample" />
                                    <asp:BoundField DataField="CourierDate" HeaderText="CourierDate" SortExpression="CourierDate" />
                                    <asp:BoundField DataField="Buyer" HeaderText="Buyer" SortExpression="Buyer" />
                                    <asp:BoundField DataField="AtcOrStyle" HeaderText="AtcOrStyle" SortExpression="AtcOrStyle" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                    <asp:BoundField DataField="Sender" HeaderText="Sender" SortExpression="Sender" />
                                    <asp:BoundField DataField="Reciever" HeaderText="Reciever" SortExpression="Reciever" />
                                    <asp:BoundField DataField="AWBnum" HeaderText="AWBnum" SortExpression="AWBnum" />
                                    <asp:BoundField DataField="Destination" HeaderText="Destination" SortExpression="Destination" />
                                    <asp:BoundField DataField="Weight" HeaderText="Weight" SortExpression="Weight" />
                                    <asp:BoundField DataField="ApprCost" HeaderText="ApprCost" SortExpression="ApprCost" />
                                    <asp:BoundField DataField="Addeddate" HeaderText="Addeddate" SortExpression="Addeddate" />
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CourierID], [TypeofSample], [CourierDate], [Buyer], [AtcOrStyle], [Quantity], [Sender], [Reciever], [AWBnum], [Destination], [Weight], [ApprCost], [Addeddate] FROM [CourierTable] ORDER BY [CourierID] DESC"></asp:SqlDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            </table>

    </div>
</asp:Content>

