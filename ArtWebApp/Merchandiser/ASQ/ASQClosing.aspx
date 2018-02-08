﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ASQClosing.aspx.cs" Inherits="ArtWebApp.Merchandiser.ASQ.ASQClosing" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
   
<%-- 
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script> --%>
  
 <%--   <link href="../../css/style.css" rel="stylesheet" />

    <script src="../../JQuery/GridJQuery.js">
     <script src="../../Scripts/jquery.table2excel.js"></script>--%>
    <script type="text/javascript">

        


     </script>
  
    <style type="text/css">
       body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}

   
      
       
        .smalltable {
            width: 50px;
        }
       
      
       
        .auto-style1 {
            height: 27px;
        }
       
      
       
        .auto-style2 {
            text-align: right;
        }
        .auto-style3 {
            font-size: x-small;
        }
       
      
       
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
     
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate> 
<div class="FullTable">
        <table class="FullTable">
        <tr  class="RedHeadding">
            <td style="color: #FFFFFF; text-align: center; background-color: #990000">asq ALLOCATION</td>
        </tr>
        <tr>
            <td >



                 <table >
                        <tr>
                            <td class="auto-style1" colspan="4">

                                   <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Show All Pending  and Non ShortClosed ASQ" />
                
                            </td>
                            </tr>

                        

                        <tr>
                            <td class="NormalTD">

                                Atc# </td>
                            <td class="NormalTD">
                                 <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                                 </ucc:DropDownListChosen>
                    
                
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="buttonAtc" runat="server" Height="26px" OnClick="buttonAtc_Click" Text="S" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                               
                                <asp:Button ID="btn_showallasq" runat="server" OnClick="btn_showallasq_Click" Text="Show All ASQ" />
                            </td>
                            </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD">
                                 PO Pack
                    
                
                            </td>
                            <td class="NormalTD">

                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <ig:WebDropDown ID="drp_popack" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId" Width="200px">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                <asp:Button ID="buttonAtc0" runat="server" Height="26px" Text="S" OnClick="buttonAtc0_Click" />
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                </table>

                
               
               
            </td>
        </tr>
        
       
        <tr>
            <td class="auto-style2">
                
               <asp:Button ID="Button4" runat="server" Text="Export" OnClick="Button4_Click" />
                
            </td>
        </tr>
    </table>
    </div>
                                        
<div>
        <table class="DataEntryTable">
                    <tr>
                      
                        <td class="auto-style8"><asp:UpdatePanel ID="upd_main" runat="server">
                                    <ContentTemplate>
                                       <%-- <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle" AutoPostBack="True" OnDataBound="cmb_ourstyle_DataBound" OnValueChanged="cmb_ourstyle_ValueChanged">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>--%>

                                        <asp:GridView ID="tbl_podata" 
                                            Width="100%" 
                                            runat="server"
                                             AutoGenerateColumns="False" 
                                         
                                             DataKeyNames="PoPackId,OurStyleID"
                                             style="font-size: small; font-family: Calibri; font-weight: 400;" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="Smaller" OnRowDataBound="tbl_podata_RowDataBound">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>      
                                                   <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                                <asp:TemplateField HeaderText="PoPackId" SortExpression="PoPackId">
                                                  
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OurStyleID"    SortExpression="OurStyleID">
                                                
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_OurStyleID" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PoPacknum" HeaderText="PoPacknum" SortExpression="PoPacknum" />
                                                <asp:BoundField DataField="BuyerPO" HeaderText="BuyerPO" SortExpression="BuyerPO" />
                                                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                                <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                                                <asp:BoundField DataField="POQty" HeaderText="POQty" SortExpression="POQty" ReadOnly="True" />
                                                <asp:BoundField DataField="ShipedQty" HeaderText="ShipedQty" SortExpression="ShipedQty" ReadOnly="True" />
                                                <asp:BoundField DataField="FirstDeliveryDate" HeaderText="FirstDeliveryDate" SortExpression="FirstDeliveryDate" />
                                                   <asp:TemplateField HeaderText="DeliveryDate" SortExpression="DeliveryDate">
                                                     
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_deliveryDate" runat="server" Text='<%# Bind("DeliveryDate") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HandoverDate" SortExpression="HandoverDate">
                                                  
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_handoverdate" runat="server" Text='<%# Bind("HandoverDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Revised HD">
                                                 
                                                    <ItemTemplate>
                                                          <asp:TextBox ID="dtp_deliverydate" runat="server" Font-Size="Smaller" Width="120px"></asp:TextBox>


                                    <ajaxToolkit:CalendarExtender ID="dtp_deliverydate_CalendarExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_deliverydate" >
                                    </ajaxToolkit:CalendarExtender>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                        <td class="NormalTD">
                           
                            <table class="DataEntryTable">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Short Close" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Add New HD" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        
                                                <div id="Messaediv" runat="server">
                                                    <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                                </div>
                                          
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                           
                        </td>
                    
                    </tr>
                   
                    
                </table>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="allPodatasorce" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        PoPackId, PoPacknum, BuyerPO, OurStyle, BuyerStyle, POQty, ShipedQty, OurStyleID, FirstDeliveryDate, DeliveryDate, HandoverDate
FROM            (SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails.PoQty) AS POQty, ISNULL
                             ((SELECT        SUM(ShippedQty) AS Expr1
FROM            ShipmentHandOverDetails
GROUP BY POPackId, OurStyleID
HAVING        (POPackId = PoPackMaster.PoPackId) AND (OurStyleID = POPackDetails.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
                         PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails.IsShortClosed) AS Expr1
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails.OurStyleID, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
                         PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate
HAVING        (PoPackMaster.AtcId = @Param1) AND (MAX(POPackDetails.IsShortClosed) &lt;&gt; N'Y')) AS tt
WHERE        (POQty - ShipedQty &gt; 0)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                </SelectParameters>
        </asp:SqlDataSource>
                    
        <br />
                    
    </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>


           <%--<asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                   <div class="modal">
        <div class="center">
          <img  src="../../Image/loader.gif" style="position: relative; top: 45%;" > </img>
        </div>
    </div>
                                     
                                       
                                        
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
</asp:Content>
