<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ASQMonthLocking.aspx.cs" Inherits="ArtWebApp.Production.Schedular.ASQMonthLocking" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
    
 
  

        


   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate> 
<div class="FullTable">
        <table class="FullTable">
        <tr  class="RedHeadding">
            <td style="color: #FFFFFF; text-align: center; background-color: #990000">asq Month closing - target</td>
        </tr>
        <tr>
            <td >



                 <table >
                        <tr>
                            <td class="auto-style1" colspan="4">

                                   &nbsp;</td>
                            <td class="NormalTD">&nbsp;</td>
                            </tr>

                        

                        <tr>
                            <td class="NormalTD">

                                Year</td>
                            <td class="NormalTD">
                                 <ucc:DropDownListChosen ID="cmb_year" runat="server" DisableSearchThreshold="10" Width="200px">
                                     <asp:ListItem>2017</asp:ListItem>
                                     <asp:ListItem>2018</asp:ListItem>
                                     <asp:ListItem>2019</asp:ListItem>
                                     <asp:ListItem>2020</asp:ListItem>
                                 </ucc:DropDownListChosen>
                    
                
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            <td  class="NormalTD">&nbsp;</td>
                            </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD">Month</td>
                            <td class="NormalTD">
                                <ucc:DropDownListChosen ID="cmb_Month" runat="server" DisableSearchThreshold="10" Width="200px">
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                      <asp:ListItem Value="11">Novemeber</asp:ListItem>
                                     <asp:ListItem Value="12">December</asp:ListItem>
                                </ucc:DropDownListChosen>
                            </td>
                            <td class="SearchButtonTD">
                                <asp:Button ID="S" runat="server" OnClick="Button3_Click1" Text="S" />
                              
                            </td>
                            <td>&nbsp;</td>
                            <td class="NormalTD">&nbsp;</td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD">From </td>
                            <td class="NormalTD">
                                <asp:Label ID="lbl_fromdate" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="SearchButtonTD">To</td>
                            <td>
                                <asp:Label ID="lbl_todate" runat="server" Text="0"></asp:Label>
                            </td>
                            <td  class="NormalTD">&nbsp;</td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD">Target locked</td>
                            <td class="NormalTD">
                                <asp:Label ID="lbl_Targetlocked" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="SearchButtonTD">Shipment Closed</td>
                            <td>
                                <asp:Label ID="lbl_shipmentclosed" runat="server" Text="0"></asp:Label>
                            </td>
                            <td  class="NormalTD">&nbsp;</td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="auto-style1" colspan="3">No of ASQ Pending to transfer/Shortclose</td>
                            <td>
                                <asp:Label ID="lbl_pendingASQ" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="NormalTD">&nbsp;</td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                </table>

                
               
               
            </td>
        </tr>
        
       
        <tr>
            <td>
                
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


     
       <asp:GridView ID="tbl_podata"   runat="server"    AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="Smaller"     Width="100%"   style="font-size: small; font-family: Calibri;  font-weight: 400;"    ShowFooter="True"   DataKeyNames="PoPackId"    OnRowDataBound="tbl_podata_RowDataBound">
                                          
                                           
                                       
                                                                                
                                           
                                      
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>         
                                                <asp:TemplateField HeaderText="PoPackId" InsertVisible="False" SortExpression="PoPackId">
                                                
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                                <asp:BoundField DataField="BuyerPO" HeaderText="BuyerPO" SortExpression="BuyerPO" />
                                                <asp:BoundField DataField="PoPacknum" HeaderText="PoPacknum" SortExpression="PoPacknum" />
                                                <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" SortExpression="DeliveryDate" />
                                                <asp:TemplateField HeaderText="PoQty" SortExpression="PoQty">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_qty" runat="server" Text='<%# Bind("PoQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                         <FooterTemplate>
                                <asp:Label runat="server" ID="lblTotalValue" ></asp:Label>
                            </FooterTemplate>
                                                </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ShippedQty" SortExpression="ShippedQty">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ShipedQtyqty" runat="server" Text='<%# Bind("ShipedQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                         <FooterTemplate>
                                <asp:Label runat="server" ID="lblTotalValueship" ></asp:Label>
                            </FooterTemplate>
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Balance" SortExpression="Balance">
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Balance" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                         <FooterTemplate>
                                <asp:Label runat="server" ID="lbl_baltoshipBalance" ></asp:Label>
                            </FooterTemplate>
                                                </asp:TemplateField>
                                   
                                                <asp:TemplateField HeaderText="OurStyleID" SortExpression="OurStyleID">
                                               
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LocationPK" SortExpression="LocationPK">
                                              
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_locationpk" runat="server" Text='<%# Bind("LocationPK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Expr1" HeaderText="SC" SortExpression="Expr1" ReadOnly="True" />
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

                                        <asp:SqlDataSource ID="SqlDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        PoPackMaster.PoPackId, AtcMaster.AtcNum, PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.DeliveryDate, SUM(POPackDetails.PoQty) AS PoQty, POPackDetails.OurStyleID, 
                        isnull( PoPackMaster.ExpectedLocation_PK,0) as LocationPK, MAX(POPackDetails.IsShortClosed) AS Expr1
FROM            AtcMaster INNER JOIN
                         PoPackMaster ON AtcMaster.AtcId = PoPackMaster.AtcId INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId
GROUP BY AtcMaster.AtcNum, PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.DeliveryDate, POPackDetails.OurStyleID, PoPackMaster.ExpectedLocation_PK
HAVING        (PoPackMaster.DeliveryDate BETWEEN @param1 AND @param2) AND (MAX(POPackDetails.IsShortClosed) = N'N')">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="lbl_fromdate" Name="param1" PropertyName="Text" />
                                                <asp:ControlParameter ControlID="lbl_todate" Name="param2" PropertyName="Text" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>

                                    </ContentTemplate>
                                </asp:UpdatePanel></td>
                        
                    </tr>
                   
                    
                    <tr>
                        <td class="NormalTD">
                           
                            <table class="DataEntryTable">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Lock ASQ" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
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