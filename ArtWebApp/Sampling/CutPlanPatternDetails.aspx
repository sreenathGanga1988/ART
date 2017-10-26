<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutPlanPatternDetails.aspx.cs" Inherits="ArtWebApp.Sampling.CutPlanPatternDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
  
    <link href="../css/style.css" rel="stylesheet" />
    
    <script src="../../JQuery/GridJQuery.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

   <script type="text/javascript">




         function SetTolerance() {
			//if (Searching == 'eID') {
		   
             debugger;
		    var num = document.getElementById('<%= txt_toleranceall.ClientID %>').value;
             var txt_newtolerance = document.getElementsByClassName("txt_newtolerance");

             for (var rowId = 0; rowId < txt_newtolerance.length; rowId++) {
                 txt_newtolerance[rowId].value=num.toString();
                

                

              }
             CalculatefabRequired();
         }


       
       function Setmarkernum() {
			//if (Searching == 'eID') {
		   
           debugger;
          
		    var num = document.getElementById('<%= txt_markernumprefix.ClientID %>').value;
           var grdvw = document.getElementById('<%= tbl_cutorderdata.ClientID %>');

           for (var rowId = 1; rowId < grdvw.rows.length; rowId++)
           {
               var lbl_markernum = grdvw.rows[rowId].getElementsByClassName("lbl_markernum")[0].innerHTML;

               var newmarkernum = num.toString() + " " + lbl_markernum.toString();
               var txt_newmarkernum = grdvw.rows[rowId].getElementsByClassName("txt_newmarkernum")[0];
               txt_newmarkernum.value = newmarkernum.toString();
               

           }
         
         }


         function CalculatefabRequired() {
			//if (Searching == 'eID') {
		   
           debugger;
          
           var grdvw = document.getElementById('<%= tbl_cutorderdata.ClientID %>');

             var totalfabreq = 0;
             var totalqty = 0;
           for (var rowId = 1; rowId < grdvw.rows.length; rowId++)
           {
               var noofplies=0;
               var markerlength=0;
               var tolerence=0;
               var qty=0;
               var lbl_noofplies = grdvw.rows[rowId].getElementsByClassName("lbl_noofplies")[0].innerHTML;

               var txt_newMarkerlength = grdvw.rows[rowId].getElementsByClassName("txt_newMarkerlength")[0].value;

               var txt_newtolerance = grdvw.rows[rowId].getElementsByClassName("txt_newtolerance")[0].value;
               
               try {
                   noofplies=parseFloat(lbl_noofplies.toString());
                   }
               catch(err) {
                   noofplies = 0;
                   grdvw.rows[rowId].getElementsByClassName("lbl_noofplies")[0].innerHTML = 0;
               }


               try {
                   markerlength = parseFloat(txt_newMarkerlength.toString());
               }
               catch (err) {
                   markerlength = 0;
                   grdvw.rows[rowId].getElementsByClassName("txt_newMarkerlength")[0].value = 0;
               }


               try {
                   tolerence = parseFloat(txt_newtolerance.toString());
                   tolerence = tolerence * 0.02778

                  
               }
               catch (err) {
                   tolerence = 0;
                   grdvw.rows[rowId].getElementsByClassName("txt_newtolerance")[0].value = 0;
               }


               var fabreq = noofplies * (markerlength + tolerence);

              
               var txt_fabreq = grdvw.rows[rowId].getElementsByClassName("txt_fabreq")[0];
               txt_fabreq.value = fabreq.toString();



               
               try {
                   qty = parseFloat(grdvw.rows[rowId].getElementsByClassName("totalQtyRow")[0].value.toString());
               }
               catch (err) {
                   qty = 0;
                  
               }

               totalqty=totalqty+parseFloat(qty.toString());
               totalfabreq = totalfabreq + parseFloat(fabreq.toString());
           }
           
           var avgreq = parseFloat(totalfabreq.toString()) / parseFloat(totalqty.toString());
           
           var bomcon = document.getElementsByClassName("lbl_newConsumptionfab")[0].innerHTML;

           if (parseFloat(avgreq) > parseFloat(bomcon))
           {
               //alert("Consumption greater Than BOM Consumtpion");
               document.getElementsByClassName("txt_overallConsumptionfab")[0].parentNode.bgColor = "yellow";
           }

           document.getElementsByClassName("txt_overallConsumptionfab")[0].value = avgreq.toString();
           document.getElementsByClassName("txt_overallfabreq")[0].value = totalfabreq.toString();
         }


       function CalculateEfficency() {
           //if (Searching == 'eID') {

           debugger;

           var effqtytotal = 0;
           var sumqtyrow = 0;

           var grdvw = document.getElementById('<%= tbl_cutorderdata.ClientID %>');

           for (var rowId = 1; rowId < grdvw.rows.length; rowId++)
           {
               var grdrow = grdvw.rows[rowId];
               
               

               var txt_eff = grdrow.getElementsByClassName("txt_eff")[0].value;

               var totalQtyRow = grdrow.getElementsByClassName("totalQtyRow")[0].value;

               effqtytotal = effqtytotal + (parseFloat(txt_eff.toString()) * parseFloat(totalQtyRow.toString()))
               sumqtyrow = sumqtyrow + parseFloat(totalQtyRow.toString());
               
           }

           var finaleff = parseFloat(effqtytotal.toString()) / parseFloat(sumqtyrow.toString());



           document.getElementsByClassName("txt_overefficency")[0].value = finaleff.toString();
           

       }


       function Addpattern(objref) {
           debugger
           var retVal = confirm("Do you want to continue  Add New Pattern from Cutplan ?");
           if (retVal == true) {
            
               var planpk = document.getElementById('<%= lbl_cutid.ClientID %>').innerHTML;
               var txtname = document.getElementById('<%= txt_patternmaenew.ClientID %>').value;
               alert(planpk);
               alert(txtname);
               PageMethods.Addpatternaysncmethod(planpk,txtname, onSucess, onError);
               function onSucess(result) {
                   alert(result);
                   document.getElementById('<%= btn_addpattern.ClientID %>').style.visibility = 'hidden';
                   
               
               }
               function onError(result) {
                   alert('Something wrong.');
               }
           }
           else {

           }




       }

</script>



 
    <style type="text/css">
        .auto-style8 {
            height: 27px;
        }
        .smalltableforapplyall {
            width: 100%;
            background-color:silver;
        }
        .smalltablefordisplay{
             width: 100%;
             background-color:lightskyblue;
             font-size:smaller;
        }
         .smallTD{
            width:80px;
             background-color:aquamarine;
             font-size:smaller;
        }
        

         .gridcolumn{
             font-size:smaller;
         }
        .auto-style9 {
            width: 100%;
        }
        .auto-style10 {
            width: 82px;
            background-color: aquamarine;
            font-size: smaller;
        }
        .auto-style11 {
            background-color: aquamarine;
            font-size: smaller;
        }
    </style>



 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">CUT ORDER</td>
        </tr>
        <tr>
            <td class="RedHeadding">cut order size details</td>
        </tr>
        <tr>
           <td class="NormalTD">
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
                            <ucc:DropDownListChosen ID="drp_cutorder" CssClass="drp_cutorder" runat="server"  DataTextField="name" DataValueField="pk" Width="200px">
                        </ucc:DropDownListChosen>
                                                
                                                
                                                
                                     </ContentTemplate>
                                        </asp:UpdatePanel>            
                                                </td>
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_cutorder" runat="server"  Text="S" OnClick="btn_cutorder_Click" /></ContentTemplate>
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD">
                            <asp:Button ID="Button4" runat="server" Font-Size="Smaller" OnClick="Button4_Click" Text="Show Report" />
                        </td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="auto-style7">
                            <asp:UpdatePanel ID="upd_cutid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_cutid" runat="server" Text="0"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD" colspan="8">
                            
                            <asp:UpdatePanel ID="upd_table" UpdateMode="Conditional" runat="server">
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
                        <td class="NormalTD" colspan="8">
                            <table class="DataEntryTable">
                                <tr>
                                    <td class="RedHeadding" colspan="6">Cut Order Details </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8" colspan="6">
                                        <asp:UpdatePanel ID="Upd_cutplandetails" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="smalltablefordisplay">
                                                    <tr>
                                                        <td class="smallTD">Atc</td>
                                                        <td class="smallTD">
                                                            <strong>
                                                            <asp:Label ID="lbl_atc" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td class="smallTD">ourstyle</td>
                                                        <td class="smallTD">
                                                            <strong>
                                                            <asp:Label ID="lbl_ourstyle" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                        <td class="smallTD">Location</td>
                                                        <td class="smallTD">
                                                            <asp:Label ID="lbl_location" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                        <td class="smallTD">Ref pattern# :</td>
                                                        <td class="smallTD">
                                                            <strong>
                                                            <asp:Label ID="lbl_refnum" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td class="auto-style10">Fabrication</td>
                                                        <td class="smallTD">
                                                            <asp:Label ID="lbl_fabrication" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="smallTD">Cutable Width</td>
                                                        <td class="smallTD">
                                                            <strong>
                                                            <asp:Label ID="lbl_with" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td class="smallTD">Shrinkage</td>
                                                        <td class="smallTD">
                                                            <strong>
                                                            <asp:Label ID="lbl_shrink" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td class="smallTD">marker Type</td>
                                                        <td class="smallTD">
                                                            <strong>
                                                            <asp:Label ID="lbl_Markertype" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td class="smallTD">Cutting Mode</td>
                                                        <td class="smallTD">
                                                            <strong>
                                                            <asp:Label ID="lbl_markermade" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td class="smallTD">BOM Consumption</td>
                                                        <td class="smallTD"><strong>
                                                            <asp:Label ID="lbl_bomconsumption" runat="server" CssClass="lbl_newConsumptionfab" Text="0"></asp:Label>
                                                            </strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="smallTD">Max length</td>
                                                        <td class="smallTD">
                                                           
                                                            <asp:Label ID="lbl_maxLenth" runat="server" Text="0"></asp:Label>
                                                           
                                                        </td>
                                                        <td class="smallTD">fabric</td>
                                                        <td class="auto-style11" colspan="3">
                                                            <strong>
                                                            <asp:Label ID="lbl_fabric" runat="server" Font-Size="X-Small" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td class="smallTD">Cut oty</td>
                                                        <td class="smallTD">
                                                            <strong>
                                                            <asp:Label ID="lbl_cutQty" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                        <td class="smallTD">Buyer Style</td>
                                                        <td class="smallTD">
                                                            <asp:Label ID="lbl_buyerstyle" runat="server" Font-Size="X-Small" Text="0"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD" colspan="7" >
                            <div>


                                <table class="smalltableforapplyall">
                                    <tr>
                                       <td class="NormalTD">MarkerNUM prefix</td>
                                       <td class="NormalTD"><asp:TextBox ID="txt_markernumprefix" CssClass="txt_markernumprefix" runat="server"></asp:TextBox></td>
                                       <td class="NormalTD"><asp:Button ID="Button1" runat="server" Text="Apply To All" Font-Size="Smaller" OnClientClick="Setmarkernum()" /></td>
                                       <td class="NormalTD">
                                            lay margin(inches)
                                        </td>
                                         <td class="NormalTD">
                                            <asp:TextBox ID="txt_toleranceall" Text="1" CssClass="txt_toleranceall" runat="server"></asp:TextBox>
                                        </td>
                                         <td class="NormalTD">
                                            <asp:Button ID="Button2" runat="server" Text="Apply to All" OnClientClick="SetTolerance()" Font-Size="Smaller" OnClick="Button2_Click"/>
                                        </td>
                                        <td>New&nbsp; pattern name</td>
                                        <td>
                                            <asp:UpdatePanel ID="upd_patternmaenew" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                            <asp:TextBox ID="txt_patternmaenew" CssClass="txt_patternmaenew" runat="server"></asp:TextBox>
                                                </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>


                            </div>
                             
                          
                        </td>
                        <td class="NormalTD" >
                           
                            <asp:UpdatePanel ID="upd_addpatern" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
 <asp:Button ID="btn_addpattern" runat="server" style="font-size: x-small" Text="Add"  OnClientClick="Addpattern(this)" OnClick="btn_addpattern_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="gridcolumn"  colspan="7">
                            <asp:UpdatePanel ID="upd_grid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    
                                
                                    <asp:GridView ID="tbl_cutorderdata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CutPlanMarkerDetails_PK" DataSourceID="CutplanmarkerData" OnDataBound="tbl_cutorderdata_DataBound" OnRowCommand="tbl_cutorderdata_RowCommand" OnRowDataBound="tbl_cutorderdata_RowDataBound" OnSelectedIndexChanged="tbl_cutorderdata_SelectedIndexChanged" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Font-Size="Smaller">
                                        <Columns>
                                            <asp:TemplateField HeaderText="PK" InsertVisible="False" SortExpression="CutOrderDet_PK" ControlStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_CutOrderDet_PK" runat="server" Text='<%# Bind("CutPlanMarkerDetails_PK") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="hidden" />
                                                <HeaderStyle CssClass="hidden" />
                                                <ItemStyle CssClass="hidden" />
                                            </asp:TemplateField>
                                            <asp:TemplateField  SortExpression="MarkerNo">
                                                <ItemTemplate>
                                                    <table style=" width: inherit; border-style: solid;  background-color: #FFFFFF" >
                                                        <tr>
                                                           <td class="NormalTD">Marker# : </td>
                                                           <td class="NormalTD">
                                                                <asp:Label ID="lbl_markernum" CssClass="lbl_markernum" runat="server" Text='<%# Bind("MarkerNo") %>' Font-Size="Smaller" Width="50px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td class="NormalTD">NoOfPlies : </td>
                                                           <td class="NormalTD">
                                                                <asp:Label ID="lbl_noofplies" CssClass="lbl_noofplies" Font-Size="Smaller" Width="50px" runat="server" Text='<%# Bind("NoOfPlies") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td class="NormalTD">Max Plies : </td>
                                                           <td class="NormalTD">
                                                                <asp:Label ID="lbl_cutperplies" CssClass="lbl_cutperplies" Font-Size="Smaller" Width="50px" runat="server" Text='<%# Bind("CutPerPlies") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td class="NormalTD">No of Cut req :</td>
                                                           <td class="NormalTD">
                                                                <asp:Label ID="Label4" runat="server" Font-Size="Smaller" Width="50px" Text='<%# Bind("Cutreq") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                           <td class="NormalTD">Lay margin : </td>
                                                           <td class="NormalTD">
                                                                <asp:TextBox ID="txt_newtolerance"  CssClass="txt_newtolerance"  Font-Size="Smaller" Width="50px" onchange=" CalculatefabRequired()"  runat="server" Text="1" Height="16px"></asp:TextBox>
                                                              
                                                            </td>
                                                           
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Marker"  SortExpression="Marker">
                                                <ItemTemplate>
                                                     <table style=" width: inherit; border-style: solid; background-color: #FFFFFF">
                                                        <tr>
                                                           <td class="NormalTD">Marker Num</td>
                                                            <td style="width:50px">
                                                                <asp:TextBox ID="txt_newmarkernum" CssClass="txt_newmarkernum" runat="server"  Font-Size="Smaller" Width="150px" Text='<%# Bind("PaternMarkerName") %>'  ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td class="NormalTD">Marker Length</td>
                                                           <td class="NormalTD">
                                                                <asp:TextBox ID="txt_newMarkerlength" CssClass="txt_newMarkerlength"   Font-Size="Smaller" Width="50px"  runat="server" onchange=" CalculatefabRequired()" Text='<%# Bind("MarkerLength") %>'  ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                           <td class="NormalTD">Eff %: </td>
                                                           <td class="NormalTD">
                                                                 
                                                                <asp:TextBox ID="txt_eff" CssClass="txt_eff" onchange=" CalculateEfficency()" Text='<%# Bind("Efficiency") %>'  runat="server" Font-Size="Smaller" Width="50px"  Height="16px" ></asp:TextBox>
                                                            </td>
                                                           
                                                        </tr>
                                                    
                                                        <tr>
                                                           <td class="NormalTD">Fab req</td>
                                                           <td class="NormalTD">
                                                                <asp:TextBox ID="txt_fabreq" EnableViewState="true" CssClass="txt_fabreq"  Text='<%# Bind("TotalfabReq") %>'  Font-Size="Smaller" Width="50px"  runat="server" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MarkerDetails" SortExpression="MarkerDetails">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="upd_table" runat="server" UpdateMode="Conditional">
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
                                            <%--      <asp:ButtonField CommandName="Add" Text="Add" ButtonType="Button" />--%>
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
           <td class="NormalTD">
                        
                    <table class="smalltablefordisplay">
                        <tr>
                            <td class="smallTD">Over All efficiency</td>
                            <td class="smallTD"><strong>
                                <asp:TextBox ID="txt_overefficency" CssClass="txt_overefficency" runat="server" Text="0"></asp:TextBox>
                                </strong></td>
                            <td class="smallTD">Overal Consumption</td>
                            <td class="smallTD"><strong>
                                <asp:TextBox ID="txt_overallConsumption" CssClass="txt_overallConsumptionfab" runat="server" Text="0" ></asp:TextBox>
                                </strong></td>
                            <td class="smallTD">Total Fab Req</td>
                            <td class="smallTD">
                                <asp:TextBox ID="txt_overallfabreq" runat="server" CssClass="txt_overallfabreq" Text="0"></asp:TextBox>
                            </td>
                            <td class="smallTD">&nbsp;</td>
                            <td class="smallTD">&nbsp;</td>
                        </tr>
                        
                        
                    </table>
                        
                    </td>
        </tr>
        <tr>
           <td class="NormalTD">
                <asp:UpdatePanel ID="upd_markertype" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="tbl_markertype" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Bold="True" Font-Size="Smaller" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="CutPlanmarkerTypeName" HeaderText="Marker Direction" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
           <td class="NormalTD">
                <div>

                    <table class="auto-style9">
                        <tr>
                           <td class="NormalTD">uPLOAD MARKER (zip)</td>
                           <td class="NormalTD">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                           <td class="NormalTD">&nbsp;</td>
                        </tr>
                    </table>

                </div></td>
        </tr>
        <tr>
           <td class="NormalTD">
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Save  Marker Details " />
                </td>
        </tr>
        <tr>
           <td class="NormalTD">
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
                           


                     
               </div></td>
        </tr>
    </table>
    <asp:SqlDataSource ID="CutplanmarkerData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT Cutreq, CutPerPlies, NoOfPlies, LayLength, MarkerLength, Qty, NoOfPc, MarkerNo, CutPlan_PK, CutPlanMarkerDetails_PK, Tolerancelength, TotalfabReq, MarkerDetAddedBy, MarkerDetAddedDate, PatternAddedDate, PatternAddedBy, PaternMarkerName, Efficiency FROM CutPlanMarkerDetails WHERE (CutPlan_PK = @CutID)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="drp_cutorder" DefaultValue="0" Name="CutID" PropertyName="SelectedValue" Type="Decimal" />
                                        </SelectParameters>
                                    </asp:SqlDataSource><asp:SqlDataSource ID="cutplanmarkertypedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutPlanMarkerTypes_PK], [CutPlanmarkerTypeName] FROM [CutPlanMarkerType] WHERE ([CutPlan_PK] = @CutPlan_PK)">
        <SelectParameters>
            <asp:ControlParameter ControlID="drp_cutorder" Name="CutPlan_PK" PropertyName="SelectedValue" Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
