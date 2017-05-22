<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Documentreceipt.aspx.cs" Inherits="ArtWebApp.Shipping.Documentreceipt" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/GridJQuery.js"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">&nbsp;Inbound Documentation</td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upd_main"  UpdateMode="Conditional"  runat="server">
                                <ContentTemplate>

 <table class="tittlebar">
                    <tr>
                        <td class="NormalTD" >
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="A" Text="dIRECT fROM sUPPLIER" />
                            &nbsp;TO WAREHOUSE</td>
                        <td class="NormalTD" >
                               

                              <asp:RadioButton ID="RadioButton2" runat="server" GroupName="A"  Text="IN DIRECT VIA ATRACO" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rbt_viageneralitem" runat="server" GroupName="A" Text="IN DIRECT VIA ATRACO(General items)" />
                        </td>
                        <td class="NormalTD" >
                            <asp:RadioButton ID="rbt_directgenitem" runat="server" GroupName="A" Text="DIRECT from Supplier (Stock ADN)" />
                        </td>
                        <td class="NormalTD" >

<%--                             <ucc:DropDownListChosen ID="drp_rcpt" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>--%>
                        </td>
                        <td class="ButtonTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">From Location : </td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="drp_ToWarehouse" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td>
                            <asp:Button ID="btn_showfromLoc" runat="server" OnClick="btn_showfromLoc_Click" Text="S" Width="23px" />
                        </td>
                        <td class="NormalTD">Document #</td>
                        <td class="NormalTD">
                            <ig:WebDropDown ID="drp_rcpt" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="name" ValueField="pk" Width="200px">
                                <DropDownItemBinding TextField="name" ValueField="pk" />
                            </ig:WebDropDown>
                            <%--                             <ucc:DropDownListChosen ID="drp_rcpt" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>--%></td>
                        <td class="ButtonTD">
                            <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="S" />
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">Mode </td>
                        <td class="NormalTD"><ucc:DropDownListChosen ID="drp_deliverymode" runat="server" Width="200px">
                        </ucc:DropDownListChosen></td>
                        <td>&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="ButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" >Shipper</td>
                        <td class="NormalTD" >
                               

                              <asp:TextBox ID="txt_shipper" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="NormalTD" >shipper inv&nbsp;</td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txt_shiperinv" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            exporter</td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txt_exporter" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" >Description</td>
                        <td class="NormalTD" >
                               

                              <asp:TextBox ID="txt_description" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="NormalTD" ><b><span style="font-size:10.0pt">NO CTNS /rolls</span></b></td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txt_noctn" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            <b><span style="font-size:10.0pt">CTNS OR roll</span></b></td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txt_ctnroll" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" >weight</td>
                        <td class="NormalTD" >
                               

                              <asp:TextBox ID="txt_weight" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="NormalTD" >type</td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txt_type" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            inv value</td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txt_invvalue" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" >Qty</td>
                        <td class="NormalTD" >
                               

                              <asp:TextBox ID="txt_qty" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="NormalTD" >bl/awb</td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txtBL" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            container </td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txt_container" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" >container type</td>
                        <td class="NormalTD" >
                               

                              <asp:TextBox ID="txt_containertype" runat="server"></asp:TextBox>

                        </td>
                        <td>
                            &nbsp;</td>
                        <td class="NormalTD" >eta</td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="dtp_deliverydate" runat="server" Width="180px"></asp:TextBox>


                                    <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender" runat="server" Enabled="True" TargetControlID="dtp_deliverydate">
                                    </asp:CalendarExtender>
</td>
                        <td class="NormalTD" >
                            Vessel</td>
                        <td class="NormalTD" >
                            <asp:TextBox ID="txt_vessel" runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" >&nbsp;</td>
                        <td class="NormalTD" >

                              &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="NormalTD" >&nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                </table>
                                       </ContentTemplate>
                            </asp:UpdatePanel>
               
            </td>
        </tr>
        <tr>
            <td><asp:UpdatePanel ID="upd_grid"  UpdateMode="Conditional"  runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="tbl_Podetails" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                                        CellPadding="4" 
                                        style="font-size: small; font-family: Calibri" Width="90%" 
                                        DataKeyNames="Doc_Pk">
                                        <Columns>
                                              <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                             <asp:TemplateField HeaderText="Doc_Pk" InsertVisible="False" SortExpression="Doc_Pk">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_doc_pk" runat="server" Text='<%# Bind("Doc_Pk") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                            <asp:BoundField DataField="DocNum" HeaderText="DocNum" 
                                                SortExpression="DocNum" />
                                            <asp:BoundField DataField="Supplierinvoice" HeaderText="Supplierinvoice" 
                                                SortExpression="Supplierinvoice" />
                                            <asp:BoundField DataField="BOENum" HeaderText="BOENum" 
                                                SortExpression="BOENum" />
                                            <asp:BoundField DataField="Remark" HeaderText="Remark" 
                                                SortExpression="Remark" />
                                            <asp:BoundField DataField="InhouseDate" HeaderText="InhouseDate" SortExpression="InhouseDate" />
                                            <asp:BoundField DataField="ETADate" HeaderText="ETADate" SortExpression="ETADate" />
                                            <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" 
                                                SortExpression="SupplierName" />
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




                                    <asp:GridView ID="tbl_dodetails" runat="server" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                                        CellPadding="4" 
                                        style="font-size: small; font-family: Calibri" Width="90%" 
                                        DataKeyNames="DO_PK">
                                        <Columns>
                                                       <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                             <asp:TemplateField HeaderText="Doc_Pk" InsertVisible="False" SortExpression="Doc_Pk">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_DO_PK" runat="server" Text='<%# Bind("DO_PK") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                           
                                            <asp:BoundField DataField="DONum" HeaderText="DONum" 
                                                SortExpression="DONum" />
                                            <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" 
                                                SortExpression="DeliveryDate" />
                                            <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" 
                                                SortExpression="AtcNum" />
                                            <asp:BoundField DataField="ExportContainer" HeaderText="ExportContainer" SortExpression="ExportContainer" />
                                            <asp:BoundField DataField="ContainerNumber" HeaderText="ContainerNumber" SortExpression="ContainerNumber" />
                                            <asp:BoundField DataField="From" HeaderText="From" 
                                                SortExpression="From" />
                                              <asp:BoundField DataField="TO Location" HeaderText="TO Location" SortExpression="TO Location" />
                                              <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" SortExpression="AddedBy" />
                                              <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" SortExpression="AddedDate" />
                                              <asp:BoundField DataField="DoType" HeaderText="DoType" SortExpression="DoType" />
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


                                    


                                    <asp:GridView ID="tbl_StockAdn" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SDoc_Pk" style="font-size: small; font-family: Calibri" Width="90%">
                                        <Columns>
                                                 <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                              <asp:TemplateField HeaderText="SDoc_Pk" InsertVisible="False" SortExpression="SDoc_Pk">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_DO_PK" runat="server" Text='<%# Bind("SDoc_Pk") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                           
                                            <asp:BoundField DataField="SDocNum" HeaderText="SDocNum" SortExpression="SDocNum" />
                                            <asp:BoundField DataField="ContainerNum" HeaderText="ContainerNum" SortExpression="ContainerNum" />
                                            <asp:BoundField DataField="BOENum" HeaderText="BOENum" SortExpression="BOENum" />
                                            <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                                            <asp:BoundField DataField="InhouseDate" HeaderText="InhouseDate" SortExpression="InhouseDate" />
                                            <asp:BoundField DataField="ETADate" HeaderText="ETADate" SortExpression="ETADate" />
                                            <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
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

                                    <asp:GridView ID="tbl_stockDO" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SalesDO_PK" style="font-size: small; font-family: Calibri" Width="90%">
                                        <Columns>
                                                 <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                              <asp:TemplateField HeaderText="SalesDO_PK" InsertVisible="False" SortExpression="SalesDO_PK">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_DO_PK" runat="server" Text='<%# Bind("SalesDO_PK") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>  
                                            <asp:BoundField DataField="SalesDO_PK" HeaderText="SalesDO_PK" SortExpression="SalesDO_PK" InsertVisible="False" ReadOnly="True" />
                                            <asp:BoundField DataField="SalesDONum" HeaderText="SalesDONum" SortExpression="SalesDONum" />
                                            <asp:BoundField DataField="SalesDate" HeaderText="SalesDate" SortExpression="SalesDate" />
                                            <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                                            <asp:BoundField DataField="SalesDODate" HeaderText="SalesDODate" SortExpression="SalesDODate" />
                                            <asp:BoundField DataField="ContainerNumber" HeaderText="ContainerNumber" SortExpression="ContainerNumber" />
                                            <asp:BoundField DataField="BoeNum" HeaderText="BoeNum" SortExpression="BoeNum" />
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


                                </ContentTemplate>
                            </asp:UpdatePanel></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btn_sumbit" runat="server" OnClick="btn_sumbit_Click" Text="Create Import Document" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        DeliveryOrderMaster.DO_PK, DeliveryOrderMaster.DONum, DeliveryOrderMaster.DeliveryDate, AtcMaster.AtcNum, DeliveryOrderMaster.ExportContainer, DeliveryOrderMaster.ContainerNumber, 
                         LocationMaster.LocationName AS [From], LocationMaster_1.LocationName AS [TO Location], DeliveryOrderMaster.AddedBy, DeliveryOrderMaster.AddedDate, DeliveryOrderMaster.DoType
FROM            DeliveryOrderMaster INNER JOIN
                         AtcMaster ON DeliveryOrderMaster.AtcID = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON DeliveryOrderMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                         LocationMaster AS LocationMaster_1 ON DeliveryOrderMaster.ToLocation_PK = LocationMaster_1.Location_PK"></asp:SqlDataSource>
</asp:Content>
