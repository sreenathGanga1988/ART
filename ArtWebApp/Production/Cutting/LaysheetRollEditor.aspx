<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="LaysheetRollEditor.aspx.cs" Inherits="ArtWebApp.Production.Cutting.LaysheetRollEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  


       <script type="text/javascript">

   


           function newselection(objrev) {

               Check_Click(objrev)

               calculatesumofyardage();

           }

           function newAllselection(objrev) {
               checkAll(objrev)

               calculatesumofyardage();

           }

              function calculatesumofyardage()
        {
            var gridView = document.getElementById("<%= tbl_RollDetails.ClientID %>");
            var sum = 0
            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_ayard = gridView.rows[i].getElementsByClassName("txtayard")[0];

                    sum = sum + parseFloat(txt_ayard.innerHTML);
                }

            } 
            var totalyardfooter = document.getElementsByClassName("totalyardfooter")[0];
            totalyardfooter.value = sum;
        }

           

           function DeleteSelection(objref) {
               debugger
               var retVal = confirm("Do you want to continue  Deleting Roll from Cutplan ?");
               if (retVal == true) {
                  
                   

                   var row = objref.parentNode.parentNode;
                   alert(row);

                   var planpk = row.getElementsByClassName("lbl_LaySheetRoll_Pk")[0].innerHTML;
                   alert(planpk);
               
                   PageMethods.Deletelaysheetrollysnc(planpk, onSucess, onError);
                   function onSucess(result) {
                       alert(result);
                       objref.innerHTML = objref.innerHTML.strike();
                       objref.innerHTML = objref.innerHTML.fontcolor("red");

                       $(objref).closest('tr').children('td,th').css('background-color', '#000');
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
           .auto-style1 {
               font-weight: bold;
           }
       </style>


 
   



 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">fabric lay sheet</td>
        </tr>
        <tr>
            <td>


                <div>

                    <table>
                         <tr>
                        <td class="NormalTD" >fACTORY</td>
                        <td class="NormalTD" >
                               <asp:UpdatePanel ID="UPD_FACT" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                               <ucc:DropDownListChosen ID="drp_fact" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                               </ucc:DropDownListChosen>
                                      </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD
                            ">&nbsp;</td>
                        <td class="NormalTD" >&nbsp;</td>
                        <td class="NormalTD" >

                               &nbsp;</td>
                        <td class="ButtonTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">atc&nbsp; : </td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="upd_atc" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <ucc:DropDownListChosen ID="drp_atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btn_atc" runat="server" OnClick="btn_atc_Click" Text="S" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">ourstyle&nbsp; #</td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="upd_ourstyle" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btn_OURSTYLE" runat="server" OnClick="btn_OURSTYLE_Click" Text="S" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
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
                       <td class="SearchButtonTD"><asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_cutorder" runat="server"  Text="S" OnClick="btn_cutorder_Click" /></ContentTemplate>
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD">lay Sheet Roll #</td>
                        <td class="NormalTD">
                            
                               <asp:UpdatePanel ID="upd_layroll" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                       <ucc:DropDownListChosen ID="drp_cutRoll" runat="server" DataTextField="LayRollRef" DataValueField="LaysheetRollmaster_Pk" Width="200px">
                                       </ucc:DropDownListChosen>
                                   </ContentTemplate>
                               </asp:UpdatePanel>
                                                </td>
                       <td class="SearchButtonTD">
                           <asp:Button ID="btn_showroll" runat="server" OnClick="btn_showroll_Click" Text="S" />
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                             &nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>

               
                    <tr>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                               &nbsp;</td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                             &nbsp;</td>

                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>

               
                    </table>

                </div>
               
            </td>
        </tr>
        <tr>

            <td>
                <div class="RedHeaddingDiv">Add new Rolls</div>
                <div>

                    <asp:UpdatePanel ID="upd_grid"  UpdateMode="Conditional"  runat="server">
                  <ContentTemplate>
                <asp:GridView ID="tbl_RollDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Roll_PK" ShowFooter="True">
                                    <Columns>
                                             <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="newAllselection(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="newselection(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Roll_PK" InsertVisible="False" SortExpression="Roll_PK">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="ASN" HeaderText="ASN" SortExpression="ASN" ReadOnly="True" />
                                        <asp:BoundField DataField="SShade" HeaderText="SShade" SortExpression="SShade" />
                                                                    
                                          
                                         
                                          
                                           <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                                           <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                                           <asp:BoundField DataField="SWidth" HeaderText="SWidth" SortExpression="SWidth" />
                                        <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                                        <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                                        <asp:BoundField DataField="SShrink" HeaderText="SShrink" SortExpression="SShrink" />
                                        <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                                        <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                                           <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                                             <asp:TemplateField HeaderText="Roll Status" SortExpression="RollStatus">
                                              
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_rollstatus" runat="server" Text='<%# Bind("RollStatus") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="AYard" SortExpression="AYard">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_ayard" CssClass="txtayard" runat="server" Text='<%# Bind("AYard") %>'></asp:Label>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                                  <asp:TextBox ID="txt_totalyard" Width="70px" CssClass="totalyardfooter" runat="server"></asp:TextBox>

                                              </FooterTemplate>
                                             </asp:TemplateField>
                                         
                                                                            
                                             <asp:TemplateField HeaderText="Plies" Visible="False">
                                                 
                                                 <ItemTemplate>
                                                    <asp:TextBox ID="txt_plies"  Text="0" CssClass="txtPlies" Width="70px" onkeypress="return isNumberKey(event,this)"  onkeyup ="sumofQty(this)"  onchange="calculatePliesSum()" runat="server" ></asp:TextBox>
                                                 </ItemTemplate>
                                                    <FooterTemplate>

                                          <asp:TextBox ID="lbl_qtyfooter" CssClass="qtyfooter" Width="70px" runat="server" ></asp:TextBox>
                                   </FooterTemplate>

                              
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Fab Utilized" Visible="False">
                                                 
                                                 <ItemTemplate>
                                                      <asp:TextBox ID="txt_fab"  Text="0" Width="70px" CssClass="txtFab" onkeypress="return isNumberKey(event,this)"  onkeyup="sumofQty(this)" onChange="calculatesumoffab()"   runat="server" ></asp:TextBox>
                                                 </ItemTemplate>

                                                    <FooterTemplate>
                                                  <asp:TextBox ID="txt_totalfab" Width="70px" CssClass="totalfabfooter" runat="server"></asp:TextBox>

                                              </FooterTemplate>

                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Balance" Visible="False">
                                                 
                                                 <ItemTemplate>
                                                     <asp:TextBox ID="txt_txtBalance" Text="0"  CssClass="txtbal" Width="70px" onkeypress="return isNumberKey(event,this)"  onkeyup="sumofQty(this)"  runat="server" ></asp:TextBox>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField  HeaderText="Excess/Short" Visible="False">
                                                
                                                 <ItemTemplate>
                                                      <asp:TextBox ID="txt_excessshort"  CssClass="txt_excessshort" Text="0" Width="70px" onkeypress="return isNumberKey(event,this)"  onkeyup="sumofQty(this)"  runat="server" ></asp:TextBox>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                                   <asp:TemplateField  HeaderText="Re Cuttable" Visible="False">
                                                
                                                 <ItemTemplate>
                                                      <asp:CheckBox ID="chk_cutable" runat="server" ></asp:CheckBox>
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
                            </asp:UpdatePanel>

                </div>

            </td>
        </tr>
        <tr>
            <td>
                <strong>
                <asp:Button ID="btn_addroll" runat="server" CssClass="auto-style1" OnClick="btn_addroll_Click" Text="Add Roll" />
                </strong>
                
            
                
               
        </tr>
        

        
        <tr>
            <td>
                <div class="RedHeaddingDiv">Already Added to laysheet</div>
                <div>

                    <asp:UpdatePanel ID="upd_alreadygridgrid"  UpdateMode="Conditional"  runat="server">
                <ContentTemplate>
                    <asp:GridView ID="tbl_AlreadyRollDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Roll_PK" ShowFooter="True" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                               
                                <ItemTemplate>
                                    <asp:Label ID="chk_select" Text="Delete" runat="server" onclick="DeleteSelection(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll_PK" InsertVisible="False" SortExpression="Roll_PK">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pk" InsertVisible="False" SortExpression="LaySheetRoll_Pk">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_LaySheetRoll_Pk" CssClass="lbl_LaySheetRoll_Pk" runat="server" Text='<%# Bind("LaySheetRoll_Pk") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                            <asp:BoundField DataField="ASN" HeaderText="ASN" ReadOnly="True" SortExpression="ASN" />
                            <asp:BoundField DataField="SShade" HeaderText="SShade" SortExpression="SShade" />
                            <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                            <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                            <asp:BoundField DataField="SWidth" HeaderText="SWidth" SortExpression="SWidth" />
                            <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                            <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                            <asp:BoundField DataField="SShrink" HeaderText="SShrink" SortExpression="SShrink" />
                            <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                            <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                            <asp:BoundField DataField="SYard" HeaderText="SYard" SortExpression="SYard" />
                            <asp:TemplateField HeaderText="AYard" SortExpression="AYard">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ayard" runat="server" CssClass="txtayard" Text='<%# Bind("AYard") %>'></asp:Label>
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
                </ContentTemplate>
                            </asp:UpdatePanel>
                </div>
                
            
                
               
        </tr>
        

        
        <tr>
            <td>
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
    </table>
    
</asp:Content>
