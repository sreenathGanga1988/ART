<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutPlanMarkerDetails.aspx.cs" Inherits="ArtWebApp.Production.CutOrder.CutPlanMarkerDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
  
    <link href="../../css/style.css" rel="stylesheet" />
    
    <script src="../../JQuery/GridJQuery.js"></script>


   <script type="text/javascript">


       function CheckBoxChecked(objText)
       {
           totalcalculation();
           Check_Click(objText);
         
       }


       var submit = 0;    
          


       function validateQty() {
           debugger;

           if (++submit > 1) {
               alert('This sometimes takes a few seconds - please be patient.');
               return false;
           }




           var isallzer = true;
        
           var headertable = document.getElementsByClassName("Headernewtable");


           var headerhtmlrow = headertable[0].rows[0];

           var balancetext = headertable[0].rows[3].getElementsByClassName("BalQty");

           for (var j = 0; j < balancetext.length; j++)
           {
               if (parseFloat(balancetext[j].value) == 0) {

                  
                 
                  
               }
               else
               {
                   alert("Extra Qty Cannot be Allowed against Cut");
                   isallzer= false;

               }
           }
          
           return isallzer;
       }









          function totalcalculation()
       {
           //   debugger;

           var gridView = document.getElementById("<%= tbl_cutorderdata.ClientID %>");

           var result = [];

           var cutplantotal = 0;
           for (var i = 1; i < gridView.rows.length; i++)
           {
               var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
               if (chkConfirm.checked) {

                   var htmltable = gridView.rows[i].getElementsByClassName("dynamicentrytable");
                   var htmlrow = htmltable[0].rows[1];
            
                   sumofQty(htmlrow);
                   var htmlratiorow = htmltable[0].rows[htmltable[0].rows.length - 1];
             
                  //sumofRatio(htmlratiorow);
                  
                   if (sumofRatio(htmlratiorow)==false)
                   {
                       return;
                   }


                   var textboxs = htmlrow.getElementsByClassName("txtCalQty");
                
               var rowsum = 0;
               for (var j = 0; j < textboxs.length; j++)
               {
                       var cellindex = textboxs[j].parentNode.cellIndex;

                       var table = textboxs[j].parentNode.parentNode.parentNode;

                       var headervalue = table.rows[0].cells[cellindex].getElementsByClassName("Headerlabel");
                       var size = headervalue[0].innerHTML;

                       var qty = textboxs[j].value;
                       rowsum += parseFloat(textboxs[j].value);
                       result.push({ sizename: size, sizeqty: qty });

               }

               



               
              
               }

               CalculatePlies(gridView.rows[i]);
           }

           
           var headertable = document.getElementsByClassName("Headernewtable");

         
           var headerhtmlrow = headertable[0].rows[0];
           var labelSize = headerhtmlrow.getElementsByClassName("HeaderSize");

           for (var i = 1; i < labelSize.length; i++)
           {
               var sizetotal = 0

               var cellindex = labelSize[i].parentNode.cellIndex;
               var headersizename = labelSize[i].innerHTML;
               for (var j = 0 ; j < result.length; j++)
               {
                  
                   if(result[j].sizename==headersizename)
                   {
                       sizetotal += parseFloat(result[j].sizeqty);
                   }

               } 
               var sumqtytext = headertable[0].rows[2].cells[cellindex].getElementsByClassName("NewQty");
             
               sumqtytext[0].value = sizetotal.toString();


               var balancetext = headertable[0].rows[3].cells[cellindex].getElementsByClassName("BalQty");

               var availabletext = headertable[0].rows[1].cells[cellindex].getElementsByClassName("AvailQty");
               var balqty = parseFloat(availabletext[0].value) - parseFloat(sizetotal.toString());

               balancetext[0].value = balqty.toString();
               
           }

       




         
       }



       function calculateply(objtext)
       {
           var grdrow = objtext.parentNode.parentNode;
           CalculatePlies(grdrow);
       }


       
       function CalculatePlies(row)
       {
           var textboxs = row.getElementsByClassName("totalQtyRow");
           var textboxsratiorow = row.getElementsByClassName("totalRatioRow");
           var totalplies = row.getElementsByClassName("totalplies");
           var totalcut = row.getElementsByClassName("totalcut");
           var totalcutreq = row.getElementsByClassName("totalcutreq");
           var lbltotalcutreq = row.getElementsByClassName("lbltotalcutreq");
           
           var x = parseFloat(textboxs[0].value)/ parseFloat(textboxsratiorow[0].value);

                 
           totalplies[0].value = x.toString();



           var cutrequired = parseFloat(totalplies[0].value) / parseFloat(totalcut[0].value);
           lbltotalcutreq[0].innerHTML = cutrequired.toString();
           totalcutreq[0].value = Math.ceil(cutrequired).toString();
       }







       //calculate the sum of qty on keypress
       function sumofQty(row) {
       
        //   alert(objText.value);
          

           var sum = 0;
           var textboxs = row.getElementsByClassName("txtCalQty");

           for (var i = 0; i < textboxs.length; i++)
           {
               sum += parseFloat(textboxs[i].value);
           }


          


           var textboxtotalqtys = row.getElementsByClassName("totalQtyRow");

           textboxtotalqtys[0].value = sum.toString();
         

       }





       function sumofRatiofromTextbox(objText)
       {
           var cell = objText.parentNode;

           var row = cell.parentNode;
           sumofRatio(row);
       }


       function sumofQtyfromTextbox(objText) {
           var cell = objText.parentNode;

           var row = cell.parentNode;
           sumofQty(row);
       }







       // calculate the sum of ratio
       function sumofRatio(row) {
              
                   

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


           //SplitQty(element[0]);

         if (SplitQty(element[0]) == false)
         {
            
             return false
         }
         else
         {
             return true;
         }
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


               if (z > 0)
               {
                   //if (confirm('Cannot split the Quanity in this ratio"?'))
                   //{
                   //     Save it!
                   //} else {
                   //     Do nothing!
                   //}
                   alert("Cannot split the Quanity in this ratio");
                 //  objText.value = 0;
                   objText.focus();
                   return false;
                 
                  
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
            <td class="RedHeadding">cut plan marker size details</td>
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
                            
                            <asp:UpdatePanel ID="upd_mastertable" runat="server">
                                       <ContentTemplate>
                                           <asp:Panel ID="masterpanel" runat="server" ViewStateMode="Enabled">
                                               <asp:Table ID="Mastertable" runat="server" ViewStateMode="Enabled" Width="400px">
                                               </asp:Table>
                                           </asp:Panel>
                                       </ContentTemplate>
                                   </asp:UpdatePanel>

                        </td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD" colspan="7" >

                             
                            <asp:UpdatePanel ID="upd_gridtable" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>

                                                
                                               
                                                <asp:GridView ID="tbl_cutorderdata" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CutPlanMarkerDetails_PK" OnDataBound="tbl_cutorderdata_DataBound" OnRowDataBound="tbl_cutorderdata_RowDataBound" OnRowCommand="tbl_cutorderdata_RowCommand" OnSelectedIndexChanged="tbl_cutorderdata_SelectedIndexChanged">
                                                    <Columns>

                        

                                                             <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="CheckBoxChecked(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="PK" InsertVisible="False" SortExpression="CutPlanMarkerDetails_PK">
                                                          
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CutOrderDet_PK" runat="server" Text='<%# Bind("CutPlanMarkerDetails_PK") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MarkerNo" SortExpression="MarkerNo">
                                                         
                                                            <ItemTemplate>                                                             
                                                                 
                                                                
                                                                 
                                                                  
                                                               
                                                                <table class="tittlebar" style=" width: inherit; border-style: solid; background-color: #FFFFFF">
                                                                    <tr>
                                                                       <td class="NormalTD"  >Marker Num</td>
                                                                       <td class="NormalTD"  ><asp:Label ID="lbl_markernum" runat="server" Text='<%# Bind("MarkerNo") %>'></asp:Label></td>
                                                                    </tr>
                                                                 
                                                                    <tr>
                                                                       <td class="NormalTD"  >Qty</td>
                                                                       <td class="NormalTD"  ><asp:TextBox ID="lbl_totalQty" CssClass="num" runat="server" onkeypress="return isNumberKey(event,this)"  onkeyup ="SplitQty(this)" onchange="totalcalculation()"  Text ='<%# Bind("Qty") %>'> </asp:TextBox></td>
                                                                    </tr>
                                                                      </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="200px" />
                                                            <FooterStyle Width="200px" />
                                                            <HeaderStyle Width="200px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MarkerDetails" SortExpression="MarkerDetails">
                                                          
                                                            <ItemTemplate>



                                                               
                            <asp:UpdatePanel ID="upd_table"   runat="server">
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
                                                        
                                                      
                                                      
                                                     
                                            
                                                             <asp:TemplateField HeaderText="Total Plies Required To Cut">
                                                                 
                                                                 <ItemTemplate>
                                                                     <asp:TextBox ID="txt_totalplies" Text="0" CssClass="totalplies" Width="70px" Enabled="false" runat="server"></asp:TextBox>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText=" Maximum Plies ">
                                                                
                                                                 <ItemTemplate>
                                                                    <asp:TextBox ID="txt_totalcut" Text="0" CssClass="totalcut" Width="70px" onkeypress="return isNumberKey(event,this)"   onkeyup="calculateply(this)" runat="server"></asp:TextBox>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Number of Cut Required">
                                                                
                                                                 <ItemTemplate>
                                                                     <asp:TextBox ID="txt_cutreq"  CssClass="totalcutreq" Width ="70px"  Enabled="false" runat="server"></asp:TextBox>
                                                                      <asp:Label ID="lbl_cutreq"  CssClass="lbltotalcutreq" Width ="70px"  Text="0" runat="server"></asp:Label>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:BoundField />
                                                     
                                            
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
                        <asp:Button ID="Button1" runat="server"  OnClientClick="return validateQty()"  Text="Save  Cut Plan" OnClick="Button1_Click3" />
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