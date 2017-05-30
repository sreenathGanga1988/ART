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
               var allowedexcess = (1/ 100) * parseFloat(htmlcell[0].value);

               var allowedqty = allowedexcess + parseFloat(htmlcell[0].value);

               if (allowedqty < parseFloat(objText.value))
               {
                   alert("Qty Cannot be greater than 1% extra of ASQ balance");
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

           
         
    function popWinDow()
 
    {
        debugger;

        
        
        
        
        var drp_fabcolor = document.getElementById("<%= drp_fabcolor.ClientID %>");
        var skudetpk = drp_fabcolor.options[drp_fabcolor.selectedIndex].value;

        var drp_shrink = document.getElementById("<%= drp_shrink.ClientID %>");
        var shrinkage = drp_shrink.options[drp_shrink.selectedIndex].value;

        var drp_width = document.getElementById("<%= drp_width.ClientID %>");
        var width = drp_width.options[drp_width.selectedIndex].value;

        var drp_markerType = document.getElementById("<%= drp_markerType.ClientID %>");
        var markertype = drp_markerType.options[drp_markerType.selectedIndex].value;


        var drp_fact = document.getElementById("<%= drp_fact.ClientID %>");
        var factid = drp_fact.options[drp_fact.selectedIndex].value;

        window.open("DisplayRoll.aspx?skudetpk=" + skudetpk + "&shrinkage=" + shrinkage + "&width=" + width + "&markertype=" + markertype + "&factid=" + factid);
       // window.open("WWW.google.co.in");
 
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
             font-size: x-small;
            font-weight: 900;
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
        .smalltableheader {
            height: 27px;
            text-align: center;
            background-color: #FF9933;
        }
        .auto-style1 {
            font-size: x-small;
        }
        .auto-style2 {
            font-size: xx-small;
        }
        .auto-style3 {
            height: 26px;
            width: 200px;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <asp:UpdatePanel ID="Upd_full" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>


                        <div class="RedHeaddingdIV">
                            New Cut Plan
                        </div>
                        <div class="FullTable">
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
                                     
                                    
                                     
                                    
                                    
                                        <asp:UpdatePanel ID="upd_skudetPK" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                  <asp:Label ID="lbl_skudet_pk" runat="server" CssClass="auto-style2" Text="0"></asp:Label>
                                     
                                              </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>
                                 <td class="SearchButtonTD"  >
                                      <asp:UpdatePanel ID="upd_colorcodeupdate" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                     <asp:Label ID="lbl_labelcode" runat="server" CssClass="auto-style2"></asp:Label>
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
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="CMN" Visible="False" CssClass="auto-style2" />
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
                                <td class="NormalTD">marker direction</td>
                                <td class="NormalTD">
                                    <ig:WebDropDown ID="drp_popack" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId" Width="200px">
                                        <Items>
                                            <ig:DropDownItem Selected="False" Text="Normal Marker" Value="Normal Marker">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="One Way ( All garment one direction)" Value="One Way ( All garment one direction)">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Two Way ( One Garment One direction)" Value="Two Way ( One Garment One direction)">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Nap UP" Value="Nap UP">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Nap Down" Value="Nap Down">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Plaid Match" Value="Plaid Match">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Horizontal cut" Value="Horizontal cut">
                                            </ig:DropDownItem>
                                            <ig:DropDownItem Selected="False" Text="Bias Cut" Value="Bias Cut">
                                            </ig:DropDownItem>
                                        </Items>
                                        <DropDownItemBinding TextField="name" ValueField="pk" />
                                    </ig:WebDropDown>
                                </td>
                                <td class="SearchButtonTD">&nbsp;</td>
                                <td class="NormalTD">Marker Made </td>
                                <td class="NormalTD">
                                    <asp:UpdatePanel ID="upd_garmentColor0" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ucc:DropDownListChosen ID="drp_markermade" runat="server" DataTextField="ColorName" DataValueField="ColorCode" Width="180px">
                                                <asp:ListItem>Gerber</asp:ListItem>
                                                <asp:ListItem>Manual</asp:ListItem>
                                            </ucc:DropDownListChosen>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="NormalTD">Fabrication</td>
                                <td class="NormalTD">
                                       <asp:UpdatePanel ID="upd_fabrication" runat="server" UpdateMode="Conditional">
                                           <ContentTemplate>
                                               <ucc:DropDownListChosen ID="drp_fabrication" runat="server" DataTextField="name" DataValueField="pk" Width="180px">
                                                  
                                               </ucc:DropDownListChosen>
                                        <%--         <ucc:DropDownListChosen ID="DropDownListChosen1" runat="server" DataTextField="ColorName" DataValueField="ColorCode" Width="180px">
                                                   <asp:ListItem>Body</asp:ListItem>
                                                   <asp:ListItem>Side Insert</asp:ListItem>
                                                   <asp:ListItem>Side Panel</asp:ListItem>
                                                   <asp:ListItem>Waist Band Rib</asp:ListItem>
                                                   <asp:ListItem>Inner Lining</asp:ListItem>
                                                   <asp:ListItem>Piping</asp:ListItem>
                                                   <asp:ListItem>Contrast Yoke</asp:ListItem>
                                                   <asp:ListItem>Elbow Patch</asp:ListItem>
                                                   <asp:ListItem>Inner Shorts</asp:ListItem>
                                                   <asp:ListItem>Waist  Band Contrast</asp:ListItem>
                                                   <asp:ListItem>Inner Shorts</asp:ListItem>
                                                   <asp:ListItem>Color Contrast</asp:ListItem>
                                                   <asp:ListItem>Elbow Patch</asp:ListItem>
                                                   <asp:ListItem>BodyColor Contrast</asp:ListItem>
                                                   <asp:ListItem>Hood LiningPatch</asp:ListItem>
                                                   <asp:ListItem>PocketingInside Mesh</asp:ListItem>
                                                   <asp:ListItem>InterLining</asp:ListItem>
                                                   <asp:ListItem>Pocketing</asp:ListItem>
                                               </ucc:DropDownListChosen>--%>
                                           </ContentTemplate>
                                       </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD"></td>
                                <td  class="NormalTD"  >Max marker length</td>
                                <td class="NormalTD">
                                    <asp:TextBox ID="txt_maximumMarkerlength" runat="server">0</asp:TextBox>
                                </td>
                                <td class="SearchButtonTD">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="NormalTD">Select garment Color</td>
                                <td class="NormalTD">
                                    <asp:UpdatePanel ID="upd_garmentColor" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ucc:DropDownListChosen ID="ddl_color" runat="server" DataTextField="ColorName" DataValueField="ColorCode" Width="180px">
                                            </ucc:DropDownListChosen>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="mergecell" >
                                  
                                </td>
                                <td class="NormalTD">
                                    <asp:UpdatePanel ID="upd_confirmgarmentcolor" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_cutorder" runat="server" Font-Size="Smaller" OnClick="btn_cutorder_Click" style="height: 26px" Text="Confirm" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="NormalTD">
                                    <asp:UpdatePanel ID="upd_confirmgarmentcolor0" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_cutordermnum" runat="server" OnClick="btn_cutordermnum_Click" style="height: 26px" Text="Common Color" CssClass="auto-style2" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="SearchButtonTD">&nbsp;</td>

                            </tr>
                         
                            
                        
                            
                            <tr>
                                <td class="NormalTD">&nbsp;</td>
                                <td class="NormalTD">&nbsp;</td>
                                <td class="mergecell">&nbsp;</td>
                                <td class="NormalTD">&nbsp;</td>
                                <td class="NormalTD">&nbsp;</td>
                                <td class="SearchButtonTD">&nbsp;</td>
                            </tr>
                         
                            
                        
                            
                        </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        </div>

                        <div class="FullTable">

                              <table class="DataEntryTable">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upd_garmentDetail" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table class="smalltablefordisplay">
                                                            <tr>
                                                                <td class="smalltableheader" colspan="2">TOTAL All Location dETAILS (STYLEWISE)</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Gmt Color</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_garmentColor" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Gmt Qty (OF sTYLE)</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_garmentQty" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">yARDAGE REQUIRED</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_reqyardforstyle" runat="server">0</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Style</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_stylename" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">BOM Consumtion(OF STYLE)(YDS)</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_consumption" runat="server" CssClass="consumption" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">issued cut plan qty:</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_alreadycut" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Apprx cutplan Yardage</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_totalcutplanyardage" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Bom Consumption</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_consumptionactual" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="upd_rolldetails" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table class="smalltablefordisplay">
                                                            <tr>
                                                                <td class="smalltableheader" colspan="2">Current Location</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Rolls Inspected :</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_rollinspected" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Total yards inspected :</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_ayard" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Rolls Delivered</td>
                                                                <td class="NormalTD">
                                                                    <asp:LinkButton ID="lbl_deliveredrolls" runat="server" OnClientClick="popWinDow()" Text="0" o ></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Delivered yards</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_deliveredYard" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="auto-style3">stores onhand (yards)</td>
                                                                <td class="auto-style3">
                                                                    <asp:Label ID="lbl_onhand" runat="server" CssClass="newyardreq" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Pending to Deliver</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_balancetodeliveryardage" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Consumption uom</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_uom" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">&nbsp;</td>
                                                                <td class="NormalTD">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="upd_alreadyCut" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table class="smalltablefordisplay">
                                                          <tr>
                                                                <td class="smalltableheader" colspan="2">Current Location</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Allocated Qty for Location</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_allocatedQty" runat="server">0</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Issued Cutplan qty (<span class="auto-style1">current selected location) </span></td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_alreadycutelectedFactory" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Issued/blocked yardage</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_locationcutplanyardage" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Balance to Cut qty</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_baltocutlocation" runat="server" Text="0" ForeColor="#FF3300"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">new Cutplan Qty :</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_newcutplan" runat="server" CssClass="newcutqty" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">New fab ReQ (yards)</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_newyard0" runat="server" CssClass="newyardreq" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td class="NormalTD">&nbsp;</td>
                                                                <td class="NormalTD">&nbsp;</td>
                                                            </tr>
                                                            
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td >
                                                <asp:UpdatePanel ID="Upd_consumption" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table class="smalltablefordisplay">
                                                              <tr>
                                                                <td class="smalltableheader" colspan="2">Current Location</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Available rolls</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_balroll" runat="server"  Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">balance yardage :</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_balyard" runat="server" CssClass="balyard" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">New balance</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="LNL" runat="server" CssClass="newbalyard" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="NormalTD">Qty cuttable with onhand rolls</td>
                                                                <td class="NormalTD">
                                                                    <asp:Label ID="lbl_apprQty" runat="server" CssClass="newbalyard" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                  <td class="NormalTD">
                                                                      Available Weight</td>
                                                                  <td class="NormalTD">
                                                                      <asp:Label ID="lbl_weight" runat="server" Text="0"></asp:Label>
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <td class="NormalTD">GSM</td>
                                                                  <td class="NormalTD">
                                                                      <asp:TextBox ID="txt_gsm" runat="server" Width="50px">0</asp:TextBox>
                                                                  </td>
                                                              </tr>
                                                              <tr>
                                                                  <td class="NormalTD">Width</td>
                                                                  <td class="NormalTD">
                                                                      <asp:TextBox ID="txt_width" runat="server" Width="50px">0</asp:TextBox>
                                                                  </td>
                                                              </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>

                        </div>


                        <div class="FullTable">
                            <table class="FullTable">
     
                 <tr>

                                  <td colspan="6">

                                      <div>
                          
                        </div>
                                </td>

                            </tr>

                            <tr>
                                  <td colspan="6">

                                      <div>
                                          <asp:UpdatePanel ID="upd_mastertable" runat="server">
                                       <ContentTemplate>
                                           <asp:Panel ID="masterpanel" runat="server" ViewStateMode="Enabled">
                                               <asp:Table ID="Mastertable" runat="server" ViewStateMode="Enabled" Width="400px">
                                               </asp:Table>
                                           </asp:Panel>
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
                                                               <td class="NormalTD">POPAckid</td>
                                                               <td class="NormalTD">
                                                                    <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               <td class="NormalTD">Outstyleid</td>
                                                               <td class="NormalTD">
                                                                    <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               <td class="NormalTD">atcid</td>
                                                               <td class="NormalTD">
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
                                                               <td class="NormalTD">ASQ</td>
                                                               <td class="NormalTD">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               <td class="NormalTD">PoPack#</td>
                                                               <td class="NormalTD">
                                                                    <asp:Label ID="na" runat="server" Text='<%# Bind("PoPacknum") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               <td class="NormalTD">BuyerPO</td>
                                                               <td class="NormalTD">
                                                                    <asp:Label ID="na1" runat="server" Text='<%# Bind("BuyerPO") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               <td class="NormalTD">BuyerStyle</td>
                                                               <td class="NormalTD">
                                                                    <asp:Label ID="na2" runat="server" Text='<%# Bind("BuyerStyle") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                               <td class="NormalTD">OurStyle</td>
                                                               <td class="NormalTD">
                                                                    <asp:Label ID="na3" runat="server" Text='<%# Bind("OurStyle") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                               <td class="NormalTD">SeasonName</td>
                                                               <td class="NormalTD">
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
                                           <asp:Button ID="btn_asqdetails" runat="server" OnClick="btn_asqdetails_Click" Text="Submit Cut Plan  PO Details" />
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
                                   
                                    
                                   
                                </td>
                            </tr>

                          

                          

                        </table>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

                        </div>
                        
 </ContentTemplate>
  </asp:UpdatePanel>
    <asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="Upd_full" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="modal">
                                                <div class="center">
                                                    <img src="../../Image/loader.gif" style="position: relative; top: 45%;"> </img>
                                                </div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel6" DisplayAfter="0" DynamicLayout="true">
                                        <ProgressTemplate>
                                            <div class="modal">
                                                <div class="center">
                                                    <img src="../../Image/loader.gif" style="position: relative; top: 45%;"> </img>
                                                </div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
</asp:Content>

