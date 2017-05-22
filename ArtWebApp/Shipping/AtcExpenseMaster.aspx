<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AtcExpenseMaster.aspx.cs" Inherits="ArtWebApp.Shipping.AtcExpenseMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
   
    <script src="../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td class="NormalTD" colspan="4">atc expense</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Factory</td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_atc" runat="server" DataSourceID="FactorydataSource" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     </td>
                     <td class="auto-style7">
                         <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="S" />
                     </td>
                     <td class="auto-style7"></td>
                 </tr>
      
                 <tr>
                     <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chk_selectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chk_selectAll_CheckedChanged" Text="Select All" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel></td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="auto-style7">&nbsp;</td>
                     <td class="auto-style7">&nbsp;</td>
                 </tr>
      
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>
    <div class="gridtable">

         <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" >
                            <Columns> 
                                <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" />
                                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" />
                                <asp:BoundField DataField="PoPacknum" HeaderText="ASQ" />
                                <asp:BoundField DataField="BuyerPO" HeaderText="BuyerPO" />
                                <asp:TemplateField HeaderText="USD Value">
                                  
                                    <ItemTemplate>
                                       <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("POvalue") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoicenum">
                                    
                                  
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="Invoicedetail" DataTextField="InvoiceNum" DataValueField="Invoice_PK">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="Invoicedetail" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT AtcDetails.AtcId, InvoiceMaster.Invoice_PK, InvoiceMaster.InvoiceNum FROM InvoiceMaster INNER JOIN InvoiceDetail ON InvoiceMaster.Invoice_PK = InvoiceDetail.Invoice_PK INNER JOIN AtcDetails ON InvoiceDetail.OurStyleID = AtcDetails.OurStyleID GROUP BY AtcDetails.AtcId, InvoiceMaster.Invoice_PK, InvoiceMaster.InvoiceNum HAVING (AtcDetails.AtcId = @Param1)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="drp_atc" Name="Param1" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drp_chargeback" runat="server" DataSourceID="ExpenseType" DataTextField="ExpenseName" DataValueField="ExpenseType_PK">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="ExpenseType" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [ExpenseType_PK], [ExpenseName] FROM [ExpenseTypeMaster] ORDER BY [ExpenseName]"></asp:SqlDataSource>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                   
                                    <ItemTemplate>
                                         <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </ItemTemplate>
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
              </ContentTemplate>
     </asp:UpdatePanel>

    </div>


    <div class="DataEntryTable">


        <asp:Button ID="btn_Submit" runat="server" Text="Submit" OnClick="btn_JCSubmit_Click" style="height: 26px" />


    </div>

       <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT AtcId, AtcNum FROM AtcMaster">
                </asp:SqlDataSource>


                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT AtcMaster.AtcNum, AtcDetails.OurStyle, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, 0 AS POvalue, AtcMaster.AtcId FROM PoPackMaster INNER JOIN POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId WHERE (AtcMaster.AtcId = @Param1)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="drp_atc" Name="Param1" PropertyName="SelectedValue" />
                    </SelectParameters>
</asp:SqlDataSource>


                </asp:Content>
