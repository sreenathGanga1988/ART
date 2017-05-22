<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SolidSizeColorPackingInstruction.aspx.cs" Inherits="ArtWebApp.Merchandiser.ASQ.Packing.SolidSizeColorPackingInstruction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../css/style.css" rel="stylesheet" />
    <script src="../../../JQuery/GridJQuery.js"></script>
       <script>



   
           function validateforserver()
           {
               var x = true;
               
    var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
    
               for (var rownum = 1; rownum < gridView.rows.length; rownum++) {
                   var txt_totalctn = row.getElementsByClassName("txt_totalctn");
                   var txt_pcperctn = row.getElementsByClassName("txt_pcperctn");
                   var txt_totalqty = row.getElementsByClassName("txt_totalqty");
                   var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                   if (chkConfirm.checked) {
                       if (parseFloat(txt_pcperctn[0].value) % 1 != 0) {
                           txt_pcperctn[0].style.backgroundColor = "red";
                           x = false;
                       }
                       else {
                           txt_pcperctn[0].style.backgroundColor = "White";
                       }
                       if (parseFloat(txt_totalctn[0].value) % 1 != 0) {
                           txt_totalctn[0].style.backgroundColor = "red";
                           x = false;
                       }
                       else {
                           txt_totalctn[0].style.backgroundColor = "White";
                       }
                       if (parseFloat(txt_totalqty[0].value) % 1 != 0) {
                           txt_totalqty[0].style.backgroundColor = "red";
                           x = false;
                       }
                       else {
                           txt_totalqty[0].style.backgroundColor = "White";
                       }
                   }
               }


               return x;
           }


      function validateValueonPcperCTN()
           {
    
          debugger
  
    var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
    
    for (var rownum = 1; rownum < gridView.rows.length; rownum++) {
        var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
        if (chkConfirm.checked) {
            var row = gridView.rows[rownum];

            var sum = 0;
            var noctn = 0;
            var lbl_balanceqty = row.getElementsByClassName("lbl_balanceqty");


            var txt_pcperctn = row.getElementsByClassName("txt_pcperctn");
            noctn = parseFloat(lbl_balanceqty[0].innerText) / parseFloat(txt_pcperctn[0].value)

            var txt_totalctn = row.getElementsByClassName("txt_totalctn");

            txt_totalctn[0].value = noctn;

            //Display new Qty
            sum = parseFloat(txt_pcperctn[0].value) * parseFloat(txt_totalctn[0].value)
            var txt_totalqty = row.getElementsByClassName("txt_totalqty");
            txt_totalqty[0].value = sum.toString();




            if (parseFloat(txt_pcperctn[0].value) % 1 != 0) {
                txt_pcperctn[0].style.backgroundColor = "red";
            }
            else {
                txt_pcperctn[0].style.backgroundColor = "White";
            }
            if (parseFloat(txt_totalctn[0].value) % 1 != 0) {
                txt_totalctn[0].style.backgroundColor = "red";
            }
            else {
                txt_pcperctn[0].style.backgroundColor = "White";
            }
            //calculate new balance
            var lbl_newBal = row.getElementsByClassName("lbl_newBal");
            var newbQty = parseFloat(lbl_balanceqty[0].innerText) - sum;
            lbl_newBal[0].value = newbQty;

           

            //if greater give alert
            if (parseFloat(txt_totalqty[0].value) > parseFloat(lbl_balanceqty[0].innerText)) {
                txt_totalqty[0].value = 0
                lbl_newBal[0].value = 0;
           
            }

        }
    }
}



           
           function validateValueontotalCTN()
           {
    
               debugger
  
    var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
    
    for (var rownum = 1; rownum < gridView.rows.length; rownum++) {
        var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
        if (chkConfirm.checked) {
            var row = gridView.rows[rownum];
            var sum = 0;
            var noctn = 0;
            var lbl_balanceqty = row.getElementsByClassName("lbl_balanceqty");
            var txt_totalctn = row.getElementsByClassName("txt_totalctn");

            var txt_pcperctn = row.getElementsByClassName("txt_pcperctn");
       

          

            //Display new Qty
            sum = parseFloat(txt_pcperctn[0].value) * parseFloat(txt_totalctn[0].value)
            var txt_totalqty = row.getElementsByClassName("txt_totalqty");
            txt_totalqty[0].value = sum.toString();




            if (parseFloat(txt_pcperctn[0].value) % 1 != 0) {
                txt_pcperctn[0].style.backgroundColor = "red";
            }
            else {
                txt_pcperctn[0].style.backgroundColor = "White";
            }
            if (parseFloat(txt_totalctn[0].value) % 1 != 0) {
                txt_totalctn[0].style.backgroundColor = "red";
            }
            else {
                txt_pcperctn[0].style.backgroundColor = "White";
            }
            //calculate new balance
            var lbl_newBal = row.getElementsByClassName("lbl_newBal");
            var newbQty = parseFloat(lbl_balanceqty[0].innerText) - sum;
            lbl_newBal[0].value = newbQty;



            //if greater give alert
            if (parseFloat(txt_totalqty[0].value) > parseFloat(lbl_balanceqty[0].innerText)) {

               
                alert("Cannot pack More than ASQ QTY");
                txt_totalqty[0].value = 0
                lbl_newBal[0].value = 0;
            }

        }
    }
}






     



$(document).ready(function () {
    // if btn clicked
    

    ///Apply to All for Pc/ctn
    $(".btn_pcperctn").click(function () {
        var ctn = $(".txt_total").first().val();
        debugger;
        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked"))
            {
                
                var div = $(this).find(".txt_pcperctn").first();
                div.val(ctn);
              
            }


        });

        validateValueonPcperCTN();
        
    });




    ///Apply to All for Length
    $(".btn_alllength").click(function () {
        debugger
        var ctn = $(".txt_alllength").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".txt_length").first();
                div.val(ctn);

            }


        });
        
    });



    $(".btn_allWidth").click(function () {
        debugger
        var ctn = $(".txt_allwidth").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".txt_width").first();
                div.val(ctn);

            }


        });

    });

    
    
    


    $(".btn_allheigth").click(function () {
        debugger
        var ctn = $(".txt_allheight").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".txt_height").first();
                div.val(ctn);

            }


        });

    });
    $(".btn_allheigth").click(function () {
        debugger
        var ctn = $(".txt_allheight").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".txt_height").first();
                div.val(ctn);

            }


        });

    }); 
    
    

    $(".btn_weightUOMALL").click(function () {
        debugger
        var ctn = $(".drp_weightuomll").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".drp_weightuom").first();
                div.val(ctn);

            }


        });

    });
    
    $(".btn_NNWeight").click(function () {
        debugger
        var ctn = $(".txt_NNWeightAll").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".txt_NNWeight").first();
                div.val(ctn);

            }


        });

    });



    $(".btn_NetAll").click(function () {
        debugger
        var ctn = $(".txt_NetAll").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".txt_Netweight").first();
                div.val(ctn);

            }


        });

    });
    

    $(".btn_grossAll").click(function () {
        debugger
        var ctn = $(".txt_grossAll").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".txt_gross").first();
                div.val(ctn);

            }


        });

    });
    
    
    
    
    
    
    

    $(".btn_NetUOMALL").click(function () {
        debugger
        var ctn = $(".drp_NetUOM").first().val();

        //iterate through each gridview row
        $(".dataenttry tr").each(function () {

            if ($(this).find('input:checkbox').attr("checked")) {
                var div = $(this).find(".drp_weightuomll").first();
                div.val(ctn);

            }


        });

    });



});

</script>

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 6px;
        }
        </style>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RedHeaddingdIV"> Solid Color Solid Size</div>
    <div>
        

          <table style="border: thin double #C0C0C0; line-height: normal; vertical-align: middle;  text-align: center; white-space: normal; word-spacing: normal; letter-spacing: normal; background-color: #99CCFF; position: relative; width: 100%;" >
                            


                            <tr>
                                <td colspan="4" class="auto-style11">
                                    
                                    
                                    <strong>Quick Fill </strong></td>
                            </tr>
                               


                         

                                   <tr>
                                
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_total" CssClass="txt_total" placeholder=" PC/CTN" runat="server" Width="93px"></asp:TextBox>
                                    
                                </td>
                                       <td>

                                            <asp:Button ID="btn_pcperctn" CssClass="btn_pcperctn" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_pcperctn_Click"    />
                                       </td>
                                <td>
                                  
                                    <table class="auto-style1" style="background-color: #FF9966">
                                        <tr>
                                            <td> 
                                                <asp:TextBox ID="txt_alllength"  CssClass="txt_alllength" runat="server" placeholder="Length" Width="50px"></asp:TextBox>
                                            </td>
                                             <td> X</td>
                                               <td >  
                                                   <asp:TextBox ID="txt_allwidth"  CssClass="txt_allwidth" runat="server" placeholder="Width" Width="50px"></asp:TextBox>
                                            </td>
                                            
                                            <td>X</td>
                                            <td> 
                                                <asp:TextBox ID="txt_allheight" CssClass="txt_allheight"  runat="server" placeholder="Height" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                
                                                <asp:DropDownList ID="drp_weightuomll" CssClass="drp_weightuomll" runat="server">
                                                    <asp:ListItem Value="KG">KG</asp:ListItem>
                                                     <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                    
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btn_alllength" CssClass="btn_alllength" runat="server" Text="S" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td >
                                                <asp:Button ID="btn_allWidth" CssClass="btn_allWidth"  runat="server" Text="S" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btn_allheigth"  CssClass="btn_allheigth" runat="server" Text="S" />
                                            </td>
                                            <td> <asp:Button ID="btn_weightUOMALL"  CssClass="btn_weightUOMALL" runat="server" Text="S" /></td>
                                            <td class="auto-style3">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                               
                                </td>
                                <td>
                                    <table class="auto-style1" style="background-color: #00FFFF">
                                        <tr>
                                            <td style="background-color: #00FFFF">
                                                <asp:TextBox ID="txt_NNWeightAll" runat="server" CssClass="txt_NNWeightAll" placeholder="NNWeight" Width="50px"></asp:TextBox>
                                            </td>
                                           
                                            <td>
                                                <asp:TextBox ID="txt_NetAll" runat="server" CssClass="txt_NetAll" placeholder="Net" Width="50px"></asp:TextBox>
                                            </td>
                                            
                                            <td>
                                                <asp:TextBox ID="txt_grossAll" runat="server" CssClass="txt_grossAll" placeholder="Gross" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drp_NetUOMALL" runat="server" CssClass="drp_NetUOMALL">
                                                    <asp:ListItem Value="KG">KG</asp:ListItem>
                                                    <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <asp:Button ID="btn_NNWeight"   runat="server" CssClass="btn_NNWeight" Text="S" />
                                            </td>
                                        
                                            <td  >
                                                <asp:Button ID="btn_NetAll" runat="server" CssClass="btn_NetAll" Text="S" />
                                            </td>
                                        
                                            <td >
                                                <asp:Button ID="btn_grossAll" runat="server" CssClass="btn_grossAll" Text="S" />
                                            </td>
                                           
                                            <td >
                                                <asp:Button ID="btn_NetUOMALL" runat="server" CssClass="btn_NetUOMALL" Text="S" />
                                            </td>
                                            <td ></td>
                                            
                                        </tr>
                                    </table>
                                    </td>
                            </tr>

                      
                              
                        </table>

    </div>
     <div>

         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
             <ContentTemplate>
                 <asp:GridView ID="tbl_podetails" CssClass="dataenttry" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PoPack_Detail_PK" Font-Size="Large" OnRowDataBound="tbl_podetails_RowDataBound" style="font-size: x-small; font-family: Calibri" Width="100%">
                     <Columns>
                            <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                            <asp:TemplateField HeaderText="PoPack_Detail_PK" InsertVisible="False" SortExpression="PoPack_Detail_PK">
                               
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PoPack_Detail_PK" runat="server" Text='<%# Bind("PoPack_Detail_PK") %>'></asp:Label>
                                </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="POPackId" SortExpression="POPackId">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lbl_POPackId" runat="server" Text='<%# Bind("POPackId") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OurStyleID" SortExpression="OurStyleID">
                          
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AtcId" SortExpression="AtcId">
                         
                                <ItemTemplate>
                                    <asp:Label ID="lbl_atcid" runat="server" Text='<%# Bind("AtcId") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="hidden" />
                                <ItemStyle CssClass="hidden" />
                            </asp:TemplateField>
                         <asp:BoundField DataField="BuyerPO" HeaderText="BuyerPO" SortExpression="BuyerPO" />
                         <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                         <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                         <asp:BoundField DataField="Locaion_PK" HeaderText="Locaion_PK" ReadOnly="True" SortExpression="Locaion_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  >
                            <HeaderStyle CssClass="hidden" />
                            <ItemStyle CssClass="hidden" />
                            </asp:BoundField>
                         <asp:BoundField DataField="ASQ" HeaderText="ASQ" ReadOnly="True" SortExpression="ASQ" />
                            <asp:TemplateField HeaderText="ColorName" SortExpression="ColorName">
                               
                                <ItemTemplate>
                                    <asp:Label ID="lbl_color" runat="server" Text='<%# Bind("ColorName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SizeName" SortExpression="SizeName">
                              
                                <ItemTemplate>
                                    <asp:Label ID="lbl_size" runat="server" Text='<%# Bind("SizeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:BoundField DataField="POQty" HeaderText="POQty" SortExpression="POQty" ReadOnly="True" />
                         <asp:BoundField DataField="PackedQty" HeaderText="PackedQty" ReadOnly="True" SortExpression="PackedQty" />
                            <asp:TemplateField HeaderText="BalanceQty" SortExpression="BalanceQty" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" >
                         
                                <ItemTemplate>
                                    <asp:Label  ID="lbl_balanceqty"  CssClass="lbl_balanceqty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Width="50px" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pc/ctn" SortExpression="Pcperctn"   HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px">
                        
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_pcperctn"  CssClass="txt_pcperctn"    onkeypress="return isNumberKey(event,this)" onchange="validateValueonPcperCTN()"    Height="16px" Width="50px" runat="server" Text='<%# Bind("Pcperctn") %>' Font-Size="small"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Width="50px" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Ctn" SortExpression="TotalCtn" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" >
                       
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_totalctn"  CssClass="txt_totalctn"    onkeypress="return isNumberKey(event,this)" onchange="validateValueontotalCTN()"  Font-Size="small"  Height="16px" Width="50px"  runat="server" Text='<%# Bind("TotalCtn") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Width="50px" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                          
                           <asp:TemplateField HeaderText="TotalQty" SortExpression="TotalQty" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" >
                           
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_totalqty"  Enabled="false"  CssClass="txt_totalqty" runat="server" Font-Size="small" Text="0"></asp:TextBox>
                                </ItemTemplate>
                                 <FooterStyle Width="50px" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                                   
                              
                            </asp:TemplateField>
                          <asp:TemplateField HeaderText="New Bal" SortExpression="TotalQty" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" >
                           
                                <ItemTemplate>
                                    <asp:TextBox ID="lbl_newBal"   CssClass="lbl_newBal"  Font-Size="small"  runat="server" ></asp:TextBox>
                                </ItemTemplate>
                                             <FooterStyle Width="50px" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                
                                <ItemTemplate>
                                    <table class="auto-style1" border="1">
                                        <tr>
                                            <td style="width:50px">Length</td>
                                            <td style="width:50px">Width</td>
                                           <td style="width:50px"">Height</td>
                                            <td style="width:50px">UOM</td>
                                          
                                        </tr>
                                        <tr>
                                            <td> <asp:TextBox ID="txt_length"  CssClass="txt_length" placeholder="Length"  Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small" Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:TextBox ID="txt_width"  CssClass="txt_width"   placeholder="Width" Text="0"  onkeypress="return isNumberKey(event,this)"  Font-Size="small" Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:TextBox ID="txt_height"  CssClass="txt_height" placeholder="Height"  Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small" Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:DropDownList ID="drp_weightuom" CssClass="drp_weightuomll" runat="server">
                                                 <asp:ListItem Value="KG">KG</asp:ListItem>
                                                     <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                </asp:DropDownList></td>
                                    
                                        </tr>
                                        <tr>
                                            <td style="width:50px">Net Wgt</td>
                                            <td style="width:50px">Weight</td>
                                           <td style="width:50px">Gross</td>
                                           <td style="width:50px">UOM</td>                                           
                                        </tr>
                                        <tr>
                                            <td> <asp:TextBox ID="txt_NNWeight"  CssClass="txt_NNWeight" placeholder="Length"  Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small"  Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:TextBox ID="txt_Netweight"  CssClass="txt_Netweight"   placeholder="Width" Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small"  Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:TextBox ID="txt_gross"  CssClass="txt_gross" placeholder="Height"  Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small"  Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td><asp:DropDownList ID="drp_NetUOM" CssClass="drp_NetUOM" runat="server">
                                                 <asp:ListItem Value="KG">KG</asp:ListItem>
                                                     <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                </asp:DropDownList></td></td>
                                    
                                        </tr>
                                  

                                    </table>
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
                 <asp:SqlDataSource ID="Newdatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT tt.PoPack_Detail_PK, tt.POPackId, tt.OurStyleID, tt.AtcId, tt.BuyerPO, tt.OurStyle, tt.BuyerStyle, tt.Locaion_PK, tt.ASQ, tt.ColorName, tt.SizeName, tt.POQty, ISNULL(PackingListDetails.TotalQty, 0) AS PackedQty, tt.POQty - ISNULL(PackingListDetails.TotalQty, 0) AS BalanceQty, 0 AS Pcperctn, 0 AS TotalCtn, 0 AS TotalQty FROM (SELECT POPackDetails.POPackId, POPackDetails.OurStyleID, PoPackMaster.AtcId, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, 0 AS Locaion_PK, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ, POPackDetails.ColorName, POPackDetails.SizeName, SUM(POPackDetails.PoQty) AS POQty, POPackDetails.PoPack_Detail_PK FROM POPackDetails INNER JOIN PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId INNER JOIN AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID GROUP BY POPackDetails.POPackId, POPackDetails.OurStyleID, PoPackMaster.AtcId, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, POPackDetails.ColorName, POPackDetails.SizeName, POPackDetails.PoPack_Detail_PK) AS tt LEFT OUTER JOIN PackingListDetails ON tt.PoPack_Detail_PK = PackingListDetails.PoPack_Detail_PK">
                 </asp:SqlDataSource>
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                     <ContentTemplate>
                         <div>
                                     <asp:Button ID="Button1" runat="server" Text="Save Packing Instruction" OnClientClick="return validateforserver()" OnClick="Button1_Click" />

                         </div>
                 


                           <div id="Messaediv" runat="server">
                                    <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                </div>
                     </ContentTemplate>
                 </asp:UpdatePanel>
             </ContentTemplate>
         </asp:UpdatePanel>

    </div>
</asp:Content>
