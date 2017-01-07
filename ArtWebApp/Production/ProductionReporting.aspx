<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProductionReporting.aspx.cs" Inherits="ArtWebApp.Production.ProductionReporting" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        
    </style>
    <script src="../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td  colspan="4"> Production reporting</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Factory</td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     <td class="NormalTD">
                         <asp:Button ID="Btn_showJC" runat="server" OnClick="Btn_showJC_Click" Text="S" />
                     </td>
                     <td class="NormalTD"></td>
                 </tr>
        <tr>
            <td class="NormalTD">Job Contract</td>
             <td class="NormalTD">
                 <ig:WebDropDown ID="drp_jobcontract" runat="server" Width="200px" TextField="Name" ValueField="pk" EnableMultipleSelection="True" EnableClosingDropDownOnSelect="False">
                     <DropDownItemBinding TextField="Name" ValueField="pk" />
                 </ig:WebDropDown>
            </td>
             <td class="NormalTD">
                 <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />
            </td>
             <td class="NormalTD"></td>
        </tr>
                 <tr>
                     <td class="NormalTD">
                        Production date

                     </td>
                     <td class="NormalTD"><ig:WebDatePicker ID="productionDate" runat="server">
                         </ig:WebDatePicker>
                        

                     </td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">

                         &nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                 </tr>
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


          


 </div>

     <div class="gridtable">

         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                <asp:TemplateField HeaderText="JCDet_pk">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_JobContractDetail_pk" runat="server" Text='<%# Bind("JobContractDetail_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="JOBContractNUM" HeaderText="JCNUM" />
                <asp:BoundField DataField="POPackNUm" HeaderText="POPackNUm" />
                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" />
                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" />
                <asp:BoundField DataField="POQTY" HeaderText="POQTY" />
                                <asp:BoundField DataField="CutQty" HeaderText="CutQty" />
                                 <asp:TemplateField HeaderText="BalCutQty">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BalCutQty" runat="server" Text='<%# Bind("BalCutQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="NewCutQty">
                                   
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_cutQty"  runat="server" onkeypress="return isNumberKey(event,this)"   Text='<%# Bind("BalCutQty") %>' Height="16px" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:BoundField DataField="SewnQty" HeaderText="SewnQty" />
                                 <asp:TemplateField HeaderText="BalSewnQty">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BalSewnQty" runat="server" Text='<%# Bind("BalSewnQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="NewSewnQty">
                                    
                                    <ItemTemplate>
                                         <asp:TextBox ID="txt_Sewnqty" onkeypress="return isNumberKey(event,this)"   Height="16px" Width="50px" runat="server" Text='<%# Bind("BalSewnQty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:BoundField DataField="WashedQty" HeaderText="WashedQty" />
                                 <asp:TemplateField HeaderText="BalWashedQty">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BalWashedQty" runat="server" Text='<%# Bind("BalWashedQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="NewWashedQty">
                                   
                                    <ItemTemplate>
                                         <asp:TextBox ID="txt_washedqty" onkeypress="return isNumberKey(event,this)"   Height="16px" Width="50px" runat="server" Text='<%# Bind("BalWashedQty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:BoundField DataField="PackedQty" HeaderText="PackedQty" />
                                 <asp:TemplateField HeaderText="BalPackedQty">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_BalPackedQty" runat="server" Text='<%# Bind("BalPackedQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="BalPackedQty">
                                    
                                    <ItemTemplate>
                                       <asp:TextBox ID="txt_packedqty" onkeypress="return isNumberKey(event,this)"   Height="16px" Width="50px" runat="server" Text='<%# Bind("BalPackedQty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                   
                                </asp:TemplateField>
                                <asp:BoundField DataField="ShippedQty" HeaderText="ShippedQty" />
                                
                                <asp:BoundField DataField="BalQty" HeaderText="BalQty" />
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

    <div>



        <asp:Button ID="btn_submitShipment" runat="server" Text="Submit" OnClick="btn_submitShipment_Click" />



    </div>

         <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
</asp:Content>
