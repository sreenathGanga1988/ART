<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Laysheet.aspx.cs" Inherits="ArtWebApp.Production.Cutting.Laysheet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    <link href="../../css/style.css" rel="stylesheet" />

    <script src="../../JQuery/GridJQuery.js"></script>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

       <script type="text/javascript">

       //calculate the sum of qty on keypress
      <%-- function sumofQty(objText) {
       

           debugger;
           enter(objText)

            if (document.getElementById('<%= txt_Laylength.ClientID %>').value=="")
           {
                 alert("Laylength can not be blank");
                 document.getElementById("<%=txt_Laylength.ClientID%>").focus();
                 return false;
           }
            else {
                var cell = objText.parentNode;
                var row = cell.parentNode;
                var txtconsumption = row.getElementsByClassName("txtFab");

                var txtplies = row.getElementsByClassName("txtPlies");

                var fabutilised = parseFloat(document.getElementById('<%= txt_Laylength.ClientID %>').value) * parseFloat(txtplies[0].value);
                txtconsumption[0].value = fabutilised.toString()
            }      
        
         

       }--%>

    
    
           function sumofQty(objText) {
       

           debugger;
          
           enter(objText)

            if (document.getElementById('<%= txt_Laylength.ClientID %>').value=="")
           {
                 alert("Laylength can not be blank");
                 document.getElementById("<%=txt_Laylength.ClientID%>").focus();
                 return false;
           }
            else {
                var cell = objText.parentNode;
                var row = cell.parentNode;
                var txtconsumption = row.getElementsByClassName("txtFab");

                var txtplies = row.getElementsByClassName("txtPlies");

                var ayard = row.getElementsByClassName("txtayard");
                var txtbal = row.getElementsByClassName("txtbal");
                var txt_excessshort = row.getElementsByClassName("txt_excessshort");
               
                var fabutilised = parseFloat(document.getElementById('<%= txt_Laylength.ClientID %>').value) * parseFloat(txtplies[0].value);
                txtconsumption[0].value = fabutilised.toString()


                var excessshortage = parseFloat(ayard[0].innerText) - (parseFloat(txtconsumption[0].value) + parseFloat(txtbal[0].value))
                txt_excessshort[0].value = excessshortage.toString()



            }      
        
         

       }



</script>


 
   



 
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">fabric lay sheet</td>
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
                        <td><asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
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
                        <td class="ButtonTD" >
                            <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_OURSTYLE" runat="server" Text="S" OnClick="btn_OURSTYLE_Click" /></ContentTemplate>
                                        </asp:UpdatePanel>  
                        </td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
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
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel8" runat="server">
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
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            <asp:Button ID="btn_marker" runat="server" Text="S" OnClick="btn_marker_Click" /></ContentTemplate>
                                        </asp:UpdatePanel></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                    </tr>
              
               
                    <tr>
                        <td class="NormalTD">cutorder Lay Length</td>
                        <td class="NormalTD">
                            <asp:TextBox ID="txt_markerLaylength" runat="server" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">New lay length</td>
                        <td class="NormalTD">
                            <asp:TextBox ID="txt_Laylength"  runat="server"></asp:TextBox>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
              
               
                
              
               
                    <tr>
                        <td class="NormalTD" colspan="7">
                            <strong>Marker Details</strong></td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" colspan="7">


                            <asp:UpdatePanel ID="upd_secndtable" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                            
                            <asp:Panel ID="panel2" runat="server" ViewStateMode="Enabled">
                                <asp:Table ID="Table2" runat="server" ViewStateMode="Enabled">
                                </asp:Table>
                            </asp:Panel>
                                                
                                                </ContentTemplate></asp:UpdatePanel>






                         
                        </td>
                        <td class="NormalTD"></td>
                    </tr>
                    <tr>
                        <td class="NormalTD" colspan="7"><strong>Cutorder Details</strong></td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD" colspan="7">
                             <asp:UpdatePanel ID="upd_table" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                        <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled">
                                        </asp:Table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>  </td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                  <tr>
                        <td class="NormalTD">Number of Plies&nbsp; Cut</td>
                        <td class="NormalTD"><asp:UpdatePanel ID="upd_pliescut" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate><asp:TextBox ID="txt_pliescut" runat="server"></asp:TextBox>  </ContentTemplate></asp:UpdatePanel></td>
                        <td class="NormalTD">
                           
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
               
                </table>
                                       </ContentTemplate>
                            </asp:UpdatePanel>
               
            </td>
        </tr>


        <tr>
            <td><asp:UpdatePanel ID="upd_grid"  UpdateMode="Conditional"  runat="server">
                  <ContentTemplate>
                <asp:GridView ID="tbl_RollDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Roll_PK">
                                    <Columns>
                                             <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
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
                                        <asp:BoundField DataField="SYard" HeaderText="SYard" SortExpression="SYard" />
                                             <asp:TemplateField HeaderText="AYard" SortExpression="AYard">
                                                
                                                 <ItemTemplate>
                                                     <asp:Label ID="lbl_ayard" CssClass="txtayard" runat="server" Text='<%# Bind("AYard") %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         
                                                                            
                                             <asp:TemplateField HeaderText="Plies">
                                                 
                                                 <ItemTemplate>
                                                    <asp:TextBox ID="txt_plies"  Text="0" CssClass="txtPlies" Width="70px" onkeypress="return isNumberKey(event,this)"  onkeyup ="sumofQty(this)" runat="server" ></asp:TextBox>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Fab Utilized">
                                                 
                                                 <ItemTemplate>
                                                      <asp:TextBox ID="txt_fab"  Text="0" Width="70px" CssClass="txtFab" onkeypress="return isNumberKey(event,this)"  onkeyup="sumofQty(this)"  runat="server" ></asp:TextBox>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Balance">
                                                 
                                                 <ItemTemplate>
                                                     <asp:TextBox ID="txt_txtBalance" Text="0"  CssClass="txtbal" Width="70px" onkeypress="return isNumberKey(event,this)"  onkeyup="sumofQty(this)"  runat="server" ></asp:TextBox>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:TemplateField  HeaderText="Excess/Short">
                                                
                                                 <ItemTemplate>
                                                      <asp:TextBox ID="txt_excessshort"  CssClass="txt_excessshort" Text="0" Width="70px" onkeypress="return isNumberKey(event,this)"  onkeyup="sumofQty(this)"  runat="server" ></asp:TextBox>
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
        <tr>
            <td>
                <asp:Button ID="btn_sumbit" runat="server" OnClick="btn_sumbit_Click" Text="Create Laysheet" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
    </table>
    
</asp:Content>
