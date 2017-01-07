<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MarkerDetails.aspx.cs" Inherits="ArtWebApp.Production.CutOrder.MarkerDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
  
    <link href="../../css/style.css" rel="stylesheet" />
    
    <script src="../../JQuery/GridJQuery.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

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



 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
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
                        <td class="NormalTD">Cutorder #</td>
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
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="auto-style7"><asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD" colspan="8">
                            
                            <asp:UpdatePanel ID="upd_table" UpdateMode="Conditional" runat="server">
                                        </asp:UpdatePanel>

                        </td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD" colspan="7" >

                             
                            <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>

                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutOrderDet_PK], [MarkerNo], [NoOfPc], [Qty], [MarkerLength], [LayLength] FROM [CutOrderDetails] WHERE ([CutID] = @CutID)">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="drp_cutorder" DefaultValue="0" Name="CutID" PropertyName="SelectedValue" Type="Decimal" />
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
                                                                        <td>Marker Num</td>
                                                                        <td><asp:Label ID="Label1" runat="server" Text='<%# Bind("MarkerNo") %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>NoOfPc</td>
                                                                        <td><asp:Label ID="Label2" runat="server" Text='<%# Bind("NoOfPc") %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Qty</td>
                                                                        <td><asp:TextBox ID="lbl_totalQty" CssClass="num" runat="server" onkeypress="return isNumberKey(event,this)"  onkeyup ="SplitQty(this)"   Text ='<%# Bind("Qty") %>'> </asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>MarkerLength</td>
                                                                        <td><asp:Label ID="Label4" runat="server" Text='<%# Bind("MarkerLength") %>'></asp:Label>;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>LayLength</td>
                                                                        <td><asp:Label ID="Label5" runat="server" Text='<%# Bind("LayLength") %>'></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="200px" />
                                                            <FooterStyle Width="200px" />
                                                            <HeaderStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MarkerDetails" SortExpression="MarkerDetails">
                                                          
                                                            <ItemTemplate>



                                                               
                            <asp:UpdatePanel ID="upd_table" UpdateMode="Conditional" runat="server">
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
                                        </asp:UpdatePanel>
                           
                        </td>
                        <td class="NormalTD" >
                            &nbsp;</td>
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
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
    </table>
    
</asp:Content>
