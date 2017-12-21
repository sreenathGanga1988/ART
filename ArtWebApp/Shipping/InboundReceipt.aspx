<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InboundReceipt.aspx.cs" Inherits="ArtWebApp.Shipping.InboundReceipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="RedHeaddingdIV">Receive Inbound Document</div>
    <div>
        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" DataSourceID="PendingADN" DataKeyNames="ShipingDoc_PK">
            <Columns>            <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                <asp:TemplateField HeaderText="ShipingDoc_PK" InsertVisible="False" SortExpression="ShipingDoc_PK">
                 
                    <ItemTemplate>
                        <asp:Label ID="lbl_ShipingDoc_PK" runat="server" Text='<%# Bind("ShipingDoc_PK") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ShipDocNum" HeaderText="ShipDocNum" ReadOnly="True" SortExpression="ShipDocNum" />
              
       
                
                      <asp:TemplateField HeaderText="Shipper" SortExpression="Description">
                  
                    <ItemTemplate>
                       
                        <table>
<tbody>
<tr>
<td>ShipperName</td>
<td> <asp:Label ID="ShipperName" runat="server" Text='<%# Bind("ShipperName") %>'></asp:Label></td>
</tr>
<tr>
<td>ExporterName</td>
<td><asp:Label ID="ExporterName" runat="server" Text='<%# Bind("ExporterName") %>'></asp:Label></td>
</tr>
<tr>
<td>ShipperInv</td>
<td><asp:Label ID="ShipperInv" runat="server" Text='<%# Bind("ShipperInv") %>'></asp:Label></td>
</tr>
</tbody>
</table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                  
                    <ItemTemplate>
                       
                        <table>
<tbody>
<tr>
<td>Description</td>
<td> <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label></td>
</tr>
<tr>
<td>No of Ctn</td>
<td><asp:Label ID="Label2" runat="server" Text='<%# Bind("NOofctnRoll") %>'></asp:Label></td>
</tr>
<tr>
<td>Package Type</td>
<td><asp:Label ID="Label3" runat="server" Text='<%# Bind("Packagetype") %>'></asp:Label></td>
</tr>
</tbody>
</table>
                    </ItemTemplate>
                </asp:TemplateField>


                 <asp:TemplateField HeaderText="Detail" SortExpression="Detail">
                  
                    <ItemTemplate>
                       
                        <table>
<tbody>
<tr>
<td>Weight</td>
<td> <asp:Label ID="LabelWeight" runat="server" Text='<%# Bind("Weight") %>'></asp:Label></td>
</tr>
<tr>
<td>Type</td>
<td><asp:Label ID="LabelType" runat="server" Text='<%# Bind("Type") %>'></asp:Label></td>
</tr>
<tr>
<td>InvoiceValue</td>
<td><asp:Label ID="LabelInvoiceValue" runat="server" Text='<%# Bind("InvoiceValue") %>'></asp:Label></td>
</tr>
</tbody>
</table>
                    </ItemTemplate>
                </asp:TemplateField>


  
               
               
                <asp:TemplateField HeaderText="Conatianer" SortExpression="Conatianer">
                    
                    <ItemTemplate>
                                           <table>
<tbody>
<tr>
<td>Container</td>
<td> <asp:Label ID="LabelConatianer" runat="server" Text='<%# Bind("Conatianer") %>'></asp:Label></td>
</tr>
<tr>
<td>Container Type</td>
<td><asp:Label ID="LabelContsainerType" runat="server" Text='<%# Bind("ContsainerType") %>'></asp:Label></td>
</tr>
<tr>
<td>ETA</td>
<td><asp:Label ID="LabelETA" runat="server" Text='<%# Bind("ETA") %>'></asp:Label></td>
</tr>
    <tr>
<td>Vessel</td>
<td><asp:Label ID="Vessel" runat="server" Text='<%# Bind("Vessel") %>'></asp:Label></td>
</tr>
</tbody>
</table>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="BL" SortExpression="Conatianer">
                    
                    <ItemTemplate>
                                           <table>
<tbody>
<tr>
<td>BL</td>
<td> <asp:Label ID="LabelBL" runat="server" Text='<%# Bind("BL") %>'></asp:Label></td>
</tr>
<tr>
<td>Mode</td>
<td><asp:Label ID="LabelMode" runat="server" Text='<%# Bind("Mode") %>'></asp:Label></td>
</tr>
<tr>
<td>DocType</td>
<td><asp:Label ID="LabelDocType" runat="server" Text='<%# Bind("DocType") %>'></asp:Label></td>
</tr>
</tbody>
</table>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AddedBy" SortExpression="Conatianer">
                    
                    <ItemTemplate>
                                           <table>
<tbody>
<tr>
<td>AddedDate</td>
<td> <asp:Label ID="LabelAddedDate" runat="server" Text='<%# Bind("AddedDate") %>'></asp:Label></td>
</tr>
<tr>
<td>AddedBY</td>
<td><asp:Label ID="LabelAddedBY" runat="server" Text='<%# Bind("AddedBY") %>'></asp:Label></td>
</tr>


</tbody>
</table>
                    </ItemTemplate>
                </asp:TemplateField>
           
                <asp:TemplateField HeaderText="TentativeETA" SortExpression="TentativeETA">
                   
                    <ItemTemplate>
                      <asp:TextBox ID="dtp_deliverydate" runat="server" Font-Size="Smaller" Width="120px"></asp:TextBox>


                                    <ajaxToolkit:CalendarExtender ID="dtp_deliverydate_CalendarExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_deliverydate" >
                                    </ajaxToolkit:CalendarExtender>            </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#000066" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Black" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
        <asp:SqlDataSource ID="PendingADN" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
            SelectCommand="SELECT        ShipingDoc_PK, AddedDate, AddedBY, ShipperName, ExporterName, ShipperInv, Description, NOofctnRoll,
 Packagetype, Weight, Type, InvoiceValue, Vessel, Conatianer, ContsainerType, ETA,  ISNULL( ShipDocNum,'') AS ShipDocNum, ISNULL( BL,'') AS BL , 
                          ISNULL( Mode,'') AS  Mode,  ISNULL(  DocType,'') AS DocType , IsReceived, ReceivedBy, ISNULL(  TentativeETA,'') AS TentativeETA 
FROM            ShippingDocumentMaster
WHERE        (IsReceived = N'N')"></asp:SqlDataSource>
    </div>
    <div>

        <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" />

    </div>
    <div id="Messaediv" runat="server">
                                                    <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                                </div>
</asp:Content>
