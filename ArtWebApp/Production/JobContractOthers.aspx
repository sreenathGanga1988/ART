<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="JobContractOthers.aspx.cs" Inherits="ArtWebApp.Production.JobContractOthers" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
  <style>

    </style>
    <script src="../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td  colspan="4">JOB Contract Others</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Factory</td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     <td class="NormalTD"></td>
                     <td class="NormalTD"></td>
                 </tr>
        <tr>
            <td class="NormalTD">atc</td>
             <td class="NormalTD" >
                  <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                 </ucc:DropDownListChosen>
            </td>
             <td class="NormalTD">
                 <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="S" />
            </td>
             <td class="NormalTD"></td>
        </tr>
        <tr>
            <td class="NormalTD">Buyer PO/.ASQ</td>
              <td class="NormalTD">
                  <ig:WebDropDown ID="drp_popack" runat="server" Width="200px" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId">
                      <DropDownItemBinding TextField="POnum" ValueField="PoPackId" />
                  </ig:WebDropDown>
            </td>
              <td class="NormalTD">
                  <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="S" />
            </td>
              <td class="NormalTD"></td>
        </tr>
        <tr>
            <td>
                oPTIONAL cOMPONENTS</td>
            <td>
                 <asp:UpdatePanel ID="udp_optionalcombo"  UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <ig:WebDropDown ID="drp_optionalcomb" runat="server" Width="200px" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="ComponentName" ValueField="CostComp_PK">
                                                <DropDownItemBinding TextField="ComponentName" ValueField="CostComp_PK" />
                                            </ig:WebDropDown>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
            </td>
            <td>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="S" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>
    <div class="gridtable">

         <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large">
                            <Columns>      
              <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="PoPackId">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                <asp:BoundField DataField="POnum" HeaderText="POnum" />
                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" />
                                <asp:TemplateField HeaderText="OurStyleID">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_OurStyleID"   runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" />
                <asp:BoundField DataField="POQTY" HeaderText="POQTY" />
                                <asp:TemplateField HeaderText="Apprcm">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_apprcm" runat="server" Text='<%# Bind("Apprcm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Washing">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_washing" Enabled="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="DryProcess">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_dryprocess" Enabled="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Emb/Printing">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_emb" Width="70px" Enabled="false"  Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                         <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Factory Logistic">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_factorylogistic"  Enabled="false" Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                         <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Company Logistic">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_cmplogistic" Enabled="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Fab Comission">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_fabcomission" Enabled="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Garment Comission">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_garcomission" Enabled="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                        <HeaderStyle Width="70px" />
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

          <asp:UpdatePanel ID="upd_btn" UpdateMode="Conditional" runat="server">
         <ContentTemplate>
        <asp:Button ID="btn_JCSubmit" runat="server" Text="Submit" OnClick="btn_JCSubmit_Click" style="height: 26px" />
                </ContentTemplate>
     </asp:UpdatePanel>

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
            
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
            
</asp:Content>
