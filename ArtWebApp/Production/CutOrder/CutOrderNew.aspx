<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutOrderNew.aspx.cs" Inherits="ArtWebApp.Production.CutOrder.CutOrderNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="../../JQuery/GridJQuery.js"></script>

<script type="text/javascript">

       //calculate the sum of qty on keypress
       function sumofQty(objText) {
       
        //   alert(objText.value);
           var cell = objText.parentNode;
           var row = cell.parentNode;

           var sum = 0;
           var textboxs = row.getElementsByClassName("txtCalQty");

           for (var i = 0; i < textboxs.length; i++)
           {
               sum += parseFloat(textboxs[i].value);
           }



           var textboxtotalqtys = row.getElementsByClassName("totalQtyRow");

           textboxtotalqtys[0].value = sum.toString();
         

       }

       // calculate the sum of ratio
       function sumofRatio(objText) {
              
           //   alert(objText.value);
           var cell = objText.parentNode;
           
           var row = cell.parentNode;

          

           var sum = 0;
           var textboxs = row.getElementsByClassName("txtCalRatio");

           for (var i = 0; i < textboxs.length; i++) {
               sum += parseFloat(textboxs[i].value);
           }



           var textboxtotalqtys = row.getElementsByClassName("totalRatioRow");

           textboxtotalqtys[0].value = sum.toString();
           // textboxtotalqtys.inn = sum;
           var grdrow = row.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
           var element = grdrow.getElementsByClassName("num");
            
         var totalqty = parseInt(element[0].value.toString());


         SplitQty(element[0]);
       }


       //split the  size qty when size change
       function SplitQty(objText) {
            
            
           try {

               var grdrow = objText.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;


               var sum = 0;
               var textboxtotalqtys = grdrow.getElementsByClassName("totalRatioRow");



               var totalqty = parseInt(objText.value.toString());

               var totalpc = parseInt(textboxtotalqtys[0].value.toString());

               var z = totalqty % totalpc;


               if (z > 0) {

               }
               else {

                   var textqtys = grdrow.getElementsByClassName("txtCalQty");
                   var textratio = grdrow.getElementsByClassName("txtCalRatio");
                   var qtysum = 0;
                   var ratiosum = 0;
                   for (var i = 0; i < textqtys.length; i++) {
                       var z = (totalqty / totalpc) * parseInt(textratio[i].value.toString());
                       textqtys[i].value = z.toString();
                       qtysum += textqtys[i].value;
                       ratiosum += textratio[i].value;
                      
                   }
                   var textboxtotalqtys = grdrow.getElementsByClassName("totalRatioRow");

                   textboxtotalqtys[0].value = qtysum.toString();

                   var textboxtotalrat = grdrow.getElementsByClassName("totalRatioRow");

                   textboxtotalrat[0].value = ratiosum.toString();



                   var qty = parseInt(objText.value.toString());
                   var totalqty = parseInt(objText.value.toString());

               }
           }
           catch (e) {

           }
        
           calaculateall(objText)
       }



       function calaculateall(objText)
       {
           var grdrow = objText.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
           var textqtys = grdrow.getElementsByClassName("txtCalQty");
        //   debugger;


           var sum = 0;
           var textboxs = grdrow.getElementsByClassName("txtCalRatio");

           for (var i = 0; i < textboxs.length; i++) {
               sum += parseFloat(textboxs[i].value);
           }
           var textboxtotalratio = grdrow.getElementsByClassName("totalRatioRow");
           textboxtotalratio[0].value = sum.toString();


           var qtysum = 0;
           var textboxsqty = grdrow.getElementsByClassName("txtCalQty");

           for (var i = 0; i < textboxsqty.length; i++) {
               qtysum += parseFloat(textboxsqty[i].value);
           }
           var textboxtotalqtys = grdrow.getElementsByClassName("totalQtyRow");
           textboxtotalqtys[0].value = qtysum.toString();

       }

    



</script>

    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">New Cut Order</td>
        </tr>
        <tr>
           <td class="DataEntryTable"  >
               
                        <table class="DataEntryTable">
                            <tr>
                                <td  >Atc #</td>
                                <td class="NormalTD"  >
                                   
                                    <ucc:DropDownListChosen ID="drp_Atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td><td class="NormalTD"  >
                                    <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="S" ValidationGroup="a" />
                                </td>
                                <td >&nbsp;</td>
                                <td >
                                     
                                </td>
                                <td  >
                                    &nbsp;</td>
                                
                            </tr>
                            <tr>
                                <td class="NormalTD" >Color</td>
                                <td  colspan="4" class="NormalTD">

                                  
                                    <ucc:DropDownListChosen ID="ddl_color" runat="server" DataTextField="ItemDescription" DataValueField="Skudet_pk">
                                    </ucc:DropDownListChosen>
                                </td>
                               <td class="NormalTD"  >
                                    <asp:Button ID="btn_color" runat="server" OnClick="btn_color_Click" Text="S" ValidationGroup="asdf" />
                                </td>
                            </tr>
                            <tr>
                               <td class="NormalTD"  >&nbsp;</td>
                                <td >
                                    
                                </td>
                                <td >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                            </tr>
                        </table>
                 
            </td>
        </tr>
        <tr>
           <td class="DataEntryTable"  >
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="6">Cut Order Details </td>
                            </tr>
                            <tr>
                                <td class="NormalTD" >our style</td>
                                <td >
                                  
                                  <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td >
                                    &nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="NormalTD">Cut order #</td>
                               <td class="NormalTD"  >
                                    <asp:TextBox ID="txt_cutno" runat="server" Width="131px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cutno" EnableTheming="True" ErrorMessage="Enter Cut Order #" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >
                                   
                                </td>
                               <td class="NormalTD"  ></td>
                               <td class="NormalTD"  ></td>
                            </tr>
                            <tr>
                                <td class="NormalTD" >CutOrder Type :</td>
                                <td >
                                
                                    <ucc:DropDownListChosen ID="drp_cutorderType"  AutoPostBack="true" runat="server" Width="200px" OnSelectedIndexChanged="drp_cutorderType_SelectedIndexChanged" >
                                        <asp:ListItem Value="Extra">Extra</asp:ListItem>
   <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                    </ucc:DropDownListChosen>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drp_cutorderType" EnableTheming="True" ErrorMessage="Enter Cut Order Type" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                                <td >Reason</td>
                                <td >
                                
                                       <ucc:DropDownListChosen ID="drp_reason" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td ></td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td class="NormalTD">To factory</td>
                               <td class="NormalTD"  >
                                      <ucc:DropDownListChosen ID="drp_fact" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drp_fact" EnableTheming="True" ErrorMessage="Enter To factory" ForeColor="#FF3300">*</asp:RequiredFieldValidator></td>
                                <td class="NormalTD">Marker/pattern name</td>
                               <td class="NormalTD"  >
                                    <asp:TextBox ID="txt_markername" runat="server"></asp:TextBox>
                                </td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="NormalTD" >CO fab Allocation</td>
                                <td class="NormalTD" >
                                    <asp:TextBox ID="txt_fabAllocation" AutoPostBack="false" ValidationGroup="k"  runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_fabAllocation" EnableTheming="True" ErrorMessage="Enter Cut Order fab Allocation" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_fabAllocation" ErrorMessage="Enter  Numeric Value for Fab Qty" ForeColor="#CC3300" ValidationExpression="^[1-9]\d*(\.\d+)?$">*</asp:RegularExpressionValidator>
                                </td>
                                <td class="NormalTD" >Cuttable Width</td>
                                <td class="NormalTD" >
                                    <ucc:DropDownListChosen ID="drp_width" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen></td>
                                <td class="NormalTD" ></td>
                                <td class="NormalTD" ></td>
                            </tr>
                            <tr>
                                <td class="NormalTD">Shrinkage</td>
                               <td class="NormalTD"  >
                                   <ucc:DropDownListChosen ID="drp_shrink" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen></td>
                               <td class="NormalTD"  >Marker type</td>
                               <td class="NormalTD"  >
                                    <ucc:DropDownListChosen ID="drp_markerType" runat="server" Width="200px">
                                                </ucc:DropDownListChosen></td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="NormalTD">&nbsp;</td>
                               <td class="NormalTD"  >
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_errordisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                               <td class="NormalTD"  >&nbsp;</td>
                            </tr>
                           
                            <tr class="RedHeadding">
                                <td colspan="6">Marker Details</td>
                            </tr>
                            <tr >
                                <td colspan="6">
                                    <table class="tittlebar">
                                        <tr>
                                            <td class="NormalTD">No of Markers</td>
                                            <td class="NormalTD">
                                                <asp:TextBox ID="txt_noofmarker" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="NormalTD">
                                                <asp:Button ID="btn_markertype" runat="server" OnClick="btn_markertype_Click" Text="S" ValidationGroup="123456" />
                                            </td>
                                            <td class="NormalTD"></td>
                                        </tr>
                                        <tr>
                                            <td class="DataEntryTable" colspan="4"> <asp:UpdatePanel ID="upd_grid"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_marker" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Marker NO:">
                                          
                                            <ItemTemplate>
                                                 <asp:TextBox ID="txt_markernum" runat="server" Text='<%# Bind("MarkerNum") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="No of Pcs"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_nopc" runat="server" Text='<%# Bind("NoofPC") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                              

                                          <asp:TemplateField HeaderText="Quantity"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_qty"    runat="server" Width="70px" Text='<%# Bind("Qty") %>' ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Marker length">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Markerlength" runat="server" Width="70px" Text='<%# Bind("Markerlength") %>' ></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lay Length">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_layLength" runat="server" Width="70px" Text='<%# Bind("layLength") %>' ></asp:TextBox>
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
                                    </table>
                                </td>
                            </tr>

                            
                            <tr>
                                <td colspan="6">
                                    <div id="Messaediv" runat="server">
                                        <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr>
                                <td  colspan="6" style="align-content:center">
                                    <asp:Button ID="btn_saveCutorder" runat="server" OnClick="btn_saveCutorder_Click" Text="Save  Cutorder" />
                                </td>
                            </tr>
                            <tr>
                                <td class="DataEntryTable" colspan="6">
                                    <asp:UpdatePanel ID="upd_gridtable" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>

                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutOrderDet_PK], [MarkerNo], [NoOfPc], [Qty], [MarkerLength], [LayLength] FROM [CutOrderDetails] WHERE ([CutID] = @CutID)">
                                                    <SelectParameters>
                                                        <asp:SessionParameter DefaultValue="0" Name="CutID" SessionField="cutid" Type="Decimal" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <br />
                                                <asp:GridView ID="tbl_cutorderdata" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CutOrderDet_PK" DataSourceID="SqlDataSource1" OnDataBound="tbl_cutorderdata_DataBound" OnRowDataBound="tbl_cutorderdata_RowDataBound" OnRowCommand="tbl_cutorderdata_RowCommand" OnSelectedIndexChanged="tbl_cutorderdata_SelectedIndexChanged">
                                                    <Columns>

                                                        <asp:TemplateField>                                   
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PK" InsertVisible="False" SortExpression="CutOrderDet_PK">
                                                          
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CutOrderDet_PK" runat="server" Text='<%# Bind("CutOrderDet_PK") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MarkerNo" SortExpression="MarkerNo">
                                                         
                                                            <ItemTemplate>                                                             
                                                                 
                                                                
                                                                 
                                                                  
                                                               
                                                                <table class="tittlebar" style=" width: inherit; border-style: solid; background-color: #FFFFFF">
                                                                    <tr>
                                                                       <td class="NormalTD"  >Marker Num</td>
                                                                       <td class="NormalTD"  ><asp:Label ID="Label1" runat="server" Text='<%# Bind("MarkerNo") %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td class="NormalTD"  >NoOfPc</td>
                                                                       <td class="NormalTD"  ><asp:Label ID="Label2" runat="server" Text='<%# Bind("NoOfPc") %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td class="NormalTD"  >Qty</td>
                                                                       <td class="NormalTD"  ><asp:TextBox ID="lbl_totalQty" CssClass="num" runat="server" onkeypress="return isNumberKey(event,this)"  onkeyup ="SplitQty(this)"   Text ='<%# Bind("Qty") %>'> </asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td class="NormalTD"  >MarkerLength</td>
                                                                       <td class="NormalTD"  ><asp:Label ID="Label4" runat="server" Text='<%# Bind("MarkerLength") %>'></asp:Label>;</td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td class="NormalTD"  >LayLength</td>
                                                                       <td class="NormalTD"  ><asp:Label ID="Label5" runat="server" Text='<%# Bind("LayLength") %>'></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="200px" />
                                                            <FooterStyle Width="200px" />
                                                            <HeaderStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MarkerDetails" SortExpression="MarkerDetails">
                                                          
                                                            <ItemTemplate>



                                                               
                            <asp:UpdatePanel ID="upd_table"  runat="server">
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
                                                        
                                                      
                                                        <asp:ButtonField CommandName="Add" Text="Add" ButtonType="Button" />
                                                     
                                            
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
                                <td class="NormalTD"></td>
                                <td class="NormalTD"></td>
                                <td class="NormalTD"></td>
                                <td class="NormalTD">
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" />
                                </td>
                                <td class="NormalTD"></td>
                                <td class="NormalTD"></td>
                            </tr>


                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

