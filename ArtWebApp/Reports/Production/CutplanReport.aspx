<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutplanReport.aspx.cs" Inherits="ArtWebApp.Reports.Production.CutplanReport" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
  
    <link href="../../css/style.css" rel="stylesheet" />
    
    <script src="../../JQuery/GridJQuery.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

   <script type="text/javascript">

       //calculate the sum of qty on keypress
       //function sumofQty(objText) {
       
       // //   alert(objText.value);
       //    var cell = objText.parentNode;
       //    var row = cell.parentNode;

       //    var sum = 0;
       //    var textboxs = row.getElementsByClassName("txtCalQty");

       //    for (var i = 0; i < textboxs.length; i++)
       //    {
       //        sum += parseFloat(textboxs[i].value);
       //    }



       //    var textboxtotalqtys = row.getElementsByClassName("totalQtyRow");

       //    textboxtotalqtys[0].value = sum.toString();
         

       //}

       //// calculate the sum of ratio
       //function sumofRatio(objText) {
              
       //    //   alert(objText.value);
       //    var cell = objText.parentNode;
           
       //    var row = cell.parentNode;

          

       //    var sum = 0;
       //    var textboxs = row.getElementsByClassName("txtCalRatio");

       //    for (var i = 0; i < textboxs.length; i++) {
       //        sum += parseFloat(textboxs[i].value);
       //    }



       //    var textboxtotalqtys = row.getElementsByClassName("totalRatioRow");

       //    textboxtotalqtys[0].value = sum.toString();
       //    // textboxtotalqtys.inn = sum;
       //    var grdrow = row.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
       //    var element = grdrow.getElementsByClassName("num");
            
       //  var totalqty = parseInt(element[0].value.toString());


       //  SplitQty(element[0]);
       //}


       ////split the  size qty when size change
       //function SplitQty(objText) {
            
            
       //    try {

       //        var grdrow = objText.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;


       //        var sum = 0;
       //        var textboxtotalqtys = grdrow.getElementsByClassName("totalRatioRow");



       //        var totalqty = parseInt(objText.value.toString());

       //        var totalpc = parseInt(textboxtotalqtys[0].value.toString());

       //        var z = totalqty % totalpc;


       //        if (z > 0) {

       //        }
       //        else {

       //            var textqtys = grdrow.getElementsByClassName("txtCalQty");
       //            var textratio = grdrow.getElementsByClassName("txtCalRatio");
       //            var qtysum = 0;
       //            var ratiosum = 0;
       //            for (var i = 0; i < textqtys.length; i++) {
       //                var z = (totalqty / totalpc) * parseInt(textratio[i].value.toString());
       //                textqtys[i].value = z.toString();
       //                qtysum += textqtys[i].value;
       //                ratiosum += textratio[i].value;
                      
       //            }
       //            var textboxtotalqtys = grdrow.getElementsByClassName("totalRatioRow");

       //            textboxtotalqtys[0].value = qtysum.toString();

       //            var textboxtotalrat = grdrow.getElementsByClassName("totalRatioRow");

       //            textboxtotalrat[0].value = ratiosum.toString();



       //            var qty = parseInt(objText.value.toString());
       //            var totalqty = parseInt(objText.value.toString());

       //        }
       //    }
       //    catch (e) {

       //    }
        
       //    calaculateall(objText)
       //}



       //function calaculateall(objText)
       //{
       //    var grdrow = objText.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
       //    var textqtys = grdrow.getElementsByClassName("txtCalQty");
       // //   debugger;


       //    var sum = 0;
       //    var textboxs = grdrow.getElementsByClassName("txtCalRatio");

       //    for (var i = 0; i < textboxs.length; i++) {
       //        sum += parseFloat(textboxs[i].value);
       //    }
       //    var textboxtotalratio = grdrow.getElementsByClassName("totalRatioRow");
       //    textboxtotalratio[0].value = sum.toString();


       //    var qtysum = 0;
       //    var textboxsqty = grdrow.getElementsByClassName("txtCalQty");

       //    for (var i = 0; i < textboxsqty.length; i++) {
       //        qtysum += parseFloat(textboxsqty[i].value);
       //    }
       //    var textboxtotalqtys = grdrow.getElementsByClassName("totalQtyRow");
       //    textboxtotalqtys[0].value = qtysum.toString();

       //}

    



</script>



 
    </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">cut order size details</td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="upd_main"  UpdateMode="Conditional"  ChildrenAsTriggers="false"       runat="server">
                                <ContentTemplate>

 <table class="tittlebar">
                    
                    <tr>
                        <td class="NormalTD">Cutorder #</td>
                        <td class="NormalTD">
                            
                               <asp:UpdatePanel ID="upd_cutorder" UpdateMode="Conditional"  runat="server">
                                            <ContentTemplate>
                            <ucc:DropDownListChosen ID="drp_cutplan" runat="server"  DataTextField="CutPlanNUM" DataValueField="CutPlan_PK" Width="200px" DataSourceID="cutplandatasource" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                
                                     </ContentTemplate>
                                        </asp:UpdatePanel>            
                                                </td>
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_cutorder" runat="server"  Text="S" OnClick="btn_cutorder_Click" /></ContentTemplate>
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD">
                            <asp:SqlDataSource ID="cutplandatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutPlan_PK], [CutPlanNUM] FROM [CutPlanMaster] WHERE (([IsApproved] = @IsApproved) AND ([IsRatioAdded] = @IsRatioAdded))">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="N" Name="IsApproved" Type="String" />
                                    <asp:Parameter DefaultValue="Y" Name="IsRatioAdded" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                 
              
               
                    <tr>
                        <td class="NormalTD" colspan="7" >

                             <asp:UpdatePanel ID="Upd_cutplandetails" runat="server" UpdateMode="Conditional">
                                 <ContentTemplate>
                                     <table width="100%">
                                         <tr>
                                             <td class="NormalTD">Atc</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_atc" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">ourstyle</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_ourstyle" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Location</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_loc" runat="server" Text="0"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="NormalTD">Cutable Width</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_with" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">Shrinkage</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_shrink" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">marker Type</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_Markertype" runat="server" Text="0"></asp:Label>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="NormalTD">bom Consumption</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_bomconsumption" runat="server" CssClass="lbl_newConsumption" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">fabric</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_fabric" runat="server" Font-Size="X-Small" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">&nbsp;</td>
                                             <td class="NormalTD">&nbsp;</td>
                                         </tr>
                                         <tr>
                                             <td class="NormalTD">Fabrication</td>
                                             <td class="NormalTD">
                                                 <asp:Label ID="lbl_fabrication" runat="server" Text="0"></asp:Label>
                                             </td>
                                             <td class="NormalTD">&nbsp;</td>
                                             <td class="NormalTD">&nbsp;</td>
                                             <td class="NormalTD">&nbsp;</td>
                                             <td class="NormalTD">&nbsp;</td>
                                         </tr>
                                     </table>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD" >
                             <asp:UpdatePanel ID="upd_markertype" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                            <asp:GridView ID="tbl_markertype" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="CutPlanmarkerType" HeaderText="Marker Direction" />
                                </Columns>
                            </asp:GridView>
                                    </ContentTemplate>
                                 </asp:UpdatePanel>
                            </td>
                    </tr>
                    <tr>
                        <td class="NormalTD" colspan="8">
                            
                                <asp:UpdatePanel ID="upd_mastertable" runat="server">
                                       <ContentTemplate>
                                           <asp:Panel ID="masterpanel" runat="server" ViewStateMode="Enabled">
                                               <asp:Table ID="Mastertable" runat="server" ViewStateMode="Enabled" Width="400px">
                                               </asp:Table>
                                           </asp:Panel>
                                       </ContentTemplate>
                                   </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td class="RedHeadding" colspan="8">Marker Details</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" colspan="7">
                            <asp:UpdatePanel ID="upd_cutplanmarkergrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <br />
                                    <asp:GridView ID="tbl_cutplanmarkerdata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CutPlanMarkerDetails_PK" Enabled="False" OnDataBound="tbl_cutorderdata_DataBound" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;">
                                        <Columns>
                                            <asp:TemplateField HeaderText="PK" InsertVisible="False" SortExpression="CutPlanMarkerDetails_PK">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_CutOrderDet_PK" runat="server" Text='<%# Bind("CutPlanMarkerDetails_PK") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MarkerNo" SortExpression="MarkerNo">
                                                <ItemTemplate>
                                                    <table class="tittlebar" style=" width: inherit; border-style: solid; background-color: #FFFFFF">
                                                        <tr>
                                                            <td>Marker Num</td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("MarkerNo") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td>NoOfPlies</td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("NoOfPlies") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td>Max Plies</td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("CutPerPlies") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td> No of Cut req</td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Cutreq") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td> No of Cut req</td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Cutreq") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ControlStyle Width="200px" />
                                                <FooterStyle Width="200px" />
                                                <HeaderStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MarkerDetails" SortExpression="MarkerDetails">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                                                <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled" Width="400px">
                                                                </asp:Table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                                <ControlStyle Width="300px" />
                                                <FooterStyle Width="300px" />
                                                <HeaderStyle Width="300px" />
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
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                </table>
                                       </ContentTemplate>
                            </asp:UpdatePanel>
               
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updASQgrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="6">asq Details </td>
                                <tr>
                                    <%----%>
                                    <td colspan="6">
                                        <asp:GridView ID="tbl_ASQdata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PoPackId"  ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" OnDataBound="tbl_ASQdata_DataBound" Enabled="False">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                
                                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="IDS" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                            <tr>
                                                                <td>POPAckid</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Outstyleid</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>CutPlan_PK</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_CutPlan_PK" runat="server" Text='<%# Bind("CutPlan_PK") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="hidden" />
                                                    <ItemStyle CssClass="hidden" />
                                                    <ControlStyle Width="200px" />
                                                    <FooterStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PO Details">
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                            <tr>
                                                                <td>ASQ</td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td>BuyerPO</td>
                                                                <td>
                                                                    <asp:Label ID="na1" runat="server" Text='<%# Bind("BuyerPO") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>BuyerStyle</td>
                                                                <td>
                                                                    <asp:Label ID="na2" runat="server" Text='<%# Bind("BuyerStyle") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>OurStyle</td>
                                                                <td>
                                                                    <asp:Label ID="na3" runat="server" Text='<%# Bind("OurStyle") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="200px" />
                                                    <FooterStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Color Size Details" SortExpression="Details">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="upd_table" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="panel2" runat="server" ViewStateMode="Enabled">
                                                                    <asp:Table ID="Table2" runat="server" ViewStateMode="Enabled" Width="400px">
                                                                    </asp:Table>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">Ref paattern if any </td>
                                    <td class="NormalTD">
                                        <asp:TextBox ID="txt_refpattern" runat="server" Height="144px" Width="198px"></asp:TextBox>
                                    </td>
                                    <td class="NormalTD">
                                        &nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD"></td>
                                    <td class="NormalTD"></td>
                                </tr>
                            </tr>
                            <tr>
                                <td class="auto-style8" colspan="6">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8" colspan="6">
                                    <asp:UpdatePanel ID="upd_Messaediv1" runat="server">
                                        <ContentTemplate>
                                            <div id="Messaediv1" runat="server">
                                                
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Approve Cut Plan " />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
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
    <asp:SqlDataSource ID="cutplanmarkerdetails" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CutPlanMarkerDetails_PK, CutPlan_PK, MarkerNo, NoOfPc, Qty, MarkerLength, LayLength, NoOfPlies, CutPerPlies, Cutreq
FROM            CutPlanMarkerDetails
WHERE        (CutPlan_PK = @CutPlan_PK) ">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drp_cutplan" DefaultValue="" Name="CutPlan_PK" PropertyName="SelectedValue" Type="Decimal" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
    <asp:SqlDataSource ID="cutplanmarkertypedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutPlanMarkerTypes_PK], [CutPlanmarkerType] FROM [CutPlanMarkerType] WHERE ([CutPlan_PK] = @CutPlan_PK)">
        <SelectParameters>
            <asp:ControlParameter ControlID="drp_cutplan" Name="CutPlan_PK" PropertyName="SelectedValue" Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

