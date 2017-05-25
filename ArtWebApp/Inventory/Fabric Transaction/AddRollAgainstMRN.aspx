<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AddRollAgainstMRN.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.AddRollAgainstMRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <script src="../../JQuery/GridJQuery.js"></script>--%>
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
    <script type="text/javascript" >

      


        function Onselection(objref)
        {
            debugger;
            Check_Click(objref)
            calculatesumofyardage();
        }

        function OnSelectAllClick(objref) {
            checkAll(objref)
            calculatesumofyardage();
        }


        //function to calculate the sum of user enter
        function calculatesumofyardage()
        {
            var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
            var sum = 0
            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_syard = gridView.rows[i].getElementsByClassName("txt_syard")[0];

                    sum = sum + parseFloat(txt_syard.value);
                }

            } 
            var totalyardfooter = document.getElementsByClassName("totalyardfooter")[0];
            totalyardfooter.value = sum;
        }



        function CopyYardage()
        {
            var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
            var txt_yardage = document.getElementsByClassName("txt_yardage")[0];
            var sum = 0
            valuenow=0
            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_syard = gridView.rows[i].getElementsByClassName("txt_syard")[0];

                    txt_syard.value = txt_yardage.value;
                }
            }

            calculatesumofyardage();
        }


          function CopyRemark()
        {
            var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
              var txt_remark1 = document.getElementsByClassName("txt_remark")[0];

            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_remark = gridView.rows[i].getElementsByClassName("txt_remark")[0];

                    txt_remark.value = txt_remark1.value;
                }
            }
          }

          function CopyShade()
        {
            var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
              var txt_shade = document.getElementsByClassName("txt_shade")[0];

            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_Sshade = gridView.rows[i].getElementsByClassName("txt_Sshade")[0];

                    txt_Sshade.value = txt_shade.value;
                }
            }
          }

         function CopyShinkage()
        {
            var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
             var txt_Shrinkage = document.getElementsByClassName("txt_Shrinkage")[0];

            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                  
                    var txt_sshrinkage = gridView.rows[i].getElementsByClassName("txt_sshrinkage")[0];

                    txt_sshrinkage.value = txt_Shrinkage.value;
                }
            }
         }

          function Copywidth()
        {
            var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
              var txt_width = document.getElementsByClassName("txt_width")[0];

            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_width1 = gridView.rows[i].getElementsByClassName("txt_width1")[0];

                    txt_width1.value = txt_width.value;
                }
            }
          }

          function Copygsm()
        {
            var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
              var txt_gsm = document.getElementsByClassName("txt_gsm")[0];

            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_gms1 = gridView.rows[i].getElementsByClassName("txt_gms1")[0];

                    txt_gms1.value = txt_gsm.value;
                }
            }
          }


          function Copyweight()
        {
            var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");
              var txt_weight = document.getElementsByClassName("txt_weight")[0];

            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_weight1 = gridView.rows[i].getElementsByClassName("txt_weight1")[0];

                    txt_weight1.value = txt_weight.value;
                }
            }
          }




        function calculateforKnit()
        {
             var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");

            var fablength=0;


            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_weight1 = gridView.rows[i].getElementsByClassName("txt_weight1")[0];
                    var txt_width1 = gridView.rows[i].getElementsByClassName("txt_width1")[0];
                    var txt_gms1 = gridView.rows[i].getElementsByClassName("txt_gms1")[0];

                    var txt_syard = gridView.rows[i].getElementsByClassName("txt_syard")[0];
                   
                    fablength = parseFloat(txt_weight1.value) / (((parseFloat(txt_width1.value) * parseFloat(txt_gms1.value)) / 1550) / 1000);
                    fablength = fablength * 0.0277778;
                    txt_syard.value = fablength;
                  
                }
            }


           

        }
















    </script>
    <style type="text/css">
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    

    
        <table class="FullTable">

            <tr>
                <td>
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="5">
                        &nbsp;&nbsp;&nbsp;&nbsp; MRN ROlls Details</td>
                </tr>
                <tr>
                    <td">
                       
                    </td>
                    <td >
                          
                        Atc

                    </td>
                    <td class="NormalTD"  >
                         

                          <asp:UpdatePanel ID="upd_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>

                    </td>
                    <td >
                          <asp:UpdatePanel ID="upd_btn_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_atc" runat="server" Text="S" Width="33px"  CssClass="NormalTD" OnClick="btn_atc_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>   </td >
                       
                </tr>
                <tr>
                    <td class="NormalTD" >
                        PO # :
                    </td>
                    <td class="auto-style2" >
                             
                      <asp:UpdatePanel ID="upd_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="ddl_po" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                    </td >
                    <td class="auto-style1">
                         <asp:UpdatePanel ID="upd_btn_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_po" runat="server" Text="S" Width="33px"  CssClass="NormalTD" OnClick="btn_po_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td class="auto-style1" >
                        </td>
                    <td class="auto-style1" >
                        </td>
                </tr>
                <tr>
                    <td class="NormalTD" >
                        MRN&nbsp; # :</td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_mrn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_mrn" runat="server" Height="25px" Width="170px"  DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD" >
                         <asp:UpdatePanel ID="upd_btn_mrn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_mrn" runat="server" Text="S" Width="33px"  CssClass="NormalTD"  OnClick="btn_mrn_Click"          /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  
                    </td>
                    <td class="NormalTD" >
                        </td>
                    <td class="NormalTD" >
                        </td>
                </tr>
                <tr>
                    <td class="NormalTD" >
                        Fabric Details :
                    </td>
                    <td class="NormalTD" >
                       <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD" >
                         <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="Button2" runat="server" Text="S" Width="33px"  CssClass="NormalTD"  OnClick="Button2_Click"          /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  
                    </td>
                    <td class="NormalTD" >
                        </td>
                    <td class="NormalTD" >
                         </td>
                </tr>
                <tr>
                    <td >
                        ASN #</td>
                    <td class="NormalTD" >
                         <asp:UpdatePanel ID="UPD_ASN" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_asn" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_btn_fabriccolor" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_color" runat="server" Text="S" Width="33px"  CssClass="NormalTD" OnClick="btn_color_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        
                        MRNQTY</td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_qty" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_mrnQty" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lbl_UOM" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel></td>
                    <td class="NormalTD" >
                        Already Added </td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_alreadyAdded" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lbl_alreadyadded" runat="server" Text="0"></asp:Label>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td >
                         &nbsp;</td>
                </tr>
    <tr>
                    <td class="NormalTD" >
                        No of Rolls</td>
                    <td class="NormalTD" >
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_addroll" runat="server" Text="S" Width="33px"   OnClick="btn_addroll_Click"  /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>
    <tr>
                    <td >
                        <asp:CheckBox ID="chk_knitreg" runat="server" Text="Knit Regular" AutoPostBack="true" OnCheckedChanged="chk_knitreg_CheckedChanged" />
                        <asp:CheckBox ID="chk_knitTubular" runat="server" Text="KNIT TUBULAR"  AutoPostBack="true"  OnCheckedChanged="chk_knitTubular_CheckedChanged" />
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" class="NormalTD">
                        
                        <table style="border: thin double #C0C0C0; line-height: normal; vertical-align: middle;  text-align: center; white-space: normal; word-spacing: normal; letter-spacing: normal; background-color: #99CCFF; position: relative; width: 100%;">
                            <tr>
                                <td class="auto-style11" colspan="12"><strong>Quick Fill </strong></td>
                            </tr>
                            <tr>
                                <td colspan="12">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_remark" CssClass="txt_remark" runat="server" placeholder="Enter Remark" Width="99px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_remark" runat="server" OnClientClick="CopyRemark()" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_yardage" CssClass="txt_yardage" runat="server" placeholder="Enter Yard" Width="93px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_yard" OnClientClick="CopyYardage()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_shade" CssClass="txt_shade" runat="server" placeholder="Enter Shade" Width="90px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_shade" OnClientClick="CopyShade()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_Shrinkage" CssClass="txt_Shrinkage" runat="server" placeholder="Enter Shrinkage" Width="90px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_shrinkage" OnClientClick="CopyShinkage()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_width" CssClass="txt_width" runat="server" placeholder="Enter Width" Width="90px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_width" OnClientClick="Copywidth()" runat="server" Font-Bold="True" Font-Size="X-Small" Height="20px" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_gsm" CssClass="txt_gsm" runat="server" placeholder="Enter GSM" Width="90px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_gsm" OnClientClick="Copygsm()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd"><asp:TextBox ID="txt_weight" CssClass="txt_weight" runat="server" placeholder="Enter Weight" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD"> <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_weight" OnClientClick="Copyweight()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="gridtable" colspan="5">
                        <asp:UpdatePanel ID="upd_grid"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                
                        <asp:UpdatePanel ID="UpdatePanel9"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails"
                                     runat="server" AutoGenerateColumns="False" 
                                    ShowHeaderWhenEmpty="True" style="font-size: small;
 font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966"
                                     BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowFooter="True">
                                    <Columns>

                                         <%--<asp:TemplateField  ControlStyle-Width="10px" HeaderStyle-Width="10px" FooterStyle-Width="10px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="OnSelectAllClick(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Onselection(this)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>         --%>                             
                                       
                            
                                                 <asp:TemplateField>  
                                    <HeaderTemplate>
                                       <asp:CheckBox ID="checkAll" runat="server" onclick="OnSelectAllClick(this)" />
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                           <asp:CheckBox ID="Chk_select" runat="server" onclick="Onselection(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Roll#">
                                          
                                            <ItemTemplate>
                                                 <asp:TextBox ID="txt_rollnum" onkeyup="enter(this)"  runat="server" Text='<%# Bind("Rollnum") %>'></asp:TextBox>
                                            </ItemTemplate>
                                             <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="LOT#">
                                             
                                             <ItemTemplate>
                                                 <asp:TextBox ID="txt_lot" onkeyup="enter(this)" runat="server" Width="70px"  Text='0'></asp:TextBox>
                                             </ItemTemplate>
                                              <HeaderStyle Width="70px" />
                                         </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Remark">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_remark" CssClass="txt_remark" onkeyup="enter(this)" runat="server" Text='<%# Bind("Remark") %>' ></asp:TextBox>
                                            </ItemTemplate>
                                             <HeaderStyle Width="80px" />
                                        </asp:TemplateField>


                              

                                          <asp:TemplateField HeaderText="Yardage" >
                                           
                                              <FooterTemplate>
                                                  <asp:TextBox ID="txt_totalyard" CssClass="totalyardfooter" runat="server"></asp:TextBox>

                                              </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_syard" onChange="calculatesumofyardage()" CssClass="txt_syard" onkeyup="enter(this)" runat="server" Width="70px" Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                               <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="UOM">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_uom" runat="server" Text='<%# Bind("UOM") %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                              <HeaderStyle Width="40px" />
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Shade">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Sshade" CssClass="txt_Sshade" onkeyup="enter(this)" runat="server" Width="70px" Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                              <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Shrinkage">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sshrinkage" CssClass="txt_sshrinkage" onkeyup="enter(this)" runat="server" Width="70px"  Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                               <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Width">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sWidth" CssClass="txt_width1" onkeyup="enter(this)"  runat="server" Width="70px"  Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                               <HeaderStyle Width="70px" />
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="GSM">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sgsm" CssClass="txt_gms1" onkeyup="enter(this)" onChange="calculateforKnit()"  runat="server" Width="70px"  Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                                <HeaderStyle Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Weight">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_sweight" CssClass="txt_weight1" onChange="calculateforKnit()" onkeyup="enter(this)" runat="server" Width="70px"  Text='0'></asp:TextBox>
                                            </ItemTemplate>
                                                <HeaderStyle Width="70px" />
                                         
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

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="ButtonTR">
                    <td >
                        <asp:Button ID="Button1" runat="server" Text="Save Roll Data" OnClick="Button1_Click" />
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
            </table>

                </td>
            </tr>
        </table>
      
            
        
   
    <div class="footer">
        
                <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Roll_PK], [RollNum], [SShrink], [SYard], [SShade], [SWidth], [SGsm] FROM [FabricRollmaster]"></asp:SqlDataSource>
                    
               
                               
    </div>

    
</asp:Content>