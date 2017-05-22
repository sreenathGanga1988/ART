<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProcurmentPlaning.aspx.cs" Inherits="ArtWebApp.Merchandiser.Atc_Chart.ProcurmentPlaning" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
    <script>

        
        function Onselection(objref) {
            Check_Click(objref)
           // calculatesumofyardage();
        }

        function OnSelectAllClick(objref) {
            checkAll(objref)
         //   calculatesumofyardage();
        }
          function CopyRemark()
        {
            var gridView = document.getElementById("<%= tbl_bom.ClientID %>");
              var txt_remark = document.getElementsByClassName("txt_remark")[0];

            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_remark1 = gridView.rows[i].getElementsByClassName("txt_remark1")[0];

                    txt_remark1.value=txt_remark.value ;
                }
            }
          }

          function CopyDate()
        {
            var gridView = document.getElementById("<%= tbl_bom.ClientID %>");
              var dtp_deliverydateall = document.getElementsByClassName("dtp_deliverydateall")[0];

            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var dtpdeliverydate = gridView.rows[i].getElementsByClassName("dtpdeliverydate")[0];

                    dtpdeliverydate.value = dtp_deliverydateall.value;
                }
            }
          }



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

    .fillfull {
        width: 100%;
    }

    .auto-style8 {
        width: 120px;
    }

    .auto-style9 {
        height: 27px;
    }

    </style>
    
      

   
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="FullTable" style="font-family: Calibri">
                <tr class="RedHeadding">
                 <td style="color: #FFFFFF; text-align: center; background-color: #990000">ATC procurement plan</td>
                </tr>
                <tr>
                    <td>
                        
                        <table class="DataEntryTable">
                            <tr>
                                <td class="NormalTD" >ATC # : </td>
                                <td class="NormalTD" >
                             
                                    <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" Height="17px" Width="200px">
                                    </ucc:DropDownListChosen>
                    
               
                               
                                </td>
                                <td class="SearchButtonTD">
                                    
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="ShowBom" runat="server"  Text="S" OnClick="ShowBom_Click"  Height="23px" Width="34px"  />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                   
                                </td>
                                <td class="NormalTD" >
                                    Qty</td>
                                <td class="NormalTD" >QTY :</td>
                                <td class="NormalTD" ></td>
                            </tr>
                            <tr>
                                <td class="NormalTD"  >RMNUM</td>
                                <td  class="NormalTD" >
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <ig:WebDropDown ID="drp_rmnum" runat="server" BorderStyle="None" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="RMNum" ValueField="Sku_pk" Width="200px">
                                                <DropDownItemBinding TextField="RMNum" ValueField="Sku_pk" />
                                            </ig:WebDropDown>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD" >
                                   
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="ShowRawmaterialBOM" runat="server" Height="23px" OnClick="ShowRawmaterialBOM_Click" Text="S" Width="34px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD" >
                                   
                                    pcd</td>
                                <td class="auto-style9" ></td>
                                <td class="auto-style9" ></td>
                            </tr>
                            <tr>
                                <td colspan="2" >
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Export to Excel" />
                             &nbsp; <asp:TextBox ID="txt_remark" CssClass="txt_remark" runat="server" placeholder="Enter Remark" Width="99px" Font-Size="Smaller"></asp:TextBox>
                                    <asp:Button ID="btn_remark" runat="server"  OnClientClick="CopyRemark()" Font-Bold="True" Font-Size="X-Small" Text="Apply to all" Width="92px" />
                             </td>
                                 <td class="SearchButtonTD" ></td>
                                <td class="NormalTD" ></td>
                                <td >
                                    <asp:TextBox ID="dtp_deliverydateall"  CssClass="dtp_deliverydateall" placeholder="Enter ETA" runat="server" Font-Size="Smaller" Width="120px"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender0" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_deliverydateAll">

                                    </asp:CalendarExtender>
                                   <asp:Button ID="btn_remark0" runat="server" Font-Bold="True" Font-Size="X-Small" OnClientClick="CopyDate()" Text="Apply to all" Width="92px" />

                                </td>
                                <td >
                                 
                                </td>
                            </tr>
                        </table>

                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <div id="grid"style="overflow:auto" >
                            <asp:UpdatePanel ID="Upd_maingrid" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                    <ContentTemplate>
                       
                        <asp:GridView ID="tbl_bom" runat="server" AutoGenerateColumns="False"
                             BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="1033px" 
                            Font-Size="Large" DataKeyNames="SkuDet_PK" OnDataBound="tbl_bom_DataBound1" OnRowDataBound="tbl_bom_RowDataBound" OnRowCommand="tbl_bom_RowCommand" >
                            <Columns>                               
                                 <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:BoundField DataField="RMNum" HeaderText="RMNum" SortExpression="RMNum" />
                                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                               
                                <asp:BoundField DataField="ColorName" HeaderText="Color Name" SortExpression="ColorName" />
                                  <asp:BoundField DataField="SizeName" HeaderText="Size Name" SortExpression="SizeName" />
                                <asp:BoundField DataField="ItemColor" HeaderText="Item Color" SortExpression="ItemColor" />
                                <asp:BoundField DataField="ItemSize" HeaderText="Item Size" SortExpression="ItemSize" />
                                <asp:BoundField DataField="UnitRate" HeaderText="Unit Rate" ReadOnly="True" SortExpression="UnitRate" />
                                <asp:BoundField DataField="GarmentQty" HeaderText="Garment Qty" ReadOnly="True" SortExpression="GarmentQty" />
                                <asp:BoundField DataField="Consumption" HeaderText="Consumption" ReadOnly="True" SortExpression="Consumption" />
                                <asp:BoundField DataField="WastagePercentage" HeaderText="Wastage %" SortExpression="WastagePercentage" />
                                 <asp:TemplateField HeaderText="RqdQty" SortExpression="RqdQty">
                                  
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_reqdqty" runat="server" Text='<%# Bind("RqdQty") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:BoundField DataField="UomCode" HeaderText="Uom" SortExpression="UomCode" />
                                <asp:TemplateField HeaderText="Po Issued Qty" SortExpression="PoIssuedQty">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_poissuedqty" runat="server" Text='<%# Bind("PoIssuedQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BalanceQty" HeaderText="Balance Qty" ReadOnly="True" SortExpression="BalanceQty" />
                          


                                 <asp:TemplateField HeaderText="Planned Qty" SortExpression="PlannedQty">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_plannedqty" runat="server" ></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>
                          
                                 <asp:TemplateField HeaderText="balance To Plan" SortExpression="BalanceQty">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_balplanqty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>
                          

                                 <asp:TemplateField HeaderText="New Plan" SortExpression="BalanceQty">
                                     <ItemTemplate>
                                         <table id="fillfull" class="hidden" runat="server"  >
                                             <tr>


                                                 <td>Qty</td>
                                                 <td class="auto-style8">ETA</td>
                                                
                                             </tr>
                                             <tr>
                                                 <td><asp:TextBox ID="txt_qty" CssClass="txtqty"   Width="50px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" Font-Size="Smaller" ></asp:TextBox>
                               </td>
                                                 <td >
                                                <%--     <ig:WebDatePicker ID="wdp_etadate" runat="server" Font-Size="Smaller" Height="16px" Width="120px">
                                                     </ig:WebDatePicker>--%>

                                                     <asp:TextBox ID="dtp_deliverydate" CssClass="dtpdeliverydate" runat="server" Font-Size="Smaller" Width="120px"></asp:TextBox>


                                    <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_deliverydate" >
                                    </asp:CalendarExtender>

                                                 </td>
                                                 <td>
                                                      <asp:LinkButton ID="btn_eta" runat="server" CausesValidation="false" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddETA" Text="ADD"></asp:LinkButton>
                                          
                                                 </td>
                                                
                                             </tr>
                                         </table>
                                     </ItemTemplate>
                                </asp:TemplateField>
                          

                                 <asp:TemplateField HeaderText="Planned Details" SortExpression="BalanceQty">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_eta" runat="server" AutoGenerateColumns="False">
                                             <Columns>
                                                 <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                 <asp:BoundField DataField="ETADate" HeaderText="ETA" DataFormatString="{0:MM/dd/yyyy}"  />
                                             </Columns>
                                             <HeaderStyle BackColor="#FF9933" Font-Names="Calibri" Font-Size="Smaller" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                           
                           



                              
                              
                           
                       
                                 <asp:TemplateField HeaderText="Remark">
                                     
                                     <ItemTemplate>
                                     
                                       <asp:GridView ID="tbl_Remark" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" 
                                             BorderColor="#DEBA84" DataKeyNames="PlanRemark_PK" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="PlanRemark_PK" HeaderText="PlanRemark_PK" />
                                                 <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                <asp:BoundField DataField="AddedDate" HeaderText="AddedDate"  DataFormatString="{0:MM/dd/yyyy}"  />
                                                 <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" />
                                                 
                                          
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="Black" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="Black" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                         </ItemTemplate>
                                 </asp:TemplateField>


                                

                              
                              
                           
                       
                               


                                

                              
                              
                           
                       
                                 <asp:TemplateField HeaderText="Remark">
                                   
                                     <ItemTemplate>
                                         
                                         <table class="fillfull">
                                             <tr>
                                                 <td><asp:TextBox ID="txt_remark" CssClass="txt_remark1"  runat="server" Text="na" Font-Size="Smaller"></asp:TextBox></td>
                                                 <td>
                                                     <asp:LinkButton ID="btn_remark" runat="server" CausesValidation="false" CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddRemark" Text="ADD"></asp:LinkButton>
                                                 </td>
                                             </tr>
                                         </table>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                 <asp:TemplateField HeaderText="SkuDet_PK" InsertVisible="False" SortExpression="SkuDet_PK">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_skudetpk" runat="server" Text='<%# Bind("SkuDet_PK") %>'></asp:Label>
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
                    </td>
                </tr>
                <tr>
                    <td class="NormalTD">
                
                    
               
                               
                            
               
                               
                    
               
                               
                            
               
                               
                                
                    
               
                               
                            
               
                               
                    
               
                               
                            
               
                            
                    
               
                               
                            
               
                               
                    
               
                               
                            
               
                               
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btn_savePlan" runat="server"  OnClick="btn_savePlan_Click" Text="Save Procurement Plan" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                
                    
               
                               
                            
               
                               
                    
               
                               
                            
               
                               
                                
                    
               
                               
                            
               
                               
                    
               
                               
                            
               
                            
                    
               
                               
                            
               
                               
                    
               
                               
                            
               
                               
                                </td>
                </tr>
                <tr>
                    <td class="NormalTD">
                       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
               
                               
                            
               
                               
                    
               
                               
                            
               
                            
                    
               
                               
                            
               
                               
                    
               
                               
                            
               
                               
                                </td>
                </tr>
            </table>
</asp:Content>


