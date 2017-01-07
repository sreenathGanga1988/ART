<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="FactoryCutPlan.aspx.cs" Inherits="ArtWebApp.Production.CutOrder.FactoryCutPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="../../JQuery/GridJQuery.js"></script>

<script type="text/javascript">

       //calculate the sum of qty on keypress
       function sumofQty(objText) {
           debugger
      //    alert(objText.value);
           var cell = objText.parentNode;
           var row = cell.parentNode;
           var htmltable = row.parentNode;

           var cellindex = objText.parentNode.cellIndex;
           var rowindex = cell.parentNode.rowIndex;
           var htmlrow = htmltable.rows[rowindex - 1];

           
           
           var htmlcell = htmlrow.cells[cellindex].getElementsByClassName("BalQty");

           if (parseFloat(htmlcell[0].value) < parseFloat(objText.value))
           {
               var allowedexcess = (3 / 100) * parseFloat(htmlcell[0].value);

               var allowedqty = allowedexcess + parseFloat(htmlcell[0].value);

               if (allowedqty < parseFloat(objText.value))
               {
                   alert("Qty Cannot be greater than 3% extra of ASQ balance");
                   objText.focus();
               }

              
           }

           var sum = 0;
           var textboxs = row.getElementsByClassName("Qty");

           for (var i = 0; i < textboxs.length; i++)
           {
               sum += parseFloat(textboxs[i].value);
           }


          

           var textboxtotalqtys = row.getElementsByClassName("GrandTotal");

           textboxtotalqtys[0].value = sum.toString();
           CheckBoxSelectionValidation();

       }


       function QtyKeyUp(objText) { // Or `updateDisplay` or some such
           sumofQty(objText);
           totalcalculation();
       }

       function totalcalculation()
       {
         
           var gridView = document.getElementById("<%= tbl_podata.ClientID %>");

           var result = [];

           var cutplantotal = 0;
           for (var i = 1; i < gridView.rows.length; i++)
           {
               var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
               if (chkConfirm.checked) {

                   var htmltable = gridView.rows[i].getElementsByClassName("Tableclass");
                   var htmlrow = htmltable[0].rows[htmltable[0].rows.length - 1];

             
               
                   //var textboxs = gridView.rows[i].getElementsByClassName("Qty");
               var textboxs = htmlrow.getElementsByClassName("Qty");
               var textboxtotalqtys = htmlrow.getElementsByClassName("GrandTotal");
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
               textboxtotalqtys[0].value = rowsum.toString();
               }
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
               var sumqtytext = headertable[0].rows[1].cells[cellindex].getElementsByClassName("Qty");
             
               sumqtytext[0].value = sizetotal.toString();
               cutplantotal += parseFloat(sizetotal);
           }

           var newyardreq = document.getElementsByClassName("newyardreq");
           var newbalanceyard = document.getElementsByClassName("newbalyard");
           var balanceyard = document.getElementsByClassName("balyard");
           var consumption = document.getElementsByClassName("consumption");
           var newcutqty = document.getElementsByClassName("newcutqty");
           newcutqty[0].innerHTML = cutplantotal.toString();


           var reqqty = parseFloat(cutplantotal) * parseFloat(consumption[0].innerHTML);
           newyardreq[0].innerHTML = reqqty.toString();

           var bal = parseFloat(balanceyard[0].innerHTML) - parseFloat(reqqty);
           newbalanceyard[0].innerHTML = bal.toString();

           if (parseFloat(reqqty) > parseFloat(balanceyard[0].innerHTML)) {

               alert("Not enough Fabric");
           }





         
       }




       function CheckBoxSelectionValidation() {
             debugger;
             var gridView = document.getElementById("<%= tbl_podata.ClientID %>");
           var tottallgrandqty = 0;
             for (var i = 1; i < gridView.rows.length; i++) {
                 var count = 0;
                 var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                 var textboxtotalqtys = gridView.rows[i].getElementsByClassName("GrandTotal");
                
                 if (chkConfirm.checked) {
                    
                     tottallgrandqty=parseFloat(textboxtotalqtys[0].value)+parseFloat(tottallgrandqty)
                      
                     }
             }
             
             var newyardreq = document.getElementsByClassName("newyardreq");
             var newbalanceyard = document.getElementsByClassName("newbalyard");
             var balanceyard = document.getElementsByClassName("balyard");
             var consumption = document.getElementsByClassName("consumption");
             var newcutqty = document.getElementsByClassName("newcutqty");
             newcutqty[0].innerHTML = tottallgrandqty.toString();
            
             
             var reqqty = parseFloat(tottallgrandqty) * parseFloat(consumption[0].innerHTML);
             newyardreq[0].innerHTML = reqqty.toString();

             var bal = parseFloat(balanceyard[0].innerHTML) - parseFloat(reqqty);
             newbalanceyard[0].innerHTML = bal.toString();

             if (parseFloat(reqqty) > parseFloat(balanceyard[0].innerHTML)) {

                 alert("Not enough Fabric");
             }


             }

           
         

       
    



</script>

    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
        .mergecell {
            height: 27px;
        }
        .smalltablefordisplay {
            width: 100%;
            border-left-style: solid;
            border-left-width: 1px;
            border-right: 1px solid #C0C0C0;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom: 1px solid #C0C0C0;
             font-size: smaller;
        }
        


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

    <table class="FullTable">
        <tr>
            <td class="RedHeadding">New Cut Plan</td>
        </tr>
        <tr>
           <td class="DataEntryTable"  >
               <div>
                <asp:UpdatePanel ID="upd_entry" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                   <table class="DataEntryTable">
                            <tr>
                                 <td class="NormalTD"  >Factory</td>
                                <td class="NormalTD"  >
                                   
                                    <ucc:DropDownListChosen ID="drp_fact" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td><td class="SearchButtonTD"  >
                                    <asp:Button ID="btn_factoryshow" runat="server" style="width: 23px" Text="S" ValidationGroup="a" OnClick="btn_factoryshow_Click" />
                                </td>
                                <td class="NormalTD"  ></td>
                                <td class="NormalTD"  >
                                     
                                    
                                     
                                    </td>
                                 <td class="SearchButtonTD"  >
                                      <asp:UpdatePanel ID="upd_colorcodeupdate" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                     <asp:Label ID="lbl_labelcode" runat="server"></asp:Label>
                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                    </td>
                                
                            </tr>
                            <tr>
                                 <td class="NormalTD"  >Atc #</td>
                                <td class="NormalTD"  >
                                   
                                    <ucc:DropDownListChosen ID="drp_Atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td><td class="SearchButtonTD"  >
                                     <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                    <asp:Button ID="btn_atcshow" runat="server" OnClick="btn_show_Click" Text="S" ValidationGroup="a" style="width: 23px" />
                         </ContentTemplate>
                                                        </asp:UpdatePanel>
                                </td>
                                <td class="NormalTD"  >OurStyle</td>
                                <td class="NormalTD"  >
                                            <asp:UpdatePanel ID="upd_ourstyle" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                    <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                </td>
                                 <td class="SearchButtonTD"  >
                                      <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                    <asp:Button ID="btn_shostyle" runat="server" Text="S" OnClick="btn_shostyle_Click" />
                         </ContentTemplate>
                                                        </asp:UpdatePanel>
                                </td>
                                
                            </tr>
                            <tr>
                                 <td class="NormalTD"  >FABRIC</td>
                                <td class="NormalTD"  >
                                   
                                    <asp:UpdatePanel ID="upd_fabcolor" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ucc:DropDownListChosen ID="drp_fabcolor" runat="server" Width="200px" DataTextField="ItemDescription" DataValueField="Skudet_pk">
                                            </ucc:DropDownListChosen>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                 </td>
                                 <td  class="SearchButtonTD"  >
                                   
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                    <asp:Button ID="BTN_FABRICSHOW" runat="server" OnClick="BTN_FABRICSHOW_Click" Text="s" />
                                              </ContentTemplate>
                                    </asp:UpdatePanel>
                                 </td>
                               <td class="NormalTD"  >Marker Type</td>
                                <td class="NormalTD"  >
                                      <asp:UpdatePanel ID="upd_marker" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                    
                         <ucc:DropDownListChosen ID="drp_markerType" runat="server" Width="200px">
                         </ucc:DropDownListChosen>
                                    
                         </ContentTemplate>
                                                        </asp:UpdatePanel>
                                    
                                </td> <td class="SearchButtonTD"  >
                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                    <asp:Button ID="btn_color" runat="server" OnClick="btn_color_Click" Text="S" ValidationGroup="asdf" Visible="False" />
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="CMN" Visible="False" />
                          </ContentTemplate>
                                                        </asp:UpdatePanel>
                                </td>
                               
                                
                            </tr>
                            
                            <tr>
                               <td class="NormalTD"  >Shrinkage</td>
                                <td class="NormalTD"  >
                                      <asp:UpdatePanel ID="upd_shrnk" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                      <ucc:DropDownListChosen ID="drp_shrink" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                      </ucc:DropDownListChosen>
                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD"  >
                                    </td>
                               <td class="NormalTD"  >
                                  
                                   Width</td>
                               <td class="NormalTD"  >
                                     <asp:UpdatePanel ID="upd_width" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                      <ucc:DropDownListChosen ID="drp_width" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                      </ucc:DropDownListChosen>
                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD"  >
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_showgrid" runat="server"  Text="s" OnClick="btn_showgrid_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>
                            </tr>
                            <tr>
                                <td class="NormalTD">Select garment Color</td>
                                <td class="NormalTD">
                                       <asp:UpdatePanel ID="upd_garmentColor" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                    <ucc:DropDownListChosen ID="ddl_color" runat="server" DataTextField="ColorName" DataValueField="ColorCode" Width="180px">
                                    </ucc:DropDownListChosen>
                          </ContentTemplate>
                                                        </asp:UpdatePanel>
                                </td>
                                <td class="mergecell" colspan="2"><asp:UpdatePanel ID="upd_confirmgarmentcolor" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_cutorder" runat="server" Text="Confirm" OnClick="btn_cutorder_Click" style="height: 26px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel></td>
                                <td class="NormalTD">&nbsp;</td>
                                <td class="SearchButtonTD">&nbsp;</td>
                            </tr>
                            <tr>
                              <td class="NormalTD"  >
                                     
                                     <asp:UpdatePanel ID="upd_garmentDetail" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                     
                                  <table class="smalltablefordisplay">
                                      <tr>
                                          <td>Gmt Color</td>
                                          <td><asp:Label ID="lbl_garmentColor" runat="server"></asp:Label></td>
                                      </tr>
                                      <tr>
                                          <td>
                                              Gmt Qty
                                          </td>
                                          <td><asp:Label ID="lbl_garmentQty" runat="server"></asp:Label></td>
                                      </tr>
                                  </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                  
                                     
                                 </td>  <td class="NormalTD"  >
                                     <asp:UpdatePanel ID="upd_rolldetails" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                      <table class="smalltablefordisplay">
                                      <tr >
                                          <td>Rolls Inspected :</td>
                                          <td>
                                              <asp:Label ID="lbl_rollinspected" runat="server" Text="0"></asp:Label>
                                          </td>
                                      </tr>
                                      <tr >
                                          <td>Total yards inspected :</td>
                                          <td>
                                              <asp:Label ID="lbl_ayard" runat="server" Text="0"></asp:Label>
                                          </td>
                                      </tr>
                                          <tr >
                                              <td>Rolls Cut</td>
                                              <td>&nbsp;</td>
                                          </tr>
                                          <tr >
                                              <td>&nbsp;</td>
                                              <td>&nbsp;</td>
                                          </tr>
                                  </table>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD"  >
                                
                               <td class="NormalTD"  >   <asp:UpdatePanel ID="upd_alreadyCut" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                  <table class="smalltablefordisplay">
                                   
                                          <tr >
                                              <td>issued cut plan qty:</td>
                                              <td>
                                                  <asp:Label ID="lbl_alreadycut" runat="server" Text="0"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr >
                                              <td>new Cutplan Qty :</td>
                                              <td>
                                                  <asp:Label ID="lbl_newcutplan" runat="server" Text="0" CssClass="newcutqty"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr >
                                              <td>New fab ReQ</td>
                                              <td>
                                                  <asp:Label ID="lbl_newyard0" runat="server" CssClass="newyardreq" Text="0"></asp:Label>
                                              </td>
                                          </tr>
                                      </table>    </ContentTemplate>
                                    </asp:UpdatePanel>
                                   
                                </td>
                                <td class="NormalTD"  >
                                      <asp:UpdatePanel ID="Upd_consumption" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                      <table class="smalltablefordisplay">
                                       <tr>
                                           <td>Bom Consuption:</td>
                                           <td>
                                               <asp:Label ID="lbl_consumption" CssClass="consumption" runat="server" Text="0"></asp:Label>
                                           </td>
                                       </tr>
                                       <tr >
                                           <td>balance yardage :</td>
                                           <td>
                                               <asp:Label ID="lbl_balyard" CssClass="balyard" runat="server" Text="0"></asp:Label>
                                           </td>
                                       </tr>
                                          <tr >
                                              <td>New balance</td>
                                              <td><asp:Label ID="LNL" CssClass="newbalyard" runat="server" Text="0"></asp:Label></td>
                                          </tr>
                                          <tr >
                                              <td>Apprx Cutplan Qty</td>
                                              <td>
                                                  <asp:Label ID="lbl_apprQty" CssClass="newbalyard" runat="server" Text="0"></asp:Label>
                                                      </td>
                                          </tr>
                                   </table>
                                              </ContentTemplate>
                                    </asp:UpdatePanel></td>
                                <td class="SearchButtonTD"  >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                               <td class="auto-style8" colspan="6"  >
                                   
                                </td>
                            </tr>
                            <tr>
                               <td class="auto-style8" colspan="6"  >
                                   <div id="Messaediv" runat="server">
                                       <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                   </div>
                                </td>
                            </tr>
                            <tr>
                               <td class="auto-style8" colspan="6"  >
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
                        </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               </div>
                        
                 
            </td>
        </tr>
        <tr>
           <td class="DataEntryTable"  >
                <asp:UpdatePanel ID="updgrid" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="6">asq Details </td>
                                <tr>  <%----%>
                                    <td colspan="6">
                                        <asp:GridView ID="tbl_podata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" 
                                            BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PoPackId" OnRowCommand="tbl_podata_RowCommand" OnRowDataBound="tbl_podata_RowDataBound"
                                            ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_select" runat="server" onclick="totalcalculation()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                                <td>atcid</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_atcid" runat="server" Text='<%# Bind("AtcId") %>'></asp:Label>
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
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>PoPack#</td>
                                                                <td>
                                                                    <asp:Label ID="na" runat="server" Text='<%# Bind("PoPacknum") %>'></asp:Label>
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
                                                             <tr>
                                                                <td>SeasonName</td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("SeasonName") %>'></asp:Label>
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
                                                                <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                                                    <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled" Width="400px">
                                                                    </asp:Table>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                      <%--          <asp:ButtonField ButtonType="Button" CommandName="Update" Text="Update" />--%>
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
                                    <td class="NormalTD"></td>
                                    <td class="NormalTD"></td>
                                    <td class="NormalTD">
                                        <asp:SqlDataSource ID="PodataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, 
                         AtcDetails.BuyerStyle, PoPackMaster.AtcId, POPackDetails.ColorName, POPackDetails.SizeName, POPackDetails.PoQty
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
GROUP BY PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, 
                         PoPackMaster.AtcId, POPackDetails.ColorName, POPackDetails.SizeName, POPackDetails.PoQty,POPackDetails.ColorCode
HAVING        (POPackDetails.OurStyleID = @Param2) AND (POPackDetails.ColorCode = @Param3)
ORDER BY PoPackMaster.PoPackId">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="drp_ourstyle" Name="Param2" PropertyName="SelectedValue" />
                                                <asp:ControlParameter ControlID="ddl_color" Name="Param3" PropertyName="SelectedValue" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                    <td class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD"></td>
                                    <td class="NormalTD"></td>
                                </tr>
                           </tr>

                            <tr>
                                <td class="auto-style8" colspan="6">
                                    
                               <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                           <asp:Button ID="btn_asqdetails" runat="server" OnClick="btn_asqdetails_Click" Text="Submit PO Details" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel> </td>
                            </tr>

                            <tr>
                                <td class="auto-style8" colspan="6">
                                    <asp:UpdatePanel ID="upd_Messaediv1" runat="server">
                                        <ContentTemplate>
                                    <div id="Messaediv1" runat="server">
                                        <asp:Label ID="lbl_msg0" runat="server" Text="*"></asp:Label>
                                    </div>
                                              </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

                          
                            <tr>
                                <td class="auto-style8" colspan="6">
                                   
                                    <asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="upd_entry" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="modal">
                                                <div class="center">
                                                    <img src="../../Image/loader.gif" style="position: relative; top: 45%;"> </img>
                                                </div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                   
                                </td>
                            </tr>

                          

                          

                        </table>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

