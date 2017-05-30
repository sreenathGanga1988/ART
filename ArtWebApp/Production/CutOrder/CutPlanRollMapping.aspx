<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutPlanRollMapping.aspx.cs" Inherits="ArtWebApp.Production.CutOrder.CutPlanRollMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
  
    <link href="../../css/style.css" rel="stylesheet" />
    

    <script src="../../JQuery/GridJQuery.js"></script>

   <script type="text/javascript">


       function initDropDown(sender, args) {
           sender.behavior.set_zIndex(200000);
       }



       function Onselection(objref) {

           Check_Click(objref)
           calculatesumofyardage();
       }

       function OnSelectAllClick(objref) {


           checkAll(objref)

           calculatesumofyardage();
       }
  
    
 function calculatesumofyardage()
        {
            var gridView = document.getElementById("<%= tbl_rolldata.ClientID %>");
            var sum = 0
            for (var i = 1; i < gridView.rows.length-1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var lbl_yard = gridView.rows[i].getElementsByClassName("lbl_yard")[0];

                    sum = sum + parseFloat(lbl_yard.innerHTML);
                }

            } 
            var totalyardfooter = document.getElementsByClassName("totalyardfooter")[0];
            totalyardfooter.value = sum;
        }



</script>

    <style type="text/css">
 


        .Headernewtable {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    width: 100%;
}

.td, th {
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}

.tr:nth-child(even) {
    background-color: #dddddd;
}
    </style>

 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">add rolls to cutplan</td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upd_main"  UpdateMode="Conditional"  ChildrenAsTriggers="false"       runat="server">
                                <ContentTemplate>

 <table class="tittlebar">
                    <tr>
                        <td class="NormalTD" >atc&nbsp; : </td>
                        <td class="NormalTD" >
                               
                               <asp:UpdatePanel ID="upd_atc"  UpdateMode="Conditional" runat ="server">
                                            <ContentTemplate>
                              <ucc:DropDownListChosen ID="drp_atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                                 </ContentTemplate>
                                        </asp:UpdatePanel>

                        </td>
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_atc" runat="server" Text="S" OnClick="btn_atc_Click" /></ContentTemplate>
                                        </asp:UpdatePanel>  
                        </td>
                        <td class="NormalTD" >ourstyle&nbsp; #</td>
                        <td class="NormalTD" >

                               <asp:UpdatePanel ID="upd_ourstyle" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                     <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                                 </ContentTemplate>
                                        </asp:UpdatePanel>
                        </td>
                        <td class="auto-style7" >
                            <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_OURSTYLE" runat="server" Text="S" OnClick="btn_OURSTYLE_Click" /></ContentTemplate>
                                        </asp:UpdatePanel>  
                        </td>
                        <td class="NormalTD" >
                            </td>
                        <td class="NormalTD" >
                            </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">CutPlan #</td>
                        <td class="NormalTD">
                            
                               <asp:UpdatePanel ID="upd_cutorder" UpdateMode="Conditional"  runat="server">
                                            <ContentTemplate>
                            <ucc:DropDownListChosen ID="drp_cutorder" runat="server"  DataTextField="name" DataValueField="pk" Width="200px">
                        </ucc:DropDownListChosen>
                                                
                                     </ContentTemplate>
                                        </asp:UpdatePanel>            
                                                </td>
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_cutorder" runat="server"  Text="S" OnClick="btn_cutorder_Click" /></ContentTemplate>
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD">&nbsp;cut plan fab req&nbsp;</td>
                        <td class="NormalTD">
                               <asp:UpdatePanel ID="upd_fabreq" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                       <asp:TextBox ID="txt_fabreq" runat="server" >
                                       </asp:TextBox>
                                   </ContentTemplate>
                               </asp:UpdatePanel>
                        </td>
                        <td class="auto-style7"><asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD" colspan="8">
                            
                            

                        </td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD" colspan="7" >

                             
                            <asp:UpdatePanel ID="Upd_roll" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="ModalPanel" runat="server" Visible="false">
                                                <table class="FullTable">
                                                    <tr>
                                                        <td>Availbale Fabric Rolls</td>
                                                    </tr>
                                                    <tr class="DataEntryTable">
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>ShadeGroup</td>
                                                                    <td class="NormalTD">
                                                                        <asp:UpdatePanel ID="upd_shade" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <ig:WebDropDown ID="drp_shade" runat="server" EnableDropDownAsChild="false" EnableMultipleSelection="True" TextField="name" ValueField="pk" Width="200px">
                                                                                    <ClientEvents Initialize="initDropDown" />
                                                                                    <DropDownItemBinding TextField="ShadeGroup" ValueField="ShadeGroup" />
                                                                                </ig:WebDropDown>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="Button2" runat="server" OnClick="Button1_Click1" Text="S" />
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="NormalTD">Shrinkage group</td>
                                                                    <td class="NormalTD"><strong>
                                                                        <asp:Label ID="lbl_shringagegroup" runat="server" Text="0"></asp:Label>
                                                                        </strong></td>
                                                                    <td class="NormalTD">Width group</td>
                                                                    <td class="NormalTD"><strong>
                                                                        <asp:Label ID="lbl_widthgroup" runat="server" Text="0"></asp:Label>
                                                                        </strong></td>
                                                                    <td class="NormalTD">MarkerType</td>
                                                                    <td class="NormalTD"><strong>
                                                                        <asp:Label ID="lbl_markerType" runat="server" Text="0"></asp:Label>
                                                                        </strong></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="tbl_rolldata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Roll_PK" ShowFooter="true" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField >
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="OnSelectAllClick(this)" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk_select" runat="server" onclick="Onselection(this)" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Roll_PK">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Roll_PK" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                                                    <asp:BoundField DataField="ASN" HeaderText="ASN" ReadOnly="True" SortExpression="ASN" />
                                                                    <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                                                                    <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" ReadOnly="True" SortExpression="itemDescription" />
                                                                    <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                                                                    <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                                                                    <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                                                                    <asp:BoundField DataField="SWeight" HeaderText="SWeight" SortExpression="SWeight" />
                                                                    <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                                                                    <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                                                                    <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                                                                    <asp:TemplateField HeaderText="AYard">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_AYard" runat="server" CssClass="lbl_yard" Text='<%# Bind("AYard") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:TextBox ID="txt_totalyard" runat="server" CssClass="totalyardfooter" Width="70px"></asp:TextBox>
                                                                        </FooterTemplate>
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>
                           
                        </td>
                        <td class="NormalTD" >
                          </td>
                    </tr>
                </table>
                                       </ContentTemplate>
                            </asp:UpdatePanel>
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button1" runat="server"   Text="Save  Cut Plan" OnClick="Button1_Click3" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                   <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
                         </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT MarkerNo, NoOfPc, Qty, MarkerLength, LayLength, CutPlanMarkerDetails_PK, CutPlan_PK FROM CutPlanMarkerDetails WHERE (CutPlan_PK = @Cutid)">
                                                     <SelectParameters>
                                                         <asp:ControlParameter ControlID="drp_cutorder" DefaultValue="0" Name="Cutid" PropertyName="SelectedValue" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
               </td>
        </tr>
        
    </table>
    
</asp:Content>

