<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="LayesheetRoll.aspx.cs" Inherits="ArtWebApp.Production.Cutting.LayesheetRoll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    <link href="../../css/style.css" rel="stylesheet" />

    <script src="../../JQuery/GridJQuery.js"></script>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

       <script type="text/javascript">

        var gridID = "<%= tbl_RollDetails.ClientID %>";

    
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
          


           function newselection(objrev)
           {
               
               Check_Click(objrev)
             
               calculatesumofyardage();
              
           }

           function newAllselection(objrev) {
               checkAll(objrev)
              
               calculatesumofyardage();
              
           }

         


    
     

           function initDropDown(sender, args) {
               sender.behavior.set_zIndex(200000);
           }




           







</script>


 
   



 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="FullTable">
        <div class="RedHeaddingdIV">
            fabric lay sheet Rolls(LAY # creation)

        </div>
        <div>
            <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding"></td>
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
                        <td class="NormalTD" >cUT #</td>
                        <td class="NormalTD" >

                               <asp:TextBox ID="txt_cutnum" PlaceHolder="Enter Manual Cut#" runat="server"></asp:TextBox>
                        </td>
                        <td class="SearchButtonTD" >
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
                        <td class="NormalTD">Fabric</td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="upd_fabcolor" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ucc:DropDownListChosen ID="drp_fabcolor" runat="server" Width="200px" DataTextField="ItemDescription" DataValueField="Skudet_pk">
                                            </ucc:DropDownListChosen>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btn_color" runat="server" Text="S" OnClick="btn_color_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                        <td class="SearchButtonTD"></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
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
                        <td class="NormalTD">marker NO: </td>
                        <td class="NormalTD">
                               <asp:UpdatePanel ID="upd_markernum" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            
                            <ucc:DropDownListChosen ID="drp_markernum" runat="server"  DataTextField="name" DataValueField="pk" Width="200px">
                        </ucc:DropDownListChosen>
                                        </ContentTemplate>
                                        </asp:UpdatePanel>         
                                                
                                                
                                                </td>
                        <td class="SearchButtonTD"><asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_marker" runat="server" Text="S" OnClick="btn_marker_Click" /></ContentTemplate>
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD">cutorder Lay Length</td>
                        <td class="NormalTD">
                               <asp:UpdatePanel ID="Upd_markerLaylength" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:TextBox ID="txt_markerLaylength" runat="server" Enabled="False"></asp:TextBox>
                                                </ContentTemplate>
                                        </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">Lay Length with Tolerance</td>
                        <td class="NormalTD">
                             <asp:UpdatePanel ID="upd_Laylength" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:TextBox ID="txt_Laylength"  runat="server"></asp:TextBox>
                                                  </ContentTemplate>
                                        </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">Total Plies</td>Total Plies</td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="upd_noofplies" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_noofplies" runat="server" Enabled="False"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>

               
                    <tr>
                        <td class="NormalTD">Number of Plies&amp;Number of Plies&nbsp;Already&nbsp; Planned</td>
                        <td class="NormalTD">
                               <asp:UpdatePanel ID="upd_pliescut" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                       <asp:TextBox ID="txt_pliescut" runat="server" ReadOnly="True"></asp:TextBox>
                                   </ContentTemplate>
                               </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;Balance Plies to Plan</td>
                        <td class="NormalTD">
                             
                            <asp:UpdatePanel ID="upd_balplies" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_balplies" runat="server" Enabled="False" OnTextChanged="txt_balplies_TextChanged">0</asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                             
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">New Plies</td>
                        <td>
                            <asp:UpdatePanel ID="upd_newplies" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_newplies" AutoPostBack="true" runat="server" OnTextChanged="txt_newplies_TextChanged"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>

               
                    <tr>
                        <td class="NormalTD">Cut per Plies</td>
                        <td class="NormalTD">
                               <asp:UpdatePanel ID="upd_cutperplies" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                       <asp:TextBox ID="txt_cutperplies" runat="server"></asp:TextBox>
                                   </ContentTemplate>
                               </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">Approximate yards req</td>
                        <td class="NormalTD">
                             <asp:UpdatePanel ID="upd_apprximateyard" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                             <asp:Label ID="lbl_apprximateyard" runat="server" Text="0"></asp:Label>
                                         </ContentTemplate>
                               </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">Already cut</td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="upd_cutplies" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_cutplies" runat="server" Enabled="False" OnTextChanged="txt_balplies_TextChanged">0</asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
           
                    <tr>
                        <td class="NormalTD">Balance Plies to cut</td>
                        <td class="NormalTD">
                                 <asp:UpdatePanel ID="upd_balanceactualtocut" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                             <asp:Label ID="lbl_balancetocut" runat="server" Text="0"></asp:Label>
                                         </ContentTemplate>
                               </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                             &nbsp;</td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                    </tr>

                    </table>
        </div>
        <div>      <asp:UpdatePanel ID="upd_main"  UpdateMode="Conditional"  ChildrenAsTriggers="false"       runat="server">
                                <ContentTemplate>

                                    <div>

                                        <table class="tittlebar">
                   
              
               
                
              
               
                    <tr>
                        <td class="NormalTD">
                            <strong>cut order Details</strong></td>
                        
                    </tr>
                    <tr>
                        <td class="NormalTD" >


                            <asp:UpdatePanel ID="upd_secndtable" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            
                            <asp:Panel ID="panel2" runat="server" ViewStateMode="Enabled">
                                <asp:Table ID="Table2" runat="server" ViewStateMode="Enabled">
                                </asp:Table>
                            </asp:Panel>
                                                
                                                </ContentTemplate></asp:UpdatePanel>






                         
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="NormalTD"><strong>marker Details</strong></td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">
                             <asp:UpdatePanel ID="upd_table" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled" Width="200px">
                                        <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled">
                                        </asp:Table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>  </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                                           
                
               
                                           
                                           
                
               
                </table>


                                    </div>

 
                                       </ContentTemplate>
                            </asp:UpdatePanel></div>
        <div>

            <table>

                 <tr>
                                                <td class="SUBRedHeadding" colspan="3">sELECt rOLL</td>
                                            </tr>
                                            <tr>
                                                     <td>ShadeGroup</td>
                        <td class="NormalTD">   <asp:UpdatePanel ID="upd_shade" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <ig:WebDropDown ID="drp_shade" runat="server" EnableMultipleSelection="True" TextField="ShadeGroup"
                     ValueField="ShadeGroup" Width="200px" EnableDropDownAsChild="false">
                    <ClientEvents Initialize="initDropDown" />

                    <DropDownItemBinding TextField="ShadeGroup" ValueField="ShadeGroup" />
                </ig:WebDropDown>
                            </ContentTemplate>
        </asp:UpdatePanel>
                </td>



                        <td><asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" /></td>
                                            </tr>

            </table>
        </div>

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


                                           <asp:TemplateField HeaderText="Cut_NO" SortExpression="Cut_NO">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblCutNO" CssClass="txtaCutNO" runat="server" Text='<%# Bind("Cut_NO") %>'></asp:Label>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                            <%--      <asp:TextBox ID="txt_totalyard" Width="70px" CssClass="totalyardfooter" runat="server"></asp:TextBox>--%>

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
            <div> <asp:Button ID="btn_sumbit" runat="server" OnClick="btn_sumbit_Click" Text="Add Rolls" style="height: 26px" /></div>
        <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>

    
       

    </div>

    
    

                </div>
          
               
            </td>
        </tr>

       
    </table>
    
</asp:Content>

