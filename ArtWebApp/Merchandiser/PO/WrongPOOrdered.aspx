<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="WrongPOOrdered.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.WrongPOOrdered" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">    
       

       
        .hidden {
            display: none;
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
                                <td class="RedHeadding" colspan="6">incorrect item on po</td>
                            </tr>
                            <tr>
                                <td >aTC#</td>
                                <td >
                                   <%-- <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableDropDownAsChild="false" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px">
                                        <DropDownItemBinding TextField="name" ValueField="pk" />
                                    </ig:WebDropDown>--%>


                                    <ucc:DropDownListChosen ID="drp_Atc" runat="server"  DataTextField="name" DataValueField="pk" Height="17px" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td >
                                    <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />
                                </td>
                                <td >po#</td>
                                <td >
                                   


                                    <ucc:DropDownListChosen ID="drp_PO" runat="server"  DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>







                                </td>
                                <td >
                                    <asp:Button ID="Button2" runat="server" Text="S" OnClick="Button2_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    explanation</td>
                                <td ><asp:TextBox ID="txtarea" runat="server" Height="47px" Width="171px"></asp:TextBox>
                                </td>
                                <td >&nbsp;</td>
                                <td>&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Merchandiser</td>
                                <td>
                                    <asp:TextBox ID="txt_merchand" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    <asp:Label ID="lbl_mssg" runat="server" Text="*"></asp:Label>
                                </td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            <caption>
                            </caption>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
        </tr>
        <tr>
            <td class="gridtable">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PODet_PK" OnRowDataBound="tbl_podetails_RowDataBound" OnRowCommand="tbl_podetails_RowCommand">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                  <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                <asp:TemplateField HeaderText="PDPK" InsertVisible="False" SortExpression="PODet_PK">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_podetpk" runat="server" Text='<%# Bind("PODet_PK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SDPK" InsertVisible="False" SortExpression="SkuDet_PK">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_skudetpk" runat="server" Text='<%# Bind("SkuDet_PK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RMNum" HeaderText="RMNum" SortExpression="RMNum" />
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="ItemName" />
                                <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" ReadOnly="True" SortExpression="ItemColor" />
                                <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" ReadOnly="True" SortExpression="ItemSize" />
                                <asp:TemplateField HeaderText="SupplierColor" SortExpression="SupplierColor">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_suppliercolor" runat="server" Text='<%# Bind("SupplierColor") %>'></asp:Label>
                                    
                                    
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SupplierSize" SortExpression="SupplierSize">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_suppliersize" runat="server" Text='<%# Bind("SupplierSize") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField DataField="POQty" HeaderText="poqty" SortExpression="poqty" />
                                <asp:TemplateField HeaderText="POQty" SortExpression="POQty">
                                  
                                    <ItemTemplate>
                                          <asp:TextBox ID="txt_poqty" runat="server" Text='<%# Bind("POQty") %>' Height="16px" Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UomCode" HeaderText="Uom" SortExpression="UomCode" />
                                <asp:TemplateField HeaderText="UnitRate" SortExpression="POUnitRate">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="txt_unitrate" runat="server" Height="16px"   Width="50px" Text='<%# Bind("POUnitRate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CURate">
                                     <ItemTemplate>
                                        <asp:Label ID="txt_curate" Height="16px"   Width="50px"  runat="server" Text='<%# Bind("CURate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
         <tr class="SmallSearchButton">
            <td>

                <asp:UpdatePanel ID="UpdatePanel3" ChildrenAsTriggers="true" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Btn_submit" runat="server" Text="Submit" Height="25px" OnClick="Btn_submit_Click" style="font-size: small; font-family: Calibri; text-align: center" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>
   

               
              <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
    </table>
</asp:Content>
