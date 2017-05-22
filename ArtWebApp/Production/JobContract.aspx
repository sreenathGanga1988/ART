<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="JobContract.aspx.cs" Inherits="ArtWebApp.Production.JobContract" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig"  %>
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
            <td colspan="4">JOB Contract CM</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Factory</td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     <td class="SearchButtonTD"></td>
                     <td class="NormalTD"></td>
                 </tr>
        <tr>
            <td class="NormalTD">atc</td>
             <td class="NormalTD" >
                 <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                 </ucc:DropDownListChosen>
            </td>
             <td class="SearchButtonTD">
                 <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="S" />
            </td>
             <td class="NormalTDauto-style7"></td>
        </tr>
        <tr>
            <td class="NormalTD">Buyer PO/.ASQ</td>
              <td class="NormalTD">
                  <ig:WebDropDown ID="drp_popack" runat="server" Width="200px" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId">
                      <DropDownItemBinding TextField="POnum" ValueField="PoPackId" />
                  </ig:WebDropDown>
            </td>
              <td class="SearchButtonTD">
                  <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="S" />
            </td>
              <td>&nbsp;</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Remark</td>
                     <td class="NormalTD">
                         <asp:TextBox ID="txt_remark" runat="server" Height="45px" TextMode="MultiLine" Width="184px"></asp:TextBox>
                     </td>
                     <td class="SearchButtonTD">&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
        <tr>
            <td class="auto-style8">
                </td>
        </tr>
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>
    <div class="gridtable">

         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" OnRowDataBound="tbl_podetails_RowDataBound">
                            <Columns>      
                   <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                <asp:BoundField DataField="POnum" HeaderText="POnum" />
                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" />
                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" />
                <asp:BoundField DataField="POQTY" HeaderText="POQTY" />
                                <asp:TemplateField HeaderText="Apprcm">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_apprcm" runat="server"  onkeypress="return isNumberKey(event,this)"  Text='<%# Bind("Apprcm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CM">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_cm"  onkeypress="return isNumberKey(event,this)" Width="70px"  Text='<%# Bind("Apprcm") %>' runat="server" Font-Size="Smaller"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JobContract" SortExpression="JobContract">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_jc" runat="server" Text='<%# Bind("JobContract") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PoPackId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OurStyleID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_OurStyleID" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
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


        <asp:Button ID="btn_JCSubmit" runat="server" Text="Submit" OnClick="btn_JCSubmit_Click" style="height: 26px" />


    </div>

       <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [AtcNum], [AtcId] FROM [AtcMaster]"></asp:SqlDataSource>
            
</asp:Content>
